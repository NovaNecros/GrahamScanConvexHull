namespace GrahamScanConvexHull
{
    public class Punto
    {
        public decimal x { get; set; }
        public decimal y { get; set; }
        private int cuadrante { get; set; }
        private decimal tangente { get; set; }

        public Punto() { }
        public Punto(decimal x, decimal y)
        {
            this.x = x;
            this.y = y;

            if (y >= 0)
            {
                if (x >= 0)
                {
                    this.cuadrante = 1;
                }
                else
                {
                    this.cuadrante = 2;
                }
            }
            else
            {
                if (x <= 0)
                {
                    this.cuadrante = 3;
                }
                else
                {
                    this.cuadrante = 4;
                }
            }

            if (x != 0)
            {
                this.tangente = y / x;
            }
            else if (y == 0)
            {
                tangente = 0;
            }
            else if (y > 0)
            {
                this.tangente = decimal.MaxValue;
            }
            else
            {
                this.tangente = decimal.MinValue;
            }
        }

        //Compara posicion angular
        public static int ComparePoints(Punto p, Punto q)
        {
            if(p.cuadrante == q.cuadrante)
            {
                return p.tangente.CompareTo(q.tangente);
            }

            return p.cuadrante.CompareTo(q.cuadrante);
        }
    }
}
