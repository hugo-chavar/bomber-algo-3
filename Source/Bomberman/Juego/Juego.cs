using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Arma;
using Bomberman.Personaje;
using Bomberman.Articulo;
using Bomberman.Mapa.Casilla;

namespace Bomberman.Juego
{
    public class Juego
    {
        private int cantDeVidas;
        private bool juegoPausado;
        private Personaje.Personaje protagonista;
        private Mapa.Mapa ambiente;
        private List<IMovible> objetosContundentes;
        private List<IMovible> enemigosVivos;
        private List<IDependienteDelTiempo> dependientesDelTiempo;
        private Salida salida;


        //declaracion del Singleton
        private static Juego instanciaDeJuego;
        
        //Constantes
        private const int VIDAS = 3;
        private const int ANCHOMAPA = 17;
        private const int ALTOMAPA = 13;

        //propiedades
        public int CantDeVidas
        {
            get { return cantDeVidas; }
            set { this.cantDeVidas = value; }
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

        //constructor
        public Juego()
        {
            this.JuegoPausado = false;
            this.CantDeVidas = VIDAS;
            this.Ambiente = new Mapa.Mapa(ANCHOMAPA,ALTOMAPA);
            Punto posicion = new Punto(0, 0);
            this.protagonista = new Personaje.Bombita(posicion);
            enemigosVivos = new List<IMovible>();
            this.objetosContundentes = new List<IMovible>();
            this.dependientesDelTiempo = new List<IDependienteDelTiempo>();
            this.salida = new Salida();
            CargarMapa();
            this.Ambiente.AgregarPersonaje(this.protagonista);

            generarEnemigos();
            //aca se carga el template del mapa

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

        public void PausarJuego()
        {
            this.JuegoPausado = true; 
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
                this.Ambiente.NivelTerminado = true;
                this.Ambiente.NivelGanado = false;
            }
        }

        public void generarEnemigos()
        {
            Punto pto = new Punto(14, 1);
            IMovible enem = new LosLopezReggae(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(12, 2);
            enem = new LosLopezReggae(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(2, 2);
            enem = new LosLopezReggaeAlado(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(1, 4);
            enem = new LosLopezReggaeAlado(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            enem = new Cecilio(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(4, 4);
            enem = new Cecilio(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(8, 6);
            enem = new Cecilio(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
        }

        public int CantidadEnemigosVivos()
        {
            return this.enemigosVivos.Count;
        }

        public void ObjetoContundenteLanzado(IDependienteDelTiempo obj)
        {
            this.dependientesDelTiempo.Add(obj);
            IMovible movil = (IMovible)obj;
            this.Ambiente.ObtenerCasilla(movil.Posicion).Transitar(movil);

        }

        public void AlojarExplosivo(Explosivo exp)
        {
            this.Ambiente.ObtenerCasilla(exp.Posicion).PlantarExplosivo(exp);
            this.dependientesDelTiempo.Add(exp);

        }

        public void AvanzarElTiempo()
        {
            foreach (IDependienteDelTiempo i in dependientesDelTiempo)
            {
                i.CuandoPasaElTiempo();
            }
            for (int j = 0; j<dependientesDelTiempo.Count; j++)
            {
                if (dependientesDelTiempo[j].DejoDeDependerDelTiempo())
                    dependientesDelTiempo.RemoveAt(j);
            }
            

           if (CantidadEnemigosVivos() == 0)
            {
                ActivarSalida();
            }
        }

        public void ActivarSalida()
        {
            this.salida.Activar();
        }

        public void CargarMapa()
        {
            List<Punto> obs = new List<Punto>() ;
            obs.Add(new Punto(0, 2));
            obs.Add(new Punto(0, 5));
            obs.Add(new Punto(0, 7));
            obs.Add(new Punto(0, 9));
            obs.Add(new Punto(2, 0));
            obs.Add(new Punto(2, 3));
            obs.Add(new Punto(2, 8));
            obs.Add(new Punto(2, 11));
            obs.Add(new Punto(2, 12));
            obs.Add(new Punto(3, 10));
            obs.Add(new Punto(4, 7));
            obs.Add(new Punto(4, 10));
            obs.Add(new Punto(4, 11));
            obs.Add(new Punto(5, 12));
            obs.Add(new Punto(6, 2));
            obs.Add(new Punto(6, 4));
            obs.Add(new Punto(6, 5));
            obs.Add(new Punto(6, 6));
            obs.Add(new Punto(7, 2));
            obs.Add(new Punto(7, 4));
            obs.Add(new Punto(7, 10));
            obs.Add(new Punto(8, 9));
            obs.Add(new Punto(9, 8));
            obs.Add(new Punto(9, 10));
            obs.Add(new Punto(10, 7));
            obs.Add(new Punto(10, 10));
            obs.Add(new Punto(11, 6));
            obs.Add(new Punto(12, 7));
            obs.Add(new Punto(12, 1));
            obs.Add(new Punto(12, 3));
            obs.Add(new Punto(14, 2));
            obs.Add(new Punto(14, 5));
            obs.Add(new Punto(14, 8));
            obs.Add(new Punto(14, 10));
            obs.Add(new Punto(14, 12));
            obs.Add(new Punto(15, 8));
            obs.Add(new Punto(16, 1));
            obs.Add(new Punto(16, 2));
            obs.Add(new Punto(16, 5));
            obs.Add(new Punto(16, 9));
            //cargo los bloques de ladrillo
            Casilla unaCasilla;
            foreach (Punto p in obs)
            {
                unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(p);
                this.ambiente.AgregarCasilla(unaCasilla);
            }
            obs.Clear();
            //cargo los bloques de cemento
            obs.Add(new Punto(0, 10));
            obs.Add(new Punto(2, 6));
            obs.Add(new Punto(4, 3));
            obs.Add(new Punto(6, 9));
            obs.Add(new Punto(8, 12));
            obs.Add(new Punto(9, 4));
            obs.Add(new Punto(10, 5));
            obs.Add(new Punto(10, 9));
            foreach (Punto p in obs)
            {
                unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(p);
                this.ambiente.AgregarCasilla(unaCasilla);
            }
            obs.Clear();
            //fabrico Aceros y pasillos
            Punto unaPosicion;
            int i, j;
            for (i = 0; i < ANCHOMAPA; i++)
                for (j = 0; j < ALTOMAPA; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
                        this.ambiente.AgregarCasilla(unaCasilla);
                    }
                    else
                    {
                        //uno de los dos es par, completo con pasillos donde no hay nada
                        if (!this.ambiente.ExisteCasillaEnPosicion(unaPosicion))
                        {
                            unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                            this.ambiente.AgregarCasilla(unaCasilla);
                        }
                    }
                    
                }

        }

        public void CargarArticulos()
        {
            // cargo ArticulosBombaToleTole
            List<Punto> obs = new List<Punto>();
            Casilla unaCasilla;
            obs.Add(new Punto(7, 2));
            obs.Add(new Punto(9, 4));
            obs.Add(new Punto(6, 5));
            obs.Add(new Punto(14, 8));
            obs.Add(new Punto(4, 10));
           // Articulo.Articulo art = new Articulo.ArticuloBombaToleTole();
            foreach (Punto p in obs)
            {
                unaCasilla = this.ambiente.ObtenerCasilla(p);
                unaCasilla.agregarArticulo(new Articulo.ArticuloBombaToleTole());
            }
            obs.Clear();

            // cargo Timer
            obs.Add(new Punto(2, 8));
            obs.Add(new Punto(5, 9));
            obs.Add(new Punto(11, 3));
            obs.Add(new Punto(11, 6));
            obs.Add(new Punto(11, 11));
            foreach (Punto p in obs)
            {
                unaCasilla = this.ambiente.ObtenerCasilla(p);
                unaCasilla.agregarArticulo(new Articulo.Timer());
            }
            obs.Clear();

            // cargo Chala
            obs.Add(new Punto(3, 3));
            obs.Add(new Punto(7, 7));
            obs.Add(new Punto(8, 12));
            obs.Add(new Punto(9, 10));
            obs.Add(new Punto(13, 9));
            foreach (Punto p in obs)
            {
                unaCasilla = this.ambiente.ObtenerCasilla(p);
                unaCasilla.agregarArticulo(new Articulo.Chala());
            }
            obs.Clear();

            // cargo Salida
           // unaCasilla = this.ambiente.ObtenerCasilla(new Punto(14, 2));
            //unaCasilla.agregarSalida();
        }
                
        //    for (i = 0; i < (dependientesDelTiempo.Count); i++)
        //        {
        //            dependientesDelTiempo[i]
        //        }
        //        for (i = 0; i < (dependientesDelTiempo.Count); i++)
        //        {
        //            if (((Explosivo)dependientesDelTiempo[i]).EstaExplotado())
        //                this.dependientesDelTiempo.RemoveAt(i);
        //        }
     }

}
