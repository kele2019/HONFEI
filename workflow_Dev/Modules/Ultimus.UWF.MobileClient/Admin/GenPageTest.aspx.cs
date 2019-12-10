using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreatePageLibrary;
using RazorEngine.Templating;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using System.Text;

namespace MobileClientBackground
{
    public partial class GenPageTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CreatePage page = new CreatePage();
                page.CreatePageFile(20);
            }
            catch (TemplateCompilationException ex)
            {
                ReadOnlyCollection<CompilerError> errors = ex.Errors;
                StringBuilder sb=new StringBuilder();
                foreach (CompilerError err in errors)
                {
                    sb.Append(err.ErrorText);
                }
                throw new Exception(sb.ToString(), ex);
            }
        }
    }
}