namespace Bomberman
{
    public class BloqueAcero :Obstaculo
    {
            public BloqueAcero(Punto Posicion)
                : base(Posicion, 1)
            { }


            public override void daniarConBombaMolotov(int unidadesDestruidas)
            {
                //La Bomba Molotov no daña Al Bloque De Acero
            }
     }
}
