using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI.JsonObjs
{
    [DataContract]
    public class JsonUIObject
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string EditorId { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public string UIComponentName { get; set; }
        [DataMember(Name = "UIParams")]
        public List<JsonUIParam> UIParams { get; set; }
        [DataMember(Name = "UIObjects")]
        public List<JsonUIObject> UIObjects { get; set; }

        public JsonUIObject()
        {
            UIParams = new List<JsonUIParam>();
            UIObjects = new List<JsonUIObject>();
        }
    }
}
