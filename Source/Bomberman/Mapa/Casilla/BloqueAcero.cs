using Bomberman;
namespace Bomberman.Mapa.Casilla
{
    public class BloqueAcero :Obstaculo
    {
            public BloqueAcero(Punto Posicion)
                : base(1)// Que es ese 1? es medio hardcodeado, alguien que vea como armar una constante interna para arreglarlo.
        { } // : base(Posicion, 1)// Nota: los obstaculos ya no necesitan posicion, son un State de Casilla


            public override void DaniarConBombaMolotov()
            {
                //La Bomba Molotov no dania Al Bloque De Acero
                //Deberia daniar a los personajes alli presentes
            }

            //public abstract void daniarConProyectil();
     }
}
