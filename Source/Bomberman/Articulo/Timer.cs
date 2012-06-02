using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public class Timer : Articulo
    {
        const int PORCENTAJEDERETRASO = 15;

        public override void modificarComedor(IComedor comedor)
        {
            comedor.reducirRetardo(PORCENTAJEDERETRASO);
        }
    }
}
