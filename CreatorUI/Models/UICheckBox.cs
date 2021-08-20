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
    public class UICheckBox : UIObject
    {
        [Description("The width of checkbox component.")]
        public int width { get; set; }
        [Description("The height of checkbox component.")]
        public int height { get; set; }
        [Description("Defines if the checkbox is checked.")]
        public bool _checked { get; set; }
        [Description("Defines if to disable the checkbox.")]
        public bool disabled { get; set; }
        [Description("The label bound to the checkbox.")]
        public string label { get; set; }
        [Description("The label width.")]
        public string labelWidth { get; set; }
        [Description("The label position. Possible values are:'before','after','top'.")]
        [TypeConverter(typeof(CheckBoxLabelPositionConverter))]
        public string labelPosition { get; set; }
        [Description("The label position. Possible values are:'left','right'.")]
        [TypeConverter(typeof(CheckBoxLabelAlignConverter))]
        public string labelAlign { get; set; }
        public UICheckBox()
        {
            EditorId = "UICheckBox";
            Info = "UICheckBox: ";
            width = 20;
            height = 20;
            _checked = false;
            disabled = false;
            label = "";
            labelWidth = "";
            labelPosition = "before";
            labelAlign = "left";
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"with:{width}");
            sb.Append($",height:{height}");
            sb.Append($",checked:{_checked.ToString().ToLower()}");
            sb.Append($",disabled:{disabled.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(label))
            {
                sb.Append($",label:'{label}'");
            }
            if (!string.IsNullOrEmpty(labelWidth))
            {
                sb.Append($",labelWidth:{labelWidth}");
            }
            sb.Append($",labelPosition:'{labelPosition}'");
            sb.Append($",labelAlign:'{labelAlign}'");
            sb.Append("\"");
            return sb.ToString();
        }
        private string GenHtmlEditor()
        {
            var sb = new StringBuilder();
            sb.Append($"<div style=\"display:inline-block;\" id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\" ><input ");
            sb.Append(GetDataOptions());
            sb.Append(" class=\"easyui-checkbox\"");
            sb.Append("/></div>\r\n");
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenHTMLType.Editor, false));
            }
            return sb.ToString();
        }
        private string GenHtmlTemplate(bool modalSupport)
        {
            var sb = new StringBuilder();
            sb.Append($"<input ");
            if(!string.IsNullOrEmpty(id))
            {
                if (modalSupport)
                {
                    sb.Append(" th:id=\"${prefix}+'" + id + "'\" ");
                }
                else
                {
                    sb.Append($"id=\"{id}\" ");
                }
            }
            sb.Append(GetDataOptions());
            sb.Append(" class=\"easyui-checkbox\"");
            sb.Append("/>\r\n");
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenHTMLType.Template, modalSupport));
            }
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            if(GenType == GenHTMLType.Editor)
            {
                return GenHtmlEditor();
            }
            else
            {
                return GenHtmlTemplate(modalSupport);
            }
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UICheckBox";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "checked", Value = _checked.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "disabled", Value = disabled.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "label", Value = label });
            if (!string.IsNullOrEmpty(labelWidth))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "labelWidth", Value = labelWidth });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "labelPosition", Value = labelPosition });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "labelAlign", Value = labelAlign });
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
            if (ParentNode.Tag!=null)
            {
                Owner = (UIObject)ParentNode.Tag;
            }

            ParentNode.Nodes.Add(node);
            sId.AddUIObjectProject(this);
            foreach (var p in json.UIParams)
            {
                switch (p.Name)
                {
                    case "width": width = int.Parse(p.Value); break;
                    case "height": height = int.Parse(p.Value); break;
                    case "checked": _checked = p.Value == "true"; break;
                    case "disabled": disabled = p.Value == "true"; break;
                    case "label": label = p.Value; break;
                    case "labelWidth": labelWidth = p.Value; break;
                    case "labelPosition": labelPosition = p.Value; break;
                    case "labelAlign": labelAlign = p.Value; break;
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
