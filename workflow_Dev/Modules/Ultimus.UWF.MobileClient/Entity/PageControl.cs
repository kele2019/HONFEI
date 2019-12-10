using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DALLibrary
{
    public class PageControl
    {
        private MobileClient_Process ProcessDAL = new MobileClient_Process();
        private MobileClient_Step StepDAL = new MobileClient_Step();
        private MobileClient_StepControl StepControlDAL = new MobileClient_StepControl();
        private MobileClient_Control ControlDAL = new MobileClient_Control();

        public EntityLibrary.PageConfig GetPageControlList(int ProcessID)
        {
            try
            {
                EntityLibrary.PageConfig model = new EntityLibrary.PageConfig();

                EntityLibrary.MobileClient_Process p = ProcessDAL.GetModel(ProcessID);
                model.ProcessName = p.ProcessName;
                model.LoGo = p.Logo;

                List<EntityLibrary.PageConfig_Step> item = new List<EntityLibrary.PageConfig_Step>();

                List<EntityLibrary.MobileClient_Step> steplist = StepDAL.GetModel("FK_ID='" + ProcessID + "'");

                foreach (EntityLibrary.MobileClient_Step i in steplist)
                {
                    EntityLibrary.PageConfig_Step step = new EntityLibrary.PageConfig_Step();
                    step.StepName = i.StepName;
                    List<EntityLibrary.MobileClient_StepControl> cons = StepControlDAL.GetModel("FK_ID='" + i.ID + "' order by Convert(int,isnull(OrderBy,0))");
                    List<EntityLibrary.PageConfig_Control> control = new List<EntityLibrary.PageConfig_Control>();
                    step.StepID = i.ID;
                    foreach (EntityLibrary.MobileClient_StepControl c in cons)
                    {
                        EntityLibrary.PageConfig_Control cc = new EntityLibrary.PageConfig_Control();
                        cc.ControlEName = ControlDAL.GetModel(Convert.ToInt32(c.ControlID)).ControlEName;
                        cc.Control = ControlDAL.GetModel(Convert.ToInt32(c.ControlID)).ControlName;
                        cc.ColumnName = c.ColumnName;
                        cc.IsShow = c.IsShow;
                        cc.FORMAT = c.Format;
                        cc.ISWILLFILL = c.IsWillFill;
                        cc.ISREADONLY = c.ReadOnly;
                        if (!string.IsNullOrEmpty(c.DestColumnName))
                        {
                            cc.DestColumnName = c.DestColumnName;
                        }
                        else if (!string.IsNullOrEmpty(c.DestVariableName))
                        {
                            cc.DestColumnName = c.DestVariableName;
                        }
                        else
                        {
                            if (cc.ControlEName == "ApprovalRemark")
                            {
                                cc.DestColumnName = "ApprovalRemark";
                            }
                            else
                            {
                                cc.DestColumnName = c.ColumnName;
                            }
                        }
                        cc.TableName = c.DestTableName;
                        //如果为电子表格，则讲format作为表名
                        if (c.DestType == "ElectronicForm" && c.IsSublist.ToLower() == "true") 
                        {
                            cc.TableName = c.Format;
                        }
                        control.Add(cc);
                    }
                    step.StepControl = control;
                    item.Add(step);
                }
                model.ProcessStep = item;
                return model;
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                return null;
            }
        }
    }
}
