using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using CreatorUI.Models;

namespace CreatorUI.Forms
{
    public partial class SettingsForm : Form
    {
        public bool AutoSave
        {
            get { return cbAutoSave.Checked; }
            set { cbAutoSave.Checked = value; }
        }

        public int AutoSaveMinutes
        {
            get { return int.Parse(txAutoSave.Text); }
            set { txAutoSave.Text = value.ToString(); }
        }

        public SettingsForm(SettingsModel settings = null)
        {
            InitializeComponent();

            if (settings != null)
            {
                AutoSave = settings.autoSave;
                AutoSaveMinutes = settings.autoSaveMinutes;
            }
            else
            {
                AutoSave = true;
                AutoSaveMinutes = 10;
            }

            CbAutoSave_CheckedChanged(null, null);
        }

        /// <summary>
        /// Обработка нажатия клавиши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxAutoSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (AutoSaveMinutes == 0)
                {
                    AutoSave = false;
                    CbAutoSave_CheckedChanged(null, null);
                }

                SettingsModel settings = new SettingsModel();

                settings.autoSave = AutoSave;
                settings.autoSaveMinutes = AutoSaveMinutes;

                settings.SaveSettingsToRegistry();
            }
        }

        private void CbAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            txAutoSave.Enabled = cbAutoSave.Checked;
            labelAutoSaveMin.Enabled = cbAutoSave.Checked;
        }
    }
}
