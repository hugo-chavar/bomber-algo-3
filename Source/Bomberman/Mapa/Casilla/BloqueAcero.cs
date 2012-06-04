using Bomberman;
namespace Bomberman.Mapa.Casilla
{
    public class BloqueAcero :Obstaculo
    {
        //Determino resistencia para el acero por si en el fututo se agrega un tipo de bomba que danie acero y no lo destruya
        private const int RESISTENCIAACERO= 10;
        public BloqueAcero()
            : base(RESISTENCIAACERO)
            { }

            public override void DaniarConBombaMolotov(int UnidadesDaniadas)
            {
                //La Bomba Molotov no dania Al Bloque De Acero
                //Deberia daniar a los personajes alli presentes
            }

            public override bool PuedeContenerArticulos()
            {
                return false;
            }

            //public abstract void daniarConProyectil();
     }
}
