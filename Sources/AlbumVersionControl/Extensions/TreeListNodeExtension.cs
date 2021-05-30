using System.Collections.Generic;
using AlbumVersionControl.Models;
using DevExpress.Utils;
using DevExpress.Xpf.Grid;

namespace AlbumVersionControl.Extensions
{
    public static class TreeListNodeExtension
    {
        public static void MapFromPorjectVersionContent(this TreeListNode treeListNode, List<ProjectVersionContent> contents)
        {
            foreach (var content in contents)
            {
                var newTreeListNode = new TreeListNode(content)
                {
                    IsExpandButtonVisible = string.IsNullOrWhiteSpace(content.Extension)
                        ? DefaultBoolean.True
                        : DefaultBoolean.False
                };
                treeListNode.Nodes.Add(newTreeListNode);
                if (content.InnerContents != null && content.InnerContents.Count > 0)
                {
                    newTreeListNode.MapFromPorjectVersionContent(content.InnerContents);
                }
            }
        }
    }
}