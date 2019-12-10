using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;

namespace TaskQueueService
{
    public class VarEntity
    {
        string _KEY;

        public string KEY
        {
            get { return _KEY; }
            set { _KEY = value; }
        }

        string _VALUE;

        public string VALUE
        {
            get { return _VALUE; }
            set { _VALUE = value; }
        }

    }

    public class VarLogic
    {
        public List<VarEntity> GetVarList(Hashtable table)
        {
            List<VarEntity> list=new List<VarEntity>();
            foreach(object obj in table.Keys)
            {
                VarEntity var = new VarEntity();
                var.KEY=ConvertUtil.ToString(obj);
                var.VALUE=ConvertUtil.ToString(table[obj]);
                list.Add(var );
            }
            return list;
        }

        public Hashtable GetHashtable(List<VarEntity> list)
        {
            Hashtable table = new Hashtable();
            foreach (VarEntity var in list)
            {
                table.Add(var.KEY, var.VALUE);
            }
            return table;
        }



    }
}
