using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using CloneEscape.PlayerBehavours;

namespace CloneEscape.LevelBase
{
    public class PlayerSelector
    {
        private static PlayerSelector instance;
        public static PlayerSelector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerSelector();
                }

                return instance;
            }
        }

        [HideInInspector]
        public UnityEvent<SelectablePlayer> OnNewPlayerSelected;

        private readonly List<SelectablePlayer> players;
        private int activePlayerID;

        private PlayerSelector()
        {
            players = new();
            OnNewPlayerSelected = new();
        }

        public int AddPlayer(SelectablePlayer newPlayer)
        {
            players.Add(newPlayer);

            int newID = players.Count - 1;

            if (newPlayer.IsActive())
            {
                activePlayerID = newID;
                OnNewPlayerSelected.Invoke(newPlayer);
            }

            return newID;
        }

        public void SetActivePlayer(int playerID)
        {
            if (players[activePlayerID])
            {
                players[activePlayerID].SetPlayerActivity(false);
                players[playerID].SetPlayerActivity(true);

                OnNewPlayerSelected.Invoke(players[playerID]);
                activePlayerID = playerID;
            }
        }

        public void RemovePlayer(SelectablePlayer newPlayer) => players.Remove(newPlayer);

        public void TrySetNearestPlayer()
        {
            if (!SetNearestPlayer())
            {
                LevelController.Instance.OnPlayerLose?.Invoke();
            }
        }

        private bool SetNearestPlayer()
        {
            float minDistance = Mathf.Infinity;
            float newDistance;
            int nearestID = activePlayerID;

            Vector2 activePlayerPos = players[activePlayerID].transform.position;
            Vector2 newPlayerPos;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == null || i == activePlayerID) continue;

                newPlayerPos = players[i].transform.position;

                newDistance = Vector2.Distance(activePlayerPos, newPlayerPos);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    nearestID = i;
                }
            }

            if (nearestID != activePlayerID)
            {
                SetActivePlayer(nearestID);

                return true;
            }

            return false;
        }
    }
}