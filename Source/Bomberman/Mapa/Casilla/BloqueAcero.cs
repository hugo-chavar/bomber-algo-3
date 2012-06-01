using Bomberman;
namespace Bomberman.Mapa.Casilla
{
    public class BloqueAcero :Obstaculo
    {
            public BloqueAcero(Punto Posicion)
                : base(Posicion, 1)
            { }


            public override void daniarConBombaMolotov()
            {
                //La Bomba Molotov no daña Al Bloque De Acero
            }
     }
}
