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

        public bool hayInterseccion(Linea linea)
        {
            //Calculamos pendientes
            double m1 = Math.Round((double)(linea.final.Y - linea.origen.Y) / (double)(linea.final.X - linea.origen.X),3);
            double m2 = Math.Round((double)(this.final.Y - this.origen.Y) / (double)(this.final.X - this.origen.X),3);

            if (m1 == m2)
            {
                return false; //las pendientes son iguales implica que son paralelas
            }

            double interseccionX = Math.Round((m1 * linea.origen.X - m2 * this.origen.X + this.origen.Y - linea.origen.Y) / (m1 - m2),3);
            double interseccionY = Math.Round(m1 * (interseccionX - linea.origen.X) + linea.origen.Y,3);

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