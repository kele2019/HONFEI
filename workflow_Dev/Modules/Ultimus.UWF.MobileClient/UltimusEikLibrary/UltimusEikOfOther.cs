using System;
using System.Collections.Generic;
using System.Text;

namespace UltimusEikLibrary
{
    public class UltimusEikOfOther
    {
        private PublicFunctionClass pfc = new PublicFunctionClass();


        /// <summary>
        /// 从administrator中获得域信息列表
        /// </summary>
        /// <returns></returns>
        public string[] GetDomains()
        {
            try
            {
                ULTCONFIGURATIONLib.ConfigureClass oConfig = new ULTCONFIGURATIONLib.ConfigureClass();
                byte by = new byte();
                int i = 0;
                string[] Domains;
                Domains = oConfig.GetDomains(by, i) as string[];
                return Domains;
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
                throw ex;
            }
        }
    }
}
