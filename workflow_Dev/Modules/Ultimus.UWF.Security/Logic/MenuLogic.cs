using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using Ultimus.UWF.Security.Entity;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Security.Logic
{
    /// <summary>
    /// 菜单逻辑类
    /// </summary>
    public class MenuLogic
    {
        /// <summary>
        /// 获取有权限的菜单列表
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <returns>菜单列表</returns>
        public List<MenuEntity> GetMenuList(string loginName)
        {
            SecurityLogic sec = new SecurityLogic();
            return sec.GetMenuList(loginName);
        }

        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <returns>所有菜单列表</returns>
        public List<MenuEntity> GetMenuList()
        {
            List<MenuEntity> list = DataAccess.Instance("BizDB").ExecuteList<MenuEntity>("select * from SEC_MENU");
            return list;
        }

        /// <summary>
        /// 获取默认菜单
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns>默认菜单</returns>
        public MenuEntity GetDefaultMenu(string loginName)
        {
            return DataAccess.Instance("BizDB").ExecuteEntity<MenuEntity>("select * from SEC_MENU where ishomepage='1'");
        }

        public MenuEntity GetEntity(string menuId)
        {
            return DataAccess.Instance("BizDB").ExecuteEntity<MenuEntity>("select * from SEC_MENU where MENUID=@MENUID",menuId);
        }

        public void Delete(string menuId)
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("DELETE FROM SEC_MENU where MENUID=@MENUID", menuId,null);
        }

        public void Save(MenuEntity ety)
        {
            MenuEntity mety = GetEntity(ety.MENUID);
            string sql = "";
            if (mety == null) //插入
            {
                sql = @"
                      insert into SEC_MENU(
                      ID
                          ,MODULE
                          ,MENUID
                          ,MENUNAME
                          ,DISPLAYNAME
                          ,MENUTYPE
                          ,PARENTID
                          ,FORMID
                          ,URL
                          ,ICON
                          ,TARGET
                          ,ACCESSLEVEL
                          ,ISACTIVE
                          ,ISHOMEPAGE
                          ,ISVISIBLE
                          ,RELATEDFOLDER
                          ,RELATEDFORM
                          ,REMARK
                          ,HEIGHT
                          ,ORDERNO
                          ,CREATEDATE
                          ,CREATEBY
                          ,UPDATEDATE
                          ,UPDATEBY
                          ,EXT01
                          ,EXT02
                          ,EXT03
                          ,EXT04
                          ,EXT05
                          ,EXT06
                          ,EXT07
                          ,EXT08
                          ,EXT09
                          ,EXT10
                          ,EXT11
                          ,EXT12
                          ,EXT13
                          ,EXT14
                          ,EXT15
                          ,EXT16
                          ,EXT17
                          ,EXT18
                          ,EXT19
                          ,EXT20
                          ,EXT21
                          ,EXT22
                          ,EXT23
                          ,EXT24
                          ,EXT25
                          ,EXT26
                          ,EXT27
                          ,EXT28
                          ,EXT29
                          ,EXT30
                      )
                      values
                      (
                      @ID
                          ,@MODULE
                          ,@MENUID
                          ,@MENUNAME
                          ,@DISPLAYNAME
                          ,@MENUTYPE
                          ,@PARENTID
                          ,@FORMID
                          ,@URL
                          ,@ICON
                          ,@TARGET
                          ,@ACCESSLEVEL
                          ,@ISACTIVE
                          ,@ISHOMEPAGE
                          ,@ISVISIBLE
                          ,@RELATEDFOLDER
                          ,@RELATEDFORM
                          ,@REMARK
                          ,@HEIGHT
                          ,@ORDERNO
                          ,@CREATEDATE
                          ,@CREATEBY
                          ,@UPDATEDATE
                          ,@UPDATEBY
                          ,@EXT01
                          ,@EXT02
                          ,@EXT03
                          ,@EXT04
                          ,@EXT05
                          ,@EXT06
                          ,@EXT07
                          ,@EXT08
                          ,@EXT09
                          ,@EXT10
                          ,@EXT11
                          ,@EXT12
                          ,@EXT13
                          ,@EXT14
                          ,@EXT15
                          ,@EXT16
                          ,@EXT17
                          ,@EXT18
                          ,@EXT19
                          ,@EXT20
                          ,@EXT21
                          ,@EXT22
                          ,@EXT23
                          ,@EXT24
                          ,@EXT25
                          ,@EXT26
                          ,@EXT27
                          ,@EXT28
                          ,@EXT29
                          ,@EXT30
                      )";
                SerialNoLogic sn = new SerialNoLogic();
                ety.ID = sn.GetMaxNo("SEC_MENU","ID");
                DataAccess.Instance("BizDB").ExecuteNonQuery<MenuEntity>(sql, ety);
            }
            else //更新
            {
                sql = @"
                      update SEC_MENU set MODULE=@MODULE,MENUNAME=@MENUNAME,DISPLAYNAME=@DISPLAYNAME,
                       MENUTYPE=@MENUTYPE,PARENTID=@PARENTID,FORMID=@FORMID,URL=@URL,ICON=@ICON,TARGET=@TARGET,
                       ACCESSLEVEL=@ACCESSLEVEL,ISACTIVE=@ISACTIVE,ISHOMEPAGE=@ISHOMEPAGE,ISVISIBLE=@ISVISIBLE,
                        RELATEDFOLDER=@RELATEDFOLDER,RELATEDFORM=@RELATEDFORM,REMARK=@REMARK,HEIGHT=@HEIGHT,
                        ORDERNO=@ORDERNO,UPDATEDATE=@UPDATEDATE,UPDATEBY=@UPDATEBY,EXT01=@EXT01,EXT02=@EXT02,EXT03=@EXT03,
                        EXT04=@EXT04,EXT05=@EXT05,EXT06=@EXT06,EXT07=@EXT07,EXT08=@EXT08,EXT09=@EXT09,EXT10=@EXT10,
                        EXT11=@EXT11,EXT12=@EXT12,EXT13=@EXT13,
                        EXT14=@EXT14,EXT15=@EXT15,EXT16=@EXT16,EXT17=@EXT17,EXT18=@EXT18,EXT19=@EXT19,EXT20=@EXT20,
                        EXT21=@EXT21,EXT22=@EXT22,EXT23=@EXT23,
                        EXT24=@EXT24,EXT25=@EXT25,EXT26=@EXT26,EXT27=@EXT27,EXT28=@EXT28,EXT29=@EXT29,EXT30=@EXT30

                        where MENUID=@MENUID
                      ";
                DataAccess.Instance("BizDB").ExecuteNonQuery<MenuEntity>(sql, ety);
            }
            
        }

    }
}
