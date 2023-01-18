﻿using UnityEngine;

namespace DecentM.UI
{
    public abstract class VirtualScrollItem : MonoBehaviour
    {
        public RectTransform rectTransform;

        internal int index = -1;
        internal object data = null;

        public void SetData(object data)
        {
            this.data = data;
            this.OnDataChange();
        }

        protected virtual void OnDataChange() { }
    }
}
