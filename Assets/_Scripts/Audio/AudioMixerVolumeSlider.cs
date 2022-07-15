using UnityEngine;
using UnityEngine.Audio;

namespace GMTK2022
{
    public class AudioMixerVolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        public void SetVolume(float linearVolume) {
            _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, ConvertLinearToDecibel(linearVolume));
        }

        private float ConvertLinearToDecibel(float linearVolume) {
            return Mathf.Log10(linearVolume) * 20;
        }
    }
}
