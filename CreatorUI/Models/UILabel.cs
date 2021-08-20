using CreatorUI.JsonObjs;
using CreatorUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI.Models
{
    public class UILabel : UIObject
    {
        [DisplayName("Text")]
        public string Text { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Font Font { get; set; }
        public UILabel()
        {
            EditorId = "UILabel";
            Info = "UILabel: ";
            Text = "UILabel";
            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType== GenHTMLType.Editor)
            {
                sb.Append($"<div style=\"display:inline-block;\" id=\"{EditorId}\"><span");
                sb.Append($" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"");
                sb.Append($" style=\"");
            }
            else
            {
                sb.Append($"<span");
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
                sb.Append($" style=\"");
            }
            if (BackColor != Color.Transparent)
            {
                sb.Append($"background-color:");
                sb.Append("#");
                sb.Append(BackColor.R.ToString("X2"));
                sb.Append(BackColor.G.ToString("X2"));
                sb.Append(BackColor.B.ToString("X2"));
                sb.Append((255).ToString("X2"));
                sb.Append($";");
            }
            if (ForeColor != Color.Transparent)
            {
                sb.Append($"color:");
                sb.Append("#");
                sb.Append(ForeColor.R.ToString("X2"));
                sb.Append(ForeColor.G.ToString("X2"));
                sb.Append(ForeColor.B.ToString("X2"));
                sb.Append((255).ToString("X2"));
                sb.Append($";");
            }
            if (Font != null)
            {
                sb.Append("font-family:");
                sb.Append(Font.FontFamily.Name);
                sb.Append(";");

                sb.Append("font-size:");
                sb.Append(Font.SizeInPoints);
                sb.Append("pt");
                sb.Append(";");

                if (Font.Underline)
                {
                    sb.Append("text-decoration: underline;");
                }
                if(Font.Bold)
                {
                    sb.Append("font-weight: bold;");
                }
                if(Font.Italic)
                {
                    sb.Append("font-style: italic;");
                }
            }
            sb.Append("\"");
            sb.Append(">");
            sb.Append(Text);
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</span>\r\n");
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
            jsonObj.UIComponentName = "UILabel";
            var strBackColor = "#"
                + BackColor.R.ToString("X2")
                + BackColor.G.ToString("X2")
                + BackColor.B.ToString("X2")
                + (255).ToString("X2");
            var strForeColor = "#"
                + ForeColor.R.ToString("X2")
                + ForeColor.G.ToString("X2")
                + ForeColor.B.ToString("X2")
                + (255).ToString("X2");
            jsonObj.UIParams.Add(new JsonUIParam { Name = "Text", Value = Text });
            if (BackColor != Color.Transparent)
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "BackColor", Value = strBackColor });
            }
            if (ForeColor != Color.Transparent)
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "ForeColor", Value = strForeColor });
            }
            if (Font != null)
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "font-family", Value = Font.FontFamily.Name });
                jsonObj.UIParams.Add(new JsonUIParam { Name = "font-size", Value = Font.SizeInPoints.ToString() });
                if (Font.Underline)
                {
                    jsonObj.UIParams.Add(new JsonUIParam { Name = "text-decoration", Value = "underline" });
                }
                if (Font.Bold)
                {
                    jsonObj.UIParams.Add(new JsonUIParam { Name = "font-weight", Value = "bold" });
                }
                if (Font.Italic)
                {
                    jsonObj.UIParams.Add(new JsonUIParam { Name = "font-style", Value = "italic" });
                }
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
            var font_family = "";
            var font_size = "";
            var text_decoration = "";
            var font_weight = "";
            var font_style = "";
            foreach (var p in json.UIParams)
            {
                switch (p.Name)
                {
                    case "Text": Text = p.Value; break;
                    case "BackColor":
                        BackColor = Color.FromArgb(Convert.ToInt32("0xFF" + p.Value.Substring(1, 6), 16));
                        break;
                    case "ForeColor":
                        ForeColor = Color.FromArgb(Convert.ToInt32("0xFF" + p.Value.Substring(1, 6), 16));
                        break;
                    case "font-family": font_family = p.Value; break;
                    case "font-size": font_size = p.Value; break;
                    case "text-decoration": text_decoration = p.Value; break;
                    case "font-weight": font_weight = p.Value; break;
                    case "font-style": font_style = p.Value; break;
                }
            }
            if (!string.IsNullOrEmpty(font_family))
            {
                var style = FontStyle.Regular;
                if (!string.IsNullOrEmpty(text_decoration))
                {
                    style |= FontStyle.Underline;
                }
                if (!string.IsNullOrEmpty(font_weight))
                {
                    style |= FontStyle.Bold;
                }
                if (!string.IsNullOrEmpty(font_style))
                {
                    style |= FontStyle.Italic;
                }
                var fnt = new Font(new FontFamily(font_family), float.Parse(font_size), style);
                Font = fnt;
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
