using UnityEngine;

namespace GMTK2022
{
    public class PlayerDiceController : MonoBehaviour
    {
        [SerializeField] private DiceSO _dice;
        [SerializeField] private SFXChannelSO _audioChannel;
        [SerializeField] private AudioClip _audioClip;
        private Player _player;

        private void Awake() {
            _player = GetComponent<Player>();
            _dice.Init();
        }

        private void OnEnable() {
            _player.OnRoll += HandlePlayerRoll;
        }

        private void OnDisable() {
            _player.OnRoll -= HandlePlayerRoll;
        }

        private void HandlePlayerRoll(Vector2 rollDirection) {
            if(rollDirection == Vector2.left) _dice.RollLeft();
            else if(rollDirection == Vector2.right) _dice.RollRight();
            else if(rollDirection == Vector2.up) _dice.RollUp();
            else if(rollDirection == Vector2.down) _dice.RollDown();

            if(_audioClip != null) _audioChannel?.PlayClip(_audioClip);
        }
    }
}
