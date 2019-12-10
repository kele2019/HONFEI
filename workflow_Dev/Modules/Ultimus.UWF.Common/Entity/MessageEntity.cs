using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Common.Entity
{
    public class MessageEntity
    {
        string _Source="";
        /// <summary>
        /// 来源系统，如BPM
        /// </summary>
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        string _To;
        /// <summary>
        /// 发送给谁，为用户AD账号，格式为domain\useraccount
        /// </summary>
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }
        string _Subject="";
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        string _Body="";
        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        string _BodyType = "text/plain";
        /// <summary>
        /// 内容类型 text/plain或者text/html
        /// </summary>
        public string BodyType
        {
            get { return _BodyType; }
            set { _BodyType = value; }
        }
        string _SendType;
        /// <summary>
        /// 发送类型，如SMS,EMAIL等等，多个以逗号分隔
        /// </summary>
        public string SendType
        {
            get { return _SendType; }
            set { _SendType = value; }
        }
        List<Byte[]> _Attachments;
        /// <summary>
        /// 附件
        /// </summary>
        private List<Byte[]> Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }
    }
}