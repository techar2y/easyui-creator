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
    public class UIWindow : UIObject
    {
        [Description("The window title text.")]
        public string title { get; set; }
        [Description("Set the panel width.")]
        public string width { get; set; }
        [Description("Set the panel height.")]
        public string height { get; set; }
        [Description("Defines if to show collapsible button.")]
        public bool collapsible { get; set; }
        [Description("Defines if to show minimizable button.")]
        public bool minimizable { get; set; }
        [Description("Defines if to show maximizable button.")]
        public bool maximizable { get; set; }
        [Description("Defines if to show closable button.")]
        public bool closable { get; set; }
        [Description("Defined if to close the window.")]
        public bool closed { get; set; }
        [Description("Window z-index,increase from it.")]
        public int zIndex { get; set; }
        [Description("Defines if window can be dragged.")]
        public bool draggable { get; set; }
        [Description("Defines if window can be resized.")]
        public bool resizable { get; set; }
        [Description("If set to true,when window show the shadow will show also.")]
        public bool shadow { get; set; }
        [Description("Defines how to stay the window, true to stay inside its parent, false to go on top of all elements.")]
        public bool inline { get; set; }
        [Description("Defines if window is a modal window.")]
        public bool modal { get; set; }
        [Description("Defines the window border style. Possible values are: true,false,'thin','thick'.")]
        [TypeConverter(typeof(BorderWindowConverter))]
        public string border { get; set; }
        [Description("Defines if to constrain the window position.")]
        public bool constrain { get; set; }
        [Description("A CSS class to display a 16x16 icon in panel.")]
        public string iconCls { get; set; }
        [Description("Add a CSS class to the panel.")]
        public string cls { get; set; }
        [Description("Add a CSS class to the panel header.")]
        public string headerCls { get; set; }
        [Description("bodyCls")]
        public string bodyCls { get; set; }
        [Description("If set to true, the panel header will not be created.")]
        public bool noheader { get; set; }
        [Description("Defines if the panel is collapsed at initialization.")]
        public bool collapsed  { get; set; }
        [Description("Defines if the panel is minimized at initialization.")]
        public bool minimized { get; set; }
        [Description("Defines if the panel is maximized at initialization.")]
        public bool maximized { get; set; }
        [Description("Свойство th:fragment")]
        public string thFragment { get; set; }
        
        public UIWindow()
        {
            EditorId = "UIWindow";
            Info = "UIWindow: ";
            title = "New Window";
            width = "320";
            height = "320";
            collapsible = true;
            minimizable = true;
            maximizable = true;
            closable = true;
            closed = false;
            zIndex = 9000;
            draggable = true;
            resizable = true;
            shadow = true;
            inline = false;
            modal = false;
            border = "true";
            constrain = false;
            iconCls = "";
            cls = "";
            headerCls = "";
            bodyCls = "";
            noheader = false;
            collapsed = false;
            minimized = false;
            maximized = false;
            thFragment = "";
        }

        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append($" data-options=\"");
            sb.Append($"title:'{title}'");
            if (!string.IsNullOrEmpty(width))
            {
                sb.Append($",width:{width}");
            }
            if (!string.IsNullOrEmpty(height))
            {
                sb.Append($",height:{height}");
            }
            sb.Append($",collapsible:{collapsible.ToString().ToLower()}");
            sb.Append($",minimizable:{minimizable.ToString().ToLower()}");
            sb.Append($",maximizable:{maximizable.ToString().ToLower()}");
            sb.Append($",closable:{closable.ToString().ToLower()}");
            sb.Append($",closed:{closed.ToString().ToLower()}");
            sb.Append($",zIndex:{zIndex}");
            sb.Append($",draggable:{draggable.ToString().ToLower()}");
            sb.Append($",resizable:{resizable.ToString().ToLower()}");
            sb.Append($",shadow:{shadow.ToString().ToLower()}");
            sb.Append($",inline:{inline.ToString().ToLower()}");
            sb.Append($",modal:{modal.ToString().ToLower()}");
            sb.Append($",border:{border}");
            sb.Append($",constrain:{constrain.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(iconCls))
            {
                sb.Append($",iconCls:'{iconCls}'");
            }
            if (!string.IsNullOrEmpty(cls))
            {
                sb.Append($",cls:'{cls}'");
            }
            if (!string.IsNullOrEmpty(headerCls))
            {
                sb.Append($",headerCls:'{headerCls}'");
            }
            if (!string.IsNullOrEmpty(bodyCls))
            {
                sb.Append($",bodyCls:'{bodyCls}'");
            }
            sb.Append($",noheader:{noheader.ToString().ToLower()}");
            sb.Append($",collapsed:{collapsed.ToString().ToLower()}");
            sb.Append($",minimized:{minimized.ToString().ToLower()}");
            sb.Append($",maximized:{maximized.ToString().ToLower()}");
            sb.Append($"\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType==GenHTMLType.Editor)
            {
                sb.Append($"<div id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\" class=\"easyui-window\"");
            }
            else
            {
                sb.Append($@"<div");
                if (!string.IsNullOrEmpty(thFragment))
                {
                    sb.Append(" th:fragment=\"" + thFragment + "\"");
                }
                sb.Append(@" class=""easyui-window""");
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
            sb.Append(GetDataOptions());
            sb.Append(">\r\n");
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
            jsonObj.UIComponentName = "UIWindow";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "title", Value = title });
            if (!string.IsNullOrEmpty(width))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width });
            }
            if (!string.IsNullOrEmpty(height))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "collapsible", Value = collapsible.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "minimizable", Value = minimizable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "maximizable", Value = maximizable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "closable", Value = closable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "closed", Value = closed.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "zIndex", Value = zIndex.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "draggable", Value = draggable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "resizable", Value = resizable.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "shadow", Value = shadow.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "inline", Value = inline.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "modal", Value = modal.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "border", Value = border });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "constrain", Value = constrain.ToString().ToLower() });
            if (!string.IsNullOrEmpty(iconCls))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "iconCls", Value = iconCls });
            }
            if (!string.IsNullOrEmpty(cls))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "cls", Value = cls });
            }
            if (!string.IsNullOrEmpty(headerCls))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "headerCls", Value = headerCls });
            }
            if (!string.IsNullOrEmpty(bodyCls))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "bodyCls", Value = bodyCls });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "noheader", Value = noheader.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "collapsed", Value = collapsed.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "minimized", Value = minimized.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "maximized", Value = maximized.ToString().ToLower() });
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
                    case "width": width = p.Value; break;
                    case "height": height = p.Value; break;
                    case "collapsible": collapsible = p.Value == "true"; break;
                    case "minimizable": minimizable = p.Value == "true"; break;
                    case "maximizable": maximizable = p.Value == "true"; break;
                    case "closable": closable = p.Value == "true"; break;
                    case "closed": closed = p.Value == "true"; break;
                    case "zIndex": zIndex = int.Parse(p.Value); break;
                    case "draggable": draggable = p.Value == "true"; break;
                    case "resizable": resizable = p.Value == "true"; break;
                    case "shadow": shadow = p.Value == "true"; break;
                    case "inline": inline = p.Value == "true"; break;
                    case "modal": modal = p.Value == "true"; break;
                    case "border": border = p.Value; break;
                    case "constrain": constrain = p.Value == "true"; break;
                    case "iconCls": iconCls = p.Value; break;
                    case "cls": cls = p.Value; break;
                    case "headerCls": headerCls = p.Value; break;
                    case "bodyCls": bodyCls = p.Value; break;
                    case "noheader": noheader = p.Value == "true"; break;
                    case "collapsed": collapsed = p.Value == "true"; break;
                    case "minimized": minimized = p.Value == "true"; break;
                    case "maximized": maximized = p.Value == "true"; break;
                    case "thFragment": thFragment = p.Value; break;
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
