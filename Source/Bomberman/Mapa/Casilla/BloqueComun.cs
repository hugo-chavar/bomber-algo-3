using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public class BloqueComun:Obstaculo
    {
        private BloqueComun(Punto Posicion, int UnidadesResistencia)
            : base(Posicion, UnidadesResistencia)
        { }

        public static BloqueComun CrearBloqueCemento(Punto posicion)
        { 
            BloqueComun BloqueCemento = new BloqueComun(posicion, 10); // Meter constante para la "vida" del bloque
            return (BloqueCemento);
        }

        public static BloqueComun CrearBloqueLadrillos(Punto posicion)
        {
            BloqueComun BloqueLadrillos = new BloqueComun(posicion, 5); // Meter constante para la "vida" del bloque
            return (BloqueLadrillos);
        }


        
    }
}
