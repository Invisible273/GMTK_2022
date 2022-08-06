using System;
using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(menuName = "GMTK2022/game/score")]
    public class ScoreSO : ScriptableObject
    {
        public int Score { get; private set; }

        public event Action<int> OnScoreChanged;

        [SerializeField] DiceSO _diceSO;

        public void AddScore(int amount) {
            Score += amount * _diceSO.TopFace.Value;
            OnScoreChanged?.Invoke(Score);
        }

        public void ResetScore() {
            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }
    }
}
