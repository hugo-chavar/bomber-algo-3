namespace BombermanModel
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
       
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Punto) )  
            {
                return false;
            }
            Punto p = (Punto)obj;
            return (this.x == p.x && this.y == p.y);
        }

        public override int GetHashCode()
        {
            int hash = 11;
            hash = (hash * 13) + this.x.GetHashCode();
            hash = (hash * 13) + this.y.GetHashCode();
            return hash;
        }

        public Punto Clonar()
        {
            return new Punto(this.X, this.Y);
        }

        //si el delta es positivo, devuelve posicion que esta delta lugares hacia arriba
        //si el delta es negativo, devuelve posicion que esta delta lugares hacia abajo
        public Punto MoverPosicionesEnSentidoVertical(int delta)
        {
            return new Punto(this.X, this.Y + delta);
        }

        //si el delta es positivo, devuelve posicion que esta delta lugares hacia la derecha
        //si el delta es negativo, devuelve posicion que esta delta lugares hacia la izquierda
        public Punto MoverPosicionesEnSentidoHorizontal(int delta)
        {
            return new Punto(this.X + delta, this.Y);
        }

        public Punto PosicionSuperior()
        {
            return this.MoverPosicionesEnSentidoVertical(1);
        }

        public Punto PosicionInferior()
        {
            return this.MoverPosicionesEnSentidoVertical(-1);
        }

        public Punto PosicionDerecha()
        {
            return this.MoverPosicionesEnSentidoHorizontal(1);
        }

        public Punto PosicionIzquierda()
        {
            return this.MoverPosicionesEnSentidoHorizontal(-1);
        }
    }

}
