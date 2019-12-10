using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Configuration;
using EntityLibrary;
using DALLibrary;
using RazorEngine;
using MobileClient.Background;
using MyLib;

namespace CreatePageLibrary
{
    public class CreatePage
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        private string TemplateName = System.Configuration.ConfigurationManager.AppSettings["TemplateName"].ToString().Trim();
        /// <summary>
        /// 生成文件的路径
        /// </summary>
        private string CreatePath = System.Configuration.ConfigurationManager.AppSettings["CreatePagePath"].ToString().Trim();


        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="id"></param>
        public void CreatePageFile(int id)
        {
            PageControl pageCtl = new PageControl();
            PageConfig config = pageCtl.GetPageControlList(id);
            string template = File.ReadAllText(HttpContext.Current.Server.MapPath(TemplateName));
            foreach (PageConfig_Step step in config.ProcessStep) //对步骤做循环生成页面
            {
                if (step.StepControl.Count > 0)
                {
                    //按钮
                    List<PageConfig_Control> buttons = step.StepControl.FindAll(p => p.Control.ToUpper().IndexOf("BUTTON") >= 0
                        && p.Control.ToUpper().IndexOf("RADIOBUTTON") < 0 && p.ControlEName.ToUpper().IndexOf("TABLE")<0);
                    //控件
                    //List<PageConfig_Control> Controls = step.StepControl.FindAll(p =>( p.Control.ToUpper().IndexOf("BUTTON") < 0
                    //    || p.Control.ToUpper().IndexOf("RADIOBUTTON") >= 0) && p.ControlEName.ToUpper().IndexOf("TABLE") < 0 && p.ControlEName.ToUpper().IndexOf("ATTACHMENT") < 0);
                    
                    //所有控件，包含表格、附件，表格默认取第一行
                    List<PageConfig_Control> allControls = new List<PageConfig_Control>();
                    foreach (PageConfig_Control pcc in step.StepControl) 
                    {
                        //除审批按钮外的普通控件
                        if ((pcc.Control.ToUpper().IndexOf("BUTTON") < 0 || pcc.Control.ToUpper().IndexOf("RADIOBUTTON") >= 0) && pcc.ControlEName.ToUpper().IndexOf("TABLE") < 0 && pcc.ControlEName.ToUpper().IndexOf("ATTACHMENT") < 0) 
                        {
                            allControls.Add(pcc);
                        }
                        //表格
                        else if (pcc.ControlEName.ToUpper().IndexOf("TABLE") >= 0) 
                        {
                            bool isExists = false;
                            foreach (PageConfig_Control pc in allControls) 
                            {
                                if (pc.FORMAT == pcc.FORMAT) isExists = true;
                            }
                            if (isExists == false && pcc.FORMAT != null && pcc.FORMAT != "") 
                            {
                                allControls.Add(pcc);
                            }
                        }
                        //附件
                        else if (pcc.ControlEName.ToUpper().IndexOf("ATTACHMENT") >= 0) 
                        {
                            if (!allControls.Contains(pcc)) 
                            {
                                if (pcc.FORMAT != null && pcc.FORMAT != "")
                                {
                                    allControls.Add(pcc);
                                }
                            }
                        }
                    }

                    //明细行
                    //List<PageConfig_Control> grid = step.StepControl.FindAll(p => p.ControlEName.ToUpper().IndexOf("TABLE") >= 0);
                    //List<string> grids = new List<string>();
                    //foreach (PageConfig_Control ctl in grid)
                    //{
                    //    if (ctl.FORMAT != null && !grids.Contains(ctl.FORMAT))
                    //    {
                    //        grids.Add(ctl.FORMAT);
                    //        allControls.Add(ctl);
                    //    }
                    //}
                    ////附件
                    //List<PageConfig_Control> attchment = step.StepControl.FindAll(p => p.ControlEName.ToUpper().IndexOf("ATTACHMENT") >= 0);
                    //List<string> attchments = new List<string>();
                    //foreach (PageConfig_Control ctl in attchment)
                    //{
                    //    if (ctl.FORMAT != null && !attchments.Contains(ctl.FORMAT))
                    //    {
                    //        attchments.Add(ctl.FORMAT);
                    //        allControls.Add(ctl);
                    //    }
                    //}

                    //创建Model
                    PageEntity entity = new PageEntity();
                    entity.ProcessName = config.ProcessName;
                    entity.StepName = step.StepName;
                    entity.Controls = allControls;// Controls;
                    entity.Buttons = buttons;
                    //entity.Grids = grids;
                    //entity.Attchments = attchments;
                    string result = Razor.Parse(template,entity);

                    //根据Model和模板生成页面
                    result = HttpUtility.HtmlDecode(result);
                    string path = HttpContext.Current.Server.MapPath(CreatePath);
                    File.WriteAllText(path + "\\" + step.StepID + ".aspx", result, System.Text.Encoding.UTF8);

                    //更新步骤已生成页面
                    DataAccess.Instance("BizDB").ExecuteNonQuery("update MOBILECLIENT_STEP set ISCREATEPAGE=1 where id=" + step.StepID);
                }
            }


        }


    }
}
