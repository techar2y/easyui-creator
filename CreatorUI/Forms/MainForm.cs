using CefSharp;
using CefSharp.WinForms;
using CreatorUI.JsonObjs;
using CreatorUI.Models;
using CreatorUI.Services;
using CreatorUI.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace CreatorUI.Forms
{
    public partial class MainForm : Form
    {
        private string HeaderHTML = "";
        private string HeaderAPP = "";
        private string EndHTML = "";
        private ChromiumWebBrowser web;
        private IdService sId = new IdService();
        private Dictionary<string, Type> UIObjects = new Dictionary<string, Type>();

        private readonly string EasyUI = "Easy UI Interface Creator";
        private string currentFileName = "";

        private CRUDService crud = new CRUDService();
        Stack<Invoker> undo = new Stack<Invoker>();
        Stack<Invoker> redo = new Stack<Invoker>();

        // Записуется модель и дерево после нажатия Ctrl + C
        private UIObject copyUIObject = null;
        private TreeNode copyUIObjectNode = null;

        private List<UIObject> findedObjects = new List<UIObject>(); // Список найденных объектов
        private int currentFindIndex = 0; // Текущий индекс в списке найденных объектов
        private string findText = ""; // Текст поиска

        System.Threading.Timer timerSave;
        System.Threading.Timer timerShowSave;

        private SettingsModel settings = new SettingsModel();

        private int findAndReplaceCounter = 0;

        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            SetSettings();
        }

        private void SetSettings()
        {
            settings.LoadSettinsFromRegistry(); // Загрузка настроек с реестра

            if (settings.autoSave)
            {
                if (timerSave != null)
                {
                    timerSave.Dispose();
                }
                timerSave = new System.Threading.Timer(new TimerCallback(Saver), false, settings.autoSaveMinutes * 60000, settings.autoSaveMinutes * 60000);
            }
            else
            {
                if (timerSave != null)
                {
                    timerSave.Dispose();
                }
            }
        }

        private void InitTemplates()
        {
            HeaderHTML = "<!DOCTYPE html>"
                       + @"<html style=""height: 100%;"">"
                       + "<head>"
                       + @"<meta charset=""UTF-8"">"
                       + "<title>App</title>"
                       + @"<link rel=""stylesheet"" type=""text/css"" href=""data/themes/gray/easyui.css"">"
                       + @"<link rel=""stylesheet"" type=""text/css"" href=""data/themes/icon.css"">"
                       + @"<link rel=""stylesheet"" type=""text/css"" href=""data/themes/app.css"">"
                       + @"<script type=""text/javascript"" src=""data/jquery.min.js""></script>"
                       + @"<script type=""text/javascript"" src=""data/jquery.easyui.min.js""></script>"
                       + @"<script type=""text/javascript"" src=""data/easyui-lang-ru.js""></script>"
                       + "</head>"
                       + @"<body style=""height:100%;"">";

            HeaderAPP = "<!DOCTYPE html>"
                       + @"<html style=""height: 100%;"">"
                       + "<head>"
                       + @"<meta charset=""UTF-8"">"
                       + "<title>App</title>"
                       + @"<link rel=""stylesheet"" type=""text/css"" href=""app://data/themes/gray/easyui.css"">"
                       + @"<link rel=""stylesheet"" type=""text/css"" href=""app://data/themes/icon.css"">"
                       + @"<link rel=""stylesheet"" type=""text/css"" href=""app://data/themes/app.css"">"
                       + @"<script type=""text/javascript"" src=""app://data/jquery.min.js""></script>"
                       + @"<script type=""text/javascript"" src=""app://data/jquery.easyui.min.js""></script>"
                       + @"<script type=""text/javascript"" src=""app://data/easyui-lang-ru.js""></script>"
                       + @"<script type=""text/javascript"" src=""app://data/CoreUI.js""></script>"
                       + "</head>"
                       + @"<body style=""height:100%;user-select:none;"">";
            EndHTML = "<div id=\"selUPRect\"></div>"
                + "<div id=\"selDownRect\"></div>"
                + "<div id=\"selLeftRect\"></div>"
                + "<div id=\"selRightRect\"></div>"
                + "</body></html>";
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitTemplates();
            UIObjects.Add("UILayout", typeof(UILayout));
            UIObjects.Add("UIRegion", typeof(UIRegion));
            UIObjects.Add("UITabs", typeof(UITabs));
            UIObjects.Add("UITabPage", typeof(UITabPage));
            UIObjects.Add("UILinkButton", typeof(UILinkButton));
            UIObjects.Add("UICheckBox", typeof(UICheckBox));
            UIObjects.Add("UILabel", typeof(UILabel));
            UIObjects.Add("UIDateBox", typeof(UIDateBox));
            UIObjects.Add("UIDateTimeBox", typeof(UIDateTimeBox));
            UIObjects.Add("UIDataGrid", typeof(UIDataGrid));
            UIObjects.Add("UIDataGridHead", typeof(UIDataGridHead));
            UIObjects.Add("UIDataGridColumn", typeof(UIDataGridColumn));
            UIObjects.Add("UIRadioButton", typeof(UIRadioButton));
            UIObjects.Add("UIProgressBar", typeof(UIProgressBar));
            UIObjects.Add("UIBlock", typeof(UIBlock));
            UIObjects.Add("UIWindow", typeof(UIWindow));
            UIObjects.Add("UITextBox", typeof(UITextBox));
            UIObjects.Add("UITree", typeof(UITree));
            UIObjects.Add("UITreeDataGrid", typeof(UITreeDataGrid));
            UIObjects.Add("UIComboBox", typeof(UIComboBox));

            CefSettings settings = new CefSettings();
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "app",
                SchemeHandlerFactory = new CefSharpSchemeHandlerFactory()
            });
            Cef.Initialize(settings);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            web = new ChromiumWebBrowser(new CefSharp.Web.HtmlString(""));
            web.RegisterJsObject("cefAPI", new CefAPI(web, this));
            /*BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            web.BrowserSettings = browserSettings;*/

            pBrowser.Controls.Add(web);
            web.Dock = DockStyle.Fill;
            web.FrameLoadEnd += Web_FrameLoadEnd;
            this.Focus();
        }

        private void Web_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Invoke((MethodInvoker)(() =>
            {
                tvComponents_AfterSelect(null, null);
            }));
        }

        private void ChangeUIObjectParent(TreeNode newParentNode, TreeNode oldParentNode, UIObject obj)
        {
            Invoker inv = new Invoker();
            MoveCommand moveCommand = new MoveCommand(newParentNode, oldParentNode, obj, tvComponents);
            inv.SetCommand(moveCommand);
            inv.Execute();
            undo.Push(inv);
            RebuildHTML();
        }

        private void AddUIObject(TreeNode ParentNode, UIObject obj)
        {
            var node = new TreeNode();
            node.Text = obj.ToString();
            node.Tag = obj;
            obj.Node = node;

            Invoker inv = new Invoker();
            AddCommand addCommand = new AddCommand(node, ParentNode, tvComponents, sId, obj);
            inv.SetCommand(addCommand);
            inv.Execute();
            undo.Push(inv);

            RebuildHTML();
        }

        private void RebuildHTML()
        {
            var sb = new StringBuilder();
            sb.Append(HeaderAPP);
            foreach (TreeNode n in tvComponents.Nodes[0].Nodes)
            {
                sb.AppendLine(((UIObject)n.Tag).GetHTMLObject(GenHTMLType.Editor, false));
            }
            sb.Append(EndHTML);
            web.LoadHtml(sb.ToString());
        }
        private void tsmiAddLayout_Click(object sender, EventArgs e)
        {
            var obj = new UILayout();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddRegionLayout_Click(object sender, EventArgs e)
        {
            var obj = new UIRegion();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var obj = (UIObject)pgProps.SelectedObject;
            if (e.ChangedItem.Label == "EditorId" || e.ChangedItem.Label == "Info")
            {
                if (e.ChangedItem.Label == "EditorId")
                {
                    var mess = sId.ChnageId((string)e.OldValue, obj.EditorId);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        MessageBox.Show(mess, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        obj.EditorId = (string)e.OldValue;
                        pgProps.SelectedObject = obj;
                        return;
                    }
                }
                tvComponents.SelectedNode.Text = obj.ToString();
            }
            RebuildHTML();
        }
        private async void SelTabPage(UIObject obj)
        {
            if (obj.Owner != null)
            {
                SelTabPage(obj.Owner);
            }
            if (obj is UITabPage)
            {
                if (obj.Owner is UITabs)
                {
                    await web.GetBrowser().MainFrame.EvaluateScriptAsync($"SelectTab(\"{obj.Owner.EditorId}\", \"{obj.EditorId}\")");
                }
            }
        }
        private async void tvComponents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = tvComponents.SelectedNode;
            if (node != null && node.Name != "tvRoot")
            {
                var obj = tvComponents.SelectedNode.Tag;
                pgProps.SelectedObject = obj;
                SelTabPage((UIObject)obj);
                await web.GetBrowser().MainFrame.EvaluateScriptAsync($"SelectControlByID(\"{((UIObject)obj).EditorId}\")");
            }
            else
            {
                pgProps.SelectedObject = null;
            }
        }
        public void SelUIObject(string Id, bool offEvent)
        {
            if (Id != null && sId.objects.ContainsKey(Id))
            {
                var obj = sId.objects[Id];
                if (offEvent)
                {
                    tvComponents.AfterSelect -= tvComponents_AfterSelect;
                    pgProps.SelectedObject = obj;
                    tvComponents.SelectedNode = sId.objects[Id].Node;
                    web.GetBrowser().MainFrame.EvaluateScriptAsync($"SelectControlByID(\"{obj.EditorId}\")");
                    tvComponents.AfterSelect += tvComponents_AfterSelect;
                }
                else
                {
                    tvComponents.SelectedNode = sId.objects[Id].Node;
                }
            }
        }

        private void tsmiAddLabel_Click(object sender, EventArgs e)
        {
            var obj = new UILabel();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddTabs_Click(object sender, EventArgs e)
        {
            var obj = new UITabs();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiTabPage_Click(object sender, EventArgs e)
        {
            var obj = new UITabPage();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        //private void tsmiSaveAsHTML_Click(object sender, EventArgs e)
        //{
        /*var sb = new StringBuilder();
        sb.Append(HeaderHTML);
        foreach (TreeNode n in tvComponents.Nodes[0].Nodes)
        {
            sb.AppendLine(((UIObject)n.Tag).GetHTMLObject(GenHTMLType.Editor));
        }
        sb.Append(EndHTML);
        File.WriteAllText("test.html", sb.ToString());*/
        //}

        /// <summary>
        /// Выход из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tvComponents.Nodes[0].Nodes.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Сохранить документ?", "Предупреждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    tsmiSave_Click(sender, e);
                }
                else if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (timerSave != null)
            {
                timerSave.Dispose();
            }

            if (timerShowSave != null)
            {
                timerShowSave.Dispose();
            }

            Cef.Shutdown();
        }

        private void tsmiDelComponent_Click(object sender, EventArgs e)
        {
            if (tvComponents.SelectedNode != tvComponents.Nodes[0])
            {
                if (DialogResult.OK ==
                       MessageBox.Show("Вы действительно хотите удалить ветку?", "Предупреждение",
                           MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    Invoker inv = new Invoker();
                    DeleteCommand deleteCommand = new DeleteCommand(tvComponents.SelectedNode, tvComponents, sId);
                    inv.SetCommand(deleteCommand);
                    inv.Execute();
                    undo.Push(inv);
                    RebuildHTML();
                }
            }
        }
        /// <summary>
        /// Добавить UILinkButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddUILinkButton_Click(object sender, EventArgs e)
        {
            var obj = new UILinkButton();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIDateBox_Click(object sender, EventArgs e)
        {
            var obj = new UIDateBox();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIDateTimeBox_Click(object sender, EventArgs e)
        {
            var obj = new UIDateTimeBox();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddComboBoxSelect_Click(object sender, EventArgs e)
        {
            var obj = new UIComboBox();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            sfd.Filter = "Json (*.json)|*.json|Все файлы (*.*)|*.*";
            if (sfd.ShowDialog() != DialogResult.OK) { return; }
            Saver(true);
            //var uiobjects = new List<JsonUIObject>();
            //foreach (TreeNode node in tvComponents.Nodes[0].Nodes)
            //{
            //    uiobjects.Add(((UIObject)node.Tag).GetJsonObject());
            //}
            //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<JsonUIObject>));
            //using (var ms = new MemoryStream())
            //{
            //    jsonFormatter.WriteObject(ms, uiobjects);
            //    ms.Position = 0;
            //    var result = Encoding.UTF8.GetString(ms.ToArray());
            //    sfd.Filter = "Json (*.json)|*.json|Все файлы (*.*)|*.*";
            //    if (sfd.ShowDialog() != DialogResult.OK) { return; }
            //    File.WriteAllText(sfd.FileName, result, Encoding.UTF8);

            //    currentFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
            //    this.Text = EasyUI + " - " + currentFileName + " (" + sfd.FileName + ")";

            //    MessageBox.Show("Форма сохранена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void tsmiOpenForm_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Json (*.json)|*.json|Все файлы (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK) { return; }

            // Очистка объектов и древа перед открытием файла для измежения конфликтов.
            sId.objects.Clear();
            tvComponents.Nodes[0].Nodes.Clear();
            findText = "";
            txSearch.Text = "Id или Info...";
            undo.Clear();
            redo.Clear();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<JsonUIObject>));
            var buf = Encoding.UTF8.GetBytes(File.ReadAllText(ofd.FileName, Encoding.UTF8));
            using (var ms = new MemoryStream())
            {
                ms.Write(buf, 0, buf.Length);
                ms.Position = 0;
                List<JsonUIObject> json = null;
                try
                {
                    json = (List<JsonUIObject>)jsonFormatter.ReadObject(ms);
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var uiObj in json)
                {
                    if (!UIObjects.ContainsKey(uiObj.UIComponentName)) { continue; }
                    var type = UIObjects[uiObj.UIComponentName];
                    var obj = (UIObject)Activator.CreateInstance(type);
                    obj.SetJsonObject(uiObj, UIObjects, tvComponents.Nodes[0], sId);
                }
            }
            currentFileName = Path.GetFileNameWithoutExtension(ofd.FileName);
            this.Text = EasyUI + " - " + currentFileName + " (" + ofd.FileName + ")";
            ofd.FileName = "";
            tvComponents.ExpandAll();
            RebuildHTML();
        }
        /// <summary>
        /// Показать инструменты разработчика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiShowDevTool_Click(object sender, EventArgs e)
        {
            web.ShowDevTools();
        }

        private void tsmiAddUICheckBox_Click(object sender, EventArgs e)
        {
            var obj = new UICheckBox();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIDataGrid_Click(object sender, EventArgs e)
        {
            var obj = new UIDataGrid();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIDataGridHead_Click(object sender, EventArgs e)
        {
            var obj = new UIDataGridHead();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddDataGridColumn_Click(object sender, EventArgs e)
        {
            var obj = new UIDataGridColumn();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddRadioButton_Click(object sender, EventArgs e)
        {
            var obj = new UIRadioButton();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIProgressBar_Click(object sender, EventArgs e)
        {
            var obj = new UIProgressBar();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIBlock_Click(object sender, EventArgs e)
        {
            var obj = new UIBlock();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUIWindows_Click(object sender, EventArgs e)
        {
            var obj = new UIWindow();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddTextBox_Click(object sender, EventArgs e)
        {
            var obj = new UITextBox();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiAddUITree_Click(object sender, EventArgs e)
        {
            var obj = new UITree();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }

        private void tsmiExportToHTML_Click(object sender, EventArgs e)
        {
            sfd.Filter = "HTML (*.html)|*.html|Все файлы (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                sb.Append(HeaderHTML);
                foreach (TreeNode n in tvComponents.Nodes[0].Nodes)
                {
                    sb.AppendLine(((UIObject)n.Tag).GetHTMLObject(GenHTMLType.Template, false));
                }
                sb.Append(EndHTML);
                File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }

        private void tsmiSaveToTemplate_Click(object sender, EventArgs e)
        {
            sfd.Filter = "HTML (*.html)|*.html|Все файлы (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                foreach (TreeNode n in tvComponents.Nodes[0].Nodes)
                {
                    sb.AppendLine(((UIObject)n.Tag).GetHTMLObject(GenHTMLType.Template, false));
                }
                File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }
        /// <summary>
        /// Ддобавить UITreeDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddTreeDataGrid_Click(object sender, EventArgs e)
        {
            var obj = new UITreeDataGrid();
            sId.AddUIObject(obj);
            AddUIObject(tvComponents.SelectedNode, obj);
        }
        /// <summary>
        /// Подъем элемента выше на позицию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiControlUp_Click(object sender, EventArgs e)
        {
            var node = tvComponents.SelectedNode;
            if (node == null || node?.Name == "tvRoot")
            {
                MessageBox.Show("Выберите компонент", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var ParentNode = node.Parent;
                var index = ParentNode.Nodes.IndexOf(node);
                if (index != 0)
                {
                    if (ParentNode.Name != "tvRoot")
                    {
                        var parentUIObj = (UIObject)ParentNode.Tag;
                        var childUIObject = (UIObject)node.Tag;
                        var childUIIndex = parentUIObj.UIObjects.FindIndex(x => x == childUIObject);
                        parentUIObj.UIObjects.Remove(childUIObject);
                        parentUIObj.UIObjects.Insert(childUIIndex - 1, childUIObject);
                    }
                    ParentNode.Nodes.Remove(node);
                    ParentNode.Nodes.Insert(index - 1, node);
                    tvComponents.SelectedNode = node;
                    RebuildHTML();
                }
            }
        }
        /// <summary>
        /// Опустить элемент на уровень ниже
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiControlDown_Click(object sender, EventArgs e)
        {
            var node = tvComponents.SelectedNode;
            if (node == null || node?.Name == "tvRoot")
            {
                MessageBox.Show("Выберите компонент", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var ParentNode = node.Parent;
                var index = ParentNode.Nodes.IndexOf(node);
                if (index != (ParentNode.Nodes.Count - 1))
                {
                    if (ParentNode.Name != "tvRoot")
                    {
                        var parentUIObj = (UIObject)ParentNode.Tag;
                        var childUIObject = (UIObject)node.Tag;
                        var childUIIndex = parentUIObj.UIObjects.FindIndex(x => x == childUIObject);
                        parentUIObj.UIObjects.Remove(childUIObject);
                        parentUIObj.UIObjects.Insert(childUIIndex + 1, childUIObject);
                    }
                    ParentNode.Nodes.Remove(node);
                    ParentNode.Nodes.Insert(index + 1, node);
                    tvComponents.SelectedNode = node;
                    RebuildHTML();
                }
            }
        }
        /// <summary>
        /// Сохранить шаблон с поддержкой приставок для идентификаторов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSaveTemplateWithPrefix_Click(object sender, EventArgs e)
        {
            sfd.Filter = "HTML (*.html)|*.html|Все файлы (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                foreach (TreeNode n in tvComponents.Nodes[0].Nodes)
                {
                    sb.AppendLine(((UIObject)n.Tag).GetHTMLObject(GenHTMLType.Template, true));
                }
                File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }

        /// <summary>
        /// Присваивание свойств с одного объекта на другой
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="toObject"></param>
        private void AssignProperties(UIObject fromObject, UIObject toObject)
        {
            PropertyInfo[] fromProperties = fromObject.GetType().GetProperties();

            foreach (PropertyInfo pI in fromProperties)
            {
                if (pI.Name != "EditorId" && pI.Name != "Owner" && pI.Name != "Node")
                {
                    pI.SetValue(toObject, pI.GetValue(fromObject));
                }
            }
        }

        /// <summary>
        /// Создание копии объекта
        /// </summary>
        /// <param name="etalonNode"></param>
        /// <param name="obj"></param>
        /// <param name="firstNode"></param>
        private void CreateNewUIObject(TreeNode etalonNode, UIObject obj, bool firstNode)
        {
            ConstructorInfo[] ci = obj.GetType().GetConstructors();

            if (ci.Length == 0) { return; }

            UIObject newUIObject = (UIObject)ci[0].Invoke(null);

            sId.AddUIObject(newUIObject);
            AssignProperties(obj, newUIObject);
            AddUIObject(tvComponents.SelectedNode, newUIObject);

            TreeNode curNode = tvComponents.SelectedNode;
            for (int i = 0; i < etalonNode.Nodes.Count; i++)
            {
                CreateNewUIObject(etalonNode.Nodes[i], (UIObject)etalonNode.Nodes[i].Tag, false);
                tvComponents.SelectedNode = curNode;
            }
        }

        private void tvComponents_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void tvComponents_DragDrop(object sender, DragEventArgs e)
        {
            // Получить координаты точки где был отпущен объект
            Point targetPoint = tvComponents.PointToClient(new Point(e.X, e.Y));

            // Получить ноду дерева по этим координатам  
            TreeNode targetNode = tvComponents.GetNodeAt(targetPoint);

            // Получить ноду которая перетягивалась
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (targetNode == null)
                return;

            if (!draggedNode.Equals(targetNode)
                    && !targetNode.Equals(draggedNode.Parent)
                )
            {
                if (!CheckSubNodes(targetNode, draggedNode))
                {
                    MessageBox.Show("Родительскую ветку нельзя перемещать в дочернию!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ChangeUIObjectParent(targetNode, draggedNode.Parent, (UIObject)draggedNode.Tag);
            }
        }

        /// <summary>
        /// Проверка дочерних нод при перетягивании
        /// </summary>
        /// <param name="targetNode"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CheckSubNodes(TreeNode targetNode, TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (targetNode.Equals(n))
                    return false;
                else
                {
                    if (!CheckSubNodes(targetNode, n))
                        return false;
                }
            }
            return true;
        }

        private void tvComponents_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void tvComponents_DragOver(object sender, DragEventArgs e)
        {
            // Получить клиентские координаты мыши
            Point targetPoint = tvComponents.PointToClient(new Point(e.X, e.Y));

            // Выбрать ноду по координатам мыши
            tvComponents.SelectedNode = tvComponents.GetNodeAt(targetPoint);
        }

        /// <summary>
        /// Обработка нажатия на ноду (для того чтоб нажатие правой кнопки мыши выбирало ноду)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvComponents_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvComponents.SelectedNode = e.Node;
        }

        private void tvComponents_KeyDown(object sender, KeyEventArgs e)
        {
            //    // Клавиши для поднятия и опускания элементов на одном уровне
            //    if (e.KeyCode == Keys.Up && (e.Modifiers == Keys.Control)) // Поднять элемент выше
            //    {
            //        tsmiControlUp_Click(sender, e);
            //    }
            //    else if (e.KeyCode == Keys.Down && e.Control) // Опустить элемент ниже
            //    {
            //        tsmiControlDown_Click(sender, e);
            //    }
            //    else if (e.KeyCode == Keys.Delete) // Удалить эелемент по нажатию Delete
            //    {
            //        if (tvComponents.SelectedNode != null)
            //        {
            //            tsmiDelComponent.PerformClick();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.C && (e.Modifiers == Keys.Control)) // Скопировать выбранный элемент Crtl + C
            //    {
            //        if (tvComponents.SelectedNode.Name != "tvRoot")
            //        {
            //            copyUIObject = (UIObject)tvComponents.SelectedNode.Tag;
            //            copyUIObjectNode = (TreeNode)copyUIObject.Node.Clone();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.X && (e.Modifiers == Keys.Control)) // Вырезать выбранный элемент Crtl + X
            //    {
            //        if (tvComponents.SelectedNode.Name != "tvRoot")
            //        {
            //            copyUIObject = (UIObject)tvComponents.SelectedNode.Tag;
            //            copyUIObjectNode = (TreeNode)copyUIObject.Node.Clone();
            //            tsmiDelComponent_Click(sender, e);
            //        }
            //    }
            //    else if (e.KeyCode == Keys.V && (e.Modifiers == Keys.Control)) // Вставить выбранный элемент Crtl + V
            //    {
            //        if (copyUIObject != null)
            //        {
            //            CreateNewUIObject(copyUIObjectNode, (UIObject)copyUIObjectNode.Tag, true);
            //        }
            //        RebuildHTML();
            //    }

            //    // Добавить UIBlock по нажатию Ctrl + A
            //    else if (e.KeyCode == Keys.A && (e.Modifiers == Keys.Control))
            //    {
            //        tsmiAddUIBlock_Click(null, null);
            //    }

            //    // Добавить UILabel по нажатию Ctrl + S
            //    else if (e.KeyCode == Keys.S && (e.Modifiers == Keys.Control))
            //    {
            //        tsmiAddLabel_Click(null, null);
            //    }

            //    // Добавить UITextBox по нажатию Ctrl + T
            //    else if (e.KeyCode == Keys.T && (e.Modifiers == Keys.Control))
            //    {
            //        tsmiAddTextBox_Click(null, null);
            //    }

            //    // Добавить UILinkButton по нажатию Ctrl + B
            //    else if (e.KeyCode == Keys.B && (e.Modifiers == Keys.Control))
            //    {
            //        tsmiAddUILinkButton_Click(null, null);
            //    }
            //    // F12 - показать инструменты разработчика
            //    else if (e.KeyCode == Keys.F12)
            //    {
            //        web.ShowDevTools();
            //    }

            //    // Развернуть текущую ветку, но не разворачивать дочерние элементы по нажатию Ctrl + E
            //    else if (e.KeyCode == Keys.OemOpenBrackets && (e.Modifiers == Keys.Control))
            //    {
            //        tvComponents.SelectedNode.Expand();
            //    }

            //    // Развернуть текущую ветку и развернуть дочерние элементы по нажатию Ctrl + Q
            //    else if (e.KeyCode == Keys.Oem6 && (e.Modifiers == Keys.Control))
            //    {
            //        tvComponents.SelectedNode.ExpandAll();
            //    }

            //    // Свернуть текущую ветку и все дочерние элементы по нажатию Ctrl + P
            //    else if (e.KeyCode == Keys.Oem7 && (e.Modifiers == Keys.Control))
            //    {
            //        tvComponents.SelectedNode.Collapse(false);
            //    }

            //    // Свернуть текущую ветку, но не сворачивать дочерние элементы по нажатию Ctrl + I
            //    else if (e.KeyCode == Keys.Oem1 && (e.Modifiers == Keys.Control))
            //    {
            //        tvComponents.SelectedNode.Collapse(true);
            //    }
        }

        // Развернуть текущую ветку, но не разворачивать дочерние элементы
        private void tsmiExpand_Click(object sender, EventArgs e)
        {
            tvComponents.SelectedNode.Expand();
        }

        // Развернуть текущую ветку и развернуть дочерние элементы
        private void tsmiExpandAll_Click(object sender, EventArgs e)
        {
            tvComponents.SelectedNode.ExpandAll();
        }

        // Свернуть текущую ветку и все дочерние элементы
        private void tsmiCollapse_Click(object sender, EventArgs e)
        {
            tvComponents.SelectedNode.Collapse(true);
        }

        // Свернуть текущую ветку, но не сворачивать дочерние элементы
        private void tsmiCollapseAll_Click(object sender, EventArgs e)
        {
            tvComponents.SelectedNode.Collapse(false);
        }

        /// <summary>
        /// Создание нового документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiNewFile_Click(object sender, EventArgs e)
        {
            if (tvComponents.Nodes[0].Nodes.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Сохранить документ?", "Предупреждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    tsmiSave_Click(sender, e);
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            // Очистка объектов и древа перед открытием файла для измежения конфликтов.
            sId.objects.Clear();
            tvComponents.Nodes[0].Nodes.Clear();
            findText = "";
            txSearch.Text = "Id или Info...";
            undo.Clear();
            redo.Clear();
            this.Text = EasyUI;
            currentFileName = "";
            RebuildHTML();
        }

        /// <summary>
        /// Фокус на текстовом поле Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txSearch_Enter(object sender, EventArgs e)
        {
            if (txSearch.Text == "Id или Info...")
            {
                txSearch.Text = "";
                txSearch.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Потеря фокуса на текстовом поле Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txSearch.Text))
            {
                txSearch.Text = "Id или Info...";
                txSearch.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Обработка нажатия клавиш в текстбоксе поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txSearch.Text != "Введите часть Id или Info")
            {
                if (e.KeyCode == Keys.Enter && txSearch.Text == findText)
                {
                    SelectFindedNode();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (tvComponents.Nodes[0].Nodes.Count == 0)
                    {
                        ShowTooltip("Нет элементов для поиска", "Результат поиска", txSearch, ToolTipIcon.Info);
                        return;
                    }

                    if (txSearch.Text == "")
                    {
                        ShowTooltip("Введите запрос", "Результат поиска", txSearch, ToolTipIcon.Info);
                        return;
                    }

                    findText = txSearch.Text;
                    findedObjects.Clear();
                    currentFindIndex = 0;
                    foreach (TreeNode node in tvComponents.Nodes[0].Nodes)
                    {
                        if (!string.IsNullOrEmpty(((UIObject)node.Tag).id))
                        {
                            if (((UIObject)node.Tag).id.ToLower().Contains(txSearch.Text.ToLower()))
                            {
                                findedObjects.Add((UIObject)node.Tag);
                                SearchRecursive((UIObject)node.Tag);
                                continue;
                            }
                            SearchRecursive((UIObject)node.Tag);
                        }
                        else if (!string.IsNullOrEmpty(((UIObject)node.Tag).Info))
                        {
                            if (((UIObject)node.Tag).Info.ToLower().Contains(txSearch.Text.ToLower()))
                            {
                                findedObjects.Add((UIObject)node.Tag);
                                SearchRecursive((UIObject)node.Tag);
                                continue;
                            }
                            SearchRecursive((UIObject)node.Tag);
                        }
                        else
                        {
                            SearchRecursive((UIObject)node.Tag);
                        }
                    }
                    ShowTooltip("Найдено элементов: " + findedObjects.Count, "Результат поиска", txSearch, ToolTipIcon.Info);
                    SelectFindedNode();
                }
            }
        }

        // Рекурсивный поиск в древе нодов
        private void SearchRecursive(UIObject obj)
        {
            foreach (UIObject child in obj.UIObjects)
            {
                if (!string.IsNullOrEmpty(child.id))
                {
                    if (child.id.ToLower().Contains(txSearch.Text.ToLower()))
                    {
                        findedObjects.Add(child);
                        if (child.UIObjects.Count > 0)
                        {
                            SearchRecursive(child);
                        }
                        continue;
                    }
                    if (child.UIObjects.Count > 0)
                    {
                        SearchRecursive(child);
                    }
                }
                else if (!string.IsNullOrEmpty(child.Info))
                {
                    if (child.Info.ToLower().Contains(txSearch.Text.ToLower()))
                    {
                        findedObjects.Add(child);
                        if (child.UIObjects.Count > 0)
                        {
                            SearchRecursive(child);
                        }
                        continue;
                    }
                    if (child.UIObjects.Count > 0)
                    {
                        SearchRecursive(child);
                    }
                }
                else
                {
                    if (child.UIObjects.Count > 0)
                    {
                        SearchRecursive(child);
                    }
                }
            }
        }

        // Выбор найденных нодов
        private void SelectFindedNode()
        {
            if (tvComponents.Nodes[0].Nodes.Count == 0)
            {
                ShowTooltip("Нет элементов для поиска", "Результат поиска", txSearch, ToolTipIcon.Info);
                return;
            }

            if (findedObjects.Count == 0)
            {
                ShowTooltip("Ничего не найдено", "Результат", txSearch, ToolTipIcon.Info);
                return;
            }

            if (txSearch.Text != findText) { return; }

            tvComponents.SelectedNode = findedObjects[currentFindIndex].Node;
            currentFindIndex++;
            if (currentFindIndex == findedObjects.Count)
            {
                currentFindIndex = 0;
            }
        }

        // Для таймера
        // Сохранение
        private void Saver(object obj)
        {
            if (tvComponents.Nodes[0].Nodes.Count == 0) { return; }

            var uiobjects = new List<JsonUIObject>();
            foreach (TreeNode node in tvComponents.Nodes[0].Nodes)
            {
                uiobjects.Add(((UIObject)node.Tag).GetJsonObject());
            }
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<JsonUIObject>));
            using (var ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, uiobjects);
                ms.Position = 0;
                var result = Encoding.UTF8.GetString(ms.ToArray());

                if ((bool)obj)
                {
                    File.WriteAllText(sfd.FileName, result, Encoding.UTF8);
                    currentFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                    this.Text = EasyUI + " - " + currentFileName + " (" + sfd.FileName + ")";
                    sfd.FileName = currentFileName;
                    MessageBox.Show("Форма сохранена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (!Directory.Exists(Application.StartupPath + "\\Autosaves\\"))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\Autosaves\\");
                    }

                    string path = "";
                    if (!string.IsNullOrEmpty(currentFileName))
                    {
                        path = Application.StartupPath + "\\Autosaves\\" + currentFileName + DateTime.Now.ToString("_dd.MM.yyyy_HH_mm_ss") + ".json";
                    }
                    else
                    {
                        path = Application.StartupPath + "\\Autosaves\\Autosave_" + DateTime.Now.ToString("_dd.MM.yyyy_HH_mm_ss") + ".json";
                    }

                    File.WriteAllText(path, result, Encoding.UTF8);
                    Invoke((MethodInvoker)delegate
                    {
                        panelSave.Visible = true;
                    });

                    timerShowSave = new System.Threading.Timer(new TimerCallback(HideSavePanel), null, 3000, 2000);
                }
            }
        }

        // Для скрытия панели с надписью "Автосохранени" и картинкой
        private void HideSavePanel(object obj)
        {
            Invoke((MethodInvoker)delegate
            {
                panelSave.Visible = false;
                timerShowSave.Dispose();
            });
        }

        // Обработка нажатия на "Изменить Id элементов" в контекстном меню
        private void tsmiChangeElementsIds_Click(object sender, EventArgs e)
        {
            if (tvComponents.Nodes[0].Nodes.Count == 0)
            {
                MessageBox.Show("Отсутствуют элементы в списке.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TreeNode node = tvComponents.SelectedNode;
            tvComponents.SelectedNode = null;

            ChangeIdsForm form = new ChangeIdsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(form.NewElemId))
                {
                    ChangeIdRecursive(tvComponents.Nodes[0], form.NewType, form.NewElemId);

                    MessageBox.Show("Заменено ID у объектов: " + findAndReplaceCounter, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    findAndReplaceCounter = 0;
                }
            }
            tvComponents.SelectedNode = node;
        }

        // Рекурсивное изменение Id у всех элементов 
        private void ChangeIdRecursive(TreeNode treeNode, string newElementType, string newElementId)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                UIObject current = (UIObject)tn.Tag;

                if (current != null)
                {
                    if (!string.IsNullOrEmpty(current.id))
                    {
                        current.id = current.id.Substring(0, current.id.IndexOf("_"))
                            + "_"
                            + newElementType
                            + "_"
                            + newElementId;
                        findAndReplaceCounter++;
                    }
                }

                ChangeIdRecursive(tn, newElementType, newElementId);
            }
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(settings);
            form.ShowDialog();
            SetSettings();
        }

        private void tsmiHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Манипуляция элементами:\n" +
                "CTRL + C - копировать выделенное дерево\n" +
                "CTRL + X - вырезать выделенное дерево\n" +
                "CTRL + V - вставить скопированное дерево\n" +
                "CTRL + [ - развернуть выбранный дерево\n" +
                "CTRL + ] - развернуть выбранное дерево и все дочерние\n" +
                "CTRL + ; - свернуть выбранное дерево\n" +
                "CTRL + ' - свернуть выбранное дерево и все дочерние\n" +
                "CTRL + Z - отменить действие (добавление, удаление, перемещение)\n" +
                "CTRL + Y - повторить действие (добавление, удаление, перемещение)\n" +
                "\nБыстрая вставка элементов:\n" +
                "CTRL + A - Вставить UIBlock\n" +
                "CTRL + S - Вставить UILabel\n" +
                "CTRL + B - Вставить UILinkButton\n" +
                "CTRL + T - Вставить UITextBox\n" +
                "\nПоиск:\n" +
                "Enter - найти элемент по запросу\n" +
                "F3 - выбрать следующий найденный элемент",
                "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Undo()
        {
            if (undo.Count == 0)
                return;

            Invoker inv = undo.Pop();
            inv.Unexecute();
            redo.Push(inv);
            RebuildHTML();
        }

        private void Redo()
        {
            if (redo.Count == 0)
                return;

            Invoker inv = redo.Pop();
            inv.Execute();
            undo.Push(inv);
            RebuildHTML();
        }

        private void ShowTooltip(string text, string title, Control ctrl, ToolTipIcon icon)
        {
            toolTip.Hide(ctrl);
            toolTip.ToolTipTitle = title;
            toolTip.ToolTipIcon = icon;
            toolTip.Show(text, ctrl, 0);
            toolTip.Show(text, ctrl, 3000);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void btnDevTools_Click(object sender, EventArgs e)
        {
            tsmiShowDevTool_Click(null, null);
        }

        private void btnChangeAllIDs_Click(object sender, EventArgs e)
        {
            tsmiChangeElementsIds_Click(null, null);
        }

        // Обработка нажатий при фокусе всего окна
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && (e.Modifiers == Keys.Control)) // Создать новый документ
            {
                tsmiNewFile_Click(null, null);
            }
            else if (e.KeyCode == Keys.O && (e.Modifiers == Keys.Control)) // Открыть файл
            {
                tsmiOpenForm_Click(null, null);
            }
            // Продолжение поиска
            else if (e.KeyCode == Keys.F3)
            {
                SelectFindedNode();
            }
            // Отменить удаление или перемещение
            else if (e.KeyCode == Keys.Z && (e.Modifiers == Keys.Control))
            {
                Undo();
            }
            // Повторить удаление или перемещение
            else if (e.KeyCode == Keys.Y && (e.Modifiers == Keys.Control))
            {
                Redo();
            }


            // Клавиши для поднятия и опускания элементов на одном уровне
            else if (e.KeyCode == Keys.Up && (e.Modifiers == Keys.Control)) // Поднять элемент выше
            {
                tsmiControlUp_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control) // Опустить элемент ниже
            {
                tsmiControlDown_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Delete) // Удалить эелемент по нажатию Delete
            {
                if (tvComponents.SelectedNode != null)
                {
                    tsmiDelComponent.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.C && (e.Modifiers == Keys.Control)) // Скопировать выбранный элемент Crtl + C
            {
                if (tvComponents.SelectedNode.Name != "tvRoot")
                {
                    copyUIObject = (UIObject)tvComponents.SelectedNode.Tag;
                    copyUIObjectNode = (TreeNode)copyUIObject.Node.Clone();
                }
            }
            else if (e.KeyCode == Keys.X && (e.Modifiers == Keys.Control)) // Вырезать выбранный элемент Crtl + X
            {
                if (tvComponents.SelectedNode.Name != "tvRoot")
                {
                    copyUIObject = (UIObject)tvComponents.SelectedNode.Tag;
                    copyUIObjectNode = (TreeNode)copyUIObject.Node.Clone();
                    tsmiDelComponent_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.V && (e.Modifiers == Keys.Control)) // Вставить выбранный элемент Crtl + V
            {
                if (copyUIObject != null)
                {
                    CreateNewUIObject(copyUIObjectNode, (UIObject)copyUIObjectNode.Tag, true);
                }
                RebuildHTML();
            }

            // Добавить UIBlock по нажатию Ctrl + A
            else if (e.KeyCode == Keys.A && (e.Modifiers == Keys.Control))
            {
                tsmiAddUIBlock_Click(null, null);
            }

            // Добавить UILabel по нажатию Ctrl + S
            else if (e.KeyCode == Keys.S && (e.Modifiers == Keys.Control))
            {
                tsmiAddLabel_Click(null, null);
            }

            // Добавить UITextBox по нажатию Ctrl + T
            else if (e.KeyCode == Keys.T && (e.Modifiers == Keys.Control))
            {
                tsmiAddTextBox_Click(null, null);
            }

            // Добавить UILinkButton по нажатию Ctrl + B
            else if (e.KeyCode == Keys.B && (e.Modifiers == Keys.Control))
            {
                tsmiAddUILinkButton_Click(null, null);
            }
            // F12 - показать инструменты разработчика
            else if (e.KeyCode == Keys.F12)
            {
                web.ShowDevTools();
            }

            // Развернуть текущую ветку, но не разворачивать дочерние элементы по нажатию Ctrl + E
            else if (e.KeyCode == Keys.OemOpenBrackets && (e.Modifiers == Keys.Control))
            {
                tvComponents.SelectedNode.Expand();
            }

            // Развернуть текущую ветку и развернуть дочерние элементы по нажатию Ctrl + Q
            else if (e.KeyCode == Keys.Oem6 && (e.Modifiers == Keys.Control))
            {
                tvComponents.SelectedNode.ExpandAll();
            }

            // Свернуть текущую ветку и все дочерние элементы по нажатию Ctrl + P
            else if (e.KeyCode == Keys.Oem7 && (e.Modifiers == Keys.Control))
            {
                tvComponents.SelectedNode.Collapse(false);
            }

            // Свернуть текущую ветку, но не сворачивать дочерние элементы по нажатию Ctrl + I
            else if (e.KeyCode == Keys.Oem1 && (e.Modifiers == Keys.Control))
            {
                tvComponents.SelectedNode.Collapse(true);
            }


            e.Handled = false;
        }

        private void btnFindAndReplace_Click(object sender, EventArgs e)
        {
            if (tvComponents.Nodes[0].Nodes.Count == 0)
            {
                MessageBox.Show("Отсутствуют элементы в списке.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TreeNode node = tvComponents.SelectedNode;
            tvComponents.SelectedNode = null;

            FindAndReplaceForm form = new FindAndReplaceForm();

            DialogResult dRes = form.ShowDialog();

            if (dRes == DialogResult.OK)
            {
                SearchAndReplace(tvComponents.Nodes[0], form.FindText, form.ReplaceText, true);
                MessageBox.Show("Текст заменён в " + findAndReplaceCounter + " местах", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                findAndReplaceCounter = 0;
            }

            tvComponents.SelectedNode = node;
        }

        private void SearchAndReplace(TreeNode node, string find, string replace, bool checkCurrent)
        {
            if (checkCurrent)
            {
                UIObject uiObj = (UIObject)node.Tag;
                if (uiObj != null)
                {
                    if (uiObj.id != null)
                    {
                        if (uiObj.id.Contains(find))
                        {
                            uiObj.id = uiObj.id.Replace(find, replace);
                            findAndReplaceCounter++;
                        }
                    }
                }
            }

            foreach (TreeNode n in node.Nodes)
            {
                UIObject childUI = (UIObject)n.Tag;
                if (childUI != null)
                {
                    if (childUI.id != null)
                    {
                        if (childUI.id.Contains(find))
                        {
                            childUI.id = childUI.id.Replace(find, replace);
                            findAndReplaceCounter++;
                        }
                    }
                }
                if (n.Nodes.Count > 0)
                {
                    SearchAndReplace(n, find, replace, false);
                }
            }
        }

    }
}

