using CreatorUI.JsonObjs;
using CreatorUI.PropertyGrid;
using CreatorUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI.Models
{
    public class UITreeDataGrid : UIObject
    {
        [Description("True to auto expand/contract the size of the columns to fit the grid width and prevent horizontal scrolling.")]
        public bool fitColumns { get; set; }
        [Description("Defines if set the row height based on the contents of that row. Set to false can improve loading performance.")]
        public bool autoRowHeight { get; set; }
        [Description("True to stripe the rows.")]
        public bool striped { get; set; }
        [Description("True to stripe the rows.")]
        public string method { get; set; }
        [Description("True to display data in one line. Set to true can improve loading performance.")]
        public bool nowrap { get; set; }
        [Description("Indicate which field is an identity field.")]
        public string idField { get; set; }
        [Description("A URL to request data from remote site.")]
        public string url { get; set; }
        [Description("When loading data from remote site, show a prompt message.")]
        public string loadMsg { get; set; }
        [Description("The message to be shown when no records exist.")]
        public string emptyMsg { get; set; }
        [Description("True to show a pagination toolbar on datagrid bottom.")]
        public bool pagination { get; set; }
        [Description("True to show a row number column.")]
        public bool rownumbers { get; set; }
        [Description("True to allow selecting only one row.")]
        public bool singleSelect { get; set; }
        [Description("True to only allow multi-selection when ctrl+click is used.")]
        public bool ctrlSelect { get; set; }
        [Description("If true, the checkbox is checked/unchecked when the user clicks on a row. If false, the checkbox is only checked/unchecked when the user clicks exactly on the checkbox.")]
        public bool checkOnSelect { get; set; }
        [Description("If set to true, clicking a checkbox will always select the row. If false, selecting a row will not check the checkbox.")]
        public bool selectOnCheck { get; set; }
        [Description("If set to true, scroll to the row automatically when selecting it.")]
        public bool scrollOnSelect { get; set; }
        [Description("Defines position of the pager bar. Available values are: 'top','bottom','both'.")]
        [TypeConverter(typeof(PagePositionConverter))]
        public string pagePosition { get; set; }
        [Description("When set pagination property, initialize the page number.")]
        public int pageNumber { get; set; }
        [Description("When set pagination property, initialize the page size.")]
        public int pageSize { get; set; }
        [Description("Defines which column can be sorted.")]
        public string sortName { get; set; }
        [Description("Defines the column sort order, can only be 'asc' or 'desc'.")]
        [TypeConverter(typeof(DataGridColumnOrderConverter))]
        public string sortOrder { get; set; }
        [Description("Defines if to enable multiple column sorting.")]
        public bool multiSort { get; set; }
        [Description("Defines if to sort data from server.")]
        public bool remoteSort { get; set; }
        [Description("Defines if to show row header.")]
        public bool showHeader { get; set; }

        [Description("Defines if to show row footer.")]
        public bool showFooter { get; set; }
        [Description("The scrollbar width(when scrollbar is vertical) or height(when scrollbar is horizontal).")]
        public int scrollbarSize { get; set; }
        [Description("The width of the row number column")]
        public int rownumberWidth { get; set; }
        [Description("The default height of the editors.")]
        public int editorHeight { get; set; }
        [Description("Defines the tree node field. required.")]
        public string treeField { get; set; }
        [Description("Defines if to show animation effect when node expand or collapse.")]
        public bool animate { get; set; }
        [Description("Defines if to cascade check.")]
        public bool cascadeCheck { get; set; }
        [Description("Defines if to show the checkbox only before leaf node.")]
        public bool onlyLeafCheck { get; set; }
        [Description("Defines if to show lines between treegrid nodes.")]
        public bool lines { get; set; }
        public UITreeDataGrid()
        {
            EditorId = "UITreeDataGrid";
            Info = "UITreeDataGrid: ";
            fitColumns = false;
            autoRowHeight = true;
            striped = false;
            method = "post";
            nowrap = true;
            idField = "";
            url = "";
            loadMsg = "Идёт згрузка, пожалуйста подождите...";
            emptyMsg = "";
            pagination = false;
            rownumbers = false;
            singleSelect = false;
            ctrlSelect = false;
            checkOnSelect = true;
            selectOnCheck = true;
            scrollOnSelect = true;
            pagePosition = "bottom";
            pageNumber = 1;
            pageSize = 10;
            sortName = "";
            sortOrder = "asc";
            multiSort = false;
            remoteSort = false;
            showHeader = true;
            showFooter = false;
            scrollbarSize = 18;
            rownumberWidth = 30;
            editorHeight = 24;
            treeField = "";
            animate = false;
            cascadeCheck = true;
            onlyLeafCheck = false;
            lines = false;
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"fitColumns:{fitColumns.ToString().ToLower()}");
            sb.Append($",autoRowHeight:{autoRowHeight.ToString().ToLower()}");
            sb.Append($",striped:{striped.ToString().ToLower()}");
            sb.Append($",method:'{method}'");
            sb.Append($",nowrap:{nowrap.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(idField))
            {
                sb.Append($",idField:'{idField}'");
            }
            if (!string.IsNullOrEmpty(url))
            {
                sb.Append($",url:'{url}'");
            }
            sb.Append($",loadMsg:'{loadMsg}'");
            sb.Append($",emptyMsg:'{emptyMsg}'");
            sb.Append($",pagination:{pagination.ToString().ToLower()}");
            sb.Append($",rownumbers:{rownumbers.ToString().ToLower()}");
            sb.Append($",singleSelect:{singleSelect.ToString().ToLower()}");
            sb.Append($",ctrlSelect:{ctrlSelect.ToString().ToLower()}");
            sb.Append($",checkOnSelect:{checkOnSelect.ToString().ToLower()}");
            sb.Append($",selectOnCheck:{selectOnCheck.ToString().ToLower()}");
            sb.Append($",scrollOnSelect:{scrollOnSelect.ToString().ToLower()}");
            if (pagination)
            {
                sb.Append($",pagePosition:'{pagePosition}'");
                sb.Append($",pageNumber:{pageNumber}");
                sb.Append($",pageSize:{pageSize}");
            }
            if (!string.IsNullOrEmpty(sortName))
            {
                sb.Append($",sortName:'{sortName}'");
            }
            sb.Append($",sortOrder:'{sortOrder}'");
            sb.Append($",multiSort:{multiSort.ToString().ToLower()}");
            sb.Append($",remoteSort:{remoteSort.ToString().ToLower()}");
            sb.Append($",showHeader:{showHeader.ToString().ToLower()}");
            sb.Append($",showFooter:{showFooter.ToString().ToLower()}");
            sb.Append($",scrollbarSize:{scrollbarSize}");
            sb.Append($",rownumberWidth:{rownumberWidth}");
            sb.Append($",editorHeight:{editorHeight}");
            if(!string.IsNullOrEmpty(treeField))
            {
                sb.Append($",treeField:'{treeField}'");
            }
            sb.Append($",animate:{animate.ToString().ToLower()}");
            sb.Append($",cascadeCheck:{cascadeCheck.ToString().ToLower()}");
            sb.Append($",onlyLeafCheck:{onlyLeafCheck.ToString().ToLower()}");
            sb.Append($",lines:{lines.ToString().ToLower()}");
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($"<div style=\"display:block;width:100%;height:100%;\" id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"><table class=\"easyui-treegrid\" ");
            }
            else
            {
                sb.Append($"<table ");
                if (!string.IsNullOrEmpty(id))
                {
                    if (modalSupport)
                    {
                        sb.Append(" th:id=\"${prefix}+'" + id + "'\" ");
                    }
                    else
                    {
                        sb.Append($" id=\"{id}\" ");
                    }
                }
                sb.Append("class=\"easyui-treegrid\" ");
            }
            sb.Append(GetDataOptions());
            sb.Append(" width=\"100%\" height=\"100%\">");
            foreach (var obj in UIObjects)
            {
                sb.Append(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</table>\r\n");
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append("</div>");
            }
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UITreeDataGrid";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "fitColumns", Value = fitColumns.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "autoRowHeight", Value = autoRowHeight.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "striped", Value = striped.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "method", Value = method });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "nowrap", Value = nowrap.ToString().ToLower() });
            if (!string.IsNullOrEmpty(idField))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "idField", Value = idField });
            }
            if (!string.IsNullOrEmpty(url))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "url", Value = url });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "loadMsg", Value = loadMsg });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "emptyMsg", Value = emptyMsg });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "pagination", Value = pagination.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "rownumbers", Value = rownumbers.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "singleSelect", Value = singleSelect.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "ctrlSelect", Value = ctrlSelect.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "checkOnSelect", Value = checkOnSelect.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "selectOnCheck", Value = selectOnCheck.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "scrollOnSelect", Value = scrollOnSelect.ToString().ToLower() });
            if (pagination)
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "pagePosition", Value = pagePosition });
                jsonObj.UIParams.Add(new JsonUIParam { Name = "pageNumber", Value = pageNumber.ToString() });
                jsonObj.UIParams.Add(new JsonUIParam { Name = "pageSize", Value = pageSize.ToString() });
            }
            if (!string.IsNullOrEmpty(sortName))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "sortName", Value = sortName });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "sortOrder", Value = sortOrder });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "multiSort", Value = multiSort.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "remoteSort", Value = remoteSort.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "showHeader", Value = showHeader.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "showFooter", Value = showFooter.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "scrollbarSize", Value = scrollbarSize.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "rownumberWidth", Value = rownumberWidth.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "editorHeight", Value = editorHeight.ToString() });
            if(!string.IsNullOrEmpty(treeField))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "treeField", Value = treeField });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "animate", Value = animate.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "cascadeCheck", Value = cascadeCheck.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "onlyLeafCheck", Value = onlyLeafCheck.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "lines", Value = lines.ToString().ToLower() });
            foreach (var uiobj in UIObjects)
            {
                jsonObj.UIObjects.Add(uiobj.GetJsonObject());
            }
            return jsonObj;
        }
        public override void SetJsonObject(JsonUIObject json, Dictionary<string, Type> UIObjects, TreeNode ParentNode, IdService sId)
        {
            id = json.id;
            EditorId = json.EditorId;
            Info = json.Info;
            var node = new TreeNode();
            node.Text = ToString();
            node.Tag = this;
            this.Node = node;
            if (ParentNode.Tag != null)
            {
                Owner = (UIObject)ParentNode.Tag;
            }
            ParentNode.Nodes.Add(node);
            sId.AddUIObjectProject(this);
            foreach (var p in json.UIParams)
            {
                switch (p.Name)
                {
                    case "fitColumns": fitColumns = p.Value == "true"; break;
                    case "autoRowHeight": autoRowHeight = p.Value == "true"; break;
                    case "striped": striped = p.Value == "true"; break;
                    case "method": method = p.Value; break;
                    case "nowrap": nowrap = p.Value == "true"; break;
                    case "idField": idField = p.Value; break;
                    case "url": url = p.Value; break;
                    case "loadMsg": loadMsg = p.Value; break;
                    case "emptyMsg": emptyMsg = p.Value; break;
                    case "pagination": pagination = p.Value == "true"; break;
                    case "rownumbers": rownumbers = p.Value == "true"; break;
                    case "singleSelect": singleSelect = p.Value == "true"; break;
                    case "ctrlSelect": ctrlSelect = p.Value == "true"; break;
                    case "checkOnSelect": checkOnSelect = p.Value == "true"; break;
                    case "selectOnCheck": selectOnCheck = p.Value == "true"; break;
                    case "scrollOnSelect": scrollOnSelect = p.Value == "true"; break;
                    case "pagePosition": pagePosition = p.Value; break;
                    case "pageNumber": pageNumber = int.Parse(p.Value); break;
                    case "pageSize": pageSize = int.Parse(p.Value); break;
                    case "sortName": sortName = p.Value; break;
                    case "sortOrder": sortOrder = p.Value; break;
                    case "multiSort": multiSort = p.Value == "true"; break;
                    case "remoteSort": remoteSort = p.Value == "true"; break;
                    case "showHeader": showHeader = p.Value == "true"; break;
                    case "showFooter": showFooter = p.Value == "true"; break;
                    case "scrollbarSize": scrollbarSize = int.Parse(p.Value); break;
                    case "rownumberWidth": rownumberWidth = int.Parse(p.Value); break;
                    case "editorHeight": editorHeight = int.Parse(p.Value); break;
                    case "treeField": treeField = p.Value; break;
                    case "animate": animate = p.Value == "true"; break;
                    case "cascadeCheck": cascadeCheck = p.Value == "true"; break;
                    case "onlyLeafCheck": onlyLeafCheck = p.Value == "true"; break;
                    case "lines": lines = p.Value == "true"; break;
                }
            }
            foreach (var uiObj in json.UIObjects)
            {
                if (!UIObjects.ContainsKey(uiObj.UIComponentName)) { continue; }
                var type = UIObjects[uiObj.UIComponentName];
                var obj = (UIObject)Activator.CreateInstance(type);
                obj.SetJsonObject(uiObj, UIObjects, node, sId);
                this.UIObjects.Add(obj);
            }
        }
    }
}
