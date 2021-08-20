using CreatorUI.JsonObjs;
using CreatorUI.PropertyGrid;
using CreatorUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CreatorUI.Models
{
    public class UILinkButton : UIObject
    {
        [Description("The width of this component.")]
        public string width { get; set; }
        [Description("The height of this component.")]
        public string height { get; set; }
        [Description("True to disable the button")]
        public bool disabled { get; set; }
        [Description("True to enable the user to switch its state to selected or unselected.")]
        public bool toggle { get; set; }
        [Description("Defines if the button's state is selected.")]
        public bool selected { get; set; }
        [Description("The group name that indicates what buttons belong to.")]
        public string group { get; set; }
        [Description("True to show a plain effect.")]
        public bool plain { get; set; }
        [Description("The button text.")]
        public string text { get; set; }
        [Description("A CSS class to display a 16x16 icon on left.")]
        public string iconCls { get; set; }
        [Description("Position of the button icon. Possible values are: 'left','right','top','bottom'.")]
        [TypeConverter(typeof(IconAlignConverter))]
        public string iconAlign { get; set; }
        [Description("The button size. Possible values are: 'small','large'")]
        [TypeConverter(typeof(SizeLinkButtonConverter))]
        public string size { get; set; }
        [Description("Refference")]
        public string href { get; set; }
        public UILinkButton()
        {
            EditorId = "UILinkButton";
            Info = "UILinkButton: ";
            width = "";
            height = "";
            disabled = false;
            toggle = false;
            selected = false;
            group = "";
            plain = false;
            text = "";
            iconCls = "icon-";
            iconAlign = "left";
            size = "small";
            href = "javascript:void(0)";
        }
        public string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"disabled:{disabled.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(width))
            {
                sb.Append($",width:{width}");
            }
            if (!string.IsNullOrEmpty(height))
            {
                sb.Append($",height:{height}");
            }
            sb.Append($",toggle:{toggle.ToString().ToLower()}");
            sb.Append($",selected:{selected.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(group))
            {
                sb.Append($",group:'{group}'");
            }
            sb.Append($",plain:{plain.ToString().ToLower()}");
            sb.Append($",text:'{text}'");
            if (!string.IsNullOrEmpty(iconCls))
            {
                sb.Append($",iconCls:'{iconCls}'");
            }
            sb.Append($",iconAlign:'{iconAlign}'");
            sb.Append($",size:'{size}'");
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($"<div style=\"display:inline-block;\" id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"><a onClick=\" return false\" ");
            }
            else
            {
                sb.Append($"<a ");
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
            }
            sb.Append($" href=\"{href}\"");
            sb.Append(" class=\"easyui-linkbutton\" ");
            sb.Append(GetDataOptions());
            sb.Append(">");
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</a>\r\n");
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
            jsonObj.UIComponentName = "UILinkButton";
            jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "disabled", Value = disabled.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "toggle", Value = toggle.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "selected", Value = toggle.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "group", Value = group });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "plain", Value = plain.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "text", Value = text });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "iconCls", Value = iconCls });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "iconAlign", Value = iconAlign });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "size", Value = size });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "href", Value = href });
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
                    case "width": width = p.Value; break;
                    case "height": height = p.Value; break;
                    case "disabled": disabled = p.Value == "true"; break;
                    case "toggle": toggle = p.Value == "true"; break;
                    case "selected": selected = p.Value == "true"; break;
                    case "group": group = p.Value; break;
                    case "plain": plain = p.Value == "true"; break;
                    case "text": text = p.Value; break;
                    case "iconCls": iconCls = p.Value; break;
                    case "iconAlign": iconAlign = p.Value; break;
                    case "size": size = p.Value; break;
                    case "href": href = p.Value; break;
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