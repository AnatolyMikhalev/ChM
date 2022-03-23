using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Sweep
    {
        double alpha0 = -1, alpha1 = 1;
        double beta0 = 0, beta1 = 1;
        double a = 0, b = 1;
        double A = 0, B = 1.38;
        double h;
        double y0;
        int  n;
        double[] mi;
        double[] ri;
        double[] fii;
        double[] ci;
        double[] di;
        double[] xi;
        double[] yi;

        double Pi(double x) => (2 * x);
        double Qi(double x) => (2);
        double Fi(double x) => (2 * (5 - 2 * x));
        double Mi(double x) => ( (2 * Math.Pow(h, 2) * Qi(x) - 4) / (2 + h * Pi(x)) );
        double Ri(double x) => ( (2 - h * Pi(x)) / (2 + h * Pi(x)) );
        double FIi(double x) => ( (2 * Math.Pow(h, 2) * Fi(x)) / (2 + h * Pi(x)) );
        double Ci(int i) => (1 / (mi[i] - ri[i] * ci[i]));
        double Di(int i) => (fii[i] - ri[i] * ci[i - 1] * di[i - 1]);
        double Yi(int i) => (di[i] * (ci[i] - yi[i + 1]));


        public void Func()
        {
            while (true)
            {
                Console.Write("Введите n: ");
                n = Convert.ToInt32(Console.ReadLine());

                Forward_stroke();
                Reverse_stroke();

                Print();

            }
        }
        void Forward_stroke()
        {
            double x = a;
            xi = new double[n + 1];
            xi[0] = a;
            xi[n] = b;

            h = (b - a) / n;

            mi = new double[n + 1];
            ri = new double[n + 1];
            fii = new double[n + 1];

            for (int i = 1; i <= n - 1; i++)
            {
                x = x + h;
                xi[i] = x;
            }

            for (int i = 1; i <= n - 1; i++)
            {

                mi[i] = Mi(xi[i]);
                ri[i] = Ri(xi[i]);
                fii[i] = FIi(xi[i]);

            }

            ci = new double[n + 1];
            di = new double[n + 1];

            ci[0] = alpha0 / (h * alpha1 - alpha0);
            di[0] = (A * h) / alpha0;

            for (int i = 1; i <= n - 1; i++)
            {
                ci[i] = Ci(i);
                di[i] = Di(i);
            }
        }
        void Reverse_stroke()
        {
            double x = b;

            h = (b - a) / n;

            yi = new double[n + 1];

            yi[n] = (B * h + beta0 * ci[n - 1] * di[n - 1]) / (beta0 * (1 + ci[n - 1]) + h * beta1);

            for (int i = n - 1; i >= 0; i--)
            {
                yi[i] = Yi(i);
            }

            y0 = (A * h - alpha0 * yi[1]) / (h * alpha1 - alpha0);
        }

        void Print()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|   n   |   xi   |  {"yi", 17}  |   mi   |   ri   |   fii  |   ci   |   di   |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            for (int i = 0; i <= n; i++)
            {
                Console.WriteLine("| {0,4}  | {1,6:F3} | {2,19:F15} | {3,6:F3} | {4,6:F3} | {5,6:F3} | {6,6:F3} | {7,6:F3} |", n, xi[i], yi[i], mi[i], ri[i], fii[i], ci[i], di[i]);
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------\n");
            Console.WriteLine($"y0 = {y0}; yi[0] = {yi[0]}; y0 - yi[0] = {y0 - yi[0],10:F7}\n");

        }


    }
}
