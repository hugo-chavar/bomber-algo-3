using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanModel
{
    public enum Estado
    {
        Inicial,
        Perdido,
        Ganado,
        EnJuego,
        Reiniciar,
        RecargarMapa,
        GuardarPartida,
        ContinuarPartidaGuardada,
        Salir,
        Pausa,
        ConErrores,
        Serializar
    }
}