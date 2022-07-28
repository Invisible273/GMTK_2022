using TMPro;
using UnityEngine;

namespace GMTK2022
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private ScoreSO _scoreSO;
        [SerializeField] private TextMeshProUGUI _scoreTextMesh;
        [SerializeField] private int _maxStringSize = 5;

        private void OnEnable() {
            UpdateScoreText(_scoreSO.Score);
            _scoreSO.OnScoreChanged += UpdateScoreText;
        }

        private void OnDisable() {
            _scoreSO.OnScoreChanged -= UpdateScoreText;
        }

        private void UpdateScoreText(int score) {
            _scoreTextMesh.text = score.ToString("D" + _maxStringSize.ToString());
        }
    }
}
