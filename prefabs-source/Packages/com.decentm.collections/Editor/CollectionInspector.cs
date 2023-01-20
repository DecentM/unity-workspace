using UnityEditor;
using UnityEngine;
using DecentM.Shared.Editor;

namespace DecentM.Collections.Editor
{
    public abstract class CollectionInspector : Inspector
    {
        private const float RowHeight = 20;

        public override void OnInspectorGUI()
        {
            Collection target = (Collection)this.target;
            object[] data = target.ToArray();

            this.HelpBox(MessageType.Info, $"Total: {target.Count}");

            Rect region = this.DrawRegion(data.Length * RowHeight);

            int i = 0;
            foreach (object item in data)
            {
                if (item == null)
                    continue;

                Rect row = this.GetRectInside(region, new Vector2(region.width, RowHeight), new Vector4(0, i * RowHeight, 0, 0));

                EditorGUI.DrawRect(row, new Color(.3f, .3f, .3f));

                this.DrawRow(row, i, item);
                i++;
            }
        }

        protected abstract void DrawRow(Rect rect, int index, object item);

        protected void DrawItem(Rect rect, object item)
        {
            GUIStyle contentStyle = EditorStyles.label;
            contentStyle.alignment = TextAnchor.MiddleLeft;

            if (item is Component)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.ObjectField(rect, (Component)item, typeof(Component), false);
                EditorGUI.EndDisabledGroup();
                return;
            }

            this.DrawLabel(rect, $"{item}", 2, contentStyle);
        }
    }
}