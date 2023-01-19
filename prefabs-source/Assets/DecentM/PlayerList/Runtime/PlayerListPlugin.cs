using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DecentM.Pubsub;

namespace DecentM.PlayerList
{
    public class PlayerListPlugin : PubsubSubscriber
    {
        protected virtual void OnPlayerAdded(string id) { }

        protected virtual void OnPlayerRemoved(string id) { }

        public override void OnPubsubEvent(object name, object[] data)
        {
            switch (name)
            {
                case PlayerListEvent.PlayerAdded:
                    {
                        string id = (string)data[0];
                        this.OnPlayerAdded(id);
                        break;
                    }

                case PlayerListEvent.PlayerRemoved:
                    {
                        string id = (string)data[0];
                        this.OnPlayerRemoved(id);
                        break;
                    }
            }
        }
    }
}
