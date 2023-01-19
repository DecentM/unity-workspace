using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecentM.CustomComponents.VLCPlayer
{
    public class VLCPlayerDemo : MonoBehaviour
    {
        public VLCPlayer player;

        public string url = "";

        // Start is called before the first frame update
        void Start()
        {
            if (this.player == null)
                return;

            this.player.Open(this.url);
            this.player.Play();
        }
    }
}
