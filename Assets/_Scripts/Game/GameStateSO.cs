using System;
using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(menuName = "GMTK2022/game/game state")]
    public class GameStateSO : ScriptableObject
    {
        [SerializeField] GameState _currentState = GameState.Initializing;
        public GameState CurrentState => _currentState;
        public void SwitchToState(GameState gameState) {
            switch(gameState) {
                case GameState.Playing:
                    if(_currentState == GameState.Paused) OnGameResume?.Invoke();
                    else OnGameStart?.Invoke();
                    break;
                case GameState.Paused:
                    OnGamePause?.Invoke();
                    break;
                case GameState.GameOver:
                    OnGameOver?.Invoke();
                    break;
                default:
                    //do nothing
                    break;
            }
            _currentState = gameState;
        }

        public event Action OnGameStart;
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action OnGameOver;

        public void ResetGameState() {
            _currentState = GameState.Initializing;
        }
    }

    public enum GameState
    {
        Initializing,
        Playing,
        Paused,
        GameOver
    }
}
