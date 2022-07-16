using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2022
{
    public class PauseManager : MonoBehaviour
    {
       [SerializeField] Canvas pauseUICanvas;
        private PlayerInputActions playerInput;
        static bool isPaused = false;
        PlayerInputActions pAct;
        
        
        void Start()
        {
            pauseUICanvas.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        
        }

        public void Pause()
        {
            if(!isPaused)
            {                
                isPaused = true;    
                pauseUICanvas.gameObject.SetActive(true);            
                Time.timeScale = 0;
            }
            else if(isPaused)
            {
                isPaused = false;
                pauseUICanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
                
    }
}
