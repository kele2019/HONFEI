using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Interface;
using System.Collections;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Workflow.Implementation
{
    public class DatabaseCategoryLogic : IProcessCategory
    {
        public List<ProcessCategoryEntity> GetCategoryList()
        {
            List<ProcessCategoryEntity> list = new List<ProcessCategoryEntity>();
            list=DataAccess.Instance("BizDB").GetList<ProcessCategoryEntity>("ProcessCategoryLogic_GetList", null);
            return list;
        }

        public List<ProcessEntity> GetProcessList(string categoryName)
        {
            return DataAccess.Instance("BizDB").GetList<ProcessEntity>("ProcessCategoryLogic_GetProcessList", categoryName);
        }
        public List<ProcessCategoryEntity> GetList()
        {
            List<ProcessCategoryEntity> list = DataAccess.Instance("BizDB").GetList<ProcessCategoryEntity>("ProcessCategoryLogic_GetList", null);
            return list;
        }

        public List<ProcessEntity> GetAllProcessList()
        {
            List<ProcessEntity> list = DataAccess.Instance("BizDB").GetList<ProcessEntity>("ProcessCategoryLogic_GetAllProcessList", null);
            return list;
        }

        public DataTable GetAll()
        {
            DataTable list = DataAccess.Instance("BizDB").GetDataSet("ProcessCategoryLogic_GetList", null).Tables[0];
            return list;
        }

        public void Clear()
        {
            DataAccess.Instance("BizDB").GetList<ProcessEntity>("ProcessCategoryLogic_Clear", null);
        }
    }
}
