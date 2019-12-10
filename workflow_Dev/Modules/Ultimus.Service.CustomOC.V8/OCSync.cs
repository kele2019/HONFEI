using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using SharedInterfaceLib;
using System.Diagnostics;
using System.Data.Common;
using System.Data;

namespace CustomDirectory
{
    [Guid("49d90196-e709-4cc2-bb81-14be758a02ee"), ClassInterface(ClassInterfaceType.None), ProgId("CustomDirectory.OCSync")]

    public class OCSync : IOrgChart, IAuthentication
    {
        // Fields
        private bool IsGetSubordinatesImplemented=true;

        // Methods
        public OCSync()
        {
            this.IsGetSubordinatesImplemented = false;
            new Helper().AppendTextToLog("-----构造函数");
        }

        public void AdvSearch(string bstrOrgName, string bstrCustomQuery, object varSearchTypes, object varSearchStrings, object varResultColumns, out object varResults)
        {
            varResults = null;
            System.UInt16[] SearchTypes = (System.UInt16[])varSearchTypes;
            System.String[] SearchStrings = (System.String[])varSearchStrings;
            System.UInt16[] ResultColsToGet = (System.UInt16[])varResultColumns;

            string strQuery = "SELECT USERID, LOGINNAME, FIRSTNAME, LASTNAME, TITLE FROM "+
                "(SELECT A.USERID AS USERID ,A.USERNAME AS FIRSTNAME,'' AS LASTNAME,LOGINNAME AS LOGINNAME,B.JOBFUNCTION AS TITLE FROM   V_ORG_USER A LEFT JOIN V_ORG_USERDEPARTMENT B ON A.USERID=B.USERID) T WHERE 1=1 ";
            string sqlClause = "";
            // Where Clause for Adv Search
            for (int i = 0; i < SearchTypes.Length; i++)
            {
                string Value = SearchStrings[i].Replace("*", "").Trim();
                if (Value == "") continue;
                switch (SearchTypes[i])
                {
                    case 0:     // For LOGINNAME                        
                        sqlClause += " and LOGINNAME LIKE '%" + Value + "%'";
                        break;

                    case 1:     // For FullName
                        sqlClause += " AND (FIRSTNAME+LASTNAME) LIKE '%" + Value + "%'";
                        break;

                    case 2:     // For JF
                        sqlClause += " AND TITLE LIKE '%" + SearchStrings[i] + "%'";
                        break;
                    default:
                        continue;
                }
            }
            sqlClause += " order by FIRSTNAME";
            strQuery += sqlClause;
            Helper helper = new Helper();
            string strError = "";
            DbDataReader dbReader = null;
            try
            {
                int TotalRows = helper.GetTotalRows(strQuery);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return;
                }

                System.String[,] result = new string[TotalRows, ResultColsToGet.Length];
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return;
                }
                int EmpID;
                if (dbReader.HasRows)
                {

                    int j = 0;
                    while (dbReader.Read()) // Read The row
                    {
                        EmpID = dbReader.GetInt32(0);

                        for (int i = 0; i < ResultColsToGet.Length; i++)
                        {
                            switch (ResultColsToGet[i])
                            {
                                case 0:  // For Short Name
                                    result[j, i] = dbReader.GetString(1);
                                    break;
                                case 1:  // For Full Name
                                    result[j, i] = dbReader.GetString(2) + " " + dbReader.GetString(3);
                                    break;
                                case 2:  // For JF
                                    result[j, i] = dbReader.GetString(4);
                                    break;
                                case 4:  // For Dept ID
                                    string bstrDeptName;
                                    GetEmployeeDepartment(EmpID, out bstrDeptName, out strError);
                                    result[j, i] = bstrDeptName;
                                    break;
                                default:
                                    result[j, i] = "-1";
                                    break;

                            }
                        }
                        j++;

                    }
                }


                varResults = result;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                varResults = null;
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        private void GetEmployeeDepartment(int nEmpID, out string bstrDeptName, out string strError)
        {
            bstrDeptName = strError = "";
            Helper helper = new Helper();
            int DeptID = 0;
            string strQuery = "Select DEPARTMENTID from V_ORG_USERDEPARTMENT where USERID= " + nEmpID;

            DbDataReader dbReader = null;
            try
            {
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return;
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    return;
                }
                if (dbReader.Read())
                {
                    DeptID = dbReader.GetInt32(0);
                }
                dbReader.Close();
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            DepartmentIdToName("", DeptID.ToString(), out bstrDeptName);

        }

        public void Configure(string bstrProgID)
        {
            new Helper().AppendTextToLog("--  configure called");

        }
        public void DepartmentIdToName(string bstrOrgName, string bstrDeptID, out string bstrDeptName)
        {
            string strQuery = bstrDeptName = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = DEPARTMENTIDToName()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select DEPARTMENTNAME from V_ORG_DEPARTMENT where DEPARTMENTID='" + bstrDeptID + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    bstrDeptName = "";
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    bstrDeptName = "";
                }
                if (dbReader.Read())
                {
                    bstrDeptName = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                bstrDeptName = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public int DoesDepartmentExist(string bstrOrgName, string bstrDeptName)
        {
            int DoesDepartmentExist;
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = DoesDepartmentExist()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select DEPARTMENTID from V_ORG_DEPARTMENT where DEPARTMENTNAME='" + bstrDeptName + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return 0;
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    return 0;
                }
                DoesDepartmentExist = 1;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                DoesDepartmentExist = 0;
                return DoesDepartmentExist;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            return DoesDepartmentExist;

        }
        public int DoesGroupExist(string bstrOrgName, string grpName)
        {
            int DoesGroupExist;
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = DoesGroupExist()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select GROUPID AS ID from V_ORG_GROUP where GROUPNAME='" + grpName + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return 0;
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    return 0;
                }
                DoesGroupExist = 1;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                DoesGroupExist = 0;
                return DoesGroupExist;
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            return DoesGroupExist;

        }

        public int DoesPersonExist(string bstrOrgName, string userName)
        {
            int DoesPersonExist;
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = DoesPersonExist()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select USERID AS USERID from V_ORG_USER where LOGINNAME='" + userName + "'";
                helper.AppendTextToLog(strQuery);
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return 0;
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    return 0;
                }
                DoesPersonExist = 1;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                DoesPersonExist = 0;
                return DoesPersonExist;

            }

            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            return DoesPersonExist;

        }

        public void FullNameToShortName(string bstrOrgName, string FullName, out string LOGINNAME)
        {
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = FullNameToLOGINNAME()");
            LOGINNAME = FullName;
            DbDataReader dbReader = null;
            try
            {
                string strFirstName = "";
                string strLastName = "";
                int nLoc = FullName.IndexOf(" ");
                if ((nLoc == -1) | (FullName == LOGINNAME))
                {
                    LOGINNAME = FullName;
                }
                else
                {
                    strFirstName = FullName.Substring(0, nLoc);
                    strLastName = FullName.Substring(nLoc + 1);
                    strQuery = "select LOGINNAME AS LOGINNAME from V_ORG_USER where USERNAME='" + FullName + "'";
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);

                    }
                    if (!dbReader.HasRows)
                    {
                        helper.AppendTextToLog("--  Query did not return any Row");

                    }
                    if (dbReader.Read())
                    {
                        LOGINNAME = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                LOGINNAME = FullName;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        public void GetAllJobFunctionsOfPerson(string bstrOrgName, string bstrPerson, out object varJobFunctions)
        {
            string strQuery = "";
            string id = "";
            string dept = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetAllJobFunctionsOfPerson()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where a.LOGINNAME='" + bstrPerson + "'";
                this.ShortNameToId("", bstrPerson, out id);
                this.GetPersonDepartment("", id, out dept);
                int nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    varJobFunctions = null;
                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        varJobFunctions = null;
                    }
                    string[] strJobFunctions = new string[(nRows - 1) + 1];
                    for (int nCounter = 0; dbReader.Read(); nCounter++)
                    {
                        strJobFunctions[nCounter] = dept + @"\" + dbReader.GetString(0);
                    }
                    varJobFunctions = strJobFunctions;
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                varJobFunctions = null;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetDepartmentID(string bstrOrgName, string deptName, out string deptID)
        {
            string strQuery = "";
            deptID = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetDEPARTMENTID()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select DEPARTMENTID from V_ORG_DEPARTMENT where DEPARTMENTNAME='" + deptName + "'";
                Debug.WriteLine(strQuery);
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    deptID = "";
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    deptID = "";
                }
                else
                {
                    if (dbReader.Read())
                    {
                        deptID = dbReader.GetInt32(0).ToString();
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                deptID = "";
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetDepartmentMembers(string bstrOrgName, string deptID, out object UserNames, out object FullNames, out object JobFunctions, out object UserIDs)
        {
            string strQuery = "";
            UserNames = null;
            FullNames = null;
            JobFunctions = null;
            UserIDs = null;
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetDepartmentMembers()");
            DbDataReader dbReader = null;
            try
            {
                if ((deptID == null) | (deptID == ""))
                {
                    strQuery = "select a.USERID as USERID ,a.USERNAME as FirstName,'' as LastName, a.LOGINNAME as LOGINNAME,b.JOBFUNCTION as Title  from V_ORG_USER a left join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID order by USERNAME ";
                }
                else
                {
                    strQuery = @" with org(DEPARTMENTID,DEPARTMENTNAME,PARENTID) 
                                 as (
                                 select DEPARTMENTID,DEPARTMENTNAME,PARENTID from ORG_DEPARTMENT where DEPARTMENTID = " + deptID + @"
                                 union all
                                 select ORG_DEPARTMENT.DEPARTMENTID,ORG_DEPARTMENT.DEPARTMENTNAME,ORG_DEPARTMENT.PARENTID from ORG_DEPARTMENT
                                  join org on ORG_DEPARTMENT.PARENTID = org.DEPARTMENTID
                                 )
                                select a.USERID as USERID ,a.USERNAME as FirstName,'' as LastName, a.LOGINNAME as LOGINNAME,b.JOBFUNCTION as Title  from V_ORG_USER a left join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where  b.DEPARTMENTID in  (select org.DEPARTMENTID from org) order by USERNAME ";
                }
                int nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        UserNames = null;
                        FullNames = null;
                        JobFunctions = null;
                        UserIDs = null;
                    }
                    string[] arrUserIds = new string[(nRows - 1) + 1];
                    string[] strMembers = new string[(nRows - 1) + 1];
                    string[] strJobFunctions = new string[(nRows - 1) + 1];
                    string[] strFullNames = new string[(nRows - 1) + 1];
                    string strFirstName = "";
                    string strLastName = "";
                    for (int nCounter = 0; dbReader.Read(); nCounter++)
                    {
                        arrUserIds[nCounter] = dbReader.GetInt32(0).ToString();
                        strFirstName = dbReader.GetString(1);
                        strLastName = dbReader.GetString(2);
                        strFullNames[nCounter] = strFirstName + " " + strLastName;
                        strMembers[nCounter] = dbReader.GetString(3);
                        strJobFunctions[nCounter] = dbReader.GetString(4);
                    }
                    UserNames = strMembers;
                    FullNames = strFullNames;
                    JobFunctions = strJobFunctions;
                    UserIDs = arrUserIds;
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        public void GetDepartments(string bstrOrgName, string deptID, out object DeptNames, out object DeptIDs)
        {
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetDepartments()");
            DbDataReader dbReader = null;
            try
            {

                if ((deptID == null) || (deptID == ""))
                {
                    strQuery = "select DEPARTMENTNAME,DEPARTMENTID from V_ORG_DEPARTMENT where PARENTID=0";
                }
                else
                    if (deptID == "-1")
                    {
                        strQuery = "select DEPARTMENTNAME,DEPARTMENTID from V_ORG_DEPARTMENT";
                    }
                    else
                    {
                        strQuery = "select DEPARTMENTNAME,DEPARTMENTID from V_ORG_DEPARTMENT where PARENTID='" + deptID + "'";
                    }
                int nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    DeptIDs = null;
                    DeptNames = null;
                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        DeptIDs = null;
                        DeptNames = null;
                    }
                    string[] strDepts = new string[(nRows - 1) + 1];
                    string[] DeptsID = new string[(nRows - 1) + 1];
                    for (int nCounter = 0; dbReader.Read(); nCounter++)
                    {
                        strDepts[nCounter] = dbReader.GetString(0);
                        DeptsID[nCounter] = dbReader.GetInt32(1).ToString();
                    }
                    DeptNames = strDepts;
                    DeptIDs = DeptsID;
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                DeptIDs = null;
                DeptNames = null;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }


        public void GetEmailAddress(string userName, out string eMail)
        {
            string strQuery = "";
            eMail = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetEmailAddress()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select EMAIL from V_ORG_USER where LOGINNAME='" + userName + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    eMail = "";
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    eMail = "";
                }
                else
                {
                    if (dbReader.Read())
                    {
                         eMail = dbReader.GetString(0);
                       // eMail = "422987191@qq.com,richard.zhong@nextev.com";
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                eMail = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        public void GetErrorMessage(out string bstrErrorMsg)
        {
            bstrErrorMsg = @"Please outer to C:\CustomOCLog.log for details";
        }

        public void GetExactCaseUser(string bstrOrgName, string bstrUserName, out string pbstrExactUser)
        {
            string strQuery = pbstrExactUser = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetExactCaseUser()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select LOGINNAME from V_ORG_USER where LOGINNAME='" + bstrUserName + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    pbstrExactUser = "";
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    pbstrExactUser = "";
                }
                else
                {
                    if (dbReader.Read())
                    {
                        pbstrExactUser = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                pbstrExactUser = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetFullJobFunctionToID(string bstrOrgName, string bstrFullJobFunction, out string lUserID)
        {
            string strQuery = "";
            lUserID = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetFullJobFunctionToID()");
            DbDataReader dbReader = null;
            try
            {
                //bstrFullJobFunction = bstrFullJobFunction.Substring(bstrFullJobFunction.IndexOf("/") + 1);
                bstrFullJobFunction = bstrFullJobFunction.Substring(bstrFullJobFunction.IndexOf(@"\") + 1);
                strQuery = "select USERID from V_ORG_USERDEPARTMENT where JOBFUNCTION='" + bstrFullJobFunction + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    lUserID = "";
                }
                else
                {
                    if (dbReader.Read())
                    {
                        lUserID = dbReader.GetInt32(0).ToString();
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                lUserID = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetFullJobFunctionToName(string bstrOrgName, string bstrFullJobFunction, out string bstrUserName2)
        {
            Debug.Assert(false);
            string strQuery = "";
            bstrUserName2 = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetFullJobFunctionToName()");
            DbDataReader dbReader = null;
            try
            {
                bstrFullJobFunction = bstrFullJobFunction.Substring(bstrFullJobFunction.IndexOf(@"\") + 1);
                strQuery = "select LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.JOBFUNCTION='" + bstrFullJobFunction + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    bstrUserName2 = "";
                }
                else
                {
                    if (dbReader.Read())
                    {
                        bstrUserName2 = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                bstrUserName2 = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetGroupID(string bstrOrgName, string bstrGrpName, out string nGID)
        {
            string strQuery = "";
            nGID = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetGroupID()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "SELECT GROUPID AS ID FROM V_ORG_GROUP WHERE GROUPNAME='" + bstrGrpName + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    nGID = "";
                }
                else
                {
                    if (dbReader.Read())
                    {
                        nGID = dbReader.GetInt32(0).ToString();
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                nGID = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetGroupMembers(string bstrOrgName, string grpName, int bFullNames, out object UserNames, out object FullNames)
        {
            UserNames = null;
            FullNames = null;
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetGroupMembers()");
            bFullNames = 0;
            DbDataReader dbReader = null;
            try
            {
                if (grpName == "All Users")
                {
                    strQuery = "select USERID, LOGINNAME, USERNAME AS FirstName,'' AS LastName from V_ORG_USER order by USERNAME";
                }
                else
                {
                    strQuery = @"DECLARE @GroupID INT
                                   SELECT @GroupID=GROUPID FROM ORG_GROUP WHERE GROUPNAME = '" + grpName + @"';
                                        with org(DEPARTMENTID,DEPARTMENTNAME,PARENTID) 
                                            as (
                                            select DEPARTMENTID,DEPARTMENTNAME,PARENTID from ORG_DEPARTMENT where DEPARTMENTID IN (SELECT  MEMBERID 
	                                                                            FROM  V_ORG_GROUPMEMBER 
	                                                                            WHERE  GROUPID =@GroupID and MEMBERTYPE = 3)
                                            union all
                                            select ORG_DEPARTMENT.DEPARTMENTID,ORG_DEPARTMENT.DEPARTMENTNAME,ORG_DEPARTMENT.PARENTID from ORG_DEPARTMENT
                                            join org on ORG_DEPARTMENT.PARENTID = org.DEPARTMENTID
                                            )
                                    SELECT DISTINCT USERID AS USERID,LOGINNAME AS LOGINNAME,USERNAME AS FIRSTNAME,'' AS LASTNAME 
                                                FROM         V_ORG_USER    
                                                WHERE  ( USERID IN (SELECT  MEMBERID 
	                                                                    FROM  V_ORG_GROUPMEMBER 
	                                                                    WHERE  GROUPID =@GroupID and MEMBERTYPE = 1)  
                                                    OR USERID IN (SELECT DISTINCT USERID
	                                                                    FROM          V_ORG_USERDEPARTMENT
	                                                                    WHERE      DEPARTMENTID in (SELECT DISTINCT DEPARTMENTID  FROM ORG )))
                                                    AND USERID not in( SELECT  MEMBERID 
	                                                                    FROM  V_GROUPMEMBER 
	                                                                    WHERE  GROUPID =@GroupID and MEMBERTYPE = 99)  order by USERNAME";

                }



                int nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    UserNames = new string[0];
                    FullNames = new string[0];
                    helper.AppendTextToLog("--  Query did not return any Row");
                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                    }
                    string[] strLOGINNAMEs = new string[(nRows - 1) + 1];
                    string[] strFullNames = new string[(nRows - 1) + 1];
                    string strFirstName = "";
                    string strLastName = "";
                    for (int nCounter = 0; dbReader.Read(); nCounter++)
                    {
                        strLOGINNAMEs[nCounter] = dbReader.GetString(1);
                        strFirstName = dbReader.GetString(2);
                        strLastName = dbReader.GetString(3);
                        strFullNames[nCounter] = strFirstName + " " + strLastName;
                    }
                    UserNames = strLOGINNAMEs;
                    FullNames = strFullNames;
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetGroups(string bstrOrgName, out object V_GROUPS)
        {
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetGroups()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select GROUPNAME from V_ORG_GROUP order by GROUPNAME";
                int nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    V_GROUPS = null;
                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        V_GROUPS = null;
                    }
                    string[] strGroups = new string[(nRows - 1) + 1];
                    for (int nCounter = 0; dbReader.Read(); nCounter++)
                    {
                        strGroups[nCounter] = dbReader.GetString(0);
                    }
                    V_GROUPS = strGroups;
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                V_GROUPS = null;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetManager(string bstrOrgName, string userName, out string Manager)
        {
            string strQuery = "";
            Manager = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetManager()");
            DbDataReader dbReader = null;
            try
            {
                string strDeptId = "";
                string bstrUserID = "";
                string strDept = "";
                this.ShortNameToId("", userName, out bstrUserID);
                this.GetPersonDepartment("", bstrUserID, out strDept);
                this.GetDepartmentID("", strDept, out strDeptId);
                strQuery = "select LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.ISMANAGER=1 and b.DEPARTMENTID="+strDeptId;
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        Manager = "";
                    }
                    if (dbReader.Read())
                    {
                        Manager = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                Manager = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetManagerFromID(string bstrOrgName, string JobFunctionID, out string Manager)
        {
            string strQuery = "";
            Manager = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetManager()");
            DbDataReader dbReader = null;
            try
            {
                string strDeptId = "";
                string bstrUserID = "";
                string strDept = "";
                this.GetFullJobFunctionToID("", JobFunctionID, out bstrUserID);
                this.GetPersonDepartment("", bstrUserID, out strDept);
                this.GetDepartmentID("", strDept, out strDeptId);
                strQuery = "select LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.ISMANAGER=1 and b.DEPARTMENTID=" + strDeptId;
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        Manager = "";
                    }
                    if (dbReader.Read())
                    {
                        Manager = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                Manager = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetManagerJobFunction(string bstrOrgName, string JobFunction, out string ManagerJF)
        {
            string strQuery = "";
            ManagerJF = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetManager()");
            DbDataReader dbReader = null;
            try
            {
                string strDeptId = "";
                string bstrUserID = "";
                this.GetFullJobFunctionToID("", JobFunction, out bstrUserID);
                this.GetPersonDepartment("", bstrUserID, out strDeptId);
                strQuery = "select LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.ISMANAGER=1 and b.DEPARTMENTID=" + strDeptId;
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        ManagerJF = "";
                    }
                    if (dbReader.Read())
                    {
                        ManagerJF = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                ManagerJF = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetMemberWeight(string bstrOrgName, string grpName, string member, out int weight)
        {
            weight = -1;
        }
        public void GetNextMember(string bstrOrgName, string grpName, string prevMember, out string member)
        {
            string strQuery = "";
            member = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetNextMember()");
            DbDataReader dbReader = null;
            try
            {
                string id = "";
                string gID = "";
                this.ShortNameToId("", prevMember, out id);
                this.GetGroupID("", grpName, out gID);
                strQuery = "select LOGINNAME from V_ORG_USER where USERID=(select Top (1) MEMBERID from V_ORG_GROUPMEMBER where (MEMBERID>'" + id + "') And (GROUPID='" + gID + "') Order By MEMBERID Asc)";
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        member = "";
                    }
                    if (dbReader.Read())
                    {
                        member = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                member = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        public void GetParentDepartmentName(string bstrOrgName, string bstrDeptName, out string bstrParentDeptName)
        {
            string strQuery = "";
            bstrParentDeptName = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetParentDepartmentName()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select DEPARTMENTNAME from V_ORG_DEPARTMENT where PARENTID IN (select PARENTID from V_ORG_DEPARTMENT where DEPARTMENTNAME='" + bstrDeptName + "'";
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                else
                {
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        bstrParentDeptName = "";
                    }
                    if (dbReader.Read())
                    {
                        bstrParentDeptName = dbReader.GetString(0);
                    }
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                bstrParentDeptName = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetPersonDepartment(string bstrOrgName, string bstrUserID, out string dept)
        {
            string strQuery = "";
            dept = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetPersonDepartment()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select DEPARTMENTNAME from V_ORG_DEPARTMENT where DEPARTMENTID IN (select DEPARTMENTID from V_ORG_USERDEPARTMENT where USERID='" + bstrUserID + "')";
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    dept = "";
                }
                if (dbReader.Read())
                {
                    dept = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                dept = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetPrimaryJobFunction(string bstrOrgName, string userName, out string JobFunction)
        {
            string strQuery = "";
            JobFunction = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetPrimaryJobFunction");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select a.JOBFUNCTION from V_ORG_USERDEPARTMENT a inner join V_ORG_USER b on a.USERID=b.USERID where b.LOGINNAME='"+userName+"' and a.ISPRIMARY=1";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    JobFunction = "";
                }
                if (dbReader.Read())
                {
                    JobFunction = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                JobFunction = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        public void GetSubordinates(string bstrOrgName, string bstrDeptName, string bstrUserName, string userID, out object UserNames, out object FullNames, out object JobFunctions, out object UserIDs, out object SubordinateTypes)
        {
            string strQuery = "";
            string strDeptQuery = "";
            Helper helper = new Helper();
            if (!this.IsGetSubordinatesImplemented)
            {
                if (string.IsNullOrEmpty(bstrDeptName))
                {
                    helper.AppendTextToLog("-- Method = GetSubordinates is not Implemented");
                    //throw new NotImplementedException();
                    UserNames = null;
                    FullNames = null;
                    JobFunctions = null;
                    UserIDs = null;
                    SubordinateTypes = null;
                    return;
                }
            }
            Debug.WriteLine("in Subordinate dep:" + bstrDeptName + " strUserName: " + bstrUserName);
            if ((bstrDeptName == null) | (bstrUserName == null))
            {
                helper.AppendTextToLog("-- Method = GetSubordinates()");
                helper.AppendTextToLog("Either strDepartment or strUserName is not specified");
                throw new ExternalException();
            }
            helper.AppendTextToLog("-- Method = GetSubordinates()");
            DbDataReader dbReader = null;
            try
            {
                string DeptID = "";
                this.GetDepartmentID(bstrDeptName, bstrDeptName, out DeptID);
                if (string.IsNullOrEmpty(DeptID)) 
                {
                    if (!string.IsNullOrEmpty(userID) && Convert.ToInt32(userID) > 0)//展开用户
                    {
                        strQuery = "Select a.USERID , USERNAME,'' AS LASTNAME,  LOGINNAME, b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.SUPERVISORUSERID=" + userID + "  order by USERNAME ";
                    }
                    else
                    {
                        //strQuery = "Select a.USERID , USERNAME,'' AS LastName,  LOGINNAME, b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID order by b.JOBFUNCTION";
                    }
                }
                else//展开部门
                {
                    strQuery = "Select a.USERID , USERNAME,'' AS LASTNAME,  LOGINNAME, b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.DEPARTMENTID="+DeptID+" order by USERNAME";
                    strDeptQuery = "Select DEPARTMENTID, DEPARTMENTNAME from V_ORG_DEPARTMENT where PARENTID='" + DeptID + "'";
                }
                
                Debug.WriteLine(strQuery);
                helper.AppendTextToLog(strQuery);
                int nRows = helper.GetTotalRows(strQuery);
                int nDeptRows = 0;
                int totalRows = 0;
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    UserNames = null;
                    FullNames = null;
                    JobFunctions = null;
                    UserIDs = null;
                    SubordinateTypes = null;
                }
                else
                {
                    if (strDeptQuery != "")
                    {
                        nDeptRows = helper.GetTotalRows(strDeptQuery);
                        if (nDeptRows <= 0)
                        {
                            totalRows = nRows - 1;
                        }
                        else
                        {
                            totalRows = (nRows + nDeptRows) - 1;
                        }
                    }
                    else
                    {
                        totalRows = nRows - 1;
                    }
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        throw new Exception(strError);
                    }
                    string[] arrUserIds = new string[totalRows + 1];
                    string[] strMembers = new string[totalRows + 1];
                    string[] strJobFunctions = new string[totalRows + 1];
                    string[] strFullNames = new string[totalRows + 1];
                    int[] strSubordinateTypes = new int[totalRows + 1];
                    string strFirstName = "";
                    string strLastName = "";
                    int nCounter = 0;
                    while (dbReader.Read())
                    {
                        arrUserIds[nCounter] = dbReader.GetInt32(0).ToString();
                        strFirstName = dbReader.GetString(1);
                        strLastName = dbReader.GetString(2);
                        strFullNames[nCounter] = strFirstName + " " + strLastName;
                        strMembers[nCounter] = dbReader.GetString(3);
                        strJobFunctions[nCounter] = dbReader.GetString(4);
                        strSubordinateTypes[nCounter] = 2;
                        nCounter++;
                    }
                    dbReader.Close();
                    if (nDeptRows > 0)
                    {
                        dbReader = helper.RunQuery(strDeptQuery, out strError);
                        while (dbReader.Read())
                        {
                            arrUserIds[nCounter] = dbReader.GetInt32(0).ToString();
                            strFullNames[nCounter] = dbReader.GetString(1);
                            strMembers[nCounter] = dbReader.GetString(1);
                            strJobFunctions[nCounter] = dbReader.GetString(1);
                            strSubordinateTypes[nCounter] = 0x20;
                            nCounter++;
                        }
                        dbReader.Close();
                    }
                    UserNames = strMembers;
                    FullNames = strFullNames;
                    JobFunctions = strJobFunctions;
                    UserIDs = arrUserIds;
                    SubordinateTypes = strSubordinateTypes;
                    helper.AppendTextToLog("-- Method = GetSubordinates() Executed Successfully");
                }
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                UserNames = null;
                FullNames = null;
                JobFunctions = null;
                UserIDs = null;
                SubordinateTypes = null;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }

        }
        public void GetSupervisor(string bstrOrgName, string userName, out string Supervisor)
        {
            string strQuery = "";
            Supervisor = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetSupervisor()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select a.LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.SUPERVISORUSERID inner join V_ORG_USER c on b.USERID=c.USERID and c.LOGINNAME='"+userName+"'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                while (dbReader.Read())
                {
                    Supervisor = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                Supervisor = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetSupervisorFromID(string bstrOrgName, string JobFunctionID, out string Supervisor)
        {
            string strQuery = "";
            Supervisor = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method =  GetSupervisorFromID()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select a.LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.SUPERVISORUSERID and b.JOBID="+JobFunctionID;
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                while (dbReader.Read())
                {
                    Supervisor = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                Supervisor = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetSupervisorJobFunction(string bstrOrgName, string JobFunction, out string SupevisorJF)
        {
            string strQuery = "";
            SupevisorJF = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method =  GetSupervisorJobFunction()");
            helper.AppendTextToLog("GetSupervisorJobFunction JobFunction = " + JobFunction);
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select b.JOBFUNCTION from V_ORG_USERDEPARTMENT a "+
                  "inner join V_ORG_USERDEPARTMENT b on a.SUPERVISORJOBID=b.JOBID "+
                  "where a.JOBFUNCTION='"+JobFunction+"' ";
                helper.AppendTextToLog("GetSupervisorJobFunction = " + strQuery);
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                while (dbReader.Read())
                {
                    SupevisorJF = dbReader.GetString(0);
                }
                helper.AppendTextToLog("GetSupervisorJobFunction SupevisorJF = " + SupevisorJF);
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                SupevisorJF = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetUserGroups(string bstrOrgName, string bstrUserName, out object varGroups, out object varDepts, out object varJFGs)
        {
            string strQuery = "";
            string strQuery1 = "";
            varGroups = null;
            varDepts = null;
            varJFGs = null;
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetUserGroups()");
            DbDataReader dbReader = null;
            try
            {
                int nCounter;
                //strQuery = "select 'All Users' union SELECT GROUPNAME FROM V_ORG_GROUP WHERE [ID] IN (SELECT [GroupID] FROM [V_ORG_GROUPMEMBER] WHERE [MEMBERID]=(SELECT USERID FROM V_ORG_USER WHERE LOGINNAME='" + bstrUserName + "'))";
                //                strQuery = @" with org(DEPARTMENTID,DEPARTMENTNAME,PARENTID) 
                //                                 as (
                //                                 select  DEPARTMENTID,DEPARTMENTNAME,PARENTID from ORG_DEPARTMENT where DEPARTMENTID in (SELECT DEPARTMENTID FROM V_ORG_USER WHERE LOGINNAME='" + bstrUserName + @"' )
                //                                 union all
                //                                 select  ORG_DEPARTMENT.DEPARTMENTID,ORG_DEPARTMENT.DEPARTMENTNAME,ORG_DEPARTMENT.PARENTID from ORG_DEPARTMENT
                //                                  join org on ORG_DEPARTMENT.DEPARTMENTID = org.PARENTID
                //                                 )
                //
                //                                  SELECT GROUPNAME 
                //                                  FROM V_ORG_GROUP 
                //                                  WHERE [ID] IN (SELECT [GroupID] 
                //                                                 FROM [V_ORG_GROUPMEMBER] 
                //                                                 WHERE ([MEMBERID] in (SELECT distinct USERID 
                //                                                                         FROM V_ORG_USER 
                //                                                                         WHERE LOGINNAME='" + bstrUserName + @"') 
                //                                                        and MEMBERTYPE=1 )
                //                                                        //or(
                //                                                        //   [MEMBERID] in (select distinct DEPARTMENTID from org) 
                //                                                        //   and
                //                                                        //   MEMBERTYPE=3
                //                                                        //  )
                //                                                        //)
                //                                        AND                 
                //                                        ID NOT IN (SELECT [GroupID] 
                //                                                 FROM [V_ORG_GROUPMEMBER] 
                //                                                 WHERE ([MEMBERID] in (SELECT distinct USERID 
                //                                                                         FROM V_ORG_USER 
                //                                                                         WHERE LOGINNAME='" + bstrUserName + @"') 
                //                                                        and MEMBERTYPE=99)
                //                      
                strQuery = "SELECT GROUPNAME FROM V_ORG_GROUP ";

                //strQuery1 = "select DEPARTMENTNAME from V_ORG_DEPARTMENT where DEPARTMENTID IN(select DEPARTMENTID from V_ORG_USERDEPARTMENT where USERID in (select USERID from V_ORG_USER where LOGINNAME='" + bstrUserName + "'))";
                strQuery1 = @"with cte as 
                    ( 
                    select a.DEPARTMENTNAME,a.PARENTID  from V_ORG_DEPARTMENT a where DEPARTMENTID
                    in (select DEPARTMENTID from V_ORG_DEPARTMENT where DEPARTMENTID IN (select DEPARTMENTID from V_ORG_USERDEPARTMENT where USERID in (select USERID from V_ORG_USER 
                    where LOGINNAME='" + bstrUserName + @"'))) 
                    union all  
                    select k.DEPARTMENTNAME,k.PARENTID  from V_ORG_DEPARTMENT k inner join cte c on c.PARENTID = k.DEPARTMENTID 
                    )select * from cte";

                int nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");

                }
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                }
                string[] strGroups = new string[(nRows - 1) + 1];
                for (nCounter = 0; dbReader.Read(); nCounter++)
                {
                    if (isGroupMemberWx(dbReader.GetString(0), bstrUserName))
                    {
                        strGroups[nCounter] = dbReader.GetString(0);
                    }
                }
                varGroups = strGroups;
                int nRows1 = helper.GetTotalRows(strQuery1);
                dbReader = helper.RunQuery(strQuery1, out strError);
                string[] strDepts = new string[(nRows1 - 1) + 1];
                for (nCounter = 0; dbReader.Read(); nCounter++)
                {
                    strDepts[nCounter] = dbReader.GetString(0);
                }
                varDepts = strDepts;
                dbReader.Close();
                varJFGs = null;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);

                return;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetUserJobFunctionInDepartment(string bstrOrgName, string bstrDepartment, string bstrUserName, out string bstrJobFunction)
        {
            string strQuery = "";
            bstrJobFunction = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method =  GetSupervisorFromID()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select JOBFUNCTION AS Title from V_ORG_USERDEPARTMENT where USERID In (select V_ORG_USER.USERID from V_ORG_USER,V_ORG_USERDEPARTMENT where (V_ORG_USER.LOGINNAME = '" + bstrUserName + "' )and (V_ORG_USERDEPARTMENT.USERID IN(SELECT USERID from V_ORG_USER where LOGINNAME = '" + bstrUserName + "')))";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                while (dbReader.Read())
                {
                    bstrJobFunction = bstrDepartment + @"\" + dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                bstrJobFunction = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetUserProperties(string bstrOrgName, string bstrUserName, out string bstrFullName, out string bstrJobFunction, out string bstrUserID, out string bstrSupervisor, out string bstrManager, out string bstrDepartment)
        {
            bstrUserID = "";
            bstrFullName = "";
            bstrJobFunction = "";
            bstrDepartment = "";
            bstrSupervisor = "";
            bstrManager = "";
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = GetUserProperties()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "Select a.USERID , USERNAME,'' AS LastName,  LOGINNAME, b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where a.LOGINNAME='" + bstrUserName + "'";
                if (helper.GetTotalRows(strQuery) <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                }
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                }
                string nUserID = "";
                string strFirstName = "";
                string strLastName = "";
                string strLOGINNAME = "";
                if (dbReader.Read())
                {
                    nUserID = dbReader.GetInt32(0).ToString();
                    strLOGINNAME = dbReader.GetString(1);
                    strFirstName = dbReader.GetString(2);
                    strLastName = dbReader.GetString(3);
                    bstrFullName = strFirstName + " " + strLastName;
                    bstrJobFunction = dbReader.GetString(4);
                    this.GetSupervisor("", strLOGINNAME, out bstrSupervisor);
                    this.GetManager("", strLOGINNAME, out bstrManager);
                    this.GetPersonDepartment("", nUserID, out bstrDepartment);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void GetWeightedGroupMembers(string bstrOrgName, string grpName, out object varUserNames, out object varWeights)
        {
            varUserNames = null;
            varWeights = null;
        }
        public void IDToJobFunction(string bstrOrgName, string bstrUserID, out string bstrJobFunction)
        {
            string strQuery = "";
            bstrJobFunction = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = IDToJobFunction()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "Select b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where a.USERID=" + bstrUserID;
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    bstrJobFunction = "";
                }
                if (dbReader.Read())
                {
                    bstrJobFunction = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                bstrJobFunction = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void IdToShortName(string bstrOrgName, string bstrUserID, out string userName)
        {
            string strQuery = "";
            userName = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = IdToLOGINNAME()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select LOGINNAME from V_ORG_USER where USERID=" + bstrUserID;
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    userName = "";
                }
                if (dbReader.Read())
                {
                    userName = dbReader.GetString(0);
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                userName = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void Init(string bstrOrg, string bstrDomain, string bstrRootDSN, string bstrRootOU, string bstrUserName, string bstrPassword, uint ulTimeOut, uint ulPort)
        {
            new Helper().AppendTextToLog("-- Method = Init");
        }
        public int IsDepartmentUser(string bstrOrgName, string detpID, string deptName, string userName)
        {
            int IsDepartmentUser;
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = IsDepartmentUser()");

            try
            {
                strQuery = "select LOGINNAME from V_ORG_USER where USERID= (select USERID from V_ORG_USER where (LOGINNAME='" + userName + "')And (USERID In (Select USERID from V_ORG_USERDEPARTMENT where DEPARTMENTID =(select DEPARTMENTID from V_ORG_DEPARTMENT where DEPARTMENTNAME='" + deptName + "'))))";
                //                strQuery = @"with org(DEPARTMENTID,DEPARTMENTNAME,PARENTID) 
                //                         as (
                //                         select DEPARTMENTID,DEPARTMENTNAME,PARENTID from ORG_DEPARTMENT where DEPARTMENTID = " + detpID + @"
                //                         union all
                //                         select ORG_DEPARTMENT.DEPARTMENTID,ORG_DEPARTMENT.DEPARTMENTNAME,ORG_DEPARTMENT.PARENTID from ORG_DEPARTMENT
                //                          join org on ORG_DEPARTMENT.PARENTID = org.DEPARTMENTID
                //                         )
                //                 select LOGINNAME from V_ORG_USER where LOGINNAME=" + userName + " And  DEPARTMENTID In ( select DEPARTMENTID from org )";
                string strError = "";
                int nRows = helper.GetTotalRows(strQuery);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return 0;
                }
                if (nRows < 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    return 0;
                }
                IsDepartmentUser = 1;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                IsDepartmentUser = 0;

                return IsDepartmentUser;

            }
            return IsDepartmentUser;

        }
        public int IsMemberOfGroup(string bstrOrgName, string grpName, string userName)
        {
            int IsMemberOfGroup;
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = IsSuperior()");
            try
            {
                object[] varDepts;
                string[] varGroups;
                object[] varJFG;
                object objGroups;
                object objDepts;
                object objVarJFG;
                this.GetUserGroups("", userName, out objGroups, out objDepts, out objVarJFG);
                varJFG = (object[])objVarJFG;
                varDepts = (object[])objDepts;
                varGroups = (string[])objGroups;
                if (varGroups == null)
                {
                    return 0;
                }
                int count = varGroups.Length - 1;
                for (int i = 0; i <= count; i++)
                {
                    if (varGroups[i] == grpName)
                    {
                        return 1;
                    }
                }
                IsMemberOfGroup = 0;
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                IsMemberOfGroup = 0;

                return IsMemberOfGroup;

            }
            return IsMemberOfGroup;

        }
        public int IsSuperior(string bstrOrgName, string bstrUserName, string bstrUserSuperior, int bIsCheckPrimary)
        {
            int IsSuperior;
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = IsSuperior()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select a.LOGINNAME from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.SUPERVISORUSERID and a.USERNAME='"+bstrUserSuperior+"' inner join V_ORG_USER c on b.USERID=c.USERID and c.USERNAME='"+bstrUserName+"'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    return 0;
                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    return 0;
                }
                IsSuperior = 1;
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                IsSuperior = 0;

                return IsSuperior;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            return IsSuperior;

        }
        public int IsWeightedGroup(string bstrOrgName, string grpName)
        {
            return 0;

        }
        public void JobFunctionToName(string bstrOrgName, string JobFunction, out string FullName)
        {
            FullName = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = JobFunctionToName() : "+JobFunction);
            //DbDataReader dbReader = null;
            try
            {
                string[] sz = JobFunction.Split('\\');
                if (sz.Length > 1)
                {
                    JobFunction = sz[1];
                }
                string sql="select b.LOGINNAME from V_ORG_USERDEPARTMENT a "+
                      "inner join V_ORG_USER b on a.USERID=b.USERID "+
                      "where a.JOBFUNCTION='{0}'";
                sql = string.Format(sql, JobFunction);
                DataTable dt= helper.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    FullName = Convert.ToString(dt.Rows[0][0]);
                }
                helper.AppendTextToLog("-- Method = JobFunctionToName() : FullName " + FullName);
                //strQuery = "Select a.USERID , USERNAME,'' AS LastName,  LOGINNAME, b.JOBFUNCTION as Title from V_ORG_USER a inner join V_ORG_USERDEPARTMENT b on a.USERID=b.USERID where b.JOBFUNCTION='"+JobFunction+"' and b.DEPARTMENTID="+DEPARTMENTID;
                //string strError = "";
                //dbReader = helper.RunQuery(strQuery, out strError);
                //if (strError != "")
                //{
                //    helper.AppendTextToLog("Query failed with Error = " + strError);
                //}
                //if (!dbReader.HasRows)
                //{
                //    helper.AppendTextToLog("--  Query did not return any Row");
                //    FullName = "";
                //}
                //if (dbReader.Read())
                //{
                //    FullName = dbReader.GetString(0);
                //}
                //dbReader.Close();
            }
            catch (Exception Ex)
            {
                helper.AppendTextToLog("Method failed with Exception =" + Ex.Message);
                FullName = "";

            }
            finally
            {
                //if (dbReader != null && !dbReader.IsClosed)
                //{
                //    dbReader.Close();
                //}
            }
        }
        public void Search(string bstrOrgName, ushort bstrSearchType, string bstrSearchString, out object varResults)
        {
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = LOGINNAMEToFullName3()");
            //helper.AppendTextToLog("-- Org Name = " + bstrOrgName + ", Search Type = " + bstrSearchType + ", Search String = " + bstrSearchString);
            DbDataReader dbReader = null;
            try
            {
                int nRows;
                switch (bstrSearchType)
                {
                    case 0:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            break;
                        }
                        strQuery = "select LOGINNAME from V_ORG_USER";
                        goto Label_0278;

                    case 1:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            goto Label_00C8;
                        }
                        strQuery = "select LOGINNAME from V_ORG_USER";
                        goto Label_0278;

                    case 2:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            goto Label_013C;
                        }
                        strQuery = @"SELECT     b.DEPARTMENTNAME+'\'+a.JOBFUNCTION
                    from V_ORG_USERDEPARTMENT a inner join V_ORG_DEPARTMENT b on a.DEPARTMENTID=b.DEPARTMENTID order by a.JOBFUNCTION";
                        goto Label_0278;

                    case 3:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            goto Label_0187;
                        }
                        strQuery = "select GROUPNAME from V_ORG_GROUP  order by GROUPNAME";
                        goto Label_0278;

                    case 4:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            goto Label_01D2;
                        }
                        strQuery = "select DEPARTMENTNAME from V_ORG_DEPARTMENT";
                        goto Label_0278;

                    case 5:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            goto Label_021D;
                        }
                        strQuery = "select LOGINNAME from V_ORG_USER";
                        goto Label_0278;

                    case 7:
                        if (!((bstrSearchString == "*") | (bstrSearchString.Trim() == "")))
                        {
                            goto Label_0265;
                        }
                        strQuery = "select LOGINNAME from V_ORG_USER";
                        goto Label_0278;

                    default:
                        goto Label_0278;
                }
                if (bstrSearchString.Contains("*"))
                {
                    bstrSearchString = bstrSearchString.Replace("*", "%");
                    strQuery = "select LOGINNAME from V_ORG_USER where LOGINNAME Like'" + bstrSearchString + "'";

                }
                else
                    strQuery = "select LOGINNAME from V_ORG_USER where LOGINNAME Like '%" + bstrSearchString + "'";
                goto Label_0278;
            Label_00C8:
                bstrSearchString = bstrSearchString.Replace("*", "");
                strQuery = "select LOGINNAME from V_ORG_USER where (USERNAME+[LastName]) like'%" + bstrSearchString + "%'";
                goto Label_0278;
            Label_013C:
                bstrSearchString = bstrSearchString.Replace("*", "");
            strQuery = @"SELECT     b.DEPARTMENTNAME+'\'+a.JOBFUNCTION
from V_ORG_USERDEPARTMENT a inner join V_ORG_DEPARTMENT b on a.DEPARTMENTID=b.DEPARTMENTID where b.DEPARTMENTNAME+'\'+a.JOBFUNCTION like '%"+bstrSearchString+"%'  order by a.JOBFUNCTION ";
                goto Label_0278;
            Label_0187:
                bstrSearchString = bstrSearchString.Replace("*", "");
                strQuery = "select GROUPNAME from V_ORG_GROUP where GROUPNAME like '%" + bstrSearchString + "%' order by GROUPNAME";
                goto Label_0278;
            Label_01D2:
                bstrSearchString = bstrSearchString.Replace("*", "");
                strQuery = "select DEPARTMENTNAME from V_ORG_DEPARTMENT where DEPARTMENTNAME like '%" + bstrSearchString + "%'";
                goto Label_0278;
            Label_021D:
                if (bstrSearchString.Contains("*"))
                {
                    bstrSearchString = bstrSearchString.Replace("*", "%");
                    strQuery = "select LOGINNAME from V_ORG_USER where EMAIL Like'" + bstrSearchString + "'";
                }
                else
                    strQuery = "select LOGINNAME from V_ORG_USER where EMAIL = '" + bstrSearchString + "'";
                goto Label_0278;
            Label_0265:
                if (bstrSearchString.Contains("*"))
                {
                    bstrSearchString = bstrSearchString.Replace("*", "%");
                    strQuery = "select LOGINNAME from V_ORG_USER where LOGINNAME Like'" + bstrSearchString + "'";
                }
                else
                    strQuery = "select LOGINNAME from V_ORG_USER where LOGINNAME '" + bstrSearchString + "'";
            Label_0278:

                //helper.AppendTextToLog("-- Query = " + strQuery);

                nRows = helper.GetTotalRows(strQuery);
                if (nRows <= 0)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    varResults = null;
                }
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);
                    varResults = null;
                }
                string[] strUsers = new string[(nRows - 1) + 1];
                for (int nCounter = 0; dbReader.Read(); nCounter++)
                {
                    strUsers[nCounter] = dbReader.GetString(0);
                }
                varResults = strUsers;
                dbReader.Close();
            }
            catch (Exception Ex)
            {

                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                varResults = null;
                return;

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void ShortNameToFullName(string bstrOrgName, string LOGINNAME, out string FullName)
        {
            string strQuery = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = ShortNameToFullName()");
            DbDataReader dbReader = null;
            try
            {
                if (LOGINNAME.IndexOf("/") < 0)
                {
                    FullName = LOGINNAME;
                }
                else
                {
                    strQuery = "select USERNAME, '' as LastName from V_ORG_USER where LOGINNAME='" + LOGINNAME + "'";
                    string strError = "";
                    dbReader = helper.RunQuery(strQuery, out strError);
                    if (strError != "")
                    {
                        helper.AppendTextToLog("Query failed with Error = " + strError);
                        FullName = "";
                    }
                    if (!dbReader.HasRows)
                    {
                        helper.AppendTextToLog("--  Query did not return any Row");
                        FullName = "";
                    }
                    string strFirstName = "";
                    string strLastName = "";
                    if (dbReader.Read())
                    {
                        strFirstName = dbReader.GetString(0);
                        strLastName = dbReader.GetString(1);
                    }
                    FullName = strFirstName + " " + strLastName;
                    dbReader.Close();
                }
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                FullName = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }
        public void ShortNameToId(string bstrOrgName, string person, out string id)
        {
            string strQuery = "";
            id = "";
            Helper helper = new Helper();
            helper.AppendTextToLog("-- Method = LOGINNAMEToFullName2()");
            DbDataReader dbReader = null;
            try
            {
                strQuery = "select USERID from V_ORG_USER where LOGINNAME='" + person + "'";
                string strError = "";
                dbReader = helper.RunQuery(strQuery, out strError);
                if (strError != "")
                {
                    helper.AppendTextToLog("Query failed with Error = " + strError);

                }
                if (!dbReader.HasRows)
                {
                    helper.AppendTextToLog("--  Query did not return any Row");
                    id = "";
                }
                if (dbReader.Read())
                {
                    id = dbReader.GetInt32(0).ToString();
                }
                dbReader.Close();
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);
                id = "";

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
        }

        public int ValidateUser(string bstrDomainName, string bstrUserName, string bstrPassword)
        {
            new Helper().AppendTextToLog("Validate User");
            return 1;
        }

        public void ValidateUserAndGetSignature(string bstrDomainName, string bstrUserName, string bstrPassword, out object pvtSignature)
        {
            new Helper().AppendTextToLog(" Method ---ValidateUserAndGetSignature()");
            pvtSignature = null;
        }

        /// <summary>
        /// 根据组名称和登陆帐号判断用户是否在组中
        /// </summary>
        /// <param name="groupname"></param>
        /// <param name="LOGINNAME"></param>
        /// <returns></returns>
        public bool isGroupMemberWx(string grpName, string LOGINNAME)
        {
            DbDataReader dbReader = null;
            Helper helper = new Helper();
            string strQuery = "";
            try
            {
                if (grpName == "All Users")
                {
                    strQuery = "select USERID, LOGINNAME, USERNAME,'' AS LastName from V_ORG_USER where 1=1 and LOGINNAME='" + LOGINNAME + "' order by USERNAME";
                }
                else
                {
                    strQuery = @"DECLARE @GroupID INT
                                   SELECT @GroupID=GroupID FROM V_ORG_GROUP WHERE GROUPNAME = '" + grpName + @"';
                                        with org(DEPARTMENTID,DEPARTMENTNAME,PARENTID) 
                                            as (
                                            select DEPARTMENTID,DEPARTMENTNAME,PARENTID from V_ORG_DEPARTMENT where DEPARTMENTID IN (SELECT  MEMBERID 
	                                                                            FROM  V_ORG_GROUPMEMBER 
	                                                                            WHERE  GROUPID =@GroupID and MEMBERTYPE = 3)
                                            union all
                                            select V_ORG_DEPARTMENT.DEPARTMENTID,V_ORG_DEPARTMENT.DEPARTMENTNAME,V_ORG_DEPARTMENT.PARENTID from V_ORG_DEPARTMENT
                                            join org on V_ORG_DEPARTMENT.PARENTID = org.DEPARTMENTID
                                            )
                                    SELECT DISTINCT LOGINNAME
                                                FROM         V_ORG_USER    
                                                WHERE  ( USERID IN (SELECT  MEMBERID 
	                                                                    FROM  V_ORG_GROUPMEMBER 
	                                                                    WHERE  GROUPID =@GroupID and MEMBERTYPE = 1)  
                                                    OR USERID IN (SELECT DISTINCT USERID
	                                                                    FROM          V_ORG_USERDEPARTMENT
	                                                                    WHERE      DEPARTMENTID in (SELECT DISTINCT DEPARTMENTID  FROM ORG )))
                                                    AND USERID not in( SELECT  MEMBERID 
	                                                                    FROM  V_ORG_GROUPMEMBER 
	                                                                    WHERE  GROUPID =@GroupID and MEMBERTYPE = 99) and LOGINNAME='" + LOGINNAME + "'";

                }
            }
            catch (Exception Ex)
            {


                helper.AppendTextToLog("Method failed with Exception = " + Ex.Message);

            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            int nRows = helper.GetTotalRows(strQuery);
            return nRows > 0;
        }
    }
}

