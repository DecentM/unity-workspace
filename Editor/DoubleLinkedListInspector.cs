using UnityEditor;
using UnityEngine;

namespace DecentM.Collections.Editor
{
    [CustomEditor(typeof(DoubleLinkedList))]
    public class DoubleLinkedListInspector : CollectionInspector
    {
        protected override void DrawRow(Rect row, int index, object item)
        {
            DoubleLinkedList target = (DoubleLinkedList)this.target;

            // CollectionInspector calls ToArray on the collection, but DoubleLinkedList
            // resolves its chain while doing that, so we need to recover the original index
            // out of channel.
            int rawIndex = target.IndexOf(item);
            int id = target.IdByIndex(rawIndex);
            int[] boundaries = target.Boundaries(id);

            GUIStyle style = EditorStyles.label;
            style.alignment = TextAnchor.MiddleCenter;

            Rect indexRect = this.GetRectInside(row, new Vector2(row.height, row.height));
            Rect prevRect = this.GetRectInside(row, new Vector2(row.height, row.height), new Vector4(row.height, 0, 0, 0));
            Rect contentRect = this.GetRectInside(row, new Vector2(row.width - row.height * 3, row.height), new Vector4(row.height * 2, 0, 0, 0));
            Rect nextRect = this.GetRectInside(row, new Vector2(row.height, row.height), new Vector4(row.height * 2 + contentRect.width, 0, 0, 0));

            this.DrawItem(indexRect, id);
            this.DrawItem(prevRect, boundaries[0]);
            this.DrawItem(contentRect, item);
            this.DrawItem(nextRect, boundaries[1]);
        }
    }
}
