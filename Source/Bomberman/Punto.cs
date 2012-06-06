﻿namespace Bomberman
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
       /* public void PosicionDerecha(int aumentoX) Hugo dice: Comento esto para demostrar que no se esta usando en ningun lado.. solo nos perjudicaría en el test de cobertura
        {                                                      //Como probar si se esta usando o no?? Posicionar el cursor sobre el nombre del metodo, presionar Ctrl + K y sin soltar Ctrl apretas R
            this.x = this.x + aumentoX;
        }
        public void PosicionSuperior(int aumentoY)
        {
            this.y = this.y + aumentoY;
        }*/
        public bool EsPuntoValido() //Hugo dice: si leyeron lo que escribí en la clase mapa, esto hay que volarlo, hacr Ctrl K,R (ver explicacio arriba)
        {
            if ((this.x >= 0) && (this.y >= 0))
            {
                return true;
            }
            return false;
            }
        
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            Punto p = (Punto)obj;
            return (this.x == p.x && this.y == p.y);
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = (hash * 13) + this.x.GetHashCode();
            hash = (hash * 13) + this.y.GetHashCode();
            return hash;
        }

        public Punto Clonar()
        {
            return new Punto(this.X, this.Y);
        }


        public Punto PosicionSuperior()
        {
            return new Punto(this.X, this.Y + 1);
        }

        public Punto PosicionInferior()
        {
            return new Punto(this.X, this.Y - 1);
        }

        public Punto PosicionDerecha()
        {
            return new Punto(this.X + 1, this.Y);
        }

        public Punto PosicionIzquierda()
        {
            return new Punto(this.X - 1, this.Y);
        }
    }

}
