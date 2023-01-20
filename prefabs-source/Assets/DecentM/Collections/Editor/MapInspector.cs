using UnityEditor;
using UnityEngine;

namespace DecentM.Collections.Editor
{
    [CustomEditor(typeof(Map))]
    public class MapInspector : CollectionInspector
    {
        protected override void DrawRow(Rect row, int index, object itemRaw)
        {
            object[] item = (object[])itemRaw;

            GUIStyle style = EditorStyles.label;
            style.alignment = TextAnchor.MiddleCenter;

            Rect indexRect = this.GetRectInside(row, new Vector2(row.width / 2, row.height));
            Rect contentRect = this.GetRectInside(row, new Vector2(row.width / 2, row.height), new Vector4(row.width / 2, 0, 0, 0));

            this.DrawItem(indexRect, item[0]);
            this.DrawItem(contentRect, item[1]);
        }
    }
}
