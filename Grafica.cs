namespace GrahamScanConvexHull
{
    public partial class Grafica : Form
    {
        private List<Punto> Puntos { get; set; }
        private List<Punto> Hull { get; set; }
        private Punto Centroid { get; set; }
        private bool Final { get; set; }


        public Grafica()
        {
            InitializeComponent();
            this.Height = 600;
            this.Width = 600;
            this.Puntos = new();
            this.Hull = new();
            this.Centroid = new();
        }

        public Grafica(List<Punto> pts, Punto cm)
        {
            InitializeComponent();
            this.Height = 500;
            this.Width = 700;
            this.Puntos = pts;
            this.Hull = new();
            this.Centroid = cm;
        }

        public Grafica(List<Punto> pts, Stack<Punto> hull, Punto cm)
        {
            InitializeComponent();
            this.Height = 500;
            this.Width = 700;
            this.Puntos = pts;
            this.Centroid = cm;
            this.Hull = new();

            foreach (Punto p in hull)
            {
                Hull.Add(p);
            }
        }

        public Grafica(List<Punto> pts, Stack<Punto> hull, Punto cm, bool final)
        {
            InitializeComponent();
            this.Height = 500;
            this.Width = 700;
            this.Final = final;
            this.Puntos = pts;
            this.Centroid = cm;
            this.Hull = new();

            foreach (Punto p in hull)
            {
                Hull.Add(p);
            }
        }

        private List<int []> TransformarParaGraficar(List<Punto> PtsInicio)
        {
            int Ox = Width / 2, Oy = Height / 2;
            List<int []> PtsFinal = new();

            foreach(Punto p in PtsInicio)
            {
                decimal x = (p.x * 0.075M + 1) * Ox;
                decimal y = -(p.y * 0.075M - 1) * Oy;
                int [] ptTr = {decimal.ToInt32(x),decimal.ToInt32(y)};
                PtsFinal.Add(ptTr);
            }

            return PtsFinal;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            List<int []> puntosGr = TransformarParaGraficar(Puntos);
            List<int []> hullGr = TransformarParaGraficar(Hull);
            List<int []> CM = TransformarParaGraficar(new List<Punto> { Centroid });
            int n = puntosGr.Count, m = hullGr.Count;

            Graphics g = e.Graphics;
            Font F = new("Times New Roman", 12.0f);

            Pen plumaHull = new(Color.LimeGreen, 3);
            Pen plumaLast = new(Color.Blue, 3);
            Pen plumaNext = new(Color.Crimson, 3);

            Brush pincelPuntos = new SolidBrush(Color.Black);
            Brush pincelHull = new SolidBrush(Color.Red);
            Brush pincelCM = new SolidBrush(Color.Goldenrod);

            if(!Final)
            {
                g.FillEllipse(pincelCM, CM[0][0], CM[0][1], 9, 9);
                g.DrawString("CM", F, pincelCM, new PointF(CM[0][0] - 20, CM[0][1] - 20));
            }

            for (int i=0; i<n; ++i)
            {
                int [] p = { puntosGr[i][0], puntosGr[i][1] };
                g.FillEllipse(pincelPuntos, p[0], p[1], 8, 8);
                g.DrawString($"{i+1}", F, pincelPuntos, new PointF(p[0]+5, p[1]+5));
            }

            foreach(int [] p in hullGr)
            {
                g.FillEllipse(pincelHull, p[0], p[1], 9, 9);
            }

            for(int i=1; i<m; ++i)
            {
                Pen pl = plumaNext;

                if (i>2 || Final)
                {
                    pl = plumaHull;
                }
                else if(i==2)
                {
                    pl = plumaLast;
                }

                g.DrawLine(pl, hullGr[i - 1][0], hullGr[i - 1][1],
                               hullGr[i][0], hullGr[i][1]);
            }

            if(m>1 && !Final)
            {
                g.DrawString("F", F, pincelPuntos, new PointF(hullGr[0][0] - 20, hullGr[0][1] - 20));
                g.DrawString("M", F, pincelPuntos, new PointF(hullGr[1][0] - 20, hullGr[1][1] - 20));

                if (m > 2)
                {
                    g.DrawString("O", F, pincelPuntos, new PointF(hullGr[2][0] - 20, hullGr[2][1] - 20));
                }
            }

            Console.Clear();
            Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();

            this.Close();
        }
    }
}