using UnityEngine;

namespace GMTK2022
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] Canvas pauseUICanvas;
        public static bool isPaused = false;

        void Awake() {
            pauseUICanvas.gameObject.SetActive(false);
        }

        public void Pause() {
            if(!isPaused) {
                isPaused = true;
                pauseUICanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
            } else if(isPaused) {
                isPaused = false;
                pauseUICanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }
}
