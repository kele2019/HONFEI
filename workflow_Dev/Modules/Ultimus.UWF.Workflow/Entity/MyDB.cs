using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Workflow.Entity
{
    public class MyDB
    {
        public MyDB()
        { }
        public MyDB(string fORMID, string pROCESSNAME, int? iNCIDENT, int? rOWID, string sAdress, string eAdress, string sDate, string eTime, string aSDate, string aETime, string comments)
        {
            this.FORMID = fORMID;
            this.PROCESSNAME = pROCESSNAME;
            this.INCIDENT = iNCIDENT;
            this.ROWID = rOWID;
            this.SAdress = sAdress;
            this.EAdress = eAdress;
            this.SDate = sDate;
            this.ETime = eTime;
            this.ASDate = aSDate;
            this.AETime = aETime;
            this.Comments = comments;
        }
        #region 实体属性
        /// <summary>
        /// FORMID
        /// </summary>
        public string FORMID { get; set; }

        /// <summary>
        /// PROCESSNAME
        /// </summary>
        public string PROCESSNAME { get; set; }

        /// <summary>
        /// INCIDENT
        /// </summary>
        public int? INCIDENT { get; set; }

        /// <summary>
        /// ROWID
        /// </summary>
        public int? ROWID { get; set; }

        /// <summary>
        /// SAdress
        /// </summary>
        public string SAdress { get; set; }

        /// <summary>
        /// EAdress
        /// </summary>
        public string EAdress { get; set; }

        /// <summary>
        /// SDate
        /// </summary>
        public string SDate { get; set; }

        /// <summary>
        /// ETime
        /// </summary>
        public string ETime { get; set; }

        /// <summary>
        /// ASDate
        /// </summary>
        public string ASDate { get; set; }

        /// <summary>
        /// AETime
        /// </summary>
        public string AETime { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }
        #endregion
      
    }
}