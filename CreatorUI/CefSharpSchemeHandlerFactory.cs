using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI
{
    public class CefSharpSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var uri = new Uri(request.Url);
            var AppPath = Application.StartupPath;
            var fileName = AppPath +"/"+ uri.Host + uri.AbsolutePath;
            var mimeType = ResourceHandler.GetMimeType(Path.GetExtension(fileName));
            return ResourceHandler.FromFilePath(fileName, mimeType, autoDisposeStream: true);
        }
    }
}
