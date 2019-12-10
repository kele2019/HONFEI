using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLib;
using System.Xml;
using System.Security.Cryptography;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Service;

namespace Ultimus.Service.Workflow
{
    /// <summary>
    /// EIKHandler 的摘要说明
    /// </summary>
    public class EIKHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string serviceId = "xxxx";
            try
            {
                byte[] byts = new byte[Request.InputStream.Length];
                Request.InputStream.Read(byts, 0, byts.Length);
                string req = System.Text.Encoding.UTF8.GetString(byts);
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                LogUtil.Info("Req:" + req);
                //加载xml
                if (string.IsNullOrEmpty(req))
                {
                    ReturnErrorResponse(context, "xxxx", "M02", "错误的报文格式");
                    return;
                }
                req = req.Replace("esb:", "");

                xmldoc.LoadXml(req);

                serviceId = GetInnerText(xmldoc, "ESB_SERVICE_ID");
                string user = GetInnerText(xmldoc, "ESB_USER");
                string pwd = GetInnerText(xmldoc, "ESB_PWD");

                //判断用户
                if (serviceId.Length >= 4)
                {
                    serviceId = serviceId.Substring(0, 4);
                }
                else
                {
                    ReturnErrorResponse(context, "xxxx", "M02", "ESB Service ID格式不正确");
                    return;
                }

                if (user != ConfigurationManager.AppSettings["esbUser"])
                {
                    ReturnErrorResponse(context, serviceId, "E02", "用户或密码错误");
                    return;
                }

                if (pwd.ToUpper() != MD5Encrypt(ConfigurationManager.AppSettings["esbUser"] + ConfigurationManager.AppSettings["esbPassword"]).ToUpper())
                {
                    ReturnErrorResponse(context, serviceId, "E02", "用户或密码错误");
                    return;
                }

                //判断方法
                string requestdata = GetInnerText(xmldoc, "REQUEST_DATA");
                requestdata = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(requestdata));
                requestdata = HttpContext.Current.Server.UrlDecode(requestdata);
                xmldoc.LoadXml("<RequestData>" + requestdata + "</RequestData>");
                XmlNode node = xmldoc.SelectSingleNode("//method");

                string xml = "";
                EIKService eik = new EIKService();
                List<TaskEntity> list1 = new List<TaskEntity>();
                string method = "";

                //执行每个方法
                if (node != null)
                {
                    method = node.InnerText;
                    switch (method)
                    {
                        case "GetInitProcessList":
                            List<ProcessEntity> list = eik.GetInitProcessList(GetInnerText(xmldoc, "userName"));
                            if (list.Count > 0)
                            {
                                xml = SerializeUtil.XMLSerialize(list);
                            }
                            break;
                        case "GetMyApprovalList":
                            list1 = eik.GetMyApprovalList(GetInnerText(xmldoc, "userName"));
                            if (list1.Count > 0)
                            {
                                xml = SerializeUtil.XMLSerialize(list1);
                            }
                            break;
                        case "GetMyRequestList":
                            list1 = eik.GetMyRequestList(GetInnerText(xmldoc, "userName"));
                            if (list1.Count > 0)
                            {
                                xml = SerializeUtil.XMLSerialize(list1);
                            }
                            break;
                        case "GetMyTaskList":
                            list1 = eik.GetMyTaskList(GetInnerText(xmldoc, "userName"),"");
                            if (list1.Count > 0)
                            {
                                xml = SerializeUtil.XMLSerialize(list1);
                            }
                            break;
                        case "GetTaskInfo":
                            TaskEntity task = eik.GetTaskInfo(GetInnerText(xmldoc, "taskId"));
                            if (task != null)
                            {
                                xml = SerializeUtil.XMLSerialize(task);
                            }
                            break;
                        case "SubmitTask":
                            int incident = eik.SubmitTask(GetInnerText(xmldoc, "userName"), GetInnerText(xmldoc, "taskId"), GetInnerText(xmldoc, "summary"), null,"","");
                            if (incident <= 0)
                            {
                                ReturnErrorResponse(context, serviceId, "E14", "提交失败，调用Ultimus EIK时出错!");
                                return;
                            }
                            xml = "<SubmitTaskResult>" + incident.ToString() + "</SubmitTaskResult>";
                            break;
                        case "ReturnTask":
                            string str = eik.ReturnTask(GetInnerText(xmldoc, "userName"), GetInnerText(xmldoc, "taskId"), GetInnerText(xmldoc, "reason"), GetInnerText(xmldoc, "summary"), null,"","");
                            xml = "<ReturnTaskResult>" + str + "</ReturnTaskResult>";
                            if (!string.IsNullOrEmpty(str))
                            {
                                ReturnErrorResponse(context, serviceId, "E15", "退回失败!" + str);
                                return;
                            }
                            break;
                        case "RejectTask":
                            string str1 = eik.RejectTask(GetInnerText(xmldoc, "userName"), GetInnerText(xmldoc, "taskId"), GetInnerText(xmldoc, "reason"), GetVars(xmldoc));
                            xml = "<RejectTaskResult>" + str1 + "</RejectTaskResult>";
                            if (!string.IsNullOrEmpty(str1))
                            {
                                ReturnErrorResponse(context, serviceId, "E16", "拒绝失败!" + str1);
                                return;
                            }
                            break;
                    }
                }

                string res = GetResponse(xml, method);
                context.Response.ContentType = "text/xml";
                context.Response.Write(res);
            }
            catch (Exception ex)
            {
                ReturnErrorResponse(context, serviceId, "Z06", ex.Message);
            }
        }

        //获取md5
        public string MD5Encrypt(string strText)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText)));
            a = a.Replace("-", "");
            return a;

        }

        //获取ultimus变量列表
        public List<VarEntity> GetVars(XmlDocument xmldoc)
        {
            XmlNodeList nodes = xmldoc.SelectNodes("//VarEntity");
            List<VarEntity> list = new List<VarEntity>();
            foreach (XmlNode node in nodes)
            {
                VarEntity var = new VarEntity();
                var.Name = node.ChildNodes[0].InnerText;
                var.Value = node.ChildNodes[1].InnerText;
                list.Add(var);
            }
            return list;
        }

        //获取xml节点的值
        public string GetInnerText(XmlDocument xmldoc, string xpath)
        {
            XmlNode node = xmldoc.SelectSingleNode("//" + xpath);
            if (node != null)
            {
                return node.InnerText;
            }
            return "";
        }

        //获取返回xml
        public string GetResponse(string xml, string method)
        {
            xml = xml.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("<?xml version=\"1.0\"?>", "");
            xml = "<" + method + "Response>" + xml + "</" + method + "Response>";
            string a = xml;
            byte[] b = System.Text.Encoding.UTF8.GetBytes(a);
            xml = Convert.ToBase64String(b);

            string str = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:esb=\"http://w3.ibm.com/gbs/ais/ei/esb\">  " +
            "<soapenv:Header/>  " +
            "<soapenv:Body>  " +
            "<esb:RESPONSE>  " +
            "<esb:RETURN_CODE>S000A000</esb:RETURN_CODE>  " +
            "<esb:RETURN_DATA>" + xml + "</esb:RETURN_DATA>  " +
            "<esb:RETURN_DESC></esb:RETURN_DESC>  " +
            "</esb:RESPONSE>  " +
            "</soapenv:Body>  " +
            "</soapenv:Envelope> ";

            return str;
        }

        //返回错误信息
        public void ReturnErrorResponse(HttpContext context, string serviceid, string code, string desc)
        {

            string str = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:esb=\"http://w3.ibm.com/gbs/ais/ei/esb\">  " +
            "<soapenv:Header/>  " +
            "<soapenv:Body>  " +
            "<esb:RESPONSE>  " +
            "<esb:RETURN_CODE>E" + serviceid + "" + code + "</esb:RETURN_CODE>  " +
            "<esb:RETURN_DATA></esb:RETURN_DATA>  " +
            "<esb:RETURN_DESC>" + desc + "</esb:RETURN_DESC>  " +
            "</esb:RESPONSE>  " +
            "</soapenv:Body>  " +
            "</soapenv:Envelope> ";

            context.Response.ContentType = "text/xml";
            context.Response.Write(str);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}