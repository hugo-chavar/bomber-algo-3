using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;
using System.IO;

namespace BombermanModel.Juego
{
    public class MapaArchivo
    {
        private List<Casilla> casillas;
        private int dimHorizontal;
        private int dimVertical;
        private Tablero mapaGenerado;


        public void ExportarCasillas()
        {
            // recorro el tablero entero
            casillas = new List<Casilla>();
            Tablero miMapa = Juego.Instancia().Ambiente;
            dimHorizontal = miMapa.DimensionHorizontal;
            dimVertical = miMapa.DimensionVertical;
            for (int vertical = 0; vertical < dimVertical; vertical++)
            {
                for (int horizontal = 0; horizontal < dimHorizontal; horizontal++)
                {
                    Punto p = new Punto(horizontal, vertical);
                    Casilla unaCasilla = miMapa.ObtenerCasilla(p);
                    casillas.Add(unaCasilla);
                }
            }
        }

        public void GuardarEstadoAArchivo()
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");

            using (XmlWriter writer = XmlWriter.Create("mapaGuardado.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Tablero");
                writer.WriteStartAttribute("ancho");
                writer.WriteValue(dimHorizontal);
                writer.WriteEndAttribute();
                writer.WriteStartAttribute("alto");
                writer.WriteValue(dimVertical);
                writer.WriteEndAttribute();

                foreach (Casilla c in casillas)
                {
                    writer.WriteStartElement("Casilla");

                    writer.WriteStartAttribute("x");
                    writer.WriteValue(c.Posicion.X);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("y");
                    writer.WriteValue(c.Posicion.Y);
                    writer.WriteEndAttribute();
                    if (c.Estado.GetType().Name != "Pasillo")
                    {
                        string tipo;
                        if (c.Estado.GetType().Name =="BloqueComun")
                        {
                            tipo = (c.Estado.Nombre == Nombres.bLadrillo) ? "BloqueLadrillo" : "BLoqueCemento";
                        } 
                        else tipo = c.Estado.GetType().Name;  
                        
                        writer.WriteStartElement(tipo);
                        writer.WriteStartAttribute("resistencia");
                        writer.WriteValue(c.Estado.UnidadesDeResistencia);
                        writer.WriteEndElement();
                    }
                    foreach (IMovible m in c.TransitandoEnCasilla)
                    {
                        //solo se almacena el que esta con mas de medio cuerpo dentro de la casilla
                        if (c.Posicion.Equals(m.Posicion) && (m.GetType().Name != "Proyectil"))
                        {
                            writer.WriteStartElement(m.GetType().Name);
                            writer.WriteStartAttribute("resistencia");
                            writer.WriteValue(((Personaje.Personaje)m).UnidadesDeResistencia);
                            writer.WriteEndAttribute();
                            writer.WriteStartAttribute("velocidad");
                            writer.WriteValue(m.Movimiento.Velocidad);
                            writer.WriteEndAttribute();
                            if (m.GetType().Name == "Bombita")
                            {
                                writer.WriteStartAttribute("lanzador");
                                writer.WriteValue(((Personaje.Personaje)m).Lanzador.GetType().Name);
                                writer.WriteEndAttribute();
                            }
                            writer.WriteEndElement();
                        }
                    }

                    writer.WriteEndElement();

                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void ContinuarPartidaGuardada()
        {
            StringBuilder output = new StringBuilder();

            StreamReader lector = new StreamReader("mapaGuardado.xml");

            String xmlString = lector.ReadToEnd();
            // Create an XmlReader
            Type tipo;


            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                //leo el inicio del mapa
                if (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        //Uso reflection a full
                        tipo = Type.GetType(reader.Name);
                        Tablero c = Activator.CreateInstance(tipo) as Tablero;

                    }

                }
                
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {

                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }

                }
            }
            Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}
