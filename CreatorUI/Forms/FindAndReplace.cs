using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI.Forms
{
    public partial class FindAndReplaceForm : Form
    {
        public string FindText
        {
            get { return txFind.Text; }
            set { txFind.Text = value; }
        }

        public string ReplaceText
        {
            get { return txReplace.Text; }
            set { txReplace.Text = value; }
        }

        public FindAndReplaceForm()
        {
            InitializeComponent();
        }

        private void FindAndReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel) { return; }

            if (string.IsNullOrEmpty(FindText))
            {
                ShowToolTip("Заполните данное поле", txFind);
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(ReplaceText))
            {
                ShowToolTip("Заполните данное поле", txReplace);
                e.Cancel = true;
                return;
            }
        }

        private void ShowToolTip(string text, Control ctrl)
        {
            toolTipFindAndReplace.Hide(ctrl);
            toolTipFindAndReplace.Show(text, ctrl, 0);
            toolTipFindAndReplace.Show(text, ctrl, 3000);
        }
    }
}
