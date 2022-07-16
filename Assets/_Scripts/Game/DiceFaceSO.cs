using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(menuName = "GMTK2022/game/dice face")]
    public class DiceFaceSO : ScriptableObject
    {
        [SerializeField] private int _faceValue;
        public int Value => _faceValue;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;
    }
}
