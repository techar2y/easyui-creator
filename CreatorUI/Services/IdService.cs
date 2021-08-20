using CreatorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI.Services
{
    public class IdService
    {
        public Dictionary<string, UIObject> objects = new Dictionary<string, UIObject>();

        public string GetId(string DefaultId)
        {
            int i = 0;
            while (true)
            {
                var NewId = DefaultId + i;
                if (!objects.ContainsKey(NewId))
                {
                    return NewId;
                }
                i++;
            }
        }
        public void AddUIObject(UIObject obj)
        {
            if (!string.IsNullOrEmpty(obj.EditorId))
            {
                obj.EditorId = System.Text.RegularExpressions.Regex.Replace(obj.EditorId, @"[\d-]", string.Empty);
            }
            obj.EditorId = GetId(obj.EditorId);
            objects.Add(obj.EditorId, obj);
        }
        public void AddUIObjectProject(UIObject obj)
        {
            objects.Add(obj.EditorId, obj);
        }
        public string ChnageId(string OldId, string NewId)
        {
            if (OldId == NewId) { return ""; }
            var obj = objects[OldId];
            if (objects.ContainsKey(NewId))
            {
                return $"Компонент формаы с Id = {NewId} уже есть на форме";
            }
            objects.Add(NewId, obj);
            objects.Remove(OldId);
            return "";
        }
        public void DeleteId(string id)
        {
            objects.Remove(id);
        }
    }
}
