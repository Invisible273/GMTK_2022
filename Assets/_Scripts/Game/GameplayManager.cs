using UnityEngine;

namespace GMTK2022
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameStateSO _gameStateSO;
        [SerializeField] private ScoreSO _scoreSO;
        [SerializeField] private Health _playerHealth;

        [Header("SFX")]
        [SerializeField] private SFXChannelSO _audioChannel;
        [SerializeField] private AudioClip _gameoverClip;

        private void Awake() {
            ResetGameData();
        }

        private void ResetGameData() {
            _gameStateSO.ResetGameState();
            _scoreSO.ResetScore();
        }

        private void OnEnable() {
            _playerHealth.onDeath += GameOver;
        }

        private void OnDisable() {
            _playerHealth.onDeath -= GameOver;
        }

        private void Update() {
            HandleGameStateTransitions();
        }

        private void HandleGameStateTransitions() {
            switch(_gameStateSO.CurrentState) {
                case GameState.Initializing:
                    _gameStateSO.SwitchToState(GameState.Playing);
                    break;
                case GameState.Paused:
                    if(Input.GetKeyDown(KeyCode.Escape)) {
                        _gameStateSO.SwitchToState(GameState.Playing);
                    }
                    break;
                case GameState.Playing:
                    if(Input.GetKeyDown(KeyCode.Escape)) {
                        _gameStateSO.SwitchToState(GameState.Paused);
                    }
                    break;
            }
        }

        /// <summary>
        /// Cleanup/disable scripts when game is over. Inefficient, as it
        /// does several GetComponent calls. Several scripts need refactoring
        /// to handle this logic better.
        /// </summary>
        public void GameOver() {
            _gameStateSO.SwitchToState(GameState.GameOver);
            if(_gameoverClip != null) _audioChannel?.PlayClip(_gameoverClip);

            _playerHealth.GetComponent<BoxCollider2D>().enabled = false;
            var rigidbody = _playerHealth.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            _playerHealth.GetComponent<Player>().enabled = false;
            _playerHealth.GetComponent<PlayerController>().enabled = false;
            _playerHealth.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
