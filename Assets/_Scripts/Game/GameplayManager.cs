using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

namespace GMTK2022
{
    public class GameplayManager : MonoBehaviour
    {
        private const int MAX_SCORE_SIZE = 6;

        [SerializeField]
        private TextMeshProUGUI scoreBoard = null;
        
        

        public static GameplayManager instance = null;
        public static event Action onPauseRequest;
        public static event Action onGameEndRequest;

        GameState gameState;
        enum GameState
        {
            Pause,
            Play,
            Dead
        }

        private int currentScore = 0;

        private void Awake()
        {
            if(FindObjectsOfType<GameplayManager>().Length > 1)
            {
                Destroy(gameObject);
            }
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
                    // if(Input.anyKey) {
                    //     ResetLevel();
                    //     SwitchStateTo(GameState.Play);
                    // }
                    break;
            }
        }

        private void SwitchStateTo(GameState state) {
            gameState = state;
            switch(gameState) {
                case GameState.Pause:
                onPauseRequest?.Invoke();
                    


                    break;
                case GameState.Play:
                    if(PauseManager.isPaused) {
                        onPauseRequest?.Invoke();
                    }


                    break;
                case GameState.Dead:
                    if(PauseManager.isPaused) {
                        onPauseRequest?.Invoke();
                    }
                    onGameEndRequest?.Invoke();
                    if(Input.anyKey)
                    {
                        
                        SwitchStateTo(GameState.Play);
                        ResetLevel();
                    }
                    
                    


                    break;
            }
        }

        public void ResetLevel() {
            SceneManager.LoadScene(0);
        }


        public void GameEnded() {                     
           
            SwitchStateTo(GameState.Dead);
        }

        public void AddScore(int scoreToAdd) {
            currentScore += scoreToAdd;
            if(scoreBoard != null) 
            {
                string score_string = currentScore.ToString();
                while(score_string.Length < MAX_SCORE_SIZE)
                    score_string = "0" + score_string;
                scoreBoard.text = "SCORE: " + score_string;
            }
            else if(scoreBoard == null)
            {
                scoreBoard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
                
            }
        }

        
        
    }
}
