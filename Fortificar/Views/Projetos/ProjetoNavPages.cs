using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fortificar.Views.Projetos
{
    public static class ProjetoNavPages
    {
        
        public static string IndexNav => "IndexNav";
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, IndexNav);

        public static string DadosGeraisNav => "DadosGeraisNav";
        public static string DadosGeraisNavClass(ViewContext viewContext) => PageNavClass(viewContext, DadosGeraisNav);
        public static string ResponsavelLegalNav => "ResponsavelLegalNav";
        public static string ResponsavelLegalNavClass(ViewContext viewContext)=> PageNavClass(viewContext, ResponsavelLegalNav);
        public static string ResponsavelTecnicoNav => "ResponsavelTecnicoNav";
        public static string ResponsavelTecnicoNavClass(ViewContext viewContext)=> PageNavClass(viewContext, ResponsavelTecnicoNav);
        public static string ProponenteNav => "ProponenteNav";
        public static string ProponenteNavClass(ViewContext viewContext)=> PageNavClass(viewContext, ProponenteNav);
        public static string DadosProjetoNav => "DadosProjetoNav";
        public static string DadosProjetoNavClass(ViewContext viewContext)=> PageNavClass(viewContext, DadosProjetoNav);
        public static string PlanoAplicacaoNav => "PlanoAplicacaoNav";
        public static string PlanoAplicacaoNavClass(ViewContext viewContext)=> PageNavClass(viewContext, PlanoAplicacaoNav);
        public static string FotosNav => "FotosNav";
        public static string FotosNavClass(ViewContext viewContext)=> PageNavClass(viewContext, FotosNav);

        
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
