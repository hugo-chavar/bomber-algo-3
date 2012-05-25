using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Rana
    {
        private int xval;
        public int X
        {
            get
            {
                return xval;
            }
            set
            {
                if (value < 100)
                    xval = value;
            }
        }
        public void DisplayX()
        {
            Console.WriteLine("The stored value is: {0}", xval);
        }

        public void croar()
        {
            Console.WriteLine("te cambie todo, croando rana nro: ",X);
        }
    }}
