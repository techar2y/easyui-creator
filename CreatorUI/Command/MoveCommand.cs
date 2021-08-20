using System;
using System.Windows.Forms;
using CefSharp.WinForms;
using CreatorUI.JsonObjs;
using CreatorUI.Models;
using CreatorUI.Services;

namespace CreatorUI.Command
{
    class MoveCommand : ICommand
    {
        private TreeNode _newNodeParent;
        private TreeNode _oldNodeParent;
        private UIObject _obj;
        private TreeView _tvComponents;
        private CRUDService crud = new CRUDService();

        public MoveCommand(TreeNode newNodeParent, TreeNode oldNodeParent, UIObject obj, TreeView tvComponents)
        {
            this._newNodeParent = newNodeParent;
            this._oldNodeParent = oldNodeParent;
            this._obj = obj;
            this._tvComponents = tvComponents;
        }

        public void Redo()
        {
            crud.MoveComponent(_newNodeParent, _oldNodeParent, _obj, _tvComponents);
        }

        public void Undo()
        {
            crud.MoveComponent(_oldNodeParent, _newNodeParent, _obj, _tvComponents);
        }
    }
}
