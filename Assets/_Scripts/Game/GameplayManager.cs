using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMTK2022
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameStateSO _gameStateSO;
        [SerializeField] private ScoreSO _scoreSO;

        private void Awake() {
            ResetGameData();
        }

        private void ResetGameData() {
            _gameStateSO.ResetGameState();
            _scoreSO.ResetScore();
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
                case GameState.GameOver:
                    if(Input.anyKeyDown) {
                        ResetLevel();
                    }
                    break;
            }
        }

        public void ResetLevel() {
            SceneManager.LoadScene(0);
        }

        public void GameOver() {
            _gameStateSO.SwitchToState(GameState.GameOver);
        }
    }
}
