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
        public void PosicionDerecha(int aumentoX)
        {
            this.x = this.x + aumentoX;
        }
        public void PosicionSuperior(int aumentoY)
        {
            this.y = this.y + aumentoY;
        }
        public bool EsPuntoValido()
        {
            if ((this.x >= 0) && (this.y >= 0))
            {
                return true;
            }
            return false;
            }
        }

}
