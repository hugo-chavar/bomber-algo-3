using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using BombermanModel;
using BombermanModel.Arma;
using BombermanModel.Personaje;
using BombermanModel.Articulo;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Mapa;

namespace BombermanModel.Juego
{
    [XmlRootAttribute("Juego", Namespace = "algo3",IsNullable = false)]
    public class Juego
    {
        [XmlElementAttribute(IsNullable = false)]
        private int cantDeVidas;
        [XmlIgnoreAttribute]
        private Personaje.Personaje protagonista;
        [XmlIgnoreAttribute]
        private Tablero ambiente;

        //declaracion del Singleton
        [XmlIgnoreAttribute]
        private static Juego instanciaDeJuego;
        [XmlIgnoreAttribute]
        private List<IMovible> objetosContundentes;
        [XmlIgnoreAttribute]
        private List<Personaje.Personaje> enemigosVivos;
        [XmlIgnoreAttribute]
        private List<IDependienteDelTiempo> dependientesDelTiempo;
        [XmlIgnoreAttribute]
        private Salida salida;
       // [XmlIgnoreAttribute]
        private int nivel;
        [XmlIgnoreAttribute]
        private Estado estado;
        [XmlIgnoreAttribute]
        private MapaArchivo guardador;
       // [XmlIgnoreAttribute]
        private string archivoMapaActual;
        
       
        //Constantes
        private const int VIDAS = 3;
        private const int ULTIMONIVEL = 4;

        public void SerializarJuego()
        {
            XmlSerializer xSer = new XmlSerializer(typeof(Juego));
            TextWriter writer = new StreamWriter("serial.xml");

            // Serialize the object and close the TextWriter.
            xSer.Serialize(writer, Instancia());
            writer.Close();
            estado = Estado.EnJuego;
        }

        //propiedades
        public int CantDeVidas
        {
            get { return cantDeVidas; }
            set { this.cantDeVidas = value; }
        }
        [XmlIgnoreAttribute]
        public Estado EstadoGeneral
        {
            get { return estado; }
            set { this.estado = value; }
        }
        [XmlIgnoreAttribute]
        public Personaje.Personaje Protagonista
        {
            get { return protagonista; }
            set { this.protagonista = value; }
        }
        //[XmlIgnoreAttribute]
        public Tablero Ambiente
        {
            get { return ambiente; }
            set { this.ambiente = value; }
        }
        [XmlIgnoreAttribute]
        public List<IDependienteDelTiempo> DependientesDelTiempo
        {
            get { return this.dependientesDelTiempo; }
            set { this.dependientesDelTiempo = value; }
        }
        [XmlIgnoreAttribute]
        public List<Personaje.Personaje> EnemigosVivos
        { 
            get { return this.enemigosVivos; }
            set { this.enemigosVivos = value; }
        }
        
        //constructor
        private Juego()
        {
            estado = Estado.Inicial;
        }

        public static Juego Reiniciar()
        {
            instanciaDeJuego = null;
            return Juego.Instancia();
        }
        
        //instanciacion del Singleton
        public static Juego Instancia()
        {
            if (instanciaDeJuego == null)
            {
                instanciaDeJuego = new Juego();
            }
            return instanciaDeJuego;
        }


        public void ComenzarDesdeElPrincipio()
        {
            this.CantDeVidas = VIDAS;
            this.nivel = 1;
            RecomenzarNivel();
        }

        public void RecomenzarNivel()
        {
            enemigosVivos = new List<Personaje.Personaje>();
            this.objetosContundentes = new List<IMovible>();
            this.dependientesDelTiempo = new List<IDependienteDelTiempo>();
            this.salida = new Salida();
        }

        //Estados posibles del Juego
        public void Jugar()
        {
            estado = Estado.EnJuego;
        }

        public void Pausar()
        {
            estado = Estado.Pausa;
        }

        public void NuevoJuego()
        {
            estado = Estado.Reiniciar;
        }

        public void SalirDelJuego()
        {
            estado = Estado.Salir;
        }

        public void ContinuarPartidaGuardada()
        {
            estado = Estado.ContinuarPartidaGuardada;
        }

        public void Guardar()
        {
            estado = Estado.GuardarPartida;
        }

        public void ModoErrores()
        {
            estado = Estado.ConErrores;
        }

        public bool Saliendo()
        {
            return (estado == Estado.Salir);
        }

        public bool JuegoPausado()
        {
            return (estado == Estado.Pausa);
        }
        //Fin de Estados posibles del Juego

        public void GuardarPartida()
        {
            guardador.ExportarCasillas();
            guardador.GuardarEstadoAArchivo();
            Jugar();
        }


        public void SeleccionarMapa()
        {
            if (estado == Estado.ContinuarPartidaGuardada)
                this.archivoMapaActual = "mapaGuardado.xml";
            else
            {
                this.archivoMapaActual = "Mapa" + nivel + ".xml";
            }
            estado = Estado.RecargarMapa;
        }

        public void CargarMapa()
        {
            RecomenzarNivel();
            this.guardador = new MapaArchivo();
            this.ambiente = guardador.ContinuarPartidaGuardada(archivoMapaActual);
            this.nivel = ambiente.NroNivel;
        }

        public void PerderVida()
        {
            this.CantDeVidas = (this.CantDeVidas-1);
            if (this.CantDeVidas < 1)
            {
                estado = Estado.Perdido;
            }
            else
            {
                UsarSiguienteVida();
            }
        }

        private void UsarSiguienteVida()
        {
            protagonista = new Bombita(ambiente.PosicionInicial);
            ambiente.AgregarPersonaje(protagonista);
        }

        public void AgregarEnemigo(Personaje.Personaje enem)
        {
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
        }


        public int CantidadEnemigosVivos()
        {
            return this.enemigosVivos.Count();
        }

        public void ObjetoContundenteDestruido(IMovible obj)
        {
            this.objetosContundentes.Remove(obj);
        }

        public void ObjetoContundenteLanzado(IDependienteDelTiempo obj)
        {
            this.dependientesDelTiempo.Add(obj);
            IMovible movil = (IMovible)obj;
            this.Ambiente.ObtenerCasilla(movil.Posicion).Transitar(movil);
            this.objetosContundentes.Add(movil);
        }

        public void AlojarExplosivo(Explosivo exp)
        {
            this.Ambiente.ObtenerCasilla(exp.Posicion).PlantarExplosivo(exp);
            this.dependientesDelTiempo.Add(exp);

        }

        private void AvanzarNivel()
        {
            nivel++;
            if (nivel <= ULTIMONIVEL)
            {
                SeleccionarMapa();
            }
            else
            {
                estado = Estado.Ganado;
            }
        }

        public void AvanzarElTiempo()
        {
            if (Juego.Instancia().Ambiente.NivelGanado)
            {
                AvanzarNivel();
            }
            else if (protagonista.UnidadesDeResistencia <= 0)
            {
                PerderVida();
            }
            else
            {
                foreach (IDependienteDelTiempo i in dependientesDelTiempo)
                {
                    i.CuandoPasaElTiempo();
                }

                int iterador = this.dependientesDelTiempo.Count;
                while (--iterador >= 0)
                {
                    if (dependientesDelTiempo[iterador].DejoDeDependerDelTiempo())
                        dependientesDelTiempo.RemoveAt(iterador);
                }

                List<IMovible> listaAux = new List<IMovible>();
                foreach (IMovible i in objetosContundentes)
                    listaAux.Add(i);

                foreach (IMovible i in listaAux)
                {
                    this.Ambiente.ResolverColisionesCon(i);
                }

                iterador = enemigosVivos.Count;
                while (--iterador >= 0)
                {

                    if (enemigosVivos[iterador].Destruido())
                        enemigosVivos.RemoveAt(iterador);
                }

                if (CantidadEnemigosVivos() == 0)
                {
                    ActivarSalida();
                }
            }
        }

        public void ActivarSalida()
        {
            this.salida.Activar();
        }

      public Salida Salida 
        {
            get { return this.salida; }
            set { this.salida = value; }
        }
    }

}
