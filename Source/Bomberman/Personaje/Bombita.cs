using Bomberman.Arma;
using Bomberman.Articulo;

namespace Bomberman.Personaje
{
    public class Bombita : Personaje , IComedor
    {
        private int reduccionRetardoBombas;

        public Bombita()
        {
            this.reduccionRetardoBombas = 0;
            this.velocidad = 1;
            this.lanzador= new LanzadorMolotov();
        }

        public void setReduccionRetardoBombas(int PorcentajeRetardo)
        {
            this.reduccionRetardoBombas= PorcentajeRetardo;        
        }

        public void setLanzadorToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        
        public void comer(IComible comible)
        {
            comible.modificarComedor(this);
        }
        
        
        public void duplicarVelocidad()
        {
            this.velocidad = 2;
        }

        public void cambiarLanzadorALanzadorToleTole()
        {
            this.setLanzadorToleTole();
        }


    }
}
