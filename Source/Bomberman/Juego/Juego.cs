using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Arma;
using Bomberman.Personaje;
using Bomberman.Articulo;

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
        private const int ANCHOMAPA = 20;
        private const int ALTOMAPA = 20;

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

        //constructor
        public Juego()
        {
            this.JuegoPausado = false;
            this.CantDeVidas = VIDAS;
            this.Ambiente = new Mapa.Mapa(ANCHOMAPA,ALTOMAPA);
            Punto posicion = new Punto(0, 0);
            this.protagonista = new Personaje.Bombita(posicion);
            this.Ambiente.AgregarPersonaje(this.protagonista);
            this.objetosContundentes = new List<IMovible>();
            this.salida = new Salida();


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
            Punto pto = new Punto(17, 11);
            IMovible enem = new LosLopezReggae(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(3, 11);
            enem = new LosLopezReggae(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(9, 5);
            enem = new LosLopezReggaeAlado(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(1, 15);
            enem = new LosLopezReggaeAlado(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            enem = new Cecilio(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(9, 7);
            enem = new Cecilio(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
            pto = new Punto(19, 19);
            enem = new Cecilio(pto);
            this.Ambiente.AgregarPersonaje(enem);
            this.enemigosVivos.Add(enem);
        }

        public int EnemigosVivos()
        {
            return this.enemigosVivos.Count;
        }

        public void ObjetoContundenteLanzado(IMovible obj)
        {
            this.dependientesDelTiempo.Add((IDependienteDelTiempo)obj);
            this.Ambiente.ObtenerCasilla(obj.Posicion).Transitar(obj);

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
                if (i.DejoDeDependerDelTiempo())
                    dependientesDelTiempo.Remove(i);
            }

           if (EnemigosVivos() == 0)
            {
                ActivarSalida();
            }
        }

        public void ActivarSalida()
        {
            this.salida.Activar();
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
