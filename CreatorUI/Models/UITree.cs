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
    public class UITree : UIObject
    {
        [Description("a URL to retrieve remote data.")]
        public string url { get; set; }
        [Description("The http method to retrieve data.")]
        public string method { get; set; }
        [Description("Defines if to show animation effect when node expand or collapse.")]
        public bool animate { get; set; }
        [Description("Defines if to show the checkbox before every node. If a function is specified, return true to show the checkbox.")]
        public bool checkbox { get; set; }
        [Description("Defines if to cascade check.")]
        public bool cascadeCheck { get; set; }
        [Description("Defines if to show the checkbox only before leaf node.")]
        public bool onlyLeafCheck { get; set; }
        [Description("Defines if to show lines between tree nodes.")]
        public bool lines { get; set; }

        [Description("Defines if to enable drag and drop.")]
        public bool dnd { get; set; }

        public UITree()
        {
            EditorId = "UITree";
            Info = "UITree: ";
            url = "";
            method = "post";
            animate = false;
            checkbox = false;
            cascadeCheck = true;
            onlyLeafCheck = false;
            lines = false;
            dnd = false;
        }
        public string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append(" data-options=\"");
            sb.Append($"lines:{lines.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(url))
            {
                sb.Append($",url:'{url}'");
            }
            sb.Append($",method:'{method}'");
            sb.Append($",animate:{animate.ToString().ToLower()}");
            sb.Append($",checkbox:{checkbox.ToString().ToLower()}");
            sb.Append($",cascadeCheck:{cascadeCheck.ToString().ToLower()}");
            sb.Append($",onlyLeafCheck:{onlyLeafCheck.ToString().ToLower()}");
            sb.Append($",dnd:{dnd.ToString().ToLower()}");
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($@"<ul Id=""{EditorId}"" onClick=""cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();""  class=""easyui-tree"" ");
            }
            else
            {
                sb.Append($@"<ul  ");
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
                sb.Append(@" class=""easyui-tree"" ");
            }
            sb.Append(GetDataOptions());
            sb.Append(">\r\n");
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append(@"    <li>
        <span>Folder</span>
        <ul>
            <li>
                <span>Sub Folder 1</span>
                <ul>
                    <li><span><a href=""#"">File 11</a></span></li>
                    <li><span>File 12</span></li>
                    <li><span>File 13</span></li>
                </ul>
            </li>
            <li><span>File 2</span></li>
            <li><span>File 3</span></li>
        </ul>
    </li>
    <li><span>File21</span></li>");
            }

            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</ul>\r\n");
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UITree";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "lines", Value = lines.ToString().ToLower() });
            if (!string.IsNullOrEmpty(url))
            {
                jsonObj.UIParams.Add(new JsonUIParam { Name = "url", Value = url });
            }
            jsonObj.UIParams.Add(new JsonUIParam { Name = "method", Value = method });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "animate", Value = animate.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "checkbox", Value = checkbox.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "cascadeCheck", Value = cascadeCheck.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "onlyLeafCheck", Value = onlyLeafCheck.ToString().ToLower() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "dnd", Value = dnd.ToString().ToLower() });
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
                    case "url": url = p.Value; break;
                    case "method": method = p.Value; break;
                    case "animate": animate = p.Value == "true"; break;
                    case "checkbox": checkbox = p.Value == "true"; break;
                    case "cascadeCheck": cascadeCheck = p.Value == "true"; break;
                    case "onlyLeafCheck": onlyLeafCheck = p.Value == "true"; break;
                    case "lines": lines = p.Value == "true"; break;
                    case "dnd": dnd = p.Value == "true"; break;
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
