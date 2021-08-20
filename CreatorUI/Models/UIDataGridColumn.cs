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
    public class UIDataGridColumn : UIObject
    {
        [Description("The column title text.")]
        public string title { get; set; }
        [Description("The column field name.")]
        public string field { get; set; }
        [Description("The width of column. If not defined, the width will auto expand to fit its contents. No width definition will reduce performance.")]
        public string width { get; set; }
        [Description("Indicate how many rows a cell should take up.")]
        public string rowspan { get; set; }
        [Description("Indicate how many columns a cell should take up.")]
        public string colspan { get; set; }
        [Description("Indicate how to align the column data. 'left','right','center' can be used.")]
        [TypeConverter(typeof(DataGridColumnAlignConverter))]
        public string align { get; set; }
        [Description("Indicate how to align the column header. Possible values are: 'left','right','center'. If not assigned, the header alignment is same as data alignment defined via 'align' property.")]
        [TypeConverter(typeof(DataGridColumnAlignConverter))]
        public string halign { get; set; }
        [Description("True to allow the column can be sorted.")]
        [TypeConverter(typeof(EditableBooleanConverter))]
        public string sortable { get; set; }
        [Description("The default sort order, can only be 'asc' or 'desc'.")]
        [TypeConverter(typeof(DataGridColumnOrderConverter))]
        public string order { get; set; }
        [Description("True to allow the column can be resized.")]
        [TypeConverter(typeof(EditableBooleanConverter))]
        public string resizable { get; set; }
        [DisplayName("fixed")]
        [Description("True to prevent from adjusting width when 'fitColumns' is set to true.")]
        [TypeConverter(typeof(EditableBooleanConverter))]
        public string _fixed { get; set; }
        [Description("True to hide the column.")]
        [TypeConverter(typeof(EditableBooleanConverter))]
        public string hidden { get; set; }
        [Description("True to show a checkbox. The checkbox column has fixed width.")]
        [TypeConverter(typeof(EditableBooleanConverter))]
        public string checkbox { get; set; }
        public UIDataGridColumn()
        {
            EditorId = "UIDataGridColumn";
            Info = "UIDataGridColumn: ";
            title = id;
            field = id;
            rowspan = "";
            colspan = "";
            align = "";
            halign = "";
            sortable = "";
            order = "";
            resizable = "";
            _fixed = "";
            hidden = "";
            checkbox = "";
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"title:'{title}'");
            if (!string.IsNullOrEmpty(field))
            {
                sb.Append($",field:'{field}'");
            }
            if (!string.IsNullOrEmpty(width))
            {
                sb.Append($",width:{width}");
            }
            if (!string.IsNullOrEmpty(rowspan))
            {
                sb.Append($",rowspan:{rowspan}");
            }
            if (!string.IsNullOrEmpty(colspan))
            {
                sb.Append($",colspan:{colspan}");
            }
            if (!string.IsNullOrEmpty(align))
            {
                sb.Append($",align:'{align}'");
            }
            if (!string.IsNullOrEmpty(halign))
            {
                sb.Append($",halign:'{halign}'");
            }
            if (!string.IsNullOrEmpty(sortable))
            {
                sb.Append($",sortable:{sortable}");
            }
            if (!string.IsNullOrEmpty(order))
            {
                sb.Append($",order:'{order}'");
            }
            if (!string.IsNullOrEmpty(resizable))
            {
                sb.Append($",resizable:{resizable}");
            }
            if (!string.IsNullOrEmpty(_fixed))
            {
                sb.Append($",fixed:{_fixed}");
            }
            if (!string.IsNullOrEmpty(hidden))
            {
                sb.Append($",hidden:{hidden}");
            }
            if (!string.IsNullOrEmpty(checkbox))
            {
                sb.Append($",checkbox:{checkbox}");
            }
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType==GenHTMLType.Editor)
            {
                sb.Append($"<th id=\"{EditorId}\" ");
            }
            else
            {
                sb.Append($"<th ");
                if(!string.IsNullOrEmpty(id))
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
            }
            sb.Append(GetDataOptions());
            sb.Append(">");
            foreach (var obj in UIObjects)
            {
                sb.Append(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</th>\r\n");
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UIDataGridColumn";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "title", Value = title });
            if (!string.IsNullOrEmpty(field))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "field", Value = field });
            }
            if (!string.IsNullOrEmpty(width))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width });
            }

            if (!string.IsNullOrEmpty(rowspan))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "rowspan", Value = rowspan });
            }

            if (!string.IsNullOrEmpty(colspan))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "colspan", Value = colspan });
            }

            if (!string.IsNullOrEmpty(align))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "align", Value = align });
            }
            if (!string.IsNullOrEmpty(halign))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "halign", Value = halign });
            }
            if (!string.IsNullOrEmpty(sortable))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "sortable", Value = sortable });
            }
            if (!string.IsNullOrEmpty(order))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "order", Value = order });
            }
            if (!string.IsNullOrEmpty(resizable))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "resizable", Value = resizable });
            }
            if (!string.IsNullOrEmpty(_fixed))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "fixed", Value = _fixed });
            }
            if (!string.IsNullOrEmpty(hidden))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "hidden", Value = hidden });
            }
            if (!string.IsNullOrEmpty(checkbox))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "checkbox", Value = checkbox });
            }
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
                    case "title": title = p.Value; break;
                    case "field": field = p.Value; break;
                    case "width": width = p.Value; break;
                    case "rowspan": rowspan = p.Value; break;
                    case "colspan": colspan = p.Value; break;
                    case "align": align = p.Value; break;
                    case "halign": halign = p.Value; break;
                    case "sortable": sortable = p.Value; break;
                    case "order": order = p.Value; break;
                    case "resizable": resizable = p.Value; break;
                    case "fixed": _fixed = p.Value; break;
                    case "hidden": hidden = p.Value; break;
                    case "checkbox": checkbox = p.Value; break;
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
