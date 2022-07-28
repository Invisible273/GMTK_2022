using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace GMTK2022
{
    public class GameplayManager : MonoBehaviour
    {
        private const int MAX_SCORE_SIZE = 6;

        [SerializeField]
        private GameStateSO _gameStateSO;

        [SerializeField]
        private TextMeshProUGUI scoreBoard = null;

        public static GameplayManager instance = null;

        private int currentScore = 0;

        private void Awake()
        {
            if(FindObjectsOfType<GameplayManager>().Length > 1)
            {
                Destroy(gameObject);
            }
            if(instance == null)
                instance = this;
        }

        private void Start() {
            _gameStateSO.Init();
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

        public void GameEnded() {
            _gameStateSO.SwitchToState(GameState.GameOver);
        }

        public void AddScore(int scoreToAdd) {
            currentScore += scoreToAdd;
            if(scoreBoard != null) 
            {
                string score_string = currentScore.ToString("D" + MAX_SCORE_SIZE.ToString());
                scoreBoard.text = "SCORE: " + score_string;
            }
            else if(scoreBoard == null)
            {
                //TODO: Fix null ref after gameover
                scoreBoard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
            }
        }
    }
}
