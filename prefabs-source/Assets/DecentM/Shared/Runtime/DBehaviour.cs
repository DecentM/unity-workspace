﻿using UnityEngine;

#if COMPILER_UDONSHARP
using UdonSharp;
#endif

namespace DecentM.Shared
{
    public class DBehaviour :
#if COMPILER_UDONSHARP
        UdonSharpBehaviour
#else
        MonoBehaviour
#endif
    {
        public void Invoke(string methodName, int delaySeconds)
        {
#if COMPILER_UDONSHARP
            this.SendCustomEventDelayedSeconds(methodName, delaySeconds);
#else
            base.Invoke(methodName, delaySeconds);
#endif
        }
    }
}
