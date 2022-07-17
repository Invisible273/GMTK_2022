using System;
using UnityEngine;

namespace GMTK2022
{
    public class DiceFaceSpriteChanger : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRendererTop;
        [SerializeField] private SpriteRenderer _spriteRendererFront;
        [SerializeField] private DiceSO _dice;
        [SerializeField] private Player _player;

        private void Awake() {
            if(!_spriteRendererFront || !_spriteRendererTop) Debug.LogError("SpriteRenderers not set!");
        }

        private void OnEnable() {
            UpdateFaces();
            _player.OnRoll += HideRenderers;
            _player.OnRollEnd += ShowRenderers;
        }

        private void OnDisable() {
            _player.OnRoll -= HideRenderers;
            _player.OnRollEnd -= ShowRenderers;
        }

        private void HideRenderers(Vector2 direction) {
            _spriteRendererTop.enabled = false;
            _spriteRendererFront.enabled = false;
        }

        private void ShowRenderers(Vector2 direction) {
            UpdateFaces();
            _spriteRendererTop.enabled = true;
            _spriteRendererFront.enabled = true;
        }

        private void UpdateFaces() {
            _spriteRendererTop.sprite = _dice.TopFace.SpriteTop;
            _spriteRendererFront.sprite = _dice.FrontFace.SpriteFront;
        }
    }
}
