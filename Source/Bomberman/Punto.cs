namespace Bomberman
{
    public class Punto
    {   private int x;
        private int y;

        public Punto(int X, int Y)
        { 
            this.x = X;
            this.y = Y;
        }

        public int X
        {
            get { return (this.x);}
            set { this.x = value;}
        }

        public int Y
        {
            get { return (this.y); }
            set { this.y = value; }
        }
        public void AumentarXEn(int aumentoX)
        {
            this.X = X + aumentoX;
        }
        public void AumentarYEn(int aumentoY)
        {
            this.Y = Y + aumentoY;
        }
        public bool EsPuntoValido()
        {
            if ((this.X >= 0) && (this.Y >= 0))
            {
                return true;
            }
            return false;
            }
        }

}
