using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace DecentM.UI
{ 
    public class VirtualScroll<Item> : MonoBehaviour
    {
        [SerializeField] private RectTransform window;
        [SerializeField] private VirtualScrollItem itemTemplate;
        [SerializeField] private Scrollbar verticalScrollbar;

        private List<VirtualScrollItem> items;

        [SerializeField] private float windowHeight = 1000;
        [SerializeField] private float itemHeight = 100;

        private int itemsPerScreen
        {
            get { return Mathf.CeilToInt(this.windowHeight / this.itemHeight); }
        }

        private void UpdateWindowSize()
        {
            this.window.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Max(this.itemHeight * this.data.Count(), this.itemHeight * this.itemsPerScreen));

            foreach (VirtualScrollItem item in this.items)
            {
                Destroy(item.gameObject);
            }

            this.items.Clear();

            // Create enough items to fit the screen, plus a buffer on the top and bottom to prevent odd flickering
            for (int i = 0; i < itemsPerScreen; i++)
            {
                GameObject item = Instantiate(this.itemTemplate.gameObject, this.window);
                VirtualScrollItem vsItem = item.GetComponent<VirtualScrollItem>();

                if (vsItem == null)
                {
                    Debug.LogError("[DecentM.VirtualScroll] Item instantiation failed, no component of base class VirtualScrollItem was found on the instance.");
                    Destroy(item);
                    return;
                }

                item.name = $"Item_{i}";
                item.transform.SetParent(this.itemTemplate.transform.parent, true);
                item.transform.localPosition = Vector3.zero;
                item.SetActive(true);
                this.items.Add(vsItem);
            }

            this.verticalScrollbar.value = 1;
        }

        private void Awake()
        {
            this.items = new List<VirtualScrollItem>();
            this._data = new List<Item>();
        }

        private void Start()
        {
            this.itemTemplate.gameObject.SetActive(false);

            /*
            this.data.FromArray(new object[]
            {
                "a",
                "ability",
                "able",
                "about",
                "above",
                "accept",
                "according",
                "account",
                "across",
                "act",
                "action",
                "activity",
                "actually",
                "add",
                "address",
                "administration",
                "admit",
                "adult",
                "affect",
                "after",
                "again",
                "against",
                "age",
                "agency",
                "agent",
                "ago",
                "agree",
                "agreement",
                "ahead",
                "air",
                "all",
                "allow",
                "almost",
                "alone",
                "along",
                "already",
                "also",
                "although",
                "always",
                "American",
                "among",
                "amount",
                "analysis",
                "and",
                "animal",
                "another",
                "answer",
                "any",
                "anyone",
                "anything",
                "appear",
                "apply",
                "approach",
                "area",
                "argue",
                "arm",
                "around",
                "arrive",
                "art",
                "article",
                "artist",
                "as",
                "ask",
                "assume",
                "at",
                "attack",
                "attention",
                "attorney",
                "audience",
                "author",
                "authority",
                "available",
                "avoid",
                "away",
                "baby",
                "back",
                "bad",
                "bag",
                "ball",
                "bank",
                "bar",
                "base",
                "be",
                "beat",
                "beautiful",
                "because",
                "become",
                "bed",
                "before",
                "begin",
                "behavior",
                "behind",
                "believe",
                "benefit",
                "best",
                "better",
                "between",
                "beyond",
                "big",
                "bill",
                "billion",
                "bit",
                "black",
                "blood",
                "blue",
                "board",
                "body",
                "book",
                "born",
                "both",
                "box",
                "boy",
                "break",
                "bring",
                "brother",
                "budget",
                "build",
                "building",
                "business",
                "but",
                "buy",
                "by",
                "call",
                "camera",
                "campaign",
                "can",
                "cancer",
                "candidate",
                "capital",
                "car",
                "card",
                "care",
                "career",
                "carry",
                "case",
                "catch",
                "cause",
                "cell",
                "center",
                "central",
                "century",
                "certain",
                "certainly",
                "chair",
                "challenge",
                "chance",
                "change",
                "character",
                "charge",
                "check",
                "child",
                "choice",
                "choose",
                "church",
                "citizen",
                "city",
                "civil",
                "claim",
                "class",
                "clear",
                "clearly",
                "close",
                "coach",
                "cold",
                "collection",
                "college",
                "color",
                "come",
                "commercial",
                "common",
                "community",
                "company",
                "compare",
                "computer",
                "concern",
                "condition",
                "conference",
                "Congress",
                "consider",
                "consumer",
                "contain",
                "continue",
                "control",
                "cost",
                "could",
                "country",
                "couple",
                "course",
                "court",
                "cover",
                "create",
                "crime",
                "cultural",
                "culture",
                "cup",
                "current",
                "customer",
                "cut",
                "dark",
                "data",
                "daughter",
                "day",
                "dead",
                "deal",
                "death",
                "debate",
                "decade",
                "decide",
                "decision",
                "deep",
                "defense",
                "degree",
                "Democrat",
                "democratic",
                "describe",
                "design",
                "despite",
                "detail",
                "determine",
                "develop",
                "development",
                "die",
                "difference",
                "different",
                "difficult",
                "dinner",
                "direction",
                "director",
                "discover",
                "discuss",
                "discussion",
                "disease",
                "do",
                "doctor",
                "dog",
                "door",
                "down",
                "draw",
                "dream",
                "drive",
                "drop",
                "drug",
                "during",
                "each",
                "early",
                "east",
                "easy",
                "eat",
                "economic",
                "economy",
                "edge",
                "education",
                "effect",
                "effort",
                "eight",
                "either",
                "election",
                "else",
                "employee",
                "end",
                "energy",
                "enjoy",
                "enough",
                "enter",
                "entire",
                "environment",
                "environmental",
                "especially",
                "establish",
                "even",
                "evening",
                "event",
                "ever",
                "every",
                "everybody",
                "everyone",
                "everything",
                "evidence",
                "exactly",
                "example",
                "executive",
                "exist",
                "expect",
                "experience",
                "expert",
                "explain",
                "eye",
                "face",
                "fact",
                "factor",
                "fail",
                "fall",
                "family",
                "far",
                "fast",
                "father",
                "fear",
                "federal",
                "feel",
                "feeling",
                "few",
                "field",
                "fight",
                "figure",
                "fill",
                "film",
                "final",
                "finally",
                "financial",
                "find",
                "fine",
                "finger",
                "finish",
                "fire",
                "firm",
                "first",
                "fish",
                "five",
                "floor",
                "fly",
                "focus",
                "follow",
                "food",
                "foot",
                "for",
                "force",
                "foreign",
                "forget",
                "form",
                "former",
                "forward",
            });
            */ 
            this.UpdateWindowSize();
        }

        private List<Item> _data;

        private List<Item> data
        {
            get { return this._data; }
            set { this._data = value.ToList(); this.UpdateWindowSize(); }
        }

        [PublicAPI]
        public void SetData(List<Item> data)
        {
            this.data = data;
        }

        private object[] GetVisibleDataRange()
        {
            object[] result = new object[this.itemsPerScreen];
            int startIndex = this.firstVisibleIndex;

            for (int i = startIndex; i < startIndex + this.itemsPerScreen; i++)
            {
                result[i - startIndex] = this.data.ElementAt(i);
            }

            return result;
        }

        private float GetHeightForIndex(int index)
        {
            return -1 * index * this.itemHeight + ((1 - this.verticalScrollbar.value) * (this.windowHeight - this.itemHeight)); //  + (this.contentHeight * this.verticalScrollbar.value);
        }

        private int GetIndexForHeight(float normalisedHeight)
        {
            int index = Mathf.FloorToInt(normalisedHeight * this.data.Count());

            return Mathf.Clamp(index, 0, this.data.Count() - 1);
        }

        private int firstVisibleIndex
        {
            get { return this.GetIndexForHeight(1 - this.verticalScrollbar.value); }
        }

        private float prevScroll = 0;

        private float GetScrollSpeed(float scroll)
        {
            float speed = scroll - this.prevScroll;
            this.prevScroll = scroll;

            return speed < 0 ? speed * -1 : speed;
        }

        [SerializeField] private Image placeholderBackground;

        private bool lastCheckPending = false;

        private void ScheduleLastCheck()
        {
            if (this.lastCheckPending) return;

            this.lastCheckPending = true;
            this.Invoke(nameof(this.LastCheck), 0.1f);
        }

        public void LastCheck()
        {
            this.lastCheckPending = false;
            this.ProcessScroll(false);
        }

        private void ProcessScroll(bool withCheck)
        {
            object[] dataRange = this.GetVisibleDataRange();
            float speed = this.GetScrollSpeed(this.verticalScrollbar.value * 100);

            if (withCheck) this.ScheduleLastCheck();

            if (speed > 0.05)
            {
                this.placeholderBackground.enabled = true;

                foreach (VirtualScrollItem item in this.items.ToArray())
                {
                    item.gameObject.SetActive(false);
                }

                return;
            }

            this.placeholderBackground.enabled = false;

            for (int i = 0; i < dataRange.Length; i++)
            {
                VirtualScrollItem item = this.items.ElementAt(i);
                if (item == null) continue;

                item.gameObject.SetActive(true);
                item.index = i;
                item.rectTransform.transform.localPosition = new Vector3(item.rectTransform.transform.localPosition.x, this.GetHeightForIndex(i + this.firstVisibleIndex), item.rectTransform.transform.localPosition.z);
                item.SetData(dataRange[i]);
            }
        }

        // Called from the viewport
        public void OnScroll()
        {
            this.ProcessScroll(true);
        }
    }
}
