using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.Workflow.Entity;
using System.Data;

namespace Ultimus.UWF.Workflow.Interface
{
    public interface IProcessCategory
    {
        List<ProcessCategoryEntity> GetCategoryList();

        List<ProcessEntity> GetProcessList(string categoryName);
        DataTable GetAll();
        List<ProcessCategoryEntity> GetList();
        List<ProcessEntity> GetAllProcessList();
        void  Clear();
    }
}
