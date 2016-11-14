using System.Web.Mvc;

namespace Apl.UI.Artifacts
{
    public class GetFile : ActionResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var ext = System.IO.Path.GetExtension(FileName);
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            if (!string.IsNullOrEmpty(ext))
            {
            if (ext.Equals(".bmp")) context.HttpContext.Response.ContentType = "image/bmp";
            else if (ext.Equals(".gif")) context.HttpContext.Response.ContentType = "image/gif";
            else if (ext.Equals(".ico")) context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
            else if (ext.Equals(".jpg")) context.HttpContext.Response.ContentType = "image/jpeg";
            else if (ext.Equals(".png")) context.HttpContext.Response.ContentType = "image/png";
            else if (ext.Equals(".tif")) context.HttpContext.Response.ContentType = "image/tiff";
            else if (ext.Equals(".wmf")) context.HttpContext.Response.ContentType = "image/wmf";
            else if (ext.Equals(".pdf")) context.HttpContext.Response.ContentType = "application/pdf";
            else if (ext.Equals(".xls")) context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
            else if (ext.Equals(".xlsx")) context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
            else if (ext.Equals(".doc")) context.HttpContext.Response.ContentType = "application/vnd.ms-word";
            else if (ext.Equals(".docx")) context.HttpContext.Response.ContentType = "application/vnd.ms-word";
            else if (ext.Equals(".rtf")) context.HttpContext.Response.ContentType = "application/vnd.ms-word"; 
            }
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));
        }
    }
}