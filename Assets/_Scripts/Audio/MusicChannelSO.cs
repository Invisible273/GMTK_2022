using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(fileName = "Audio Channel Music", menuName = "GMTK2022/audio/music channel")]
    public class MusicChannelSO : ScriptableObject
    {
        [SerializeField] private bool _loopClip = true;
        public void PlayClip(AudioClip clip) {
            AudioManager.Instance.PlayMusic(clip, _loopClip);
        }
    }
}
