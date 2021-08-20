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
    public class UIBlock : UIObject
    {
        [TypeConverter(typeof(DisplayCSSConverter))]
        public string display { get; set; }
        [TypeConverter(typeof(VerticalAlignCSSConverter))]
        public string verticalAlign {get;set;}
        public string width { get; set; }
        public string height { get; set; }
        public string style { get; set;}
        public string cssclass { get; set; }
        public UIBlock()
        {
            EditorId = "UIBlock";
            Info = "UIBlock: ";
            display = "";
            style = "";
            cssclass = "";
        }

        public UIBlock(UIBlock copy)
        {
            EditorId = "UIBlock";
            Info = copy.Info;
            display = copy.display;
            style = copy.style;
            cssclass = copy.cssclass;
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType==GenHTMLType.Editor)
            {
                sb.Append($"<div Id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"");
            }
            else
            {
                sb.Append($"<div ");
                if (!string.IsNullOrEmpty(id))
                {
                    if (modalSupport)
                    {
                        sb.Append(" th:id=\"${prefix}+'" + id + "'\" ");
                    }
                    else
                    {
                        sb.Append($" Id=\"{id}\" ");
                    }
                }
            }
            if(!string.IsNullOrEmpty(cssclass))
            {
                sb.Append("class=\"");
                sb.Append(cssclass);
                sb.Append("\" ");
            }
            sb.Append("style=\"");
            if(!string.IsNullOrEmpty(display))
            {
                sb.Append($"display:{display};");
            }
            if (!string.IsNullOrEmpty(verticalAlign))
            {
                sb.Append($"vertical-align:{verticalAlign};");
            }
            if (!string.IsNullOrEmpty(width))
            {
                sb.Append($"width:{width};");
            }
            if (!string.IsNullOrEmpty(height))
            {
                sb.Append($"height:{height};");
            }
            if(!string.IsNullOrEmpty(style))
            {
                sb.Append($"{style}");
            }
            sb.Append("\"");
            sb.Append(">\r\n");
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append($"</div>\r\n");
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UIBlock";

            if (!string.IsNullOrEmpty(display))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "display", Value = display });
            }
            if (!string.IsNullOrEmpty(verticalAlign))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "verticalAlign", Value = verticalAlign });
            }
            if (!string.IsNullOrEmpty(width))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width });
            }
            if (!string.IsNullOrEmpty(height))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height });
            }
            if (!string.IsNullOrEmpty(style))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "style", Value = style });
            }
            if (!string.IsNullOrEmpty(cssclass))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "cssclass", Value = cssclass });
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
                    case "display": display = p.Value; break;
                    case "verticalAlign": verticalAlign = p.Value; break;
                    case "width": width = p.Value; break;
                    case "height": height = p.Value; break;
                    case "style": style = p.Value; break;
                    case "cssclass": cssclass = p.Value; break;
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
