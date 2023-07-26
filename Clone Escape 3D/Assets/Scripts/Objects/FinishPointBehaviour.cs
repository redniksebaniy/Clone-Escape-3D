using UnityEngine;
using DG.Tweening;
using CloneEscape.PlayerBehavours;
using CloneEscape.Audio;
using CloneEscape.LevelBase;

namespace CloneEscape.Objects
{
    [RequireComponent(typeof(Collider))]
    public class FinishPointBehaviour : MonoBehaviour
    {
        [SerializeField]
        private uint neededPlayerCount = 1;

        private uint currentPlayerCount = 0;


        [SerializeField]
        private uint flySpeed = 15;

        [SerializeField]
        private uint flyDistance = 25;

        [SerializeField]
        private AudioClip _flyClip;

        private void Start()
        {
            LevelController.Instance.OnPlayerWin.AddListener(FlyAway);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject gameObject = other.gameObject;
            if (gameObject.TryGetComponent(typeof(SelectablePlayer), out _))
            {
                if (++currentPlayerCount == neededPlayerCount)
                {
                    LevelController.Instance.OnPlayerWin?.Invoke();
                }
                else
                {
                    PlayerSelector.Instance.TrySetNearestPlayer();
                }

                Destroy(gameObject);
            }
        }

        private void FlyAway()
        {
            Vector3 newPosition = transform.position + transform.forward * flyDistance;
            float flyTime = (float)flyDistance / flySpeed;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            transform.DOMove(newPosition, flyTime).SetEase(Ease.InBack);
            AudioManager.Instance.AudioSource.PlayOneShot(_flyClip);
        }

        private void OnDestroy() => transform.DOKill();
    }
}