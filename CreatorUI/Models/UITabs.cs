using CreatorUI.JsonObjs;
using CreatorUI.PropertyGrid;
using CreatorUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI.Models
{
    [DataContract]
    public class UITabs : UIObject
    {
        [Description("True to render the tab strip without a background container image.")]
        [DataMember]
        public bool plain { get; set; }

        [Description("True to set the size of tabs container to fit it's parent container.")]
        [DataMember]
        public bool fit { get; set; }
        [Description("True to show tabs container border.")]
        [DataMember]
        public bool border { get; set; }
        [Description("The number of pixels to scroll each time a tab scroll button is pressed.")]
        [DataMember]
        public int scrollIncrement { get; set; }
        [Description("The number of milliseconds that each scroll animation should last.")]
        [DataMember]
        public int scrollDuration { get; set; }
        [Description("The tab position. Possible values: 'top','bottom','left','right'.")]
        [TypeConverter(typeof(TabPositionConverter))]
        [DataMember]
        public string tabPosition { get; set; }
        [Description("The tab header width, it is valid only when tabPosition is set to 'left' or 'right'.")]
        [DataMember]
        public int headerWith { get; set; }
        [Description("The width of tab strip.")]
        [DataMember]
        public string tabWidth { get; set; }
        [Description("The height of tab strip.")]
        [DataMember]
        public int tabHeight { get; set; }
        [Description("The initialized selected tab index.")]
        [DataMember]
        public int selected { get; set; }
        [Description("True to display tabs header.")]
        [DataMember]
        public bool showHeader { get; set; }
        [Description("True to make tab strips equal widths of their parent container.")]
        [DataMember]
        public bool justified { get; set; }
        [Description("True to remove the space between tab strips.")]
        [DataMember]
        public bool narrow { get; set; }
        [Description("True to make tab strips look like pills.")]
        [DataMember]
        public bool pill { get; set; }
        private bool first = true;
        public UITabs()
        {
            EditorId = "UITabs";
            Info = "UITabs: ";
            fit = false;
            plain = false;
            border = true;
            scrollIncrement = 100;
            scrollDuration = 400;
            tabPosition = "top";
            headerWith = 150;
            tabWidth = "";
            tabHeight = 27;
            selected = 0;
            showHeader = true;
            justified = false;
            narrow = false;
            pill = false;
        }
        public string GetDataOptions(GenHTMLType GenType)
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"fit:{plain.ToString().ToLower()}");
            sb.Append($",plain:{plain.ToString().ToLower()}");
            sb.Append($",border:{border.ToString().ToLower()}");
            sb.Append($",scrollIncrement:{scrollIncrement}");
            sb.Append($",scrollDuration:{scrollDuration}");
            sb.Append($",tabPosition:'{tabPosition}'");
            sb.Append($",headerWith:{headerWith}");
            if (!string.IsNullOrEmpty(tabWidth))
            {
                sb.Append($",tabWith:{tabWidth}");
            }
            sb.Append($",tabHeight:{tabHeight}");
            sb.Append($",selected:{selected}");
            sb.Append($",showHeader:{showHeader.ToString().ToLower()}");
            sb.Append($",justified:{justified.ToString().ToLower()}");
            sb.Append($",narrow:{narrow.ToString().ToLower()}");
            sb.Append($",pill:{pill.ToString().ToLower()}");

           if (first&&GenType == GenHTMLType.Editor)
            {
                first = false;
                sb.Append(",onSelect:function(title){ var target = this; var pp = $(target).tabs('getSelected'); cefAPI.selUIObject(pp[0].id, true);}");
            }

            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType == GenHTMLType.Editor)
            {
                sb.Append($@"<div Id=""{EditorId}"" onClick=""cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();"" class=""easyui-tabs"" ");
            }
            else
            {
                sb.Append($@"<div");
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
                sb.Append($@" class=""easyui-tabs"" ");
            }
            sb.Append(GetDataOptions(GenType));
            sb.Append(">");
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</div>\r\n");
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UITabs";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "fit", Value = fit.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "plain", Value = plain.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "border", Value = border.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "scrollIncrement", Value = scrollIncrement.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "scrollDuration", Value = scrollDuration.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "tabPosition", Value = tabPosition.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "headerWith", Value = headerWith.ToString() });
            if (!string.IsNullOrEmpty(tabWidth))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "tabWith", Value = tabWidth.ToString() });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "tabHeight", Value = tabHeight.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "selected", Value = selected.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "showHeader", Value = showHeader.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "justified", Value = justified.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "narrow", Value = narrow.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "pill", Value = pill.ToString().ToLower() });
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
                    case "fit": fit = p.Value == "true"; break;
                    case "plain": plain = p.Value == "true"; break;
                    case "border": border = p.Value == "true"; break;
                    case "scrollIncrement": scrollIncrement = int.Parse(p.Value); break;
                    case "scrollDuration": scrollDuration = int.Parse(p.Value); break;
                    case "tabPosition": tabPosition = p.Value; break;
                    case "headerWith": headerWith = int.Parse(p.Value); break;
                    case "tabWith": tabWidth = p.Value; break;
                    case "tabHeight": tabHeight = int.Parse(p.Value); break;
                    case "selected": selected = int.Parse(p.Value); break;
                    case "showHeader": showHeader = p.Value == "true"; break;
                    case "justified": justified = p.Value == "true"; break;
                    case "narrow": narrow = p.Value == "true"; break;
                    case "pill": pill = p.Value == "true"; break;
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
