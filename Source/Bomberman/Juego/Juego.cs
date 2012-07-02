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
        private bool juegoPausado;
        private Personaje.Personaje protagonista;
        private Tablero ambiente;
        private List<IMovible> objetosContundentes;
        private List<Personaje.Personaje> enemigosVivos;
        private List<IDependienteDelTiempo> dependientesDelTiempo;
        private Salida salida;
        private int nivel;
        private Estado estado;
        private bool mapaVisible;
        private MapaArchivo guardador;
        
        //declaracion del Singleton
        private static Juego instanciaDeJuego;
        
        //Constantes
        private const int VIDAS = 3;
       // private const int ANCHOMAPA = 17;
       // private const int ALTOMAPA = 13;
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
            Recomenzar();
            nivel = 1;
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


        public void Recomenzar()
        {
            this.CantDeVidas = VIDAS;
            enemigosVivos = new List<Personaje.Personaje>();
            this.objetosContundentes = new List<IMovible>();
            this.dependientesDelTiempo = new List<IDependienteDelTiempo>();
            this.salida = new Salida();
            
            estado = Estado.enJuego;
            mapaVisible = false;
        }


        public void GuardarPartida()
        {
            guardador.ExportarCasillas();
            guardador.GuardarEstadoAArchivo();
        }
        
        public void ContinuarPartidaGuardada()
        {
            this.guardador =  new MapaArchivo();
            this.ambiente = guardador.ContinuarPartidaGuardada("mapaGuardado.xml");
        }

        public void PerderVida()
        {
            this.CantDeVidas = (this.CantDeVidas-1);
            if (this.CantDeVidas < 1)
            {
                estado = Estado.perdido;
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
           // mapaVisible = false;
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
            mapaVisible = false;
            if (nivel <= ULTIMONIVEL)
            {
                CargarMapa();
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
            //CargadorDeMapa cargador = new CargadorDeMapa();
            //cargador.LeerMapa("mapa"+nivel+".xml");
            this.guardador = new MapaArchivo();
            this.ambiente = guardador.ContinuarPartidaGuardada("Mapa" + nivel + ".xml");
        }

      public Salida Salida 
        {
            get { return this.salida; }
            set { this.salida = value; }
        }
    }

}
