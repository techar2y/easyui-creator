namespace CreatorUI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Компоненты формы");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmsAddUIObj = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddRegionLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddTabPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUILinkButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIDateBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIDateTimeBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddComboBoxSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUICheckBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIDataGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIDataGridHead = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDataGridColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddRadioButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIProgressBar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUIWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUITree = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddTreeDataGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiControlUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiControlDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeElementsIds = new System.Windows.Forms.ToolStripMenuItem();
            this.ms = new System.Windows.Forms.MenuStrip();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenForm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowDevTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportToHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveToTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveTemplateWithPrefix = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tvComponents = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pBrowser = new System.Windows.Forms.Panel();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.txtAutoSave = new System.Windows.Forms.TextBox();
            this.panelSave = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnDevTools = new System.Windows.Forms.Button();
            this.buttonIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnChangeAllIDs = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnFindAndReplace = new System.Windows.Forms.Button();
            this.PanelTools = new System.Windows.Forms.Panel();
            this.cmsAddUIObj.SuspendLayout();
            this.ms.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PanelTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsAddUIObj
            // 
            this.cmsAddUIObj.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsAddUIObj.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiDelComponent,
            this.tsmiControlUp,
            this.tsmiControlDown,
            this.tsmiExpand,
            this.tsmiExpandAll,
            this.tsmiCollapse,
            this.tsmiCollapseAll,
            this.tsmiChangeElementsIds});
            this.cmsAddUIObj.Name = "cmsAddUIObj";
            this.cmsAddUIObj.Size = new System.Drawing.Size(249, 202);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddLayout,
            this.tsmiAddRegionLayout,
            this.tsmiAddLabel,
            this.tsmiAddTabs,
            this.tsmiAddTabPage,
            this.tsmiAddUILinkButton,
            this.tsmiAddUIDateBox,
            this.tsmiAddUIDateTimeBox,
            this.tsmiAddComboBoxSelect,
            this.tsmiAddUICheckBox,
            this.tsmiAddUIDataGrid,
            this.tsmiAddUIDataGridHead,
            this.tsmiAddDataGridColumn,
            this.tsmiAddRadioButton,
            this.tsmiAddUIProgressBar,
            this.tsmiAddUIBlock,
            this.tsmiAddUIWindows,
            this.tsmiAddTextBox,
            this.tsmiAddUITree,
            this.tsmiAddTreeDataGrid});
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(248, 22);
            this.tsmiAdd.Text = "Добавить";
            // 
            // tsmiAddLayout
            // 
            this.tsmiAddLayout.Name = "tsmiAddLayout";
            this.tsmiAddLayout.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddLayout.Text = "Добавить UILayout";
            this.tsmiAddLayout.Click += new System.EventHandler(this.tsmiAddLayout_Click);
            // 
            // tsmiAddRegionLayout
            // 
            this.tsmiAddRegionLayout.Name = "tsmiAddRegionLayout";
            this.tsmiAddRegionLayout.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddRegionLayout.Text = "Добавить UIRegionLayout";
            this.tsmiAddRegionLayout.Click += new System.EventHandler(this.tsmiAddRegionLayout_Click);
            // 
            // tsmiAddLabel
            // 
            this.tsmiAddLabel.Name = "tsmiAddLabel";
            this.tsmiAddLabel.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddLabel.Text = "Добавить UILabel";
            this.tsmiAddLabel.Click += new System.EventHandler(this.tsmiAddLabel_Click);
            // 
            // tsmiAddTabs
            // 
            this.tsmiAddTabs.Name = "tsmiAddTabs";
            this.tsmiAddTabs.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddTabs.Text = "Добавить UITabs";
            this.tsmiAddTabs.Click += new System.EventHandler(this.tsmiAddTabs_Click);
            // 
            // tsmiAddTabPage
            // 
            this.tsmiAddTabPage.Name = "tsmiAddTabPage";
            this.tsmiAddTabPage.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddTabPage.Text = "Добавить UITabPage";
            this.tsmiAddTabPage.Click += new System.EventHandler(this.tsmiTabPage_Click);
            // 
            // tsmiAddUILinkButton
            // 
            this.tsmiAddUILinkButton.Name = "tsmiAddUILinkButton";
            this.tsmiAddUILinkButton.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUILinkButton.Text = "Добавить UILinkButton";
            this.tsmiAddUILinkButton.Click += new System.EventHandler(this.tsmiAddUILinkButton_Click);
            // 
            // tsmiAddUIDateBox
            // 
            this.tsmiAddUIDateBox.Name = "tsmiAddUIDateBox";
            this.tsmiAddUIDateBox.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIDateBox.Text = "Добавить UIDateBox";
            this.tsmiAddUIDateBox.Click += new System.EventHandler(this.tsmiAddUIDateBox_Click);
            // 
            // tsmiAddUIDateTimeBox
            // 
            this.tsmiAddUIDateTimeBox.Name = "tsmiAddUIDateTimeBox";
            this.tsmiAddUIDateTimeBox.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIDateTimeBox.Text = "Добавить UIDateTimeBox";
            this.tsmiAddUIDateTimeBox.Click += new System.EventHandler(this.tsmiAddUIDateTimeBox_Click);
            // 
            // tsmiAddComboBoxSelect
            // 
            this.tsmiAddComboBoxSelect.Name = "tsmiAddComboBoxSelect";
            this.tsmiAddComboBoxSelect.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddComboBoxSelect.Text = "Добавить UIComboBox";
            this.tsmiAddComboBoxSelect.Click += new System.EventHandler(this.tsmiAddComboBoxSelect_Click);
            // 
            // tsmiAddUICheckBox
            // 
            this.tsmiAddUICheckBox.Name = "tsmiAddUICheckBox";
            this.tsmiAddUICheckBox.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUICheckBox.Text = "Добавить UICheckBox";
            this.tsmiAddUICheckBox.Click += new System.EventHandler(this.tsmiAddUICheckBox_Click);
            // 
            // tsmiAddUIDataGrid
            // 
            this.tsmiAddUIDataGrid.Name = "tsmiAddUIDataGrid";
            this.tsmiAddUIDataGrid.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIDataGrid.Text = "Добавить UIDataGrid";
            this.tsmiAddUIDataGrid.Click += new System.EventHandler(this.tsmiAddUIDataGrid_Click);
            // 
            // tsmiAddUIDataGridHead
            // 
            this.tsmiAddUIDataGridHead.Name = "tsmiAddUIDataGridHead";
            this.tsmiAddUIDataGridHead.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIDataGridHead.Text = "Добавить UIDataGidHead";
            this.tsmiAddUIDataGridHead.Click += new System.EventHandler(this.tsmiAddUIDataGridHead_Click);
            // 
            // tsmiAddDataGridColumn
            // 
            this.tsmiAddDataGridColumn.Name = "tsmiAddDataGridColumn";
            this.tsmiAddDataGridColumn.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddDataGridColumn.Text = "Добавить UIDataGridColumn";
            this.tsmiAddDataGridColumn.Click += new System.EventHandler(this.tsmiAddDataGridColumn_Click);
            // 
            // tsmiAddRadioButton
            // 
            this.tsmiAddRadioButton.Name = "tsmiAddRadioButton";
            this.tsmiAddRadioButton.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddRadioButton.Text = "Добавить UIRadioButton";
            this.tsmiAddRadioButton.Click += new System.EventHandler(this.tsmiAddRadioButton_Click);
            // 
            // tsmiAddUIProgressBar
            // 
            this.tsmiAddUIProgressBar.Name = "tsmiAddUIProgressBar";
            this.tsmiAddUIProgressBar.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIProgressBar.Text = "Добавить UIProgressBar";
            this.tsmiAddUIProgressBar.Click += new System.EventHandler(this.tsmiAddUIProgressBar_Click);
            // 
            // tsmiAddUIBlock
            // 
            this.tsmiAddUIBlock.Name = "tsmiAddUIBlock";
            this.tsmiAddUIBlock.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIBlock.Text = "Добавить UIBlock";
            this.tsmiAddUIBlock.Click += new System.EventHandler(this.tsmiAddUIBlock_Click);
            // 
            // tsmiAddUIWindows
            // 
            this.tsmiAddUIWindows.Name = "tsmiAddUIWindows";
            this.tsmiAddUIWindows.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUIWindows.Text = "Добавить UIWindow";
            this.tsmiAddUIWindows.Click += new System.EventHandler(this.tsmiAddUIWindows_Click);
            // 
            // tsmiAddTextBox
            // 
            this.tsmiAddTextBox.Name = "tsmiAddTextBox";
            this.tsmiAddTextBox.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddTextBox.Text = "Добавить UITextBox";
            this.tsmiAddTextBox.Click += new System.EventHandler(this.tsmiAddTextBox_Click);
            // 
            // tsmiAddUITree
            // 
            this.tsmiAddUITree.Name = "tsmiAddUITree";
            this.tsmiAddUITree.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddUITree.Text = "Добавить UITree";
            this.tsmiAddUITree.Click += new System.EventHandler(this.tsmiAddUITree_Click);
            // 
            // tsmiAddTreeDataGrid
            // 
            this.tsmiAddTreeDataGrid.Name = "tsmiAddTreeDataGrid";
            this.tsmiAddTreeDataGrid.Size = new System.Drawing.Size(229, 22);
            this.tsmiAddTreeDataGrid.Text = "Добавить UITreeDataGrid";
            this.tsmiAddTreeDataGrid.Click += new System.EventHandler(this.tsmiAddTreeDataGrid_Click);
            // 
            // tsmiDelComponent
            // 
            this.tsmiDelComponent.Name = "tsmiDelComponent";
            this.tsmiDelComponent.Size = new System.Drawing.Size(248, 22);
            this.tsmiDelComponent.Text = "Удалить";
            this.tsmiDelComponent.Click += new System.EventHandler(this.tsmiDelComponent_Click);
            // 
            // tsmiControlUp
            // 
            this.tsmiControlUp.Name = "tsmiControlUp";
            this.tsmiControlUp.Size = new System.Drawing.Size(248, 22);
            this.tsmiControlUp.Text = "Поднять элемент выше";
            this.tsmiControlUp.Click += new System.EventHandler(this.tsmiControlUp_Click);
            // 
            // tsmiControlDown
            // 
            this.tsmiControlDown.Name = "tsmiControlDown";
            this.tsmiControlDown.Size = new System.Drawing.Size(248, 22);
            this.tsmiControlDown.Text = "Опустить элемент ниже";
            this.tsmiControlDown.Click += new System.EventHandler(this.tsmiControlDown_Click);
            // 
            // tsmiExpand
            // 
            this.tsmiExpand.Name = "tsmiExpand";
            this.tsmiExpand.Size = new System.Drawing.Size(248, 22);
            this.tsmiExpand.Text = "Развернуть ветку";
            this.tsmiExpand.ToolTipText = "Разворачивает текушую ветку, без ее дочерних веток";
            this.tsmiExpand.Click += new System.EventHandler(this.tsmiExpand_Click);
            // 
            // tsmiExpandAll
            // 
            this.tsmiExpandAll.Name = "tsmiExpandAll";
            this.tsmiExpandAll.Size = new System.Drawing.Size(248, 22);
            this.tsmiExpandAll.Text = "Развернуть ветку и её дочерние";
            this.tsmiExpandAll.ToolTipText = "Разворачивает текушую ветку и её дочерние ветки";
            this.tsmiExpandAll.Click += new System.EventHandler(this.tsmiExpandAll_Click);
            // 
            // tsmiCollapse
            // 
            this.tsmiCollapse.Name = "tsmiCollapse";
            this.tsmiCollapse.Size = new System.Drawing.Size(248, 22);
            this.tsmiCollapse.Text = "Свернуть ветку";
            this.tsmiCollapse.ToolTipText = "Сворачивает текушую ветку,  без её дочерних веток";
            this.tsmiCollapse.Click += new System.EventHandler(this.tsmiCollapse_Click);
            // 
            // tsmiCollapseAll
            // 
            this.tsmiCollapseAll.Name = "tsmiCollapseAll";
            this.tsmiCollapseAll.Size = new System.Drawing.Size(248, 22);
            this.tsmiCollapseAll.Text = "Cвернуть ветку и её дочерние";
            this.tsmiCollapseAll.ToolTipText = "Сворачивает текушую ветку и её дочерние ветки";
            this.tsmiCollapseAll.Click += new System.EventHandler(this.tsmiCollapseAll_Click);
            // 
            // tsmiChangeElementsIds
            // 
            this.tsmiChangeElementsIds.Name = "tsmiChangeElementsIds";
            this.tsmiChangeElementsIds.Size = new System.Drawing.Size(248, 22);
            this.tsmiChangeElementsIds.Text = "Изменить Id элементам";
            this.tsmiChangeElementsIds.Click += new System.EventHandler(this.tsmiChangeElementsIds_Click);
            // 
            // ms
            // 
            this.ms.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ms.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.ExportTSMI,
            this.HelpTSMI});
            this.ms.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ms.Location = new System.Drawing.Point(0, 0);
            this.ms.Name = "ms";
            this.ms.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.ms.Size = new System.Drawing.Size(895, 24);
            this.ms.TabIndex = 1;
            this.ms.Text = "menuStrip1";
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewFile,
            this.tsmiOpenForm,
            this.tsmiSave,
            this.tsmiShowDevTool,
            this.tsmiSettings,
            this.tsmiExit});
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(48, 22);
            this.tsmiOpen.Text = "Файл";
            // 
            // tsmiNewFile
            // 
            this.tsmiNewFile.Name = "tsmiNewFile";
            this.tsmiNewFile.ShortcutKeyDisplayString = "CTRL + N";
            this.tsmiNewFile.Size = new System.Drawing.Size(280, 22);
            this.tsmiNewFile.Text = "Новый документ";
            this.tsmiNewFile.Click += new System.EventHandler(this.tsmiNewFile_Click);
            // 
            // tsmiOpenForm
            // 
            this.tsmiOpenForm.Name = "tsmiOpenForm";
            this.tsmiOpenForm.ShortcutKeyDisplayString = "CTRL + O";
            this.tsmiOpenForm.Size = new System.Drawing.Size(280, 22);
            this.tsmiOpenForm.Text = "Открыть";
            this.tsmiOpenForm.Click += new System.EventHandler(this.tsmiOpenForm_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(280, 22);
            this.tsmiSave.Text = "Сохранить";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiShowDevTool
            // 
            this.tsmiShowDevTool.Name = "tsmiShowDevTool";
            this.tsmiShowDevTool.Size = new System.Drawing.Size(280, 22);
            this.tsmiShowDevTool.Text = "Показать инструменты разработчика";
            this.tsmiShowDevTool.Click += new System.EventHandler(this.tsmiShowDevTool_Click);
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(280, 22);
            this.tsmiSettings.Text = "Настройки";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(280, 22);
            this.tsmiExit.Text = "Выход";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // ExportTSMI
            // 
            this.ExportTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportToHTML,
            this.tsmiSaveToTemplate,
            this.tsmiSaveTemplateWithPrefix});
            this.ExportTSMI.Name = "ExportTSMI";
            this.ExportTSMI.Size = new System.Drawing.Size(64, 22);
            this.ExportTSMI.Text = "Экспорт";
            // 
            // tsmiExportToHTML
            // 
            this.tsmiExportToHTML.Name = "tsmiExportToHTML";
            this.tsmiExportToHTML.Size = new System.Drawing.Size(465, 22);
            this.tsmiExportToHTML.Text = "Экспортировать в HTML";
            this.tsmiExportToHTML.Click += new System.EventHandler(this.tsmiExportToHTML_Click);
            // 
            // tsmiSaveToTemplate
            // 
            this.tsmiSaveToTemplate.Name = "tsmiSaveToTemplate";
            this.tsmiSaveToTemplate.Size = new System.Drawing.Size(465, 22);
            this.tsmiSaveToTemplate.Text = "Экспортировать в HTML-шаблон";
            this.tsmiSaveToTemplate.Click += new System.EventHandler(this.tsmiSaveToTemplate_Click);
            // 
            // tsmiSaveTemplateWithPrefix
            // 
            this.tsmiSaveTemplateWithPrefix.Name = "tsmiSaveTemplateWithPrefix";
            this.tsmiSaveTemplateWithPrefix.Size = new System.Drawing.Size(465, 22);
            this.tsmiSaveTemplateWithPrefix.Text = "Экспортировать в HTML-шаблон (с приставкой для идентификаторов)";
            this.tsmiSaveTemplateWithPrefix.Click += new System.EventHandler(this.tsmiSaveTemplateWithPrefix_Click);
            // 
            // HelpTSMI
            // 
            this.HelpTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelp});
            this.HelpTSMI.Name = "HelpTSMI";
            this.HelpTSMI.Size = new System.Drawing.Size(68, 22);
            this.HelpTSMI.Text = "Помощь";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(123, 22);
            this.tsmiHelp.Text = "Помощь";
            this.tsmiHelp.Click += new System.EventHandler(this.tsmiHelp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pgProps);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 507);
            this.panel1.TabIndex = 2;
            // 
            // pgProps
            // 
            this.pgProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProps.Location = new System.Drawing.Point(0, 271);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(245, 236);
            this.pgProps.TabIndex = 2;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 268);
            this.splitter2.MinExtra = 230;
            this.splitter2.MinSize = 230;
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(245, 3);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tvComponents);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 268);
            this.panel2.TabIndex = 0;
            // 
            // tvComponents
            // 
            this.tvComponents.AllowDrop = true;
            this.tvComponents.BackColor = System.Drawing.Color.Silver;
            this.tvComponents.ContextMenuStrip = this.cmsAddUIObj;
            this.tvComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvComponents.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tvComponents.FullRowSelect = true;
            this.tvComponents.HideSelection = false;
            this.tvComponents.Location = new System.Drawing.Point(0, 27);
            this.tvComponents.Name = "tvComponents";
            treeNode2.Name = "tvRoot";
            treeNode2.Text = "Компоненты формы";
            this.tvComponents.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvComponents.Size = new System.Drawing.Size(245, 241);
            this.tvComponents.TabIndex = 0;
            this.tvComponents.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvComponents_ItemDrag);
            this.tvComponents.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvComponents_AfterSelect);
            this.tvComponents.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvComponents_NodeMouseClick);
            this.tvComponents.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvComponents_DragDrop);
            this.tvComponents.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvComponents_DragEnter);
            this.tvComponents.DragOver += new System.Windows.Forms.DragEventHandler(this.tvComponents_DragOver);
            this.tvComponents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvComponents_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txSearch);
            this.panel3.Controls.Add(this.labelSearch);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 27);
            this.panel3.TabIndex = 1;
            // 
            // txSearch
            // 
            this.txSearch.BackColor = System.Drawing.Color.White;
            this.txSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txSearch.ForeColor = System.Drawing.Color.Gray;
            this.txSearch.Location = new System.Drawing.Point(62, 3);
            this.txSearch.Name = "txSearch";
            this.txSearch.Size = new System.Drawing.Size(182, 20);
            this.txSearch.TabIndex = 5;
            this.txSearch.Text = "Id или Info...";
            this.txSearch.Enter += new System.EventHandler(this.txSearch_Enter);
            this.txSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txSearch_KeyDown);
            this.txSearch.Leave += new System.EventHandler(this.txSearch_Leave);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelSearch.Location = new System.Drawing.Point(12, 6);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(42, 13);
            this.labelSearch.TabIndex = 6;
            this.labelSearch.Text = "Поиск:";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(245, 55);
            this.splitter1.MinExtra = 500;
            this.splitter1.MinSize = 244;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 507);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // pBrowser
            // 
            this.pBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBrowser.Location = new System.Drawing.Point(248, 55);
            this.pBrowser.Name = "pBrowser";
            this.pBrowser.Size = new System.Drawing.Size(647, 507);
            this.pBrowser.TabIndex = 4;
            // 
            // txtAutoSave
            // 
            this.txtAutoSave.BackColor = System.Drawing.SystemColors.Control;
            this.txtAutoSave.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAutoSave.Location = new System.Drawing.Point(4, 4);
            this.txtAutoSave.Name = "txtAutoSave";
            this.txtAutoSave.Size = new System.Drawing.Size(119, 15);
            this.txtAutoSave.TabIndex = 0;
            this.txtAutoSave.Text = "Автосохранение";
            // 
            // panelSave
            // 
            this.panelSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSave.BackColor = System.Drawing.SystemColors.Control;
            this.panelSave.Controls.Add(this.pictureBox1);
            this.panelSave.Controls.Add(this.txtAutoSave);
            this.panelSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelSave.Location = new System.Drawing.Point(751, 1);
            this.panelSave.Name = "panelSave";
            this.panelSave.Size = new System.Drawing.Size(142, 22);
            this.panelSave.TabIndex = 2;
            this.panelSave.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BackgroundImage = global::CreatorUI.Properties.Resources.saveicon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.InitialImage = global::CreatorUI.Properties.Resources.saveicon;
            this.pictureBox1.Location = new System.Drawing.Point(119, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 3000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // btnDevTools
            // 
            this.btnDevTools.ImageIndex = 4;
            this.btnDevTools.ImageList = this.buttonIcons;
            this.btnDevTools.Location = new System.Drawing.Point(109, 1);
            this.btnDevTools.Name = "btnDevTools";
            this.btnDevTools.Size = new System.Drawing.Size(27, 27);
            this.btnDevTools.TabIndex = 11;
            this.toolTip.SetToolTip(this.btnDevTools, "Показать инструменты разработчика");
            this.btnDevTools.UseVisualStyleBackColor = true;
            this.btnDevTools.Click += new System.EventHandler(this.btnDevTools_Click);
            // 
            // buttonIcons
            // 
            this.buttonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonIcons.ImageStream")));
            this.buttonIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.buttonIcons.Images.SetKeyName(0, "Undo.png");
            this.buttonIcons.Images.SetKeyName(1, "Redo.png");
            this.buttonIcons.Images.SetKeyName(2, "Search.png");
            this.buttonIcons.Images.SetKeyName(3, "FinAndReplace.png");
            this.buttonIcons.Images.SetKeyName(4, "DevTools.png");
            // 
            // btnChangeAllIDs
            // 
            this.btnChangeAllIDs.ImageIndex = 3;
            this.btnChangeAllIDs.ImageList = this.buttonIcons;
            this.btnChangeAllIDs.Location = new System.Drawing.Point(82, 1);
            this.btnChangeAllIDs.Name = "btnChangeAllIDs";
            this.btnChangeAllIDs.Size = new System.Drawing.Size(27, 27);
            this.btnChangeAllIDs.TabIndex = 10;
            this.toolTip.SetToolTip(this.btnChangeAllIDs, "Изменить ID всем элементам");
            this.btnChangeAllIDs.UseVisualStyleBackColor = true;
            this.btnChangeAllIDs.Click += new System.EventHandler(this.btnChangeAllIDs_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.ImageIndex = 1;
            this.btnRedo.ImageList = this.buttonIcons;
            this.btnRedo.Location = new System.Drawing.Point(28, 1);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(27, 27);
            this.btnRedo.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnRedo, "Повторить действие");
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.ImageIndex = 0;
            this.btnUndo.ImageList = this.buttonIcons;
            this.btnUndo.Location = new System.Drawing.Point(1, 1);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(27, 27);
            this.btnUndo.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnUndo, "Отменить действие");
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnFindAndReplace
            // 
            this.btnFindAndReplace.ImageIndex = 2;
            this.btnFindAndReplace.ImageList = this.buttonIcons;
            this.btnFindAndReplace.Location = new System.Drawing.Point(55, 1);
            this.btnFindAndReplace.Name = "btnFindAndReplace";
            this.btnFindAndReplace.Size = new System.Drawing.Size(27, 27);
            this.btnFindAndReplace.TabIndex = 9;
            this.toolTip.SetToolTip(this.btnFindAndReplace, "Найти и заменить текст в ID элементов");
            this.btnFindAndReplace.UseVisualStyleBackColor = true;
            this.btnFindAndReplace.Click += new System.EventHandler(this.btnFindAndReplace_Click);
            // 
            // PanelTools
            // 
            this.PanelTools.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelTools.Controls.Add(this.btnDevTools);
            this.PanelTools.Controls.Add(this.btnChangeAllIDs);
            this.PanelTools.Controls.Add(this.btnFindAndReplace);
            this.PanelTools.Controls.Add(this.btnRedo);
            this.PanelTools.Controls.Add(this.btnUndo);
            this.PanelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTools.Location = new System.Drawing.Point(0, 24);
            this.PanelTools.Name = "PanelTools";
            this.PanelTools.Size = new System.Drawing.Size(895, 31);
            this.PanelTools.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(895, 562);
            this.Controls.Add(this.panelSave);
            this.Controls.Add(this.pBrowser);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanelTools);
            this.Controls.Add(this.ms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms;
            this.MinimumSize = new System.Drawing.Size(911, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasyUI Interface Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.cmsAddUIObj.ResumeLayout(false);
            this.ms.ResumeLayout(false);
            this.ms.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelSave.ResumeLayout(false);
            this.panelSave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.PanelTools.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsAddUIObj;
        private System.Windows.Forms.MenuStrip ms;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pBrowser;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddLayout;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRegionLayout;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddLabel;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddTabs;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddTabPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelComponent;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUILinkButton;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIDateBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIDateTimeBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddComboBoxSelect;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenForm;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowDevTool;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUICheckBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIDataGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIDataGridHead;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDataGridColumn;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRadioButton;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIProgressBar;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIBlock;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUIWindows;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddTextBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUITree;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddTreeDataGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiControlUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiControlDown;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpand;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCollapse;
        private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
        private System.Windows.Forms.TextBox txSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtAutoSave;
        private System.Windows.Forms.Panel panelSave;
        private System.Windows.Forms.TreeView tvComponents;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeElementsIds;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel PanelTools;
        private System.Windows.Forms.Button btnFindAndReplace;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.ImageList buttonIcons;
        private System.Windows.Forms.Button btnChangeAllIDs;
        private System.Windows.Forms.ToolStripMenuItem ExportTSMI;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportToHTML;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveToTemplate;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveTemplateWithPrefix;
        private System.Windows.Forms.ToolStripMenuItem HelpTSMI;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.Button btnDevTools;
        private System.Windows.Forms.Panel panel3;
    }
}

