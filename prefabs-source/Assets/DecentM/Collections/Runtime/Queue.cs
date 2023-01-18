using JetBrains.Annotations;

namespace DecentM.Collections
{
    public class Queue : Collection
    {
        [PublicAPI]
        public void Enqueue(object item)
        {
            this.value = this.Add(this.value, item);
        }

        [PublicAPI]
        public bool EnqueueFirst(object item)
        {
            int length = this.value.Length;
            this.value = this.Insert(this.value, 0, item);

            return length + 1 == this.value.Length;
        }

        [PublicAPI]
        public object Peek()
        {
            return this.ElementAt(this.value, 0);
        }

        [PublicAPI]
        public object Dequeue()
        {
            object item = this.Peek();

            if (item == null)
                return null;

            this.value = this.RemoveAt(this.value, 0);

            return item;
        }
    }
}
