using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GMTK2022
{
    public class MenuController : MonoBehaviour
    {
        [Header("Levels To Load")]
        public string _newGameLevel;

        public void NewGameDialogYes() {
            SceneManager.LoadScene(_newGameLevel);
        }

        public void ExitButton() {
            Application.Quit();
        }
    }
}
