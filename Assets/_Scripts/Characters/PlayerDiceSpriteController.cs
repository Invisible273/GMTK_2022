using UnityEngine;

namespace GMTK2022
{
    public class PlayerDiceSpriteController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private DiceSO _dice;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable() {
            UpdateTopFace();
            _dice.OnRollUp += UpdateTopFace;
            _dice.OnRollDown += UpdateTopFace;
            _dice.OnRollLeft += UpdateTopFace;
            _dice.OnRollRight += UpdateTopFace;
        }

        private void OnDisable() {
            _dice.OnRollUp -= UpdateTopFace;
            _dice.OnRollDown -= UpdateTopFace;
            _dice.OnRollLeft -= UpdateTopFace;
            _dice.OnRollRight -= UpdateTopFace;
        }

        private void UpdateTopFace() {
            _spriteRenderer.sprite = _dice.TopFace.Sprite;
        }
    }
}
