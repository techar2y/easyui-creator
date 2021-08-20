namespace CreatorUI.Forms
{
    partial class FindAndReplaceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindAndReplaceForm));
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lFind = new System.Windows.Forms.Label();
            this.lReplace = new System.Windows.Forms.Label();
            this.txFind = new System.Windows.Forms.TextBox();
            this.txReplace = new System.Windows.Forms.TextBox();
            this.toolTipFindAndReplace = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnReplace
            // 
            this.btnReplace.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnReplace.Location = new System.Drawing.Point(12, 104);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 0;
            this.btnReplace.Text = "Заменить";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(290, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lFind
            // 
            this.lFind.AutoSize = true;
            this.lFind.Location = new System.Drawing.Point(17, 15);
            this.lFind.Name = "lFind";
            this.lFind.Size = new System.Drawing.Size(40, 13);
            this.lFind.TabIndex = 2;
            this.lFind.Text = "Найти";
            // 
            // lReplace
            // 
            this.lReplace.AutoSize = true;
            this.lReplace.Location = new System.Drawing.Point(17, 52);
            this.lReplace.Name = "lReplace";
            this.lReplace.Size = new System.Drawing.Size(74, 13);
            this.lReplace.TabIndex = 3;
            this.lReplace.Text = "Заменить на";
            // 
            // txFind
            // 
            this.txFind.Location = new System.Drawing.Point(130, 12);
            this.txFind.Name = "txFind";
            this.txFind.Size = new System.Drawing.Size(219, 22);
            this.txFind.TabIndex = 4;
            // 
            // txReplace
            // 
            this.txReplace.Location = new System.Drawing.Point(130, 49);
            this.txReplace.Name = "txReplace";
            this.txReplace.Size = new System.Drawing.Size(219, 22);
            this.txReplace.TabIndex = 5;
            // 
            // toolTipFindAndReplace
            // 
            this.toolTipFindAndReplace.AutoPopDelay = 3000;
            this.toolTipFindAndReplace.InitialDelay = 500;
            this.toolTipFindAndReplace.IsBalloon = true;
            this.toolTipFindAndReplace.ReshowDelay = 100;
            this.toolTipFindAndReplace.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTipFindAndReplace.ToolTipTitle = "Ошибка";
            // 
            // FindAndReplaceForm
            // 
            this.AcceptButton = this.btnReplace;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(377, 139);
            this.Controls.Add(this.txReplace);
            this.Controls.Add(this.txFind);
            this.Controls.Add(this.lReplace);
            this.Controls.Add(this.lFind);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindAndReplaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Найти и заменить текст в ID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindAndReplaceForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lFind;
        private System.Windows.Forms.Label lReplace;
        private System.Windows.Forms.TextBox txFind;
        private System.Windows.Forms.TextBox txReplace;
        private System.Windows.Forms.ToolTip toolTipFindAndReplace;
    }
}