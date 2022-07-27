using UnityEngine;

namespace GMTK2022
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] Canvas pauseUICanvas;
        [SerializeField] Canvas gameEndCanvas;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameStateSO _gameStateSO;
        
        private bool _isPaused = false;

        private void Start() {
            pauseUICanvas.gameObject.SetActive(false);
            gameEndCanvas.gameObject.SetActive(false);
        }

        private void OnEnable() {
            _gameStateSO.OnGamePause += Pause;
            _gameStateSO.OnGameResume += Resume;
            _gameStateSO.OnGameOver += GameEnded;
        }

        private void OnDisable() {
            _gameStateSO.OnGamePause -= Pause;
            _gameStateSO.OnGameResume -= Resume;
            _gameStateSO.OnGameOver -= GameEnded;
        }

        private void Pause() {
            _isPaused = true;
            pauseUICanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            _playerController.enabled = false;
        }

        private void Resume() {
            _isPaused = false;
            pauseUICanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            _playerController.enabled = true;
        }

        private void GameEnded()
        {
            gameEndCanvas.gameObject.SetActive(true);
        }
    }
}
