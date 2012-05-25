using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hola Bomberman!");
            //modifica copia de la copia
            Console.WriteLine("modifica copia de la copia");
            //prueba en clase
            Console.WriteLine("algo");
            Console.WriteLine("Estoy dibujando a mi clase cuadrado:");
            Cuadrado uncuadrado = new Cuadrado();
            uncuadrado.dibujate();
            //Modificacion de Andy
            PruebaAndy prueba=new PruebaAndy();
            prueba.saludoAndy();
            Console.WriteLine("Escriba su mensaje y presione Enter para salir..");
            Rana unarana =new Rana();
            unarana.croar();
            
            Console.ReadLine();
        }
    }
}
