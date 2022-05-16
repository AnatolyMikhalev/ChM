using System;

namespace lab4
{
    class Program
    {
        static double gamma = 0.6;
        static double m = -1.1;
        static double alpha = -1.3;
        static double beta = 0.6;
        static double M = -1.2;
        static double N = 1.3;

        static int l = 1;
        static int n = 10;
        static int o = 11;


        static double x0 = 0, x1 = x0 + l;
        static double h = l / Convert.ToDouble(n);
        static double k = h;

        static double[,] U = new double[n + 1, o + 1];
        static double[] U_1 = new double[n + 1];

        static double[] xi = new double[n];
        static double[] tj = new double[n + 1];

        static double Func_f(double x) => (gamma * Math.Exp(m * x) + Math.Cos(gamma * x));
        static double Func_fi(double t) => (alpha * t + Math.Sin(beta * t));
        static double Func_psi(double t) => (Math.Exp(N * t) + M * Math.Sin(N + m * t));


        static void Print(double[,] U)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");
            Console.Write("t:          |");
            for (int i = 0; i < o; i++)
                Console.Write($" x = {i * h, -5:F1}|");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");


            for (int j = 0; j < o; j++)
            {
                Console.Write($"t={j * k, -10:F5}|");
                for (int i = 0; i <= n; i++)
                {
                    Console.Write($"{U[i, j],8:F4}  |");
                }
                Console.WriteLine();
            }


        }

        static void YavnSchema()
        {
            k = h * h / 6;
            //int n = Convert.ToInt32((X1 - X0) / h);
            double[,] u = new double[n + 1, o];

            for (int i = 0; i <= n; i++)
                u[i, 0] = Func_f(x0 + i * h);    // рассчитываем положение струны u(x, t) в момент времени: t = 0, j = 0

            for (int j = 0; j < o; j++)
            {
                u[0, j] = Func_fi(k * j);  // рассчитываем положение струны u(x, t) в точке x = 0
                u[n, j] = Func_psi(k * j); // рассчитываем положение струны u(x, t) в точке x = l
            }


            for (int j = 0; j < o - 1; j++)
            {
                for (int i = 1; i < n; i++)
                    u[i, j + 1] = (u[i + 1, j] + 4 * u[i, j] + u[i - 1, j]) / 6.0;  // рассчитываем значения в слоях: j = [0, 9],  i = [1; n - 1]
            }
            Console.WriteLine("\n" + "Вычисления по явной схеме, шаг по времени={0:F5}", k);
            Print(u);
        }
        private static void NeyavnSchema(int s)
        {
            k = h * h / s;

            int n = Convert.ToInt32((x1 - x0) / h);
            double[,] u = new double[n + 1, o];
            double[,] a = new double[n + 1, o];
            double[,] b = new double[n + 1, o];

            for (int i = 0; i <= n; i++)
                u[i, 0] = Func_f(h * i);    // рассчитываем положение струны u(x, t) в момент времени: t = 0, j = 0

            for (int j = 0; j < o; j++)
            {
                u[0, j] = Func_fi(k * j);  // рассчитываем положение струны u(x, t) в точке x = 0
                u[n, j] = Func_psi(k * j); // рассчитываем положение струны u(x, t) в точке x = l
            }

            for (int j = 0; j < o - 1; j++)
            {
                a[1, j + 1] = 1.0 / (2 + s);
                b[1, j + 1] = Func_fi(k * j) + s * u[1, j];
            }

            for (int j = 0; j < o - 1; j++)
            {
                for (int i = 2; i < n; i++)
                {
                    a[i, j + 1] = 1.0 / (2 + s - a[i - 1, j + 1]);
                    b[i, j + 1] = a[i - 1, j + 1] * b[i - 1, j + 1] + s * u[i, j];
                }
                for (int i = n - 1; i > 0; i--)
                    u[i, j + 1] = a[i, j + 1] * (b[i, j + 1] + u[i + 1, j + 1]);    
            }
            Console.WriteLine($"\nВычисления по неявной схеме, шаг по времени={k,0:F5},   s = {s}    ");

            Print(u);
        }

        static void Main(string[] args)
        {

            YavnSchema();

            for(int i = 0; i < 3; i++)
            {
                Console.Write("Введите S: ");
                NeyavnSchema(Convert.ToInt32(Console.ReadLine()));
            }
        }
    }
}
