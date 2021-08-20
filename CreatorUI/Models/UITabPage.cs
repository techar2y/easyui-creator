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
    public class UITabPage : UIObject
    {
        [Description("The tab panel title text.")]
        public string title { get; set; }
        [Description("The tab panel content.")]
        public string content { get; set; }
        [Description("A URL to load remote content to fill the tab panel.")]
        public string href { get; set; }
        [Description("True to cache the tab panel, valid when href property is setted.")]
        public bool cache { get; set; }
        [Description("An icon CSS class to show on tab panel title.")]
        public string iconCls { get; set; }
        [Description("True to allow tab panel to be collapsed.")]
        public bool collapsible { get; set; }
        [Description("When set to true, the tab panel will show a closable button which can be clicked to close the tab panel.")]
        public bool closable { get; set; }
        [Description("When set to true, the tab panel will be selected.")]
        public bool selected { get; set; }
        [Description("When set to true, the tab panel will be disabled.")]
        public bool disabled { get; set; }

        public UITabPage()
        {
            EditorId = "UITabPage";
            Info = "UITabPage: ";
            title = "UITabPage";
            content = "";
            href = "";
            cache = true;
            iconCls = "";
            collapsible = false;
            closable = false;
            selected = false;
            disabled = false;
        }
        public string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"content:'{content}'");
            if (!string.IsNullOrEmpty(href))
            {
                sb.Append($",href:'{href}'");
            }
            sb.Append($",cache:{cache.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(iconCls))
            {
                sb.Append($",iconCls:'{iconCls}'");
            }
            sb.Append($",collapsible:{collapsible.ToString().ToLower()}");
            sb.Append($",closable:{closable.ToString().ToLower()}");
            sb.Append($",selected:{selected.ToString().ToLower()}");
            sb.Append($",disabled:{disabled.ToString().ToLower()}");
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType == GenHTMLType.Editor)
            {
                sb.Append($"<div Id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\" ");
            }
            else
            {
                sb.Append($"<div ");
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
            sb.Append(@" title=""");
            sb.Append(title);
            sb.Append(@""">");

            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</div>");
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UITabPage";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "title", Value = title });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "content", Value = content });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "href", Value = href });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "cache", Value = cache.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "iconCls", Value = iconCls });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "collapsible", Value = collapsible.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "closable", Value = closable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "selected", Value = selected.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "disabled", Value = disabled.ToString().ToLower() });
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
                    case "content": content = p.Value; break;
                    case "href": href = p.Value; break;
                    case "cache": cache = p.Value == "true"; break;
                    case "iconCls": iconCls = p.Value; break;
                    case "collapsible": collapsible = p.Value == "true"; break;
                    case "closable": closable = p.Value == "true"; break;
                    case "selected": selected = p.Value == "true"; break;
                    case "disabled": disabled = p.Value == "true"; break;
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
