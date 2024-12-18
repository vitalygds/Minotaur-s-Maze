using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(AudioSource))]
    internal sealed class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioClip _exploreClip;
        [SerializeField] private AudioClip _escapeClip;
        [SerializeField] private AudioClip _collectClip;
        [SerializeField] private AudioClip _roarClip;
        [SerializeField] [Range(0f, 1f)] private float _volume = 0.1f;
        private AudioSource _audioSource;

        public static MusicController Instance;

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = _exploreClip;
            _audioSource.volume = _volume;
            _audioSource.Play();
        }

        public void OnObjectiveComplete()
        {
            _audioSource.Stop();
            Invoke(nameof(PlayCollectSound), 0f);
            Invoke(nameof(PlayRoarSound), 0.7f);
            Invoke(nameof(PlayEscapeClip), 3.5f);
        }

        private void PlayEscapeClip()
        {
            _audioSource.volume = 0.9f;
            _audioSource.clip = _escapeClip;
            _audioSource.Play();
        }

        private void PlayCollectSound()
        {
            _audioSource.volume = 1f;
            _audioSource.PlayOneShot(_collectClip);
        }

        private void PlayRoarSound()
        {
            _audioSource.volume = 0.9f;
            _audioSource.PlayOneShot(_roarClip);
        }
    }
}
