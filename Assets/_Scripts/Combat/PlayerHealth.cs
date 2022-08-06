using UnityEngine;

namespace GMTK2022
{
    public class PlayerHealth : Health
    {
        [SerializeField] Player _player;
        public bool Invincible { get; private set; }

        [Header("SFX")]
        [SerializeField] private SFXChannelSO _audioChannel;
        [SerializeField] private AudioClip _hitClip;

        private void OnEnable() {
            _player.OnRoll += RollStart;
            _player.OnRollEnd += RollEnd;
        }

        private void OnDisable() {
            _player.OnRoll -= RollStart;
            _player.OnRollEnd -= RollEnd;
        }

        public override void GetDamaged(float amount) {
            if(Invincible) return;

            base.GetDamaged(amount);
            if(_hitClip != null) _audioChannel?.PlayClip(_hitClip);
        }

        private void RollStart(Vector2 direction) {
            Invincible = true;
        }

        private void RollEnd(Vector2 direction) {
            Invincible = false;
        }
    }
}
