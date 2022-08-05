using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMTK2022
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] TextMeshProUGUI _menuLabel;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameStateSO _gameStateSO;

        private void Start() {
            _canvas.gameObject.SetActive(false);
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

        private void ShowCanvas(bool show) {
            _canvas.gameObject.SetActive(show);
        }

        //Using timescale to pause the game is starting to get messy.
        //Should consider using a different approach.
        private void Pause() {
            ShowCanvas(true);
            Time.timeScale = 0;
            _playerController.enabled = false;
        }

        private void Resume() {
            ShowCanvas(false);
            Time.timeScale = 1;
            _playerController.enabled = true;
        }

        private void GameEnded()
        {
            _menuLabel.text = "Game Over";
            ShowCanvas(true);
        }

        public void RestartGame() {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }

        public void BackToMenu() {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
