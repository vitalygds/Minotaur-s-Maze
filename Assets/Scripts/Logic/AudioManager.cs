using UnityEngine;

namespace Logic
{
    [System.Serializable]
    public class Sound
    {
        public string m_name;
        public AudioClip[] m_clips;
        public AudioSource m_sourcePrefab;

        [Range(0f, 1f)] public float volume = 1.0f;
        [Range(0f, 1.5f)] public float pitch = 1.0f;
        public Vector2 m_randomVolumeRange = new Vector2(1.0f, 1.0f);
        public Vector2 m_randomPitchRange = new Vector2(1.0f, 1.0f);

        private AudioSource m_source;

        public void SetSource(AudioSource source)
        {
            m_source = source;
            int randomClip = Random.Range(0, m_clips.Length - 1);
            m_source.clip = m_clips[randomClip];
        }

        public void Play()
        {
            if (m_clips.Length > 1)
            {
                int randomClip = Random.Range(0, m_clips.Length - 1);
                m_source.clip = m_clips[randomClip];
            }

            m_source.volume = volume * Random.Range(m_randomVolumeRange.x, m_randomVolumeRange.y);
            m_source.pitch = pitch * Random.Range(m_randomPitchRange.x, m_randomPitchRange.y);
            m_source.Play();
        }
    }

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Sound[] m_sounds;

        private void Start()
        {
            for (int i = 0; i < m_sounds.Length; i++)
            {
                var source = Instantiate(m_sounds[i].m_sourcePrefab);
                source.gameObject.name = "Sound_" + i + "_" + m_sounds[i].m_name;
                source.transform.SetParent(transform);
                source.transform.localPosition = Vector3.zero;
                m_sounds[i].SetSource(source);
            }
        }

        private void PlaySound(string name)
        {
            for (int i = 0; i < m_sounds.Length; i++)
            {
                if (m_sounds[i].m_name == name)
                {
                    m_sounds[i].Play();
                    return;
                }
            }

            Debug.LogWarning("AudioManager: Sound name not found in list: " + name);
        }

        public void PlayFootstep()
        {
            PlaySound("Footstep");
        }

        public void PlaySweep()
        {
            PlaySound("Sweep");
        }
    }
}