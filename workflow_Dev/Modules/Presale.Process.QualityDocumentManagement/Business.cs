using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Ultimus.UWF.FunctionManager.Quality_document_management
{
    public class Business
    {
        public DataTable getdepartmentlist()
        {
            DAL da = new DAL();
            DataTable dt = da.GetDataTable("select departmentname from ORG_DEPARTMENT");
            return dt;
        }
    }
}