using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanModel.Excepciones
{
    public class FormatoMapaXMLInvalidoException : Exception
    {
        public FormatoMapaXMLInvalidoException()
        { }

        public FormatoMapaXMLInvalidoException(string message)
            : base(message)
        { }
    }
}
