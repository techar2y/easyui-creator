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
    public partial class ChangeIdsForm : Form
    {
        public string NewType
        {
            get { return txType.Text; }
            set { txType.Text = value; }
        }
        public string NewElemId
        {
            get { return txNewId.Text; }
            set { txNewId.Text = value; }
        }

        public ChangeIdsForm()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txType_Enter(object sender, EventArgs e)
        {
            if (txType.Text == "Module")
            {
                txType.Text = "";
                txType.ForeColor = Color.Black;
            }
        }

        private void txType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txType.Text))
            {
                txType.Text = "Module";
                txType.ForeColor = Color.Gray;
            }
        }

        private void ChangeIdsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel) { return; }

            if (string.IsNullOrEmpty(txType.Text))
            {
                ShowToolTip("Заполните данное поле", txType);
                e.Cancel = true;
                return;
            }
            if (string.IsNullOrEmpty(txNewId.Text))
            {
                ShowToolTip("Заполните данное поле", txNewId);
                e.Cancel = true;
                return;
            }

        }

        private void ShowToolTip(string text, Control ctrl)
        {
            toolTipChangeAllIDs.Hide(ctrl);
            toolTipChangeAllIDs.Show(text, ctrl, 0);
            toolTipChangeAllIDs.Show(text, ctrl, 3000);
        }
    }
}
