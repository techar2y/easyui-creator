namespace CreatorUI.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cbAutoSave = new System.Windows.Forms.CheckBox();
            this.txAutoSave = new System.Windows.Forms.TextBox();
            this.labelAutoSaveMin = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbAutoSave
            // 
            this.cbAutoSave.AutoSize = true;
            this.cbAutoSave.Location = new System.Drawing.Point(46, 26);
            this.cbAutoSave.Name = "cbAutoSave";
            this.cbAutoSave.Size = new System.Drawing.Size(115, 17);
            this.cbAutoSave.TabIndex = 0;
            this.cbAutoSave.Text = "Автосохранение";
            this.cbAutoSave.UseVisualStyleBackColor = true;
            this.cbAutoSave.CheckedChanged += new System.EventHandler(this.CbAutoSave_CheckedChanged);
            // 
            // txAutoSave
            // 
            this.txAutoSave.Location = new System.Drawing.Point(161, 24);
            this.txAutoSave.MaxLength = 3;
            this.txAutoSave.Name = "txAutoSave";
            this.txAutoSave.Size = new System.Drawing.Size(63, 22);
            this.txAutoSave.TabIndex = 2;
            this.txAutoSave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxAutoSave_KeyPress);
            // 
            // labelAutoSaveMin
            // 
            this.labelAutoSaveMin.AutoSize = true;
            this.labelAutoSaveMin.Location = new System.Drawing.Point(230, 27);
            this.labelAutoSaveMin.Name = "labelAutoSaveMin";
            this.labelAutoSaveMin.Size = new System.Drawing.Size(37, 13);
            this.labelAutoSaveMin.TabIndex = 3;
            this.labelAutoSaveMin.Text = "(мин)";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(43, 308);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(76, 23);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Применить";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(189, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(306, 343);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.labelAutoSaveMin);
            this.Controls.Add(this.txAutoSave);
            this.Controls.Add(this.cbAutoSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbAutoSave;
        private System.Windows.Forms.TextBox txAutoSave;
        private System.Windows.Forms.Label labelAutoSaveMin;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
    }
}