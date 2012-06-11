using Bomberman;
namespace Bomberman.Mapa.Casilla
{
    public class BloqueAcero : Obstaculo
    {
        private const int RESISTENCIAACERO = 10;
        public BloqueAcero()
            : base(RESISTENCIAACERO)
        { }

        public override void DaniarConBombaMolotov(int UnidadesDaniadas)
        {           
        }

        public override bool PuedeContenerSalida()
        {
            return false;
        }

        //public abstract void daniarConProyectil();
     }
}
