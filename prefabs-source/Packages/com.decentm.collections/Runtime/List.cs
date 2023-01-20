using JetBrains.Annotations;

namespace DecentM.Collections
{
    public class List : Collection
    {
        [PublicAPI]
        public object ElementAt(int index)
        {
            return this.ElementAt(this.value, index);
        }

        [PublicAPI]
        public bool Add(object item)
        {
            if (this.Contains(item))
                return false;

            int length = this.value.Length;
            this.value = this.Add(this.value, item);

            return this.value.Length == length + 1;
        }

        [PublicAPI]
        public bool AddRange(object[] items)
        {
            return this.InsertRange(0, items);
        }

        [PublicAPI]
        public bool AddRange(Collection collection)
        {
            return this.AddRange(collection.ToArray());
        }

        [PublicAPI]
        public bool Insert(int index, object item)
        {
            if (this.Contains(item))
                return false;

            int length = this.value.Length;
            this.value = this.Insert(this.value, index, item);

            return this.value.Length == length + 1;
        }

        [PublicAPI]
        public bool InsertRange(int startIndex, object[] items)
        {
            int length = this.value.Length;
            this.value = this.InsertRange(this.value, startIndex, items);

            return this.value.Length == length + items.Length;
        }

        [PublicAPI]
        public bool InsertRange(int startIndex, Collection collection)
        {
            return this.InsertRange(startIndex, collection.ToArray());
        }

        [PublicAPI]
        public int IndexOf(object item)
        {
            return this.IndexOf(this.value, item);
        }

        [PublicAPI]
        public bool RemoveAt(int index)
        {
            int length = this.value.Length;
            this.value = this.RemoveAt(this.value, index);

            // Return success if the result array is one smaller than the previous value
            return this.value.Length == length - 1;
        }

        [PublicAPI]
        public bool Remove(object item)
        {
            int index = this.IndexOf(item);

            if (index == -1)
                return false;

            return this.RemoveAt(index);
        }

        [PublicAPI]
        public bool RemoveRange(int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
                return false;

            int length = this.value.Length;
            this.value = this.RemoveRange(this.value, startIndex, endIndex);

            return this.value.Length == length - (endIndex - startIndex);
        }

        [PublicAPI]
        public void Reverse()
        {
            object[] tmp = new object[this.value.Length];

            for (int i = 0; i < this.value.Length; i++)
            {
                tmp[i] = this.value[this.value.Length - 1 - i];
            }

            this.value = tmp;
        }
    }
}
