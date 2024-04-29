using System.Drawing;

namespace ProyectoAviones
{

    public class Linea
    {
        public Point origen;
        public Point final;


        public Linea(Point origen, Point final)
        {
            this.origen = origen;
            this.final = final;
        }

        public bool CheckIntersec(Linea l2)
        {
            
            double mySlope = (double)(this.final.Y - this.origen.Y) / (double)(this.final.X - this.origen.X);
            double l2Slope = (double)(l2.final.Y - l2.origen.Y) / (double)(l2.final.X - l2.origen.X);

            if (mySlope == l2Slope)
            {
                return false;
            }

            double b1 = this.origen.Y - mySlope * this.origen.X;
            double b2 = l2.origen.Y - l2Slope * l2.origen.X;

            double x = (b2 - b1) / (mySlope - l2Slope);
            double y = mySlope * x + b1;
            
            Console.WriteLine("El punto de intersección es: ({0}, {1})", x, y);

            if ((x >= this.origen.X && x <= this.final.X) || (x >= this.final.X && x >= this.origen.X))
            {
                if ((y <= this.origen.Y && y <= this.final.Y) || (y >= this.final.Y && y >= this.origen.Y))
                {
                    if ((x <= l2.origen.X && x <= l2.final.X) || (x >= l2.origen.X && x >= this.final.X))
                    {
                        if ((y <= l2.origen.Y && y <= l2.final.Y) || (y >= l2.final.Y && y >= this.origen.Y))
                        {
                            Console.WriteLine("El punto intersecciona las dos rectas");
                            return true;
                        }
                    }
                }
            }

            
            return false;
           
        }
        public bool HayInterseccion(Linea linea)
        {
            //Calculamos pendientes
            double m1 = Math.Round((double)(linea.final.Y - linea.origen.Y) / (double)(linea.final.X - linea.origen.X), 3);
            double m2 = Math.Round((double)(this.final.Y - this.origen.Y) / (double)(this.final.X - this.origen.X), 3);

            if (m1 == m2)
            {
                return false; //las pendientes son iguales implica que son paralelas
            }

            double interseccionX = Math.Round((m1 * linea.origen.X - m2 * this.origen.X + this.origen.Y - linea.origen.Y) / (m1 - m2), 3);
            double interseccionY = Math.Round(m1 * (interseccionX - linea.origen.X) + linea.origen.Y, 3);

            if ((interseccionX >= this.origen.X && interseccionX <= this.final.X) || (interseccionX >= this.final.X && interseccionX <= this.origen.X))
            {
                if ((interseccionY >= this.origen.Y && interseccionY <= this.final.Y) || (interseccionY >= this.final.Y && interseccionY <= this.origen.Y))
                {
                    return true;
                }
            }

            return false;


        }
    }
}