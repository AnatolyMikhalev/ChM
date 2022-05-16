using System;

namespace lab3
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

        static double Func_f(double x) => (gamma * Math.Exp(m * x) + Math.Cos(gamma * x));
        static double Func_F(double x) => (alpha * Math.Exp(x) + beta * Math.Cos(gamma * x));
        static double Func_fi(double t) => (alpha * t + Math.Sin(beta * t));
        static double Func_psi(double t) => (Math.Exp(N * t) + M * Math.Sin(m * t) + N);


        static void Print()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");
            Console.Write("t:          |");

            for (int i = 0; i < o; i++)
                Console.Write($" x = {i*h, -5:F1}|");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");

            Console.Write("Вирт. слой: |");
            for (int i = 0; i <= n; i++)
            {
                Console.Write($"  {U_1[i],-8:F4}|");
            }
            Console.WriteLine();


            for (int j = 0; j < o; j++)
            {
                Console.Write($"t={j*k, -10:F5}|");
                for (int i = 0; i <= n; i++)
                {
                    Console.Write($"{U[i,j],8:F4}  |");
                }
                Console.WriteLine();
            }

            Console.ReadKey();


        }
        static void Main(string[] args)
        {
            for (int i = 0; i <= n; i++)        
                U[i, 0] = Func_f(x0 + i * h); // рассчитываем положение струны u(x, t) в момент времени: t = 0, j = 0

            for (int j = 0; j < o; j++)
            {
                U[0, j] = Func_fi(j * k);   // рассчитываем положение струны u(x, t) в точке x = 0
                U[n, j] = Func_psi(j * k);  // рассчитываем положение струны u(x, t) в точке x = l
            }


            for (int i = 1; i < n; i++)
                U_1[i] = U[i, 0] - k * Func_F(x0 + i * h);      // рассчитываем значения в "виртуальном" минус первом слое: j = -1

            for (int i = 1; i < n; i++)
            {
                U[i, 1] = U[i + 1, 0] + U[i - 1, 0] - U_1[i];   // рассчитываем значения в слое: j = 1
            }

            for (int j = 1; j < o; j++)
                for (int i = 1; i < n; i++)
                    U[i, j + 1] = U[i + 1, j] + U[i - 1, j] - U[i, j - 1];  // рассчитываем значения в слоях: j = [1, 10],  i = [1; n-1]

            Print();
        }
    }
}
