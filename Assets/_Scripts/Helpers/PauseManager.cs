using UnityEngine;

namespace GMTK2022
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] Canvas pauseUICanvas;
        [SerializeField] Canvas gameEndCanvas;
        [SerializeField] private PlayerController _playerController;
        public static bool isPaused = false;

        void Awake() {
            pauseUICanvas.gameObject.SetActive(false);
            gameEndCanvas.gameObject.SetActive(false);
        }

        public void Pause() {
            if(!isPaused) {
                isPaused = true;
                pauseUICanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                _playerController.enabled = false;
            } else if(isPaused) {
                isPaused = false;
                pauseUICanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                _playerController.enabled = true;
            }
        }

        public void GameEnded()
        {
            gameEndCanvas.gameObject.SetActive(true);
        }

    }
}
