using UnityEngine;

namespace CloneEscape.Audio
{
    [RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
    public class BoxAudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _moveClip;

        private AudioSource _audioSource;
        private Rigidbody _rigidbody;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _moveClip;

            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (AudioManager.Instance.IsMuted) return;

            float magnitude = _rigidbody.velocity.magnitude;

            if (!_audioSource.isPlaying && magnitude > 0)
            {
                _audioSource.pitch = Random.Range(0.95f, 1.05f);
                _audioSource.Play();
            }
            else if (_audioSource.isPlaying && magnitude < 0.1f)
            {
                _audioSource.Stop();
            }
        }
    }
}