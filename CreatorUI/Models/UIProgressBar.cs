using CreatorUI.JsonObjs;
using CreatorUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorUI.Models
{
    public class UIProgressBar : UIObject
    {
        public string width { get; set; }
        public int height { get; set; }
        public int value { get; set; }
        public string text { get; set; }

        public UIProgressBar()
        {
            EditorId = "UIProgressBar";
            Info = "UIProgressBar: ";
            width = "";
            height = 22;
            value = 0;
            text = "{value}%";
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"text:'{text}'");
            if (!string.IsNullOrEmpty(width))
            {
                sb.Append($",width:'{width}'");
            }
            sb.Append($",height:{height}");
            sb.Append($",value:{value}");
            sb.Append($"\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType == GenHTMLType.Editor)
            {
                sb.Append($"<div Id=\"{EditorId}\" class=\"easyui-progressbar\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\" ");
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
                        sb.Append($" id=\"{id}\" ");
                    }
                }
                sb.Append($" class=\"easyui-progressbar\" ");
            }
            sb.Append(GetDataOptions());
            sb.Append("></div>\r\n");
            foreach (var obj in UIObjects)
            {
                sb.AppendLine(obj.GetHTMLObject(GenType, modalSupport));
            }
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UIProgressBar";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "width", Value = width });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "height", Value = height.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "value", Value = value.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "text", Value = text });
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
                    case "height": height = int.Parse(p.Value); break;
                    case "value": value = int.Parse(p.Value); break;
                    case "text": text = p.Value; break;
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
