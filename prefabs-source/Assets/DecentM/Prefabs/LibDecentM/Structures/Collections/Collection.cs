﻿using UdonSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecentM.Collections
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class Collection : UdonSharpBehaviour
    {
        protected object[] value = new object[0];

        public bool Contains(object item)
        {
            bool result = false;

            foreach (object valueItem in this.value)
            {
                if (valueItem == item)
                    return true;
            }

            return result;
        }

        public int Count
        {
            get { return this.value.Length; }
        }

        public object[] ToArray()
        {
            return this.value;
        }

        public void FromArray(object[] newValue)
        {
            this.value = newValue;
        }

        public void Clear()
        {
            this.value = new object[0];
        }
    }
}
