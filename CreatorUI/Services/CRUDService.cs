using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CreatorUI.Models;

namespace CreatorUI.Services
{
    public class CRUDService
    {
        public void DeleteComponent(TreeNode node, TreeNode parent, IdService sId)
        {
            if (node == null || node?.Name == "tvRoot")
            {
                MessageBox.Show("Выберите компонент", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ((UIObject)node.Tag).DeleteObjectFromIdService(sId);
                if (parent.Tag != null)
                {
                    ((UIObject)parent.Tag).UIObjects.Remove(((UIObject)node.Tag));
                }
                parent.Nodes.Remove(node);
            }
        }

        public bool HasNode(TreeNode targetNode, TreeNode parent)
        {
            if (parent?.Name == "tvRoot")
                if (parent.Nodes.Contains(targetNode))
                    return true;

            foreach(TreeNode n in parent.Nodes)
            {
                if (n.Nodes.Contains(targetNode))
                    return true;
                else
                {
                    if (HasNode(targetNode, n))
                        return true;
                }
            }

            return false;
        }

        public void AddComponent(TreeNode node, TreeNode nodeParent, UIObject obj, TreeView tvComponents)
        {
            if (HasNode(node, tvComponents.Nodes[0]))
                return;

            if (nodeParent == null || nodeParent?.Name == "tvRoot")
            {
                tvComponents.Nodes[0].Nodes.Add(node);
            }
            else
            {

                obj.Owner = (UIObject)nodeParent.Tag;
                ((UIObject)nodeParent.Tag).UIObjects.Add(obj);
                nodeParent.Nodes.Add(node);
            }
            tvComponents.SelectedNode = node;
        }


        public void MoveComponent(TreeNode newParentNode, TreeNode oldParentNode, UIObject obj, TreeView tvComponents)
        {
            if (obj == null)
                return;

            if (newParentNode == null)
                return;

            obj.Owner = (UIObject)newParentNode.Tag;

            if (newParentNode.Name == "tvRoot")
            {
                oldParentNode.Nodes.Remove(obj.Node);
                tvComponents.Nodes[0].Nodes.Add(obj.Node);
                ((UIObject)oldParentNode.Tag).UIObjects.Remove(obj);
            }
            else
            {
                ((UIObject)newParentNode.Tag).UIObjects.Add(obj);
                oldParentNode.Nodes.Remove(obj.Node);
                newParentNode.Nodes.Add(obj.Node);
                if (oldParentNode.Name != "tvRoot")
                    ((UIObject)oldParentNode.Tag).UIObjects.Remove(obj);
            }

            tvComponents.SelectedNode = obj.Node;
        }
    }
}
