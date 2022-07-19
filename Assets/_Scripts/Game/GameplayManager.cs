using UnityEngine;
using TMPro;

namespace GMTK2022
{
    public class GameplayManager : MonoBehaviour
    {
        private const int MAX_SCORE_SIZE = 6;

        [SerializeField]
        private TextMeshProUGUI scoreBoard = null;
        [SerializeField] PauseManager pManager = null;

        public static GameplayManager instance = null;

        GameState gameState;
        enum GameState
        {
            Pause,
            Play,
            Dead
        }

        private int currentScore = 0;

        private void Awake() {
            if(instance == null)
                instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            SwitchStateTo(GameState.Play);
        }

        private void Update() {
            switch(gameState) {
                case GameState.Pause:
                    if(Input.GetKeyDown(KeyCode.Escape)) {

                        SwitchStateTo(GameState.Play);
                    }
                    break;
                case GameState.Play:
                    if(Input.GetKeyDown(KeyCode.Escape)) {

                        SwitchStateTo(GameState.Pause);
                    }
                    break;
                case GameState.Dead:
                    if(Input.anyKey) {
                        ResetLevel();
                        SwitchStateTo(GameState.Play);
                    }
                    break;
            }
        }

        private void SwitchStateTo(GameState state) {
            gameState = state;
            switch(gameState) {
                case GameState.Pause:

                    pManager.Pause();


                    break;
                case GameState.Play:
                    if(PauseManager.isPaused) {
                        pManager.Pause();
                    }


                    break;
                case GameState.Dead:
                    if(PauseManager.isPaused) {
                        pManager.Pause();
                    }


                    break;
            }
        }

        private void ResetLevel() {
            currentScore = 0;
            AddScore(0);
        }


        public void GameEnded() {
            Debug.Log("Your score is: " + currentScore + ". You lost!");
            SwitchStateTo(GameState.Dead);
        }

        public void AddScore(int scoreToAdd) {
            currentScore += scoreToAdd;
            if(scoreBoard != null) {
                string score_string = currentScore.ToString();
                while(score_string.Length < MAX_SCORE_SIZE)
                    score_string = "0" + score_string;
                scoreBoard.text = "SCORE: " + score_string;
            }
        }
    }
}
