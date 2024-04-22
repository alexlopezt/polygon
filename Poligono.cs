

using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ProyectoAviones
{
    public class Poligono
    {
        List<Point> vertices;
        public Point superior, inferior;
        public Poligono(List<Point> vertices)
        {
            this.vertices = vertices;
            calcularMinMax();
        }

        public bool estaDentro(Point punto)
        {
            int intersecciones = 0;
            List<Linea> lineasImaginariasDesdePunto = crearLineasImaginariasDesdePunto(punto);
            List<Linea> lineasPoligono = new List<Linea>();
            for (int i = 0; i < vertices.Count(); i++)
            {
                Linea lineaPoligono;
                if (i == vertices.Count() - 1)
                {
                    lineaPoligono = new Linea(vertices.ElementAt(i), vertices.ElementAt(0));
                }
                else
                {
                    lineaPoligono = new Linea(vertices.ElementAt(i), vertices.ElementAt(i + 1));
                }
                lineasPoligono.Add(lineaPoligono);
            }
            foreach (Linea imaginaria in lineasImaginariasDesdePunto)
            {
                intersecciones = 0;
                foreach (Linea lp in lineasPoligono)
                {
                    if (imaginaria.hayInterseccion(lp))
                    {
                        intersecciones++;
                    }
                }
                if (intersecciones % 2 == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Linea> crearLineasImaginariasDesdePunto(Point punto)
        {
            List<Linea> lineasImaginarias = new List<Linea>();
            lineasImaginarias.Add(new Linea(punto, new Point(punto.X + 1, superior.Y)));
            lineasImaginarias.Add(new Linea(punto, new Point(inferior.X, punto.Y)));
            lineasImaginarias.Add(new Linea(punto, new Point(punto.X+1, inferior.Y)));
            lineasImaginarias.Add(new Linea(punto, new Point(superior.X, punto.Y)));
            
            return lineasImaginarias;
        }


        private void calcularMinMax()
        {

            superior = vertices.ElementAt(0);
            inferior = vertices.ElementAt(0);
            foreach (Point p in this.vertices)
            {
                if (superior.X > p.X) { superior.X = p.X; }
                if (superior.Y > p.Y) { superior.Y = p.Y; }
                if (inferior.X < p.X) { inferior.X = p.X; }
                if (inferior.Y < p.Y) { inferior.Y = p.Y; }
            }

            superior.X -= 5;
            superior.Y -= 5;
            inferior.X += 5;
            inferior.Y += 5;
        }


    }
}


