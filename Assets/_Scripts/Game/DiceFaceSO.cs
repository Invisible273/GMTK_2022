using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(menuName = "GMTK2022/game/dice face")]
    public class DiceFaceSO : ScriptableObject
    {
        [SerializeField] private int _faceValue;
        public int Value => _faceValue;

        [SerializeField] private Sprite _spriteTop;
        public Sprite SpriteTop => _spriteTop;

        [SerializeField] private Sprite _spriteFront;
        public Sprite SpriteFront => _spriteFront;

        [SerializeField] private Sprite _spriteSide;
        public Sprite SpriteSide => _spriteSide;

        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;
    }
}
