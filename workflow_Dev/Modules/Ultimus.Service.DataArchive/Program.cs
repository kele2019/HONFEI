using System;
using System.Collections.Generic;
using System.Text;
using MyLib;

namespace UltimusArchive
{
    class Program
    {
        static void Main(string[] args)
        {
            if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
            {
                UltimusArchiveOracleLogic logic = new UltimusArchiveOracleLogic();
                logic.Run();
            }
            else
            {
                UltimusArchiveSQLLogic logic = new UltimusArchiveSQLLogic();
                logic.Run();
            }
        }
    }
}
