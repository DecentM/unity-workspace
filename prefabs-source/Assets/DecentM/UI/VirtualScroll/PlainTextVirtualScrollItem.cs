using UnityEngine;
using TMPro;

namespace DecentM.UI.Examples
{
    public class PlainTextVirtualScrollItem : VirtualScrollItem
    {
        [SerializeField] private TextMeshProUGUI slot;

        private string GetText()
        {
            if (this.data == null) return string.Empty;
            string text = this.data.ToString();
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            return text;
        }

        protected override void OnDataChange()
        {
            this.slot.text = this.GetText();
        }
    }
}
