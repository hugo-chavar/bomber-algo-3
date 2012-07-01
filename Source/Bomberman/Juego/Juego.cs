using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel;
using BombermanModel.Arma;
using BombermanModel.Personaje;
using BombermanModel.Articulo;
using BombermanModel.Mapa.Casilla;

namespace BombermanModel.Juego
{
    public class Juego
    {
        private int cantDeVidas;
        private bool juegoPausado;
        private Personaje.Personaje protagonista;
        private Mapa.Mapa ambiente;
        private List<IMovible> objetosContundentes;
        private List<Personaje.Personaje> enemigosVivos;
        private List<IDependienteDelTiempo> dependientesDelTiempo;
        private Salida salida;
        private int nivel;
        private Estado estado;
        private bool mapaVisible;
        private MapaArchivo guardador = new MapaArchivo();
        
        //declaracion del Singleton
        private static Juego instanciaDeJuego;
        public static Punto Izquierda = new Punto(-1, 0);
        public static Punto Derecha = new Punto(1, 0);
        public static Punto Arriba = new Punto(0, 1);
        public static Punto Abajo = new Punto(0, -1);
        
        //Constantes
        private const int VIDAS = 3;
        private const int ANCHOMAPA = 17;
        private const int ALTOMAPA = 13;
        private const int ULTIMONIVEL = 3;

        //propiedades
        public int CantDeVidas
        {
            get { return cantDeVidas; }
            set { this.cantDeVidas = value; }
        }

        public bool MapaVisible
        {
            get { return mapaVisible; }
            set { this.mapaVisible = value; }
        }
        public bool JuegoPausado
        {
            get { return juegoPausado; }
            set { this.juegoPausado = value; }
        }

        public Personaje.Personaje Protagonista
        {
            get { return protagonista; }
            set { this.protagonista = value; }
        }

        public Mapa.Mapa Ambiente
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
            this.JuegoPausado = false;
            this.CantDeVidas = VIDAS;
            this.Ambiente = new Mapa.Mapa(ANCHOMAPA,ALTOMAPA);
            Punto posicion = new Punto(0, 0);
            this.protagonista = new Personaje.Bombita(posicion);
            enemigosVivos = new List<Personaje.Personaje>();
            this.objetosContundentes = new List<IMovible>();
            this.dependientesDelTiempo = new List<IDependienteDelTiempo>();
            this.salida = new Salida();
            nivel = 1;
            estado = Estado.enJuego;
            mapaVisible = false;
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
                instanciaDeJuego.CargarMapa();
            }
            return instanciaDeJuego;
        }

        public void PausarJuego()
        {
            this.JuegoPausado = true; 
        }

        public void GuardarPartida()
        {
            guardador.ImportarCasillas();
            guardador.GuardarEstadoAArchivo();
        }

        public void DesPausarJuego()
        {
            this.JuegoPausado = false;
        }

        public void PerderVida()
        {
            this.CantDeVidas = (this.CantDeVidas-1);
            if (this.CantDeVidas == 0)
            {
                estado = Estado.perdido;
            }
            else
            {
                VolverACargarMapa();
            }
        }

        private void VolverACargarMapa()
        {
            ambiente = new Mapa.Mapa(ANCHOMAPA, ALTOMAPA);
            this.enemigosVivos = new List<Personaje.Personaje>();
            CargarMapa();
            protagonista = new Bombita(new Punto(0, 0));
            mapaVisible = false;
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
                VolverACargarMapa();
            }
            else
            {
                estado = Estado.ganado;
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

        public void CargarMapa()
        {
            CargadorDeMapa cargador = new CargadorDeMapa();
            cargador.LeerMapa("mapa"+nivel+".xml");
        }

      public Salida Salida 
        {
            get { return this.salida; }
            set { this.salida = value; }
        }
    }

}
