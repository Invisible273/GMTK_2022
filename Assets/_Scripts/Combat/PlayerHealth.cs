using UnityEngine;

namespace GMTK2022
{
    public class PlayerHealth : Health
    {
        [SerializeField] Player _player;
        bool _invincible = false;

        private void OnEnable() {
            _player.OnRoll += RollStart;
            _player.OnRollEnd += RollEnd;
        }

        private void OnDisable() {
            _player.OnRoll -= RollStart;
            _player.OnRollEnd -= RollEnd;
        }

        public override void GetDamaged(float amount) {
            if(_invincible) return;

            base.GetDamaged(amount);
        }

        private void RollStart(Vector2 direction) {
            _invincible = true;
        }

        private void RollEnd(Vector2 direction) {
            _invincible = false;
        }
    }
}
