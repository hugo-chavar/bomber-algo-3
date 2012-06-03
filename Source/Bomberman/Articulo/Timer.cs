using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public class Timer : Articulo
    {
        const int PORCENTAJEDERETRASO = 15;

        public override void ModificarComedor(IComedor comedor)
        {
            comedor.ReducirRetardo(PORCENTAJEDERETRASO);
            this.EstaOculto = true;
        }
    }
}
