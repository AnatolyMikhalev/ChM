using System;

namespace lab1
{
    class Simpson
    {
        int m;
        int n;
        double[,] lambda;
        double a = 0.0; double A = 2; double b = 0.0; double B = 1;

        public void Func()
        {
            while (true)
            {
                Matrix();

                double h = (A - a) / (2.0 * n);
                double k = (B - b) / (2.0 * m);
                double[] x = new double[2 * n + 1];
                double[] y = new double[2 * m + 1];

                x[0] = a;
                y[0] = b;

                for (int i = 1; i <= 2 * n; i++)
                {
                    x[i] = x[i - 1] + h;
                }
                for (int j = 1; j <= 2 * m; j++)
                {
                    y[j] = y[j - 1] + k;
                }

                double result = 0.0;
                for (int j = 0; j <= 2 * m; j++)
                {
                    for (int i = 0; i <= 2 * n; i++)
                    {
                        if (y[j] <= Math.Sqrt(x[i]) && y[j] <= 1 / x[i])
                        {
                            result += lambda[j, i] * f(x[i], y[j]);
                        }
                    }
                }
                result *= ((h * k) / 9.0);
                double varJ = J();
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("|  n |  m |    Jточн   |  Jкуб.ф.С. | |Jточн-Jкуб.ф.С.| |");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("| {0,2} | {1,2} | {2,10} | {3,10:F7} |     {4,10:F7}    |", n, m, varJ, result, Math.Abs(varJ - result));
                Console.WriteLine("---------------------------------------------------------\n");
            }
        }

        double f(double x, double y) => (x + 6 * y);
        double J1(double x) => ((2 * Math.Pow(x, 5 / 2)) / 5 + (3 * Math.Pow(x, 2)) / 2);
        double J2(double x) => (x - 3 / x);
        double J() =>  (J1(1.0) - J1(0.0) + J2(2.0) - J2(1.0));

        void Matrix()
        {
            Console.Write("Введите n = ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите m = ");
            m = Convert.ToInt32(Console.ReadLine());

            lambda = new double[2 * m + 1, 2 * n + 1];

            for (int j = 0; j <= 2 * m; j++)
            {
                for (int i = 0; i <= 2 * n; i++)
                {
                    if (j % 2 == 0 && i % 2 == 0) lambda[j, i] = 4.0;
                    else if (j % 2 == 0 && i % 2 == 1) lambda[j, i] = 8.0;
                    else if (j % 2 == 1 && i % 2 == 0) lambda[j, i] = 8.0;
                    else if (j % 2 == 1 && i % 2 == 1) lambda[j, i] = 16.0;
                    if (j == 0 && i % 2 == 1) lambda[j, i] = 4.0;
                    else if (j == 0 && i % 2 == 0) lambda[j, i] = 2.0;
                    else if (j == 2 * m && i % 2 == 1) lambda[j, i] = 4.0;
                    else if (j == 2 * m && i % 2 == 0) lambda[j, i] = 2.0;
                    else if (i == 0 && j % 2 == 1) lambda[j, i] = 4.0;
                    else if (i == 0 && j % 2 == 0) lambda[j, i] = 2.0;
                    else if (i == 2 * n && j % 2 == 1) lambda[j, i] = 4.0;
                    else if (i == 2 * n && j % 2 == 0) lambda[j, i] = 2.0;
                }
            }
            lambda[0, 0] = 1.0;
            lambda[0, 2 * n] = 1.0;
            lambda[2 * m, 0] = 1.0;
            lambda[2 * m, 2 * n] = 1.0;
        }

    }
}
