using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreatorUI.Forms;

namespace CreatorUI
{
    class CefAPI
    {
        // Declare a local instance of chromium and the main form in order to execute things from here in the main thread
        private static ChromiumWebBrowser _instanceBrowser = null;
        // The form class needs to be changed according to yours
        private static MainForm _instanceMainForm = null;

        public CefAPI(ChromiumWebBrowser originalBrowser, MainForm mainForm)
        {
            _instanceBrowser = originalBrowser;
            _instanceMainForm = mainForm;
        }
        public void SelUIObject(string Id, bool offSelect)
        {
            _instanceMainForm.Invoke((MethodInvoker)(() => { _instanceMainForm.SelUIObject(Id, offSelect); }));
        }
    }
}
