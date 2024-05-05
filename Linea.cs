using System.ComponentModel;
using System.Drawing;
using System.Net.WebSockets;

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
        /**
        Fuente https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines
    */
    public  bool FindIntersection(Linea l2, double tolerance = 0.001)
    {
        double x1 = this.origen.X, y1 = this.origen.Y;
        double x2 = this.final.X, y2 = this.final.Y;

        double x3 = l2.origen.X, y3 = l2.origen.Y;
        double x4 = l2.final.X, y4 = l2.final.Y;

        // equations of the form x=c (two vertical lines) with overlapping
        if (Math.Abs(x1 - x2) < tolerance && Math.Abs(x3 - x4) < tolerance && Math.Abs(x1 - x3) < tolerance)
        {
            throw new Exception("Both lines overlap vertically, ambiguous intersection points.");
        }

        //equations of the form y=c (two horizontal lines) with overlapping
        if (Math.Abs(y1 - y2) < tolerance && Math.Abs(y3 - y4) < tolerance && Math.Abs(y1 - y3) < tolerance)
        {
            throw new Exception("Both lines overlap horizontally, ambiguous intersection points.");
        }

        //equations of the form x=c (two vertical parallel lines)
        if (Math.Abs(x1 - x2) < tolerance && Math.Abs(x3 - x4) < tolerance)
        {   
            //return default (no intersection)
            return false;
        }

        //equations of the form y=c (two horizontal parallel lines)
        if (Math.Abs(y1 - y2) < tolerance && Math.Abs(y3 - y4) < tolerance)
        {
            //return default (no intersection)
            return false;
        }

        //general equation of line is y = mx + c where m is the slope
        //assume equation of line 1 as y1 = m1x1 + c1 
        //=> -m1x1 + y1 = c1 ----(1)
        //assume equation of line 2 as y2 = m2x2 + c2
        //=> -m2x2 + y2 = c2 -----(2)
        //if line 1 and 2 intersect then x1=x2=x & y1=y2=y where (x,y) is the intersection point
        //so we will get below two equations 
        //-m1x + y = c1 --------(3)
        //-m2x + y = c2 --------(4)

        double x, y;

        //lineA is vertical x1 = x2
        //slope will be infinity
        //so lets derive another solution
        if (Math.Abs(x1 - x2) < tolerance)
        {
            //compute slope of line 2 (m2) and c2
            double m2 = (y4 - y3) / (x4 - x3);
            double c2 = -m2 * x3 + y3;

            //equation of vertical line is x = c
            //if line 1 and 2 intersect then x1=c1=x
            //subsitute x=x1 in (4) => -m2x1 + y = c2
            // => y = c2 + m2x1 
            x = x1;
            y = c2 + m2 * x1;
        }
        //lineB is vertical x3 = x4
        //slope will be infinity
        //so lets derive another solution
        else if (Math.Abs(x3 - x4) < tolerance)
        {
            //compute slope of line 1 (m1) and c2
            double m1 = (y2 - y1) / (x2 - x1);
            double c1 = -m1 * x1 + y1;

            //equation of vertical line is x = c
            //if line 1 and 2 intersect then x3=c3=x
            //subsitute x=x3 in (3) => -m1x3 + y = c1
            // => y = c1 + m1x3 
            x = x3;
            y = c1 + m1 * x3;
        }
        //lineA & lineB are not vertical 
        //(could be horizontal we can handle it with slope = 0)
        else
        {
            //compute slope of line 1 (m1) and c2
            double m1 = (y2 - y1) / (x2 - x1);
            double c1 = -m1 * x1 + y1;

            //compute slope of line 2 (m2) and c2
            double m2 = (y4 - y3) / (x4 - x3);
            double c2 = -m2 * x3 + y3;

            //solving equations (3) & (4) => x = (c1-c2)/(m2-m1)
            //plugging x value in equation (4) => y = c2 + m2 * x
            x = (c1 - c2) / (m2 - m1);
            y = c2 + m2 * x;

            //verify by plugging intersection point (x, y)
            //in orginal equations (1) & (2) to see if they intersect
            //otherwise x,y values will not be finite and will fail this check
            if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                && Math.Abs(-m2 * x + y - c2) < tolerance))
            {
                //return default (no intersection)
                return false;
            }
        }

        //x,y can intersect outside the line segment since line is infinitely long
        //so finally check if x, y is within both the line segments
        if (IsInsideLine(this, x, y) &&
            IsInsideLine(l2, x, y))
        {
            return true;
        }

        //return false (no intersection)
        return false;

    }

    // Returns true if given point(x,y) is inside the given line segment
    private static bool IsInsideLine(Linea line, double x, double y)
    {
        return (x >= line.origen.X && x <= line.final.X
                    || x >= line.final.X && x <= line.origen.X)
               && (y >= line.origen.Y && y <= line.final.Y
                    || y >= line.final.Y && y <= line.origen.Y);
    }

          /*
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

            if ((x >= this.origen.X && x <= this.final.X) || (x >= this.final.X && x <= this.origen.X))
            {
                if ((y >= this.origen.Y && y <= this.final.Y) || (y >= this.final.Y && y <= this.origen.Y))
                {
                    if ((x >= l2.origen.X && x <= l2.final.X) || (x >= l2.origen.X && x <= this.final.X))
                    {
                        if ((y >= l2.origen.Y && y <= l2.final.Y) || (y >= l2.final.Y && y <= this.origen.Y))
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
        */
    }
}