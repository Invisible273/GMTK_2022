using UnityEngine;

namespace GMTK2022
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource _musicSource, _sfxSource;

        private void Awake() {
            Instance = this;
        }

        public void PlayMusic(AudioClip clip, bool loop) {
            _musicSource.Stop();
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        public void PlaySFX(AudioClip clip) {
            _sfxSource.PlayOneShot(clip);
        }
    }
}
