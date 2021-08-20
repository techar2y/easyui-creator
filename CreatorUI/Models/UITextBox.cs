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
    public class UITextBox : UIObject
    {
        [Description("The width of the component.")]
        public string width { get; set; }
        [Description("The height of the component.")]
        public string height { get; set; }
        [Description("Add a CSS class to the textbox.")]
        public string cls { get; set; }
        [Description("The prompt message to be displayed in input box.")]
        public string prompt { get; set; }
        [Description("The default value.")]
        public string value { get; set; }
        [Description("The textbox type. Possible values are 'text' and 'password'.")]
        [TypeConverter(typeof(TypeTextBoxConverter))]
        public string type { get; set; }
        [Description("Defines if this is a multiline textbox.")]
        public bool multiline { get; set; }
        [Description("Defines if the user can type text directly into the field.")]
        public bool editable { get; set; }
        [Description("Defines if to disable the field.")]
        public bool disabled { get; set; }
        [Description("Defines if the component is read-only.")]
        public bool _readonly { get; set; }
        [Description("The background icon displayed on the textbox.")]
        public string iconCls { get; set; }
        [Description("Position of the icons. Possible values are 'left','right'.")]
        public string iconAlign { get; set; }
        [Description("The icon width.")]
        public int iconWidth { get; set; }
        [Description("The displaying text of button that attached to the textbox.")]
        public string buttonText { get; set; }
        [Description("The displaying icon of button that attached to the textbox.")]
        public string buttonIcon { get; set; }
        [Description("Position of the button. Possible values are 'left','right'.")]
        public string buttonAlign { get; set; }
        public UITextBox()
        {
            EditorId = "UITextBox";
            Info = "UITextBox: ";
            width = "";
            cls = "";
            prompt = "";
            type = "text";
            multiline = false;
            editable = true;
            disabled = false;
            _readonly = false;
            iconCls = "";
            iconAlign = "right";
            iconWidth = 18;
            buttonText = "";
            buttonIcon = "";
            buttonAlign = "right";
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"multiline:{multiline.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(width))
            {
                sb.Append($",width:{width}");
            }
            if (!string.IsNullOrEmpty(height))
            {
                sb.Append($",height:{height}");
            }
            if (!string.IsNullOrEmpty(cls))
            {
                sb.Append($",cls:'{cls}'");
            }
            sb.Append($",prompt:'{prompt}'");
            sb.Append($",value:'{value}'");
            sb.Append($",type:'{type}'");
            sb.Append($",editable:{editable.ToString().ToLower()}");
            sb.Append($",disabled:{disabled.ToString().ToLower()}");
            sb.Append($",readonly:{_readonly.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(iconCls))
            {
                sb.Append($",iconCls:'{iconCls}'");
            }
            sb.Append($",iconAlign:'{iconAlign}'");
            sb.Append($",iconWidth:{iconWidth}");
            sb.Append($",buttonText:'{buttonText}'");
            sb.Append($",buttonIcon:'{buttonIcon}'");
            sb.Append($",buttonAlign:'{buttonAlign}'");
            sb.Append("\" ");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($@"<div style=""display:inline-block;"" Id=""{EditorId}"" onClick=""cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();""><input class=""easyui-textbox"" ");
            }
            else
            {
                sb.Append($@"<input ");
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
                sb.Append(@" class=""easyui-textbox"" ");
            }
            sb.Append(GetDataOptions());
            sb.Append("/>\r\n");

            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append("</div>\r\n");
            }
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UITextBox";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "multiline", Value = multiline.ToString().ToLower() });
            if (!string.IsNullOrEmpty(width))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width });
            }
            if (!string.IsNullOrEmpty(height))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height });
            }
            if (!string.IsNullOrEmpty(cls))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "cls", Value = cls });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "prompt", Value = prompt });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "value", Value = value });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "type", Value = type });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "editable", Value = editable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "disabled", Value = disabled.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "readonly", Value = _readonly.ToString().ToLower() });
            if (!string.IsNullOrEmpty(iconCls))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "iconCls", Value = iconCls });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "iconAlign", Value = iconAlign });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "iconWidth", Value = iconWidth.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "buttonText", Value = buttonText });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "buttonIcon", Value = buttonIcon });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "buttonAlign", Value = buttonAlign });
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
                    case "multiline": multiline = p.Value == "true"; break;
                    case "width": width = p.Value; break;
                    case "height": height = p.Value; break;
                    case "cls": cls = p.Value; break;
                    case "prompt": prompt = p.Value; break;
                    case "value": value = p.Value; break;
                    case "type": type = p.Value; break;
                    case "editable": editable = p.Value == "true"; break;
                    case "disabled": disabled = p.Value == "true"; break;
                    case "readonly": _readonly = p.Value == "true"; break;
                    case "iconCls": iconCls = p.Value; break;
                    case "iconAlign": iconAlign = p.Value; break;
                    case "iconWidth": iconWidth = int.Parse(p.Value); break;
                    case "buttonText": buttonText = p.Value; break;
                    case "buttonIcon": buttonIcon = p.Value; break;
                    case "buttonAlign": buttonAlign = p.Value; break;
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
