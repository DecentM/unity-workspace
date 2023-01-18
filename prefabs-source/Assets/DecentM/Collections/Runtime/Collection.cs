using UnityEngine;
using System;
using JetBrains.Annotations;

using DecentM.Shared;

namespace DecentM.Collections
{
    public abstract class Collection : DBehaviour
    {
        // Returns true if the specified range is completely within the array's
        // boundaries. Returns false otherwise.
        private bool RangeValidForRemoval(object[] array, int start, int end)
        {
            return
                // Start must be non-negative,
                start >= 0
                // and before the end of the array.
                && start <= array.Length
                // End must not be less than start (also guarantees non-negative),
                && end >= start
                // and before the end of the array.
                // End may be the same as start.
                && end < array.Length;
        }

        private bool RangeValidForInsertion(object[] array, int start, int end)
        {
            return
                // Start must be non-negative,
                start >= 0
                // and before the end of the array.
                && start <= array.Length
                // End must not be less than start (also guarantees non-negative),
                && end >= start;
        }

        protected object[] value = new object[0];

        protected bool Contains(object[] array, object item)
        {
            return this.IndexOf(array, item) >= 0;
        }

        protected int IndexOf(object[] array, object item)
        {
            if (array == null)
                return -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (Equals(array[i], item))
                    return i;
            }

            return -1;
        }

        protected object ElementAt(object[] array, int index)
        {
            if (array == null || index < 0 || index >= array.Length)
                return null;

            return array[index];
        }

        protected object[] RemoveAt(object[] array, int index)
        {
            // RemoveAt is just a RemoveRange that only removes a single item
            return this.RemoveRange(array, index, index);
        }

        protected object[] RemoveRange(object[] array, int startIndex, int endIndex)
        {
            if (array == null || !this.RangeValidForRemoval(array, startIndex, endIndex))
                return array;

            // Allocate an array that's the size of the original, minus the amount
            // of items we want to remove
            object[] tmp = new object[array.Length - (endIndex - startIndex + 1)];

            // Copy all items except the range we want to remove
            Array.Copy(array, 0, tmp, 0, startIndex);
            Array.Copy(array, endIndex + 1, tmp, startIndex, array.Length - endIndex - 1);

            return tmp;
        }

        protected object[] Add(object[] array, object item)
        {
            // Add is just Insert that inserts to the end of the array
            return this.Insert(array, array.Length, item);
        }

        protected object[] Insert(object[] array, int index, object item)
        {
            // Insert is just InsertRange that only inserts one item
            return this.InsertRange(array, index, new object[] { item });
        }

        protected object[] InsertRange(object[] array, int index, object[] items)
        {
            if (array == null)
                return array;

            object[] tmp = new object[array.Length + items.Length];

            if (!this.RangeValidForInsertion(array, index, index + items.Length))
                return array;

            // Copy items until the index we want to insert at
            Array.Copy(array, 0, tmp, 0, index);
            // Copy the items we're inserting
            Array.Copy(items, 0, tmp, index, items.Length);
            // Copy the rest of the items from the source array
            Array.Copy(array, index, tmp, index + items.Length, array.Length - index);

            return tmp;
        }

        [PublicAPI]
        public virtual bool Contains(object item)
        {
            return this.Contains(this.value, item);
        }

        [PublicAPI]
        public virtual int Count => this.value.Length;

        [PublicAPI]
        public virtual bool IsEmpty => this.value.Length == 0;

        [PublicAPI]
        public virtual object[] ToArray()
        {
            return this.value;
        }

        [PublicAPI]
        public virtual void FromArray(object[] newValue)
        {
            this.value = newValue;
        }

        [PublicAPI]
        public void FromCollection(Collection collection)
        {
            this.FromArray(collection.ToArray());
        }

        [PublicAPI]
        public virtual void Clear()
        {
            this.value = new object[0];
        }
    }
}
