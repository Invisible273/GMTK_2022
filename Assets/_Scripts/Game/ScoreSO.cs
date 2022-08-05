using System;
using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(menuName = "GMTK2022/game/score")]
    public class ScoreSO : ScriptableObject
    {
        [SerializeField] int _score = 0;
        public int Score => _score;

        public event Action<int> OnScoreChanged;

        [SerializeField] DiceSO _diceSO;

        public void AddScore(int amount) {
            _score += amount * _diceSO.TopFace.Value;
            OnScoreChanged?.Invoke(_score);
        }

        public void ResetScore() {
            _score = 0;
        }
    }
}
