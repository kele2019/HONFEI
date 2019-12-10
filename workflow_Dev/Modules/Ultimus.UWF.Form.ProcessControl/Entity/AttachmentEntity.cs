using System;
using System.Collections.Generic;
using System.Web;

namespace Ultimus.UWF.Form.ProcessControl.Entity
{
    /// <summary>
    /// 文件上传实体类
    /// </summary>
    [Serializable]
    public class AttachmentEntity
    {
        string _ID;
        /// <summary>
        /// 主键、唯一标示
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _ProcessName;
        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        int _Incident;
        /// <summary>
        /// 实例编号
        /// </summary>
        public int Incident
        {
            get { return _Incident; }
            set { _Incident = value; }
        }
        string _UploadStepName;
        /// <summary>
        /// 上传步骤
        /// </summary>
        public string UploadStepName
        {
            get { return _UploadStepName; }
            set { _UploadStepName = value; }
        }
        string _FileName;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        string _NewName;
        /// <summary>
        /// 新文件名称
        /// </summary>
        public string NewName
        {
            get { return _NewName; }
            set { _NewName = value; }
        }
        decimal _FileSize;
        /// <summary>
        /// 文件大小
        /// </summary>
        public decimal FileSize
        {
            get { return _FileSize; }
            set { _FileSize = value; }
        }
        string _FileType;
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }
        string _Comments;
        /// <summary>
        /// 文件描述
        /// </summary>
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        string _CreateByBadge;
        /// <summary>
        /// 上传人员工号
        /// </summary>
        public string CreateByBadge
        {
            get { return _CreateByBadge; }
            set { _CreateByBadge = value; }
        }
        string _CreateByName;
        /// <summary>
        /// 上传人姓名
        /// </summary>
        public string CreateByName
        {
            get { return _CreateByName; }
            set { _CreateByName = value; }
        }
        string _CreateByDomain;

        public string CreateByDomain
        {
            get { return _CreateByDomain; }
            set { _CreateByDomain = value; }
        }

        string _AttachmentType;
        
        public string AttachmentType
        {
            get { return _AttachmentType; }
            set { _AttachmentType = value; }
        }
        DateTime _CreateDate;
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        string _Status;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        string _TYPE;

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        string _TASKID;

        public string TASKID
        {
            get { return _TASKID; }
            set { _TASKID = value; }
        }

        string _FORMID;

        public string FORMID
        {
            get { return _FORMID; }
            set { _FORMID = value; }
        }

        string _Ext01;
        /// <summary>
        /// 预留字段
        /// </summary>
        public string Ext01
        {
            get { return _Ext01; }
            set { _Ext01 = value; }
        }
        string _Ext02;
        /// <summary>
        /// 预留字段
        /// </summary>
        public string Ext02
        {
            get { return _Ext02; }
            set { _Ext02 = value; }
        }
        string _Ext03;
        /// <summary>
        /// 预留字段
        /// </summary>
        public string Ext03
        {
            get { return _Ext03; }
            set { _Ext03 = value; }
        }
        string _Ext04;
        /// <summary>
        /// 预留字段
        /// </summary>
        public string Ext04
        {
            get { return _Ext04; }
            set { _Ext04 = value; }
        }
        string _Ext05;
        /// <summary>
        /// 预留字段
        /// </summary>
        public string Ext05
        {
            get { return _Ext05; }
            set { _Ext05 = value; }
        }
    }
}