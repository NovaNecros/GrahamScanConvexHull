namespace GrahamScanConvexHull
{
    internal static class Program
    {
        [STAThread]
        static int Main(string [] args)
        {
            int n = 30;
            Punto [] points = new Punto[n];

            ////TEST DATA
            //points[0] = new Punto(4, -2);
            //points[1] = new Punto(7, 6);
            //points[2] = new Punto(0, 9);
            //points[3] = new Punto(-3, 4);
            //points[4] = new Punto(-8, -5);
            //points[5] = new Punto(6, -4);
            //points[6] = new Punto(3, 5);
            //points[7] = new Punto(-6, 1);

            //RANDOM DATA
            decimal x, y;
            Random rnd = new();
            for(int i=0; i<n; ++i)
            {
                x = 0.00002M * rnd.Next(0, 1000001)-10;
                y = 0.00002M * rnd.Next(0, 1000001)-10;
                points[i] = new Punto(x,y);
            }

            
            GrahamScan.GScan(points);

            return 69;
        }
    }
}