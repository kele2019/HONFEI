using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorEngine.Templating;
using System.IO;
using RazorEngine;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using System.Web;

namespace Ultimus.UWF.Common.Logic
{
    public class TemplateEngine
    {
        public static void CreatePage<T>(string templatePath, string formPath, T model)
        {
            try
            {
                string template = File.ReadAllText(templatePath);
                string result = Razor.Parse<T>(template, model);
                result = HttpUtility.HtmlDecode(result);
                File.WriteAllText(formPath, result, System.Text.Encoding.UTF8);
            }
            catch (TemplateCompilationException ex)
            {
                ReadOnlyCollection<CompilerError> errors = ex.Errors;
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError err in errors)
                {
                    sb.Append(err.ErrorText);
                }

                throw new Exception(sb.ToString());
            }
        }
    }
}
