using CreatorUI.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CreatorUI.JsonObjs;
using System.Windows.Forms;
using CreatorUI.Services;

namespace CreatorUI.Models
{
    [DataContract]

    public class UILayout : UIObject
    {
        [DataMember]
        public string Width { get; set; }
        [DataMember]
        public string Height { get; set; }
        [Description("Set to true to set the layout size fit its parent container. When creating layout on 'body' tag, it will be auto maximized to the full size of whole page.")]
        [DataMember]
        public bool fit { get; set; }
        [Description("Свойство th:fragment")]
        public string thFragment { get; set; }

        public UILayout()
        {
            EditorId = "UILayout";
            Info = "UILayout";
            Width = "100%";
            Height = "100%";
            fit = false;
            thFragment = "";
        }

        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"fit:");
            sb.Append(fit.ToString().ToLower());
            sb.Append("\" ");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($@"<div Id=""{EditorId}"" onClick=""cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();"" class=""easyui-layout"" style=""");
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
                if (!string.IsNullOrEmpty(thFragment))
                {
                    sb.Append(" th:fragment=\"" + thFragment + "\"");
                }
                sb.Append(@" class=""easyui-layout"" style=""");
            }
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
            sb.Append(@""" ");
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
            jsonObj.UIComponentName = "UILayout";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "Width", Value = Width });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "Height", Value = Height });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "fit", Value = fit.ToString().ToLower() });
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
                    case "Width": Width = p.Value; break;
                    case "Height": Height = p.Value; break;
                    case "fit": fit = p.Value == "true"; break;
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
