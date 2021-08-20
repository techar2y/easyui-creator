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
    public enum GenHTMLType { Editor, Template }
    [DataContract]
    public class UIObject
    {
        [DisplayName("Id")]
        [Description("Уникалный идентификатор копонента")]
        [DataMember]
        public string id { get; set; }
        [Browsable(false)]
        [Description("Уникалный идентификатор копонента (в редакторе)")]
        [DataMember]
        public string EditorId { get; set; }
        [DisplayName("Info")]
        [Description("Описание элемента формы")]
        [DataMember]
        public string Info { get; set; }

        [DataMember(Name = "UIObjects")]
        public List<UIObject> UIObjects = new List<UIObject>();
        [IgnoreDataMember]
        [Browsable(false)]
        public UIObject Owner { get; set; }
        [IgnoreDataMember]
        [Browsable(false)]
        public TreeNode Node { get; set; }

        public virtual string GetHTMLObject(GenHTMLType GenType, bool modalSupport)
        {
            var sb = new StringBuilder();
            foreach(var uiObj in UIObjects)
            {
                sb.AppendLine(uiObj.GetHTMLObject(GenType, modalSupport));
            }
            return sb.ToString();
        }
        public void DeleteObjectFromIdService(IdService sId)
        {
            foreach(var uiObj in UIObjects)
            {
                uiObj.DeleteObjectFromIdService(sId);
                sId.DeleteId(uiObj.EditorId);
            }
            sId.DeleteId(EditorId);
        }
        public virtual JsonUIObject GetJsonObject()
        {
            var obj = new JsonUIObject();
            obj.id = id;
            obj.EditorId = EditorId;
            obj.Info = Info;
            foreach(var uiobj in UIObjects)
            {
                obj.UIObjects.Add(uiobj.GetJsonObject());
            }
            return obj;
        }
        public virtual void SetJsonObject(JsonUIObject json, Dictionary<string, Type> UIObjects, TreeNode ParentNode, IdService sId)
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
            foreach (var uiObj in json.UIObjects)
            {
                if (!UIObjects.ContainsKey(uiObj.UIComponentName)) { continue; }
                var type = UIObjects[uiObj.UIComponentName];
                var obj = (UIObject)Activator.CreateInstance(type);
                obj.SetJsonObject(uiObj, UIObjects, node, sId);
                this.UIObjects.Add(obj);
            }
        }
        public override string ToString()
        {
            return $"Info= {Info};";
        }
    }
}
