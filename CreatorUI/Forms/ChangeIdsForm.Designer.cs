namespace CreatorUI.Forms
{
    partial class ChangeIdsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeIdsForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2_ = new System.Windows.Forms.Label();
            this.txNewId = new System.Windows.Forms.TextBox();
            this.toolTipChangeAllIDs = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(91, 80);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Изменить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(200, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txType
            // 
            this.txType.ForeColor = System.Drawing.Color.Gray;
            this.txType.Location = new System.Drawing.Point(69, 34);
            this.txType.Name = "txType";
            this.txType.Size = new System.Drawing.Size(84, 22);
            this.txType.TabIndex = 5;
            this.txType.Text = "Module";
            this.txType.Enter += new System.EventHandler(this.txType_Enter);
            this.txType.Leave += new System.EventHandler(this.txType_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "[elementId]_";
            // 
            // label2_
            // 
            this.label2_.AutoSize = true;
            this.label2_.Location = new System.Drawing.Point(151, 38);
            this.label2_.Name = "label2_";
            this.label2_.Size = new System.Drawing.Size(12, 13);
            this.label2_.TabIndex = 6;
            this.label2_.Text = "_";
            // 
            // txNewId
            // 
            this.txNewId.Location = new System.Drawing.Point(159, 34);
            this.txNewId.Name = "txNewId";
            this.txNewId.Size = new System.Drawing.Size(196, 22);
            this.txNewId.TabIndex = 7;
            // 
            // toolTipChangeAllIDs
            // 
            this.toolTipChangeAllIDs.AutoPopDelay = 3000;
            this.toolTipChangeAllIDs.InitialDelay = 500;
            this.toolTipChangeAllIDs.IsBalloon = true;
            this.toolTipChangeAllIDs.ReshowDelay = 100;
            this.toolTipChangeAllIDs.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTipChangeAllIDs.ToolTipTitle = "Предупреждение";
            // 
            // ChangeIdsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(364, 111);
            this.Controls.Add(this.txNewId);
            this.Controls.Add(this.txType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2_);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeIdsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Изменить ID всем элементам";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangeIdsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2_;
        private System.Windows.Forms.TextBox txNewId;
        private System.Windows.Forms.ToolTip toolTipChangeAllIDs;
    }
}