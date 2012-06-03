using Bomberman;
namespace Bomberman.Mapa.Casilla
{
    public class BloqueAcero :Obstaculo
    {
            public BloqueAcero(Punto Posicion)
                : base(Posicion, 1)
            { }


            public override void DaniarConBombaMolotov()
            {
                //La Bomba Molotov no dania Al Bloque De Acero
                //Deberia daniar a los personajes alli presentes
            }

            //public abstract void daniarConProyectil();
     }
}
