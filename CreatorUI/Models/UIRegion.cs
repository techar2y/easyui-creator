using CreatorUI.JsonObjs;
using CreatorUI.PropertyGrid;
using CreatorUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI.Models
{
    [DataContract]
    public class UIRegion : UIObject
    {
        [Description("Defines the layout panel position, the value is one of following: north, south, east, west, center.")]
        [TypeConverter(typeof(RegionTypeConverter))]
        [DataMember]
        public string region { get; set; }
        [DataMember]
        public string style { get; set; }
        [Description("The layout panel title text.")]
        [DataMember]
        public string title { get; set; }
        [Description("True to show layout panel border.")]
        [DataMember]
        public bool border { get; set; }
        [Description("True to show a split bar which user can change the panel size.")]
        [DataMember]
        public bool split { get; set; }
        [Description("Defines if to show collapsible button.")]
        [DataMember]
        public bool collapsible { get; set; }
        [Description("The minimum panel width.")]
        [DataMember]
        public int minWith { get; set; }
        [Description("The minimum panel height.")]
        [DataMember]
        public int minHeight { get; set; }

        [Description("The maximum panel width.")]
        [DataMember]
        public int maxWith { get; set; }
        [Description("The maximum panel height.")]
        [DataMember]
        public int maxHeight { get; set; }
        [Description(@"The expanding mode when click on the collapsed panel. Possible values are 'float','dock' and null.
float: the region panel will expand and float on the top.
dock: the region panel will expand and dock on the layout.
null: nothing happens.")]
        [TypeConverter(typeof(RegionExpandModeConverter))]
        [DataMember]
        public string expandMode { get; set; }
        [Description("The collapsed panel size.")]
        [DataMember]
        public int collapsedSize { get; set; }
        [Description("True to hide the expand tool on the collapsed panel.")]
        [DataMember]
        public bool hideExpandTool { get; set; }
        [Description("True to hide the title bar on the collapsed panel.")]
        [DataMember]
        public bool hideCollapsedContent { get; set; }
        [Description("An icon CSS class to show a icon on panel header.")]
        [DataMember]
        public string iconCls { get; set; }
        [Description("An URL to load data from remote server.")]
        [DataMember]
        public string href { get; set; }

        [DisplayName("Width")]
        [Description("Ширина компонента")]
        [DataMember]
        public string Width { get; set; }
        [DisplayName("Height")]
        [Description("Высота компонента")]
        [DataMember]
        public string Height { get; set; }
        [DisplayName("Color")]
        [Description("Цвет фона")]
        [DataMember]
        public Color BackColor { get; set; }
        [DisplayName("Font")]
        [Description("Шрифт текста")]
        [DataMember]
        public Font Font { get; set; }
        public UIRegion()
        {
            EditorId = "UIRegion";
            Info = "UIRegion: ";
            region = "north";
            title = "";
            split = false;
            collapsible = false;
            Width = "100%";
            Height = "100%";
            BackColor = Color.Transparent;
            border = true;
            minWith = 10;
            minHeight = 10;
            maxWith = 10000;
            maxHeight = 10000;
            expandMode = "float";
            collapsedSize = 28;
            hideExpandTool = false;
            hideCollapsedContent = true;
            iconCls = "";
            href = "";
            style = "";
        }

        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("region:'");
            sb.Append(region);
            sb.Append("',collapsible:");
            sb.Append(collapsible.ToString().ToLower());
            sb.Append(",split:");
            sb.Append(split.ToString().ToLower());
            if (!string.IsNullOrEmpty(title))
            {
                sb.Append(",title:'");
                sb.Append(title);
                sb.Append("'");
            }
            sb.Append(",border:");
            sb.Append(border.ToString().ToLower());
            sb.Append($",minWidth:{minWith}");
            sb.Append($",minHeight:{minHeight}");
            sb.Append($",maxWidth:{maxWith}");
            sb.Append($",maxHeight:{maxHeight}");
            if (expandMode == "null")
            {
                sb.Append($",expandMode:null");
            }
            else
            {
                sb.Append($",expandMode:'{expandMode}'");
            }
            sb.Append($",collapsedSize:{collapsedSize}");
            sb.Append($",hideExpandTool:{hideExpandTool.ToString().ToLower()}");
            sb.Append($",hideCollapsedContent:{hideCollapsedContent.ToString().ToLower()}");
            if (string.IsNullOrEmpty(iconCls))
            {
                sb.Append($",iconCls:null");
            }
            else
            {
                sb.Append($",iconCls:'{iconCls}'");
            }
            if (string.IsNullOrEmpty(href))
            {
                sb.Append($",href:null");
            }
            else
            {
               sb.Append($",href:'{href}'");
            }
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType == GenHTMLType.Editor)
            {
                sb.Append($"<div Id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"");
            }
            else
            {
                sb.Append($"<div");
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
            sb.Append($" data-options=\"");
            sb.Append(GetDataOptions());
            sb.Append("\" style=\"");
            if (!string.IsNullOrEmpty(Width))
            {
                sb.Append("width:");
                sb.Append(Width);
                sb.Append(";");
            }
            if (!string.IsNullOrEmpty(Height))
            {
                sb.Append("height:");
                sb.Append(Height);
                sb.Append(";");
            }
            if (BackColor != Color.Transparent)
            {
                sb.Append("background-color:");
                sb.Append("#");
                sb.Append(BackColor.R.ToString("X2"));
                sb.Append(BackColor.G.ToString("X2"));
                sb.Append(BackColor.B.ToString("X2"));
                sb.Append((255).ToString("X2"));
                sb.Append(";");
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
                if (Font.Bold)
                {
                    sb.Append("font-weight: bold;");
                }
                if (Font.Italic)
                {
                    sb.Append("font-style: italic;");
                }
            }
            sb.Append(style ?? "");
            sb.Append("\">");
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
            jsonObj.UIComponentName = "UIRegion";
            var strBackColor = "#"
                + BackColor.R.ToString("X2")
                + BackColor.G.ToString("X2")
                + BackColor.B.ToString("X2")
                + (255).ToString("X2");
            jsonObj.UIParams.Add(new JsonUIParam { Name = "region", Value = region });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "title", Value = title });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "split", Value = split.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "collapsible", Value = collapsible.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "Width", Value = Width });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "Height", Value = Height });
            if (BackColor != Color.Transparent)
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "BackColor", Value = strBackColor });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "border", Value = border.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "minWith", Value = minWith.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "minHeight", Value = minHeight.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "maxWith", Value = maxWith.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "maxHeight", Value = maxHeight.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "expandMode", Value = expandMode });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "collapsedSize", Value = collapsedSize.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "hideExpandTool", Value = hideExpandTool.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "hideCollapsedContent", Value = hideCollapsedContent.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "iconCls", Value = iconCls });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "href", Value = href });
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
            jsonObj.UIParams.Add(new JsonUIParam { Name = "style", Value = style });
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
                    case "region": region = p.Value; break;
                    case "title": title = p.Value; break;
                    case "split": split = p.Value == "true"; break;
                    case "collapsible": collapsible = p.Value == "true"; break;
                    case "Width": Width = p.Value; break;
                    case "Height": Height = p.Value; break;
                    case "BackColor":
                        BackColor = Color.FromArgb(Convert.ToInt32("0xFF"+p.Value.Substring(1,6), 16));
                        break;
                    case "border": border = p.Value == "true"; break;
                    case "minWith": minWith = int.Parse(p.Value); break;
                    case "minHeight": minHeight = int.Parse(p.Value); break;
                    case "maxWith": maxWith = int.Parse(p.Value); break;
                    case "maxHeight": maxHeight = int.Parse(p.Value); break;
                    case "expandMode": expandMode = p.Value; break;
                    case "collapsedSize": collapsedSize = int.Parse(p.Value); break;
                    case "hideExpandTool": hideExpandTool = p.Value == "true"; break;
                    case "hideCollapsedContent": hideCollapsedContent = p.Value == "true"; break;
                    case "iconCls": iconCls = p.Value; break;
                    case "href": href = p.Value; break;
                    case "font-family": font_family = p.Value; break;
                    case "font-size": font_size = p.Value; break;
                    case "text-decoration": text_decoration = p.Value; break;
                    case "font_weight": font_weight = p.Value; break;
                    case "font_style": font_style = p.Value; break;
                    case "style": style = p.Value; break;
                }
            }
            if(!string.IsNullOrEmpty(font_family))
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
