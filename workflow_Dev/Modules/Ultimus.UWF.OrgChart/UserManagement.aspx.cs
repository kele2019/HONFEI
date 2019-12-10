using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using MyLib;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity; 

namespace Ultimus.UWF.OrgChart
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private StringBuilder StrSQL;
        public string SelectType = "1";
        private List<string> DepartmentsList;
        private DataTable SelectedTable = new DataTable();
        public string SecurityList_ConfirmDelete =Lang.Get("SecurityList_ConfirmDelete");
        protected void Page_Load(object sender, EventArgs e)
        {
            UserTreeView.ShowExpandCollapse = true;
            UserTreeView.ShowLines = true;
            if (Request.QueryString["Type"] != null)
            {
                SelectType = Request.QueryString["Type"].ToString().Trim();
            }
            bool bindGroups = true;
            switch (SelectType)
            {
                case "0": //所有
                    UserTreeView.ShowCheckBoxes = TreeNodeTypes.All;

                    //Button1.Visible = true;
                    //Button2.Visible = true;
                    SelectedList.Visible = true;
                    break;
                case "1": //单选人
                    bindGroups = false;
                    break;
                case "2": //多选人
                    //Button1.Visible = true;
                    //Button2.Visible = true;
                    SelectedList.Visible = true;
                    bindGroups = false;
                    break;
                case "4": //多选部门
                    //Button1.Visible = false;
                    //Button2.Visible = false;
                    UserTreeView.ShowCheckBoxes = TreeNodeTypes.All;
                    Repeater1.Visible = false;
                    bindGroups = false;
                    break;
                default:
                    //Button1.Visible = false;
                    //Button2.Visible = false;
                    break;
            }

            if (hidSelectType.Value.StartsWith("3"))
            {
                UserTreeView.ShowCheckBoxes = TreeNodeTypes.Leaf | TreeNodeTypes.Parent;
            }
            if (!IsPostBack)
            {
                hidSelectType.Value = this.SelectType;
                InitTable();
                BindDepartments();
                if (bindGroups)
                {
                    BindGroups();
                }
            }
            
        }

        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        private void BindDepartments()
        {
            //StrSQL = new StringBuilder();
            //StrSQL.AppendLine("select departmentid,departmentname,parentid as parentdept from V_ORG_DEPARTMENT  vt where parentid=0");
            //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            //int i = 0;
            //foreach (DataRow row in dt.Rows)
            //{
            //    TreeNode node = new TreeNode();
            //    node.Text = getDeptName(row["departmentname"].ToString().Trim());
            //    node.Value = row["departmentid"].ToString().Trim() + "|DEPT";
                
            //    UserTreeView.Nodes.Add(node);
            //    if (i == 0)
            //    {
            //        BingFirstNodes(node);
            //    }
            //    i++;
            //}

            UserTreeView.Nodes.Clear();
            List<DepartmentEntity> list = _org.GetDepartmentList();

            //加第一层节点
            List<DepartmentEntity> rootlist = list.FindAll(p => p.PARENTID == 0);
            foreach (DepartmentEntity ety in rootlist)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ety.DEPARTMENTNAME;

                tn.Value = ety.DEPARTMENTID.ToString() + "|DEPT";
                UserTreeView.Nodes.Add(tn);

                //递归插入子节点
                AddChildNodes(tn, ety.DEPARTMENTID, list);
                if (!flag)
                {
                    tn.Expand();
                    flag = true;
                }
            }
            UserTreeView.ExpandDepth = 0;
        }

        bool flag = false;
        void AddChildNodes(TreeNode parent, int id, List<DepartmentEntity> list)
        {
            List<DepartmentEntity> childlist = list.FindAll(p => p.PARENTID == id);
            foreach (DepartmentEntity ety in childlist)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ety.DEPARTMENTNAME;

                tn.Value = ety.DEPARTMENTID.ToString() + "|DEPT";
                parent.ChildNodes.Add(tn);

                AddChildNodes(tn, ety.DEPARTMENTID, list);


            }
        }

        private void BindGroups()
        {
            TreeNode tn = new TreeNode();
            tn.Text = "Groups组";
            UserTreeView.Nodes.Add(tn);
            StrSQL = new StringBuilder();
            StrSQL.AppendLine("SELECT   GROUPID AS  ID, GROUPNAME FROM         V_ORG_GROUP");
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = getDeptName(row["GROUPNAME"].ToString().Trim());
                node.Value = row["ID"].ToString().Trim()+"|Group" ;

                tn.ChildNodes.Add(node);
                
                i++;
            }
        }

        string rtnJson = "";
        protected void btnOK_Click(object sender, EventArgs e)
        {
            IterateTreeView(UserTreeView.Nodes);
            Page.ClientScript.RegisterStartupScript(this.GetType(),"RtnVal", "<script>Confirm(\""+rtnJson+"\");</script>");
        }

        public void IterateTreeView(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                if (tn.Checked)
                {
                    if (tn.Value.Split(new char[] { '|' }).Length > 1)
                    {
                        rtnJson += "{'Name':'" + tn.Text + "[" + tn.Value.Split(new char[] { '|' })[1] + "]',";
                        rtnJson += "'ID':'" + tn.Value.Split(new char[] { '|' })[0] + "',";
                        rtnJson += "'Type':'" + tn.Value.Split(new char[] { '|' })[1] + "'},";
                    }
                }
                IterateTreeView(tn.ChildNodes);
            }
        }

        protected void UserTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            BingChildNodes();
            BingUserList();
        }

        private void BingFirstNodes(TreeNode nn)
        {
            string nodeValue = nn.Value;
            string id = "";
            if (nodeValue.IndexOf("|") > 0)
            {
                id = nodeValue.Split('|')[0].ToString().Trim();
            }
            StrSQL = new StringBuilder();
            StrSQL.AppendFormat("select departmentid,departmentname,parentid as  parentdept from V_ORG_DEPARTMENT  vt where parentid='{0}'", id);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            if (dt.Rows.Count > 0)
            {
                if (nn.ChildNodes.Count > 0)
                {
                    nn.ChildNodes.Clear();
                }
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = getDeptName(row["departmentname"].ToString().Trim());
                    node.Value = row["departmentid"].ToString().Trim() + "|DEPT";
                    nn.ChildNodes.Add(node);
                }
                nn.Expand();
            }
        }

        private void BingChildNodes()
        {
            string nodeValue = UserTreeView.SelectedNode.Value;
            
            string id = "";
            string type = "";
            if (nodeValue.IndexOf("|") > 0)
            {
                id = nodeValue.Split('|')[0].ToString().Trim();
                type = nodeValue.Split('|')[1].ToString().Trim();
            }
            hidDepartmentID.Value = id;
            hidDepartmentName.Value = UserTreeView.SelectedNode.Text;

            if (UserTreeView.SelectedNode.ChildNodes.Count > 0)
            {
                return;
            }

            if (type == "DEPT")
            {
                StrSQL = new StringBuilder();
                StrSQL.AppendFormat("select departmentid,departmentname,parentid as  parentdept from V_ORG_DEPARTMENT  vt where parentid='{0}'", id);
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (UserTreeView.SelectedNode.ChildNodes.Count > 0)
                    {
                        UserTreeView.SelectedNode.ChildNodes.Clear();
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = getDeptName(row["departmentname"].ToString().Trim());
                        node.Value = row["departmentid"].ToString().Trim() + "|DEPT";
                        UserTreeView.SelectedNode.ChildNodes.Add(node);
                    }
                    UserTreeView.SelectedNode.Expand();
                }
            }
        }

        private string getDeptName(string name) {
            if (!String.IsNullOrEmpty(name))
            {
                int index = name.IndexOf('[');
                if (index > 0)
                {
                    return name.Substring(0, index);
                }
                else
                {
                    return name;
                }
            }
            else {
                return "";
            }
        }

        private void BingUserList()
        {
            string nodeValue = UserTreeView.SelectedNode.Value;
            string id = "";
            if (nodeValue.IndexOf('|') > 0)
            {
                id = nodeValue.Split('|')[0].ToString().Trim();
            }
            StrSQL = new StringBuilder();

            StrSQL.AppendFormat("select b.USERID as EmployeeID,a.JOBFUNCTION as Title,b.LOGINNAME as ShortName,b.USERNAME as FirstName,b.EMAIL,a.JOBID from V_ORG_USERDEPARTMENT a inner join V_ORG_USER b on a.USERID=b.USERID where a.DEPARTMENTID='{0}'", id);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            Repeater1.DataSource = dt.DefaultView;
            Repeater1.DataBind();
        }

        private decimal GetUsers(string DepartmentID)
        {
            DepartmentsList = new List<string>();
            GetDepartments(DepartmentID);
            decimal UserCount = 0;
            StrSQL = new StringBuilder();
            if (DepartmentsList.Count > 0)
            {
                StrSQL.AppendLine("select count(*) from V_ORG_USERDEPARTMENT ved inner join V_ORG_USER ve on ved.USERID=ve.USERID where ved.DepartmentID in (");
                foreach (string str in DepartmentsList)
                {
                    StrSQL.AppendLine("'" + str + "',");
                }
                StrSQL = new StringBuilder(StrSQL.ToString().Substring(0, StrSQL.ToString().LastIndexOf(',')));
                StrSQL.AppendLine(")");
            }
            else
            {
                StrSQL.AppendLine("select count(*) from V_ORG_USERDEPARTMENT ved inner join V_ORG_USER ve on ved.USERID=ve.USERID where ved.DepartmentID='" + DepartmentID + "'");
            }
            UserCount =ConvertUtil.ToDecimal( DataAccess.Instance("BizDB").ExecuteScalar(StrSQL.ToString()));
               
            
            return UserCount;
        }

        private void GetDepartments(string DepartmentID)
        {
            StrSQL = new StringBuilder();
            StrSQL.AppendLine("select * from V_ORG_DEPARTMENT   where parentid= '" + DepartmentID + "'");
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            foreach (DataRow row in dt.Rows)
            {
                DepartmentsList.Add(row["departmentid"].ToString());
                GetDepartments(row["departmentid"].ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["table"] as DataTable;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox cb = item.FindControl("CheckBox1") as CheckBox;
                if (cb.Checked)
                {
                    //cb.Enabled = false;
                    string str=(item.FindControl("UserID") as HiddenField).Value;
                    if (dt.Select("EmployeeID="+str).Length == 0)
                    {
                        dt.Rows.Add(dt.NewRow());
                        dt.Rows[dt.Rows.Count - 1]["Title"] = (item.FindControl("Label2") as Label).Text;
                        dt.Rows[dt.Rows.Count - 1]["ShortName"] = (item.FindControl("UserAccount") as HiddenField).Value;
                        dt.Rows[dt.Rows.Count - 1]["FirstName"] = (item.FindControl("Label1") as Label).Text;
                        dt.Rows[dt.Rows.Count - 1]["email"] = (item.FindControl("Label3") as Label).Text;
                        dt.Rows[dt.Rows.Count - 1]["EmployeeID"] = (item.FindControl("UserID") as HiddenField).Value;
                    }
                }
            }
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
            ViewState["table"] = dt;

            //Button2.Visible = true;
        }

        private void InitTable()
        {
            this.SelectedTable.Columns.Add("EmployeeID");
            this.SelectedTable.Columns.Add("Title");
            this.SelectedTable.Columns.Add("ShortName");
            this.SelectedTable.Columns.Add("FirstName");
            this.SelectedTable.Columns.Add("email");
            ViewState["table"] = this.SelectedTable;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            (e.Item.FindControl("CheckBox1") as CheckBox).Visible = false;
            (e.Item.FindControl("RadioButton1") as RadioButton).Visible = false;
            switch (this.SelectType)
            {
                case "0":
            (e.Item.FindControl("CheckBox1") as CheckBox).Visible = true;
                    break;
                case "1":
                    (e.Item.FindControl("RadioButton1") as RadioButton).Visible = true;
                    break;
                case "2":
                    (e.Item.FindControl("CheckBox1") as CheckBox).Visible = true;
                    break;
            }
            //if (this.SelectType == "1")
            //{
            //    (e.Item.FindControl("CheckBox1") as CheckBox).Visible = false;
            //}
            //else if (this.SelectType == "2")
            //{
            //    (e.Item.FindControl("RadioButton1") as RadioButton).Visible = false;
            //    DataTable dt = ViewState["table"] as DataTable;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        if (row["EmployeeID"].ToString() == (e.Item.FindControl("UserID") as HiddenField).Value)
            //        {
            //            (e.Item.FindControl("CheckBox1") as CheckBox).Checked = true;
            //            (e.Item.FindControl("CheckBox1") as CheckBox).Enabled = false;
            //        }
            //    }
            //}

            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater2.Items)
            {
                if ((item.FindControl("CheckBox2") as CheckBox).Checked)
                {
                    string id = (item.FindControl("UserID") as HiddenField).Value;
                    DataTable dt = ViewState["table"] as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["EmployeeID"].ToString() == id)
                        {
                            row.Delete();
                            break;
                        }
                    }
                }
            }
                    //InitTable();
            DataTable table = ViewState["table"] as DataTable;
            Repeater2.DataSource = table;
            Repeater2.DataBind();
            //foreach (RepeaterItem item in Repeater1.Items)
            //{
            //    CheckBox cb = item.FindControl("CheckBox1") as CheckBox;
            //    if (cb.Checked)
            //    {
            //        cb.Enabled = false;
            //    }
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                StrSQL = new StringBuilder();

                StrSQL.AppendFormat("select ve.USERID as EmployeeID,ved.JOBFUNCTION as Title,ve.LOGINNAME as ShortName,ve.USERNAME as FirstName,ve.EMAIL,ved.JOBID from V_ORG_USERDEPARTMENT ved inner join V_ORG_USER ve on ved.SUPERVISORUSERID=ve.USERID where JOBFUNCTION like '%{0}%' or USERNAME like '%{0}%' or LOGINNAME like '%{0}%' or email like '%{0}%'", txtSearch.Text.Trim());
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();
            }
        }

        protected void UserTreeView_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (UserTreeView.SelectedNode != null)
            {
                Response.Redirect("UserDetail.aspx?DepartmentID=" + UserTreeView.SelectedNode.Value + "&DepartmentName=" +Server.UrlEncode( UserTreeView.SelectedNode.Text));
            }
            else
            {
                Response.Redirect("UserDetail.aspx");
            }
        }

        IOrg logic = ServiceContainer.Instance().GetService<IOrg>();
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string str = e.CommandArgument.ToString(); 
                logic.Delete(ConvertUtil.ToInt32(str));
                ViewState["list"] = null;
                BingUserList();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + Lang.Get("SecurityList_DeleteSuccess") + "!');", true);
            }
        }


    }
}