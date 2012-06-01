using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public class BombaToleTole : Articulo
    {
        public override void modificarComedor(IComedor comedor)
        {
            comedor.cambiarLanzadorALanzadorToleTole();
        }
    }
}
