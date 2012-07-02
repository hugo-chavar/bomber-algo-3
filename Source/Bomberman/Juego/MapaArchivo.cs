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
        private Juego elJuego = Juego.Instancia();
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

            string tipo;
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
                        
                        if (c.Estado.GetType().Name =="BloqueComun")
                        {
                            tipo = (c.Estado.Nombre == Nombres.bLadrillo) ? "BloqueLadrillo" : "BloqueCemento";
                        } 
                        else tipo = c.Estado.GetType().Name;  
                        
                        writer.WriteStartElement(tipo);
                        writer.WriteStartAttribute("resistencia");
                        writer.WriteValue(c.Estado.UnidadesDeResistencia);
                        writer.WriteEndElement();
                    }
                    if (c.ArticuloContenido != null)
                    {
                        tipo = c.ArticuloContenido.GetType().Name;
                        writer.WriteStartElement(tipo);
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

        public Tablero ContinuarPartidaGuardada(string pathName)
        {
            Casilla casillaActual =null;
            Punto posActual = null;
            Tablero tableroNuevo = new Tablero();

            StreamReader lector = new StreamReader(pathName);
            //Uso reflection a full
            String xmlString = lector.ReadToEnd();

            Type tipo;
            int res;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString), settings))
            {
                //leo el inicio del mapa
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //Es el tablero
                            if (reader.Name == "Tablero")
                            {
                                tipo = Type.GetType("BombermanModel.Mapa." + reader.Name);
                                tableroNuevo = Activator.CreateInstance(tipo) as Tablero;
                                if (reader.HasAttributes) //deberia entrar, tiene 2 attr
                                {
                                    tableroNuevo.DimensionHorizontal = Convert.ToInt32(reader.GetAttribute("ancho"));
                                    tableroNuevo.DimensionVertical = Convert.ToInt32(reader.GetAttribute("alto"));
                                }
                                elJuego.Ambiente = tableroNuevo;
                            }
                            else
                            {
                                //es una casilla
                                if (reader.Name == "Casilla")
                                {
                                    int x, y;
                                    //leo coordenadas x e y
                                    x = Convert.ToInt32(reader.GetAttribute("x"));
                                    y = Convert.ToInt32(reader.GetAttribute("y"));
                                    //tableroNuevo.AgregarCasilla(casillaActual);
                                    if (reader.IsEmptyElement)
                                    {
                                        casillaActual = FabricaDeCasillas.FabricarPasillo(new Punto(x, y));
                                        tableroNuevo.AgregarCasilla(casillaActual);
                                    }
                                    else
                                    {
                                        posActual = new Punto(x, y);
                                    }
                                }
                                else
                                {
                                    
                                    //es algun obstaculo
                                    if ((reader.Name.Length >=6) && (reader.Name.Substring(0, 6).Equals("Bloque")))
                                    {
                                        res = Convert.ToInt32(reader.GetAttribute("resistencia"));
                                        switch (reader.Name)
                                               {
                                                    case "BloqueLadrillo":
                                                       casillaActual = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(posActual);
                                                        break;
                                                    case "BloqueCemento":
                                                        casillaActual = FabricaDeCasillas.FabricarCasillaConBloqueCemento(posActual);
                                                       break;
                                                    case "BloqueAcero":
                                                       casillaActual = FabricaDeCasillas.FabricarCasillaConBloqueAcero(posActual);
                                                        break;
                                                }
                                        casillaActual.Estado.UnidadesDeResistencia = res;
                                        
                                        tableroNuevo.AgregarCasilla(casillaActual);

                                    }
                                    else //es un personaje o un articulo
                                    {
                                        if (!tableroNuevo.ExisteCasillaEnPosicion(posActual))
                                        {
                                            casillaActual = FabricaDeCasillas.FabricarPasillo(posActual);
                                            tableroNuevo.AgregarCasilla(casillaActual);
                                        }
                                        
                                        if (reader.HasAttributes) //es personaje
                                        {
                                            int vel = Convert.ToInt32(reader.GetAttribute("velocidad"));
                                            res = Convert.ToInt32(reader.GetAttribute("resistencia"));
                                            Personaje.Personaje p = null;
                                            if (reader.Name == "Bombita")
                                            {
                                                string lanz = reader.GetAttribute("lanzador"); //TODO: falta guardar el Lanzador
                                                p = new Bombita(posActual);
                                                casillaActual.Transitar(p);
                                                elJuego.Protagonista = p;
                                                tableroNuevo.PosicionInicial = posActual;
                                            }
                                            else //es algun enemigo
                                            {
                                                tipo = Type.GetType("BombermanModel.Personaje." + reader.Name);
                                                p = Activator.CreateInstance(tipo, new object[] { posActual }) as Personaje.Personaje;
                                                elJuego.AgregarEnemigo(p);
                                            }

                                           
                                            p.Movimiento.Velocidad = vel;
                                            p.UnidadesDeResistencia = res;
                                        }
                                        else //es articulo
                                        {
                                            tipo = Type.GetType("BombermanModel.Articulo." + reader.Name);
                                            Articulo.Articulo a = Activator.CreateInstance(tipo) as Articulo.Articulo;
                                            casillaActual.ArticuloContenido = a;
                                            if (reader.Name == "Salida")
                                                elJuego.Salida = (Articulo.Salida)a;
                                        }
                                    }
                                }
                            }
                            break;

                           

                    }

                }

                return tableroNuevo;
                
            }
        }
    }
}
