using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using EntityLibrary;
using DataBaseLibrary;
using MyLib;
namespace DALLibrary
{
    public class ProcessStep
    {
        public List<EntityLibrary.ProcessStep> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PROCESSNAME,PROCESSVERSION,STEPID,STEPLABEL FROM PROCESSSTEPS ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<EntityLibrary.ProcessStep> list = new List<EntityLibrary.ProcessStep>();
            //DbHelperSQLP db = new DbHelperSQLP();
            IDataReader reader = DataAccess.Instance("UltDB").ExecuteReader(strSql.ToString());
            while (reader.Read())
            {
                EntityLibrary.ProcessStep model = new EntityLibrary.ProcessStep();
                model.ProcessName = reader["PROCESSNAME"].ToString();
                model.ProcessVersion = reader["PROCESSVERSION"].ToString();
                model.StepID = reader["STEPID"].ToString();
                model.StepName = reader["STEPLABEL"].ToString();
                list.Add(model);
            }
            reader.Close();
            return list;
        }
    }
}
