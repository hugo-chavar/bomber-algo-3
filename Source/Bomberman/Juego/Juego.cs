using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel;
using BombermanModel.Arma;
using BombermanModel.Personaje;
using BombermanModel.Articulo;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Mapa;

namespace BombermanModel.Juego
{
    public class Juego
    {
        private int cantDeVidas;
        private Personaje.Personaje protagonista;
        private Tablero ambiente;
        private List<IMovible> objetosContundentes;
        private List<Personaje.Personaje> enemigosVivos;
        private List<IDependienteDelTiempo> dependientesDelTiempo;
        private Salida salida;
        private int nivel;
        private Estado estado;
        private MapaArchivo guardador;
        private string archivoMapaActual;
        
        //declaracion del Singleton
        private static Juego instanciaDeJuego;
        
        //Constantes
        private const int VIDAS = 3;
        private const int ULTIMONIVEL = 4;

        //propiedades
        public int CantDeVidas
        {
            get { return cantDeVidas; }
            set { this.cantDeVidas = value; }
        }

        public Estado EstadoGeneral
        {
            get { return estado; }
            set { this.estado = value; }
        }

        public Personaje.Personaje Protagonista
        {
            get { return protagonista; }
            set { this.protagonista = value; }
        }

        public Tablero Ambiente
        {
            get { return ambiente; }
            set { this.ambiente = value; }
        }

        public List<IDependienteDelTiempo> DependientesDelTiempo
        {
            get { return this.dependientesDelTiempo; }
            set { this.dependientesDelTiempo = value; }
        }

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

        public bool Salir()
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
