using UnityEngine;

namespace CloneEscape.PlayerBehavours
{
    public class PlayerBehaviour : MonoBehaviour { }

    [RequireComponent(typeof(MeshRenderer))]
    public class SelectablePlayer : MonoBehaviour
    {
        private int ID = -1;

        private bool isActive = false;

        private void OnBecameVisible()
        {
            if (ID == -1)
            {
                isActive = GetComponent<PlayerBehaviour>().enabled;
                ID = PlayerSelector.Instance.AddPlayer(this);
            }
        }

        private void OnMouseUp()
        {
            PlayerSelector.Instance.SetActivePlayer(ID);
            isActive = true;
        }

        public void SetPlayerActivity(bool newState)
        {
            PlayerBehaviour[] behaviours = GetComponents<PlayerBehaviour>();

            foreach (var b in behaviours)
            {
                b.enabled = newState;
            }

            isActive = newState;
        }

        public bool IsActive() => isActive;

        private void OnDestroy() => PlayerSelector.Instance.RemovePlayer(this);
    }
}
