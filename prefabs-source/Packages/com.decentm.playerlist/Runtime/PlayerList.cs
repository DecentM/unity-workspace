using UnityEngine;

using DecentM.Collections;
using DecentM.Pubsub;

namespace DecentM.PlayerList
{
    public enum PlayerListEvent
    {
        PlayerAdded,
        PlayerRemoved,
    }

    public class PlayerList : PubsubHost
    {
        [Header("Settings")]
        [Tooltip("A list of player names")]
        [SerializeField] private List/*<string>*/ players;

        public bool CheckPlayer(string id)
        {
            return this.players.Contains(id);
        }

        public bool AddPlayer(string id)
        {
            if (this.players.Contains(id))
                return false;

            bool added = this.players.Add(id);

            if (added)
                this.BroadcastEvent(PlayerListEvent.PlayerAdded, id);

            return added;
        }

        public bool RemovePlayer(string id)
        {
            if (!this.players.Contains(id))
                return false;

            bool removed = this.players.Remove(id);

            if (removed)
                this.BroadcastEvent(PlayerListEvent.PlayerRemoved, id);

            return removed;
        }
    }
}
