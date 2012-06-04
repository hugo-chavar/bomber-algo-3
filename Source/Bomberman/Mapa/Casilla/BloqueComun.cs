using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public class BloqueComun:Obstaculo
    {

        private const int RESISTENCIALADRILLO = 5;
        private const int RESISTENCIACEMENTO = 10;

        private BloqueComun(int UnidadesResistencia)
            : base(UnidadesResistencia)
        { }

        public static BloqueComun CrearBloqueCemento()
        { 
            BloqueComun BloqueCemento = new BloqueComun(RESISTENCIACEMENTO); 
            return (BloqueCemento);
        }

        public static BloqueComun CrearBloqueLadrillos()
        {
            BloqueComun BloqueLadrillos = new BloqueComun(RESISTENCIALADRILLO); 
            return (BloqueLadrillos);
        }

        public override bool PuedeContenerArticulos()
        {
            return true;
        }


        
    }
}
