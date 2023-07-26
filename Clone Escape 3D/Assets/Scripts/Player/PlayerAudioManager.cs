using UnityEngine;
using CloneEscape.Audio;

namespace CloneEscape.PlayerBehavours
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioManager : PlayerBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _stepClip;

        private bool isPushing;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _stepClip;
            _audioSource.loop = true;

            isPushing = false;
        }

        private void Update()
        {
            if (AudioManager.Instance.IsMuted || Time.timeScale == 0f) return;

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            float magnitude = new Vector2(x, z).magnitude;

            if (!_audioSource.isPlaying && magnitude > 0 && !isPushing)
            {
                _audioSource.pitch = Random.Range(0.95f, 1.05f);
                _audioSource.Play();
            }
            else if (_audioSource.isPlaying && (magnitude == 0 || isPushing))
            {
                _audioSource.Stop();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!enabled) return;

            if (collision.collider.TryGetComponent(typeof(Rigidbody), out _))
            {
                isPushing = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!enabled) return;

            if (collision.collider.TryGetComponent(typeof(Rigidbody), out _))
            {
                isPushing = false;
            }
        }

        private void OnDisable() => _audioSource.Stop();

        //private void OnDestroy() => AudioManager.Instance.Play();
    }
}
