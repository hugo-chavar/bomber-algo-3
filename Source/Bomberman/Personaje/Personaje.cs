using Bomberman.Arma;


namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible, IPosicionable
    {
        protected int velocidad;
        protected Punto posicion;
        protected Lanzador lanzador;

        public Lanzador Lanzador 
        { 
            get { return this.lanzador; } //Usado solamente para pruebas!!!
        }

        public int Velocidad
        {
            get { return this.velocidad; }
            set { this.velocidad = Velocidad; }
        }

        public void LanzarExplosivo(int x,int y,int retardo)
        {
            this.lanzador.lanzar(x,y,retardo);
        }
        
        public void mover()
        {
            //falta implementar
        }

        public bool atraviesaObstaculos()
        {
            //hacer un override de este metodo solo en el personaje que atraviesa obstaculos
            return false;
        }

        public Punto getPosicion()
        {
            return this.posicion;
        }

    }
}
