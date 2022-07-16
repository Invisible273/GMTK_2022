using System;
using UnityEngine;

namespace GMTK2022
{
    //[CreateAssetMenu(menuName = "GMTK2022/game/dice")]
    public class DiceSO : ScriptableObject
    {
        public Action OnRollUp;
        public Action OnRollDown;
        public Action OnRollLeft;
        public Action OnRollRight;

        [SerializeField] private DiceFaceSO[] _diceFaces = new DiceFaceSO[6];
        private Dice _dice;

        public DiceFaceSO TopFace => _diceFaces[_dice.TopFace - 1];
        public DiceFaceSO BottomFace => _diceFaces[_dice.BottomFace - 1];
        public DiceFaceSO FrontFace => _diceFaces[_dice.FrontFace - 1];
        public DiceFaceSO BackFace => _diceFaces[_dice.BackFace - 1];
        public DiceFaceSO RightFace => _diceFaces[_dice.RightFace - 1];
        public DiceFaceSO LeftFace => _diceFaces[_dice.LeftFace - 1];


        public void Init() {
            _dice = new();
        }

        public void RollUp() {
            _dice.RollUp();
            OnRollUp?.Invoke();
        }

        public void RollDown() {
            _dice.RollDown();
            OnRollDown?.Invoke();
        }

        public void RollLeft() {
            _dice.RollLeft();
            OnRollLeft?.Invoke();
        }

        public void RollRight() {
            _dice.RollRight();
            OnRollRight?.Invoke();
        }
    }
}
