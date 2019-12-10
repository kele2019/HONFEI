using System;
using System.Collections.Generic;
using System.Text;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Form.ProcessControl.Logic
{
    public class CommonLogic
    {
        public int GetTaskStatusBySql(string strTaskid)
        {
            try
            {
                string strSql = "select t.status from tasks t where t.taskid ='" + strTaskid + "'";
                return Convert.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar(strSql));

            }
            catch (Exception ex)
            {
                return 0;
                throw ex;

            }
        }
        public UserInfo GetUserDataInfo(string UserID)
        {
            try
            {
                UserInfo mode = new UserInfo();
              DataTable dtUserInfo=DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from ORG_USER where  LOGINNAME='{0}' ", UserID));
              if (dtUserInfo.Rows.Count > 0)
              {
                  mode = GetDeptInfo(dtUserInfo.Rows[0]["USERID"].ToString());
                  if (mode.DeptType.ToLower() == "subdept")
                  {
                      mode = GetDeptInfo1(mode.ParentID);
                  }
				  mode.USERCODE = dtUserInfo.Rows[0]["USERCODE"].ToString();
                  mode.UserEnName = dtUserInfo.Rows[0]["EXT04"].ToString();
              }
              return mode;
            }
            catch (Exception ex)
            {
                return new UserInfo();
            }
        }
        public UserInfo GetDeptInfo(string UserID)
        { 
              UserInfo mode = new UserInfo();
              DataTable dtDeptInfo=DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from ORG_DEPARTMENT where DEPARTMENTID=(select DEPARTMENTID from ORG_JOB where USERID='{0}') ", UserID));
              if (dtDeptInfo.Rows.Count > 0)
              {
                  mode.DeptType = dtDeptInfo.Rows[0]["DEPARTMENTTYPE"].ToString();
                  mode.DeptEnName = dtDeptInfo.Rows[0]["EXT03"].ToString();
                  mode.CostCenter = dtDeptInfo.Rows[0]["EXT02"].ToString();
                  mode.ParentID = dtDeptInfo.Rows[0]["PARENTID"].ToString();
                  mode.DeptName = dtDeptInfo.Rows[0]["DEPARTMENTNAME"].ToString();
              }
              return mode;
        }
        public UserInfo GetDeptInfo1(string DeptID)
        {
            UserInfo mode = new UserInfo();
            DataTable dtDeptInfo = DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from ORG_DEPARTMENT where DEPARTMENTID='{0}' ", DeptID));
            if (dtDeptInfo.Rows.Count > 0)
            {
                mode.DeptType = dtDeptInfo.Rows[0]["DEPARTMENTTYPE"].ToString();
                mode.DeptEnName = dtDeptInfo.Rows[0]["EXT03"].ToString();
                mode.CostCenter = dtDeptInfo.Rows[0]["EXT02"].ToString();
                mode.ParentID = dtDeptInfo.Rows[0]["PARENTID"].ToString();
                mode.DeptName = dtDeptInfo.Rows[0]["DEPARTMENTNAME"].ToString();
            }
            return mode;
        }
    }
    public class UserInfo
    {
        public UserInfo()
		{}
        private string userEnName;
        public string UserEnName
        {
            get { return userEnName; }
            set { userEnName = value; }
        }
		private string userCode;
		 public string USERCODE
        {
			get { return userCode; }
			set { userCode = value; }
        }
		
        private string costCenter;
        public string CostCenter
        {
            get { return costCenter; }
            set { costCenter = value; }
        }
        private string deptEnName;
        public string DeptEnName
        {
            get { return deptEnName; }
            set { deptEnName = value; }
        }
        private string deptType;
        public string DeptType
        {
            get { return deptType; }
            set { deptType = value; }
        }
        private string parentID;
        public string ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
        private string deptName;
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
    }
}
