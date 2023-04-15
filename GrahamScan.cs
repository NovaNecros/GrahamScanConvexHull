namespace GrahamScanConvexHull
{
    internal class GrahamScan
    {
        private static Punto NextToTop(Stack<Punto> s)
        {
            Punto top = s.Pop();
            Punto sec = s.Peek();
            s.Push(top);

            return sec;
        }

        private static String PrintStack(Stack<Punto> pila)
        {
            String s = "";
            int n = pila.Count, i = 0;

            foreach (Punto p in pila)
            {
                s += $"({p.x},{p.y})";
                ++i;

                if (i < n)
                {
                    s += ", ";
                }
            }

            return s;
        }

        //Centroide geometrico de los puntos
        private static Punto Centroide(Punto [] points)
        {
            decimal cmx = 0, cmy = 0;
            int n = points.Length;

            foreach (Punto p in points)
            {
                cmx += p.x;
                cmy += p.y;
            }

            return new Punto(cmx / n, cmy / n);
        }

        //Traslada los puntos
        private static Punto Displace(Punto p, Punto origin)
        {
            decimal x = p.x - origin.x;
            decimal y = p.y - origin.y;

            return new Punto(x, y);
        }

        //Obtiene la direccion en la que se avanza
        private static int Orientacion(Punto p, Punto q, Punto r)
        {
            decimal ori = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

            if (ori == 0)
            {
                return 0; //puntos colineales
            }

            return (ori > 0) ? 1 : 2; //1 = dextrogiro, 2 = levogiro
        }

        public static void GScan(Punto [] points)
        {
            int n = points.Length, min = 0;
            Punto cm = Centroide(points);
            List<Punto> orderedPoints = new(), pointsCMCopy = new();
            Punto[] pointsCM = new Punto[n];

            //Desplaza el centroide al origen
            for (int i = 0; i < n; ++i)
            {
                pointsCM[i] = Displace(points[i], cm);
                pointsCMCopy.Add(pointsCM[i]);
            }

            Array.Sort(pointsCM, Punto.ComparePoints);

            //Punto inferior
            decimal ymin = pointsCM[min].y;
            for (int i = 1; i < n; ++i)
            {
                decimal y = pointsCM[i].y;

                if (y < ymin || (ymin == y && pointsCM[i].x < pointsCM[min].x))
                {
                    ymin = y;
                    min = i;
                }
            }

            //Rotar arreglo de puntos
            if (min != 0)
            {
                Punto[] aux = new Punto[n];

                for (int i = 0; i < n; ++i)
                {
                    aux[i] = pointsCM[(i + min) % n];
                }

                for (int i = 0; i < n; ++i)
                {
                    pointsCM[i] = aux[i];

                }
            }

            //Desplazar a coordenadas originales
            for (int i = 0; i < n; ++i)
            {
                orderedPoints.Add(points[pointsCMCopy.IndexOf(pointsCM[i])]);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Grafica(orderedPoints, cm));

            Stack<Punto> hull = new();
            hull.Push(orderedPoints[0]);
            hull.Push(orderedPoints[1]);

            for (int i = 2; i <= n; ++i)
            {
                while (hull.Count() > 1 && 
                    Orientacion(NextToTop(hull), hull.Peek(), orderedPoints[i%n]) != 2)
                {
                    hull.Push(orderedPoints[i%n]);
                    Application.Run(new Grafica(orderedPoints, hull, cm));
                    hull.Pop();
                    hull.Pop();
                }

                hull.Push(orderedPoints[i%n]);
                Application.Run(new Grafica(orderedPoints, hull, cm));
            }

            Application.Run(new Grafica(orderedPoints, hull, cm, true));

            hull.Pop();
            Console.Clear();
            Console.WriteLine("\nConvex Hull:\n");
            Console.WriteLine(PrintStack(hull));
        }
    }
}
