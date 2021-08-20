using System;
using System.Windows.Forms;
using CefSharp.WinForms;
using CreatorUI.JsonObjs;
using CreatorUI.Models;
using CreatorUI.Services;

namespace CreatorUI.Command
{
    class AddCommand : ICommand
    {
        private TreeNode _node;
        private TreeNode _parent;
        private TreeView _tvComponents;
        private UIObject _obj;
        private IdService _sId;
        private CRUDService crud = new CRUDService();

        public AddCommand(TreeNode node, TreeNode parent, TreeView tvComponents, IdService sId, UIObject obj)
        {
            this._node = node;
            this._parent = parent;
            this._tvComponents = tvComponents;
            this._sId = sId;
            this._obj = obj;
        }

        public void Redo()
        {
            UIObject obj = (UIObject)_node.Tag;

            crud.AddComponent(_node, _parent, _obj, _tvComponents);
        }

        public void Undo()
        {
            crud.DeleteComponent(_node, _parent, _sId);
        }
    }
}
