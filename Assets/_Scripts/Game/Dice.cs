namespace GMTK2022
{
    public class Dice
    {
        public int TopFace { get; private set; }
        public int BottomFace => 7 - TopFace;
        public int FrontFace { get; private set; }
        public int BackFace => 7 - FrontFace;
        public int RightFace { get; private set; }
        public int LeftFace => 7 - RightFace;

        public Dice() {
            TopFace = 1;
            FrontFace = 2;
            RightFace = 3;
        }

        public void RollLeft() {
            int newSide = 7 - TopFace;
            TopFace = RightFace;
            RightFace = newSide;
        }

        public void RollDown() {
            int newTop = 7 - FrontFace;
            FrontFace = TopFace;
            TopFace = newTop;
        }

        public void RollRight() {
            int newTop = 7 - RightFace;
            RightFace = TopFace;
            TopFace = newTop;
        }

        public void RollUp() {
            int newFront = 7 - TopFace;
            TopFace = FrontFace;
            FrontFace = newFront;
        }
    }
}
