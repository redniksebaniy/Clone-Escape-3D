using UnityEngine;
using UnityEngine.Audio;

namespace CloneEscape.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                return instance;
            }
        }

        private AudioMixer _audioMixer;

        public AudioSource AudioSource { get; private set; }

        public const string IS_MUTE = "Is Mute";
        private const string CUTOFF_FREQ = "cutoff frequency";

        public bool IsMuted
        {
            get
            {
                return AudioSource.mute;
            }

            set
            {
                AudioSource.mute = value;
            }
        }

        private void Awake()
        {
            instance = this;
            AudioSource = GetComponent<AudioSource>();
            AudioSource.mute = PlayerPrefs.GetInt(IS_MUTE, 0) > 0;
            _audioMixer = AudioSource.outputAudioMixerGroup.audioMixer;
        }

        public void TooglePauseEffect(bool value)
        {
            _audioMixer.SetFloat(CUTOFF_FREQ, value ? 350 : 22000);
        }
    }
}