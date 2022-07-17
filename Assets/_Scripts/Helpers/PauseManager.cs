using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PauseManager : MonoBehaviour
    {
       [SerializeField] Canvas pauseUICanvas;
        private PlayerInputActions playerInput;
        public static bool isPaused = false;
        
        
        
        void Awake()
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

