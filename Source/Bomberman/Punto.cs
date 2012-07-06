using BombermanModel.Mapa;

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

        //constructor sin parametros para permitir serializar la clase
        public Punto()
        {
            this.x = 0;
            this.y = 0;
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

        public Punto TransformarDireccionEnPunto(int direccion)
        {
            int a, b;
            switch (direccion)
            {
                case Tablero.ARRIBA:
                    {
                        a = 0;
                        b = 1;
                        break;
                    }
                case Tablero.ABAJO:
                    {
                        a = 0;
                        b = -1;
                        break;
                    }
                case Tablero.DERECHA:
                    {
                        a = 1;
                        b = 0;
                        break;
                    }
                case Tablero.IZQUIERDA:
                    {
                        a = -1;
                        b = 0;
                        break;
                    }
                default:
                    {
                        a = 0;
                        b = 0;
                        break;
                    }
            }
            return (new Punto(a, b));
        }


        public Punto PosicionSuperior()
        {
            return this.PosicionHaciaUnaDireccion(Tablero.ARRIBA);
        }

        public Punto PosicionInferior()
        {
            return this.PosicionHaciaUnaDireccion(Tablero.ABAJO);
        }

        public Punto PosicionDerecha()
        {
            return this.PosicionHaciaUnaDireccion(Tablero.DERECHA);
        }

        public Punto PosicionIzquierda()
        {
            return this.PosicionHaciaUnaDireccion(Tablero.IZQUIERDA);
        }

        public Punto PosicionHaciaUnaDireccion(int direccion)
        { 
            Punto dirPto = TransformarDireccionEnPunto(direccion);
            return (new Punto(this.X + dirPto.X, this.Y + dirPto.Y));
        }

    }

}
