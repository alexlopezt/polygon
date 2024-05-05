using System.Drawing;

namespace ProyectoAviones
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Poligono pol = new Poligono(creaVertices());
            // pol.calcularMinMax();
            // Console.WriteLine("{0}, {1} - {2},{3}", pol.superior.X, pol.superior.Y, pol.inferior.X, pol.inferior.Y);
            // Linea l1 = new Linea(new Point(1,1), new Point(18,7));
            // Linea l2 = new Linea(new Point(20,0), new Point(19,8));

            // bool seCruzan = l1.hayInterseccion(l2);

            // Console.WriteLine("Se cruzan?: {0}", seCruzan);

            // Linea l1 = new Linea(new Point(5,1), new Point(5,20));
            // Linea l2 = new Linea(new Point(3,1), new Point(3,20));
            // l1.FindIntersection(l2);
            Poligono pol = new Poligono(creaVertices());
            bool resultado = pol.estaDentro(new Point(10,26));
            Console.WriteLine("Está dentro?: {0}",resultado);
        }

        static List<Point> creaVertices()
        {

            Point uno = new Point(10, 10);
            Point dos = new Point(30, 11);
            Point tres = new Point(11, 30);

            List<Point> pp = new List<Point>();
            pp.Add(uno);
            pp.Add(dos);
            pp.Add(tres);

            return pp;
        }
    }
}
