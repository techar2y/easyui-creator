using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CreatorUI.Models
{
    public class SettingsModel
    {
        public bool autoSave;
        public int autoSaveMinutes;

        public SettingsModel()
        {
            autoSave = true;
            autoSaveMinutes = 10;
        }

        public void SaveSettingsToRegistry()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\CreatorUI");
            key.SetValue("AutoSave", autoSave ? "1" : "0");
            key.SetValue("AutoSaveMinutes", autoSaveMinutes.ToString());
            key.Close();
        }

        public void LoadSettinsFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CreatorUI");
            if (key != null)
            {
                autoSave = key.GetValue("AutoSave").ToString() == "1" ? true : false;
                autoSaveMinutes = int.Parse(key.GetValue("AutoSaveMinutes").ToString());
                key.Close();
            }
        }
    }
}
