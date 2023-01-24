using UnityEditor;
using UnityEngine;

namespace DecentM.Collections.Editor
{
    [CustomEditor(typeof(List))]
    public class ListInspector : CollectionInspector
    {
        protected override void DrawRow(Rect row, int index, object item)
        {
            GUIStyle style = EditorStyles.label;
            style.alignment = TextAnchor.MiddleCenter;

            Rect indexRect = this.GetRectInside(row, new Vector2(row.height, row.height));
            Rect contentRect = this.GetRectInside(row, new Vector2(row.width - row.height, row.height), new Vector4(row.height, 0, 0, 0));

            this.DrawItem(indexRect, index);
            this.DrawItem(contentRect, item);
        }
    }
}
