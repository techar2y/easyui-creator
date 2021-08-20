using System;
using System.Windows.Forms;
using CefSharp.WinForms;
using CreatorUI.JsonObjs;
using CreatorUI.Models;
using CreatorUI.Services;

namespace CreatorUI.Command
{
    class DeleteCommand : ICommand
    {
        private TreeNode _node;
        private TreeNode _parent;
        private TreeView _tvComponents;
        private IdService _sId;
        private CRUDService crud = new CRUDService();

        public DeleteCommand(TreeNode node, TreeView tvComponents, IdService sId)
        {
            this._node = node;
            this._parent = node.Parent;
            this._tvComponents = tvComponents;
            this._sId = sId;
        }

        public void Redo()
        {
            crud.DeleteComponent(_node, _parent, _sId);
        }

        public void Undo()
        {

            UIObject obj = (UIObject)_node.Tag;

            crud.AddComponent(_node, _parent, obj, _tvComponents);
        }
    }
}
