using CreatorUI.JsonObjs;
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
    public class UIComboBox : UIObject
    {
        [Description("The width of the component.")]
        public string width { get; set; }
        [Description("The height of the component.")]
        public int height { get; set; }
        [Description("The width of the drop down panel.")]
        public string panelWidth { get; set; }
        [Description("The height of the drop down panel.")]
        public int panelHeight { get; set; }
        [Description("Defines if to select an item when navigating items with keyboard.")]
        public bool selectOnNavigation { get; set; }
        [Description("Defines if user can type text directly into the field.")]
        public bool editable { get; set; }
        [Description("Defines if to disable the field.")]
        public bool disabled { get; set; }
        [Description("Defines if the component is read-only.")]
        public bool _readonly { get; set; }
        [Description("The default value.")]
        public string value { get; set; }
        [Description("The underlying data value name to bind to this ComboBox.")]
        public string valueField { get; set; }
        [Description("The underlying data field name to bind to this ComboBox.")]
        public string textField { get; set; }
        [Description("Indicate what field to be grouped.")]
        public string groupField { get; set; }
        public UIComboBox()
        {
            EditorId = "UIComboBox";
            Info = "UIComboBox: ";
            width = "";
            height = 22;
            panelHeight = 200;
            selectOnNavigation = true;
            editable = true;
            disabled = false;
            _readonly = false;
            value = "";
            valueField = "value";
            textField = "text";
            groupField = "";
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"height:{height}");
            if(!string.IsNullOrEmpty(width))
            {
                sb.Append($",width:{width}");
            }
            sb.Append($",panelHeight:{panelHeight}");
            sb.Append($",selectOnNavigation:{selectOnNavigation.ToString().ToLower()}");
            sb.Append($",editable:{editable.ToString().ToLower()}");
            sb.Append($",disabled:{disabled.ToString().ToLower()}");
            sb.Append($",readonly:{_readonly.ToString().ToLower()}");
            sb.Append($",value:'{value}'");
            sb.Append($",valueField:'{valueField}'");
            sb.Append($",textField:'{textField}'");
            if (!string.IsNullOrEmpty(groupField))
            {
                sb.Append($",groupField:'{groupField}'");
            }
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType == GenHTMLType.Editor)
            {
                sb.Append($"<div style=\"display:inline-block;\" id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"><select class=\"easyui-combobox\" ");
            }
            else
            {
                sb.Append($"<select");
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
                sb.Append($" class=\"easyui-combobox\" ");
            }
            sb.Append(GetDataOptions());
            sb.Append(">\r\n");
            foreach (var obj in UIObjects)
            {
                sb.Append(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append($"</select>");
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($"</div>");
            }
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UIComboBox";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height.ToString() });
            if (!string.IsNullOrEmpty(width))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width.ToString() });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "panelHeight", Value = panelHeight.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "selectOnNavigation", Value = selectOnNavigation.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "editable", Value = editable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "disabled", Value = disabled.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "readonly", Value = _readonly.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "value", Value = value });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "valueField", Value = valueField });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "textField", Value = textField });
            if (!string.IsNullOrEmpty(groupField))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "groupField", Value = groupField });
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
                    case "height": height = int.Parse(p.Value); break;
                    case "width": width = p.Value; break;
                    case "panelHeight": panelHeight = int.Parse(p.Value); break;
                    case "selectOnNavigation": selectOnNavigation = p.Value == "true"; break;
                    case "editable": editable = p.Value == "true"; break;
                    case "disabled": disabled = p.Value == "true"; break;
                    case "readonly": _readonly = p.Value == "true"; break;
                    case "value": value = p.Value; break;
                    case "valueField": valueField = p.Value; break;
                    case "textField": textField = p.Value; break;
                    case "groupField": groupField = p.Value; break;
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
