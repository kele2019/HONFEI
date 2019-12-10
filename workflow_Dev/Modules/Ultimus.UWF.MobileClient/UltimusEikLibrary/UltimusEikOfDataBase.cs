using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EntityLibrary;

namespace UltimusEikLibrary
{
    public class UltimusEikOfDataBase
    {
        private DALLibrary.MobileClient_Classification ClassificationDAL = new DALLibrary.MobileClient_Classification();

        /// <summary>
        /// 获得所有分类集合
        /// </summary>
        /// <returns></returns>
        public List<MobileClient_Classification> GetCategory()
        {
            return ClassificationDAL.GetCategoryName("");
        }

        /// <summary>
        /// 获得指定分类下的流程个数
        /// </summary>
        /// <param name="ClassificationInfo">分类名称</param>
        /// <returns>int</returns>
        public int GetCountByClassificationInfo(string ClassificationInfo)
        {
            return ClassificationDAL.GetCategoryNameCount("CategoryName='" + ClassificationInfo + "' or CategoryENName='" + ClassificationInfo + "'");
        }

        /// <summary>
        /// 根据行号获得集合
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<MobileClient_Classification> GetProcessInfoByIndex(int index)
        {
            return ClassificationDAL.GetMobileClient_ClassificationInfoListByIndex(index);
        }

    }
}
