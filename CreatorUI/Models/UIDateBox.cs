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
    public class UIDateBox : UIObject
    {
        [Description("The drop down calendar panel width.")]
        public int panelWidth { get; set; }
        [Description("The text to display for the current day button.")]
        public string currentText { get; set; }
        [Description("The text to display for the close button.")]
        public string closeText { get; set; }
        [Description("	The text to display for the ok button, reserved for datetimebox plugin.")]
        public string okText { get; set; }
        [Description("When true to disable the field.")]
        public bool disabled { get; set; }
        public UIDateBox()
        {
            EditorId = "UIDateBox";
            Info = "UIDateBox: ";
            panelWidth = 250;
            currentText = "Сегодня";
            closeText = "Закрыть";
            okText = "Ок";
            disabled = false;
        }
        private string GetDataOptions()
        {
            var sb = new StringBuilder();
            sb.Append("data-options=\"");
            sb.Append($"panelWidth:{panelWidth}");
            sb.Append($",currentText:'{currentText}'");
            sb.Append($",closeText:'{closeText}'");
            sb.Append($",okText:'{okText}'");
            sb.Append($",disabled:{disabled.ToString().ToLower()}");
            sb.Append("\"");
            return sb.ToString();
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if(GenType==GenHTMLType.Editor)
            {
                sb.Append($"<div style=\"display:inline-block;\" id=\"{EditorId}\" onClick=\"cefAPI.selUIObject('{EditorId}', false); event.stopPropagation();\"><input type=\"text\" class=\"easyui-datebox\" ");
            }
            else
            {
                sb.Append($"<input");
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
                sb.Append($" type=\"text\" class=\"easyui-datebox\" ");
            }
            sb.Append(GetDataOptions());
            sb.Append("/>\r\n");
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($"</div>");
            }
            foreach (var obj in UIObjects)
            {
                sb.Append(obj.GetHTMLObject(GenType, modalSupport));
            }
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UIDateBox";

            jsonObj.UIParams.Add(new JsonUIParam { Name = "panelWidth", Value = panelWidth.ToString() });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "currentText", Value = currentText });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "closeText", Value = closeText });
            jsonObj.UIParams.Add(new JsonUIParam { Name = "okText", Value = okText });
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
                    case "panelWidth": panelWidth = int.Parse(p.Value); break;
                    case "currentText": currentText = p.Value; break;
                    case "closeText": closeText = p.Value; break;
                    case "okText": okText = p.Value; break;
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
