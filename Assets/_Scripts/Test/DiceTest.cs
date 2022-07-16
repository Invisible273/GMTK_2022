using TMPro;
using UnityEngine;

namespace GMTK2022
{
    public class DiceTest : MonoBehaviour
    {
        [SerializeField] private DiceSO _diceSO;
        [SerializeField] private SFXChannelSO _audioChannel;

        [Header("UI")]
        public TMP_Text TopText;
        public TMP_Text BottomText;
        public TMP_Text FrontText;
        public TMP_Text BackText;
        public TMP_Text RightText;
        public TMP_Text LeftText;


        private void Awake() {
            _diceSO.Init();
        }

        private void OnEnable() {
            UpdateUI();

            _diceSO.OnRollUp += Feedback;
            _diceSO.OnRollDown += Feedback;
            _diceSO.OnRollLeft += Feedback;
            _diceSO.OnRollRight += Feedback;
        }

        private void OnDisable() {
            _diceSO.OnRollUp -= Feedback;
            _diceSO.OnRollDown -= Feedback;
            _diceSO.OnRollLeft -= Feedback;
            _diceSO.OnRollRight -= Feedback;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.UpArrow)) _diceSO.RollUp();
            else if(Input.GetKeyDown(KeyCode.RightArrow)) _diceSO.RollRight();
            else if(Input.GetKeyDown(KeyCode.DownArrow)) _diceSO.RollDown();
            else if(Input.GetKeyDown(KeyCode.LeftArrow)) _diceSO.RollLeft();
        }

        private void Feedback() {
            _audioChannel.PlayClip(_diceSO.TopFace.AudioClip);

            UpdateUI();
        }

        private void UpdateUI() {
            TopText.text = _diceSO.TopFace.Value.ToString();
            BottomText.text = _diceSO.BottomFace.Value.ToString();
            FrontText.text = _diceSO.FrontFace.Value.ToString();
            BackText.text = _diceSO.BackFace.Value.ToString();
            RightText.text = _diceSO.RightFace.Value.ToString();
            LeftText.text = _diceSO.LeftFace.Value.ToString();
        }
    }
}
