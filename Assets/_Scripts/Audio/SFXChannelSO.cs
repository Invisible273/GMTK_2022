using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(fileName = "Audio Channel SFX", menuName = "GMTK2022/audio/sfx channel")]
    public class SFXChannelSO : ScriptableObject
    {
        public void PlayClip(AudioClip clip) {
            AudioManager.Instance.PlaySFX(clip);
        }
    }
}
