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
    public class UIDataGridHead : UIObject
    {
        public UIDataGridHead()
        {
            EditorId = "UIDataGridHead";
            Info = "UIDataGridHead: ";
        }
        public override string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            if (GenType == GenHTMLType.Editor)
            {
                sb.Append($"<thead id=\"{EditorId}\">");
            }
            else
            {
                sb.Append($"<thead ");
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
                sb.Append(">\r\n");
            }
            sb.Append("<tr>");
            foreach (var obj in UIObjects)
            {
                sb.Append(obj.GetHTMLObject(GenType, modalSupport));
            }
            sb.Append("</tr>");
            sb.Append("</thead>\r\n");
            return sb.ToString();
        }
        public override JsonUIObject GetJsonObject()
        {
            var jsonObj = new JsonUIObject();
            jsonObj.id = id;
            jsonObj.EditorId = EditorId;
            jsonObj.Info = Info;
            jsonObj.UIComponentName = "UIDataGridHead";

            //jsonObj.UIParams.Add(new JsonUIParam { Name = "Width", Value = Width });
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
            /*foreach (var p in json.UIParams)
            {
                switch (p.Name)
                {
                    //case "Width": Width = p.Value; break;
                    //case "Height": Height = p.Value; break;
                    //case "fit": fit = p.Value == "true"; break;
                }
            }*/
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
