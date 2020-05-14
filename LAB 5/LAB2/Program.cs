using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LAB2
{
    class Program
    {
        ////Функція вводу масиву з клавіатури
        static double[,] EnterArray(int n, string name)
        {
            Console.WriteLine(name + ":");
            double[,] arr = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr[i,j] = int.Parse(Console.ReadLine().ToString());
                }
            }
            return arr;
        }
        static double[,] EnterVector(int n, string name)
        {
            Console.WriteLine(name + ":");
            double[,] arr = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                        arr[i, j] = int.Parse(Console.ReadLine().ToString());
                    else
                        arr[i, j] = 0;

                }

            }
            return arr;
        }
        //Множення масиву на число
        public static double[,] MulArrNumber(double[,] vector, double number)
        {
            double[,] vectorNew = new double[vector.GetLength(0), vector.GetLength(0)];
            for (int i = 0; i < vector.GetLength(0); i++)
                for (int j = 0; j < vector.GetLength(0); j++)
                {
                    vectorNew[i, j] = vector[i, j] * number;

                }
            return vectorNew;
        }
        //множення масива на вектор
        public static double[,] MulArrVector(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(0); col++)
                {
                    res[col, 0] += A[col, row] * B[row, 0];
                }
            }
            return res;
        }
        public static double[,] MulArrArr(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int i = 0; i < B.GetLength(0); i++)
                for (int j = 0; j < B.GetLength(0); j++)
                {
                    res[i, j] = 0;
                    for (int k = 0; k < B.GetLength(0); k++)
                        res[i, j] += A[i, k] * B[k, j];
                }
                    return res;
        }
        //Додавання масивів
        public static double[,] AddArrs(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int i = 0; i < A.GetLength(1); i++)
            {
                for (int j = 0; j < A.GetLength(0); j++)

                    res[i, j] = A[i, j] + B[i, j];

            }
            return res;
        }
        // функція виводу масива або вектора
        public static void Show(double[,] arr, int n, string name)
        {
            Console.WriteLine(name);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(arr[i, j] + "\t\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть n:");
            int n = int.Parse(Console.ReadLine().ToString());
            double k = 10;
            double[,] A = EnterArray(n, "A");
            //Show(A, n, "A");

            double[,] b = new double [n,n];

            //Show(y1, n, "y1");

            double[,] A1 = EnterArray(n, "A1");
            double[,] b1 = EnterVector(n, "b1");
            double[,] c1 = EnterVector(n, "c1");

            double[,] A2 = EnterArray(n, "A2");
            double[,] B2 = EnterArray(n, "B2");
            double[,] C2 = new double[n, n];

            double[,] Y3 = new double[n, n];
            double[,] y1 = new double[n, n];
            double[,] Y2 = new double[n, n];
            double[,] X = new double[n, n];

            Task q1 = new Task(
            () =>
            {
                double[,] d1 = AddArrs(b1, c1);
                Y2 = MulArrVector(A1, d1);

            });
            Task q2 = new Task(
            () =>
            {
                double q;
                for (int i = 0; i < n; ++i)
                {

                    if ((i + 1) % 2 == 0)
                    {
                        q = ((i + 1) * (i + 1) + 2);
                        b[i, 0] = 1 / q;
                    }
                    else
                    {
                        q = (i + 1);
                        b[i, 0] = 1 / q;
                    }



                }

                y1 = MulArrVector(A, b);


            });
           Task q3 = new Task(
           () =>
            {
                double w;
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        w = (i + 1 + 2 * (j + 1));
                        C2[i, j] =1/w;
                    }
                }
               
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        X[i, j] = B2[i, j] - C2[i, j];
                    }
                }
               
                Y3 = MulArrArr(A2, X);
                

            }
            );
            q1.Start();
            q2.Start();
            q3.Start();
            Task.WaitAll(new Task[]
        {
            q1,q2,q3
        });
            Show(Y2, n, "Y2");
            Show(b, n, "b");
            Show(y1, n, "y1");
            Show(C2, n, "C2");
            Show(X, n, "X");
            Show(Y3, n, "Y3");

            double[,] Y3_2=new double[n, n];
            double[,] Y2_k = new double[n, n];
            double[,] g = new double[n, n];
            Task q4 = new Task(
           () =>
           {
               Y2_k = MulArrNumber(Y2, k);

               g = AddArrs(Y2, y1);

           });
            Task q5 = new Task(
          () =>
           {
               Y3_2 = MulArrArr(Y3, Y3);
              
           }
           );
            q4.Start();
            q5.Start();
            Task.WaitAll(new Task[]
            {
            q4,q5
            });
            Show(Y2_k, n, "Y2_k");
            Show(g, n, "g");
            Show(Y3_2, n, "Y3_2");
            double[,] J = new double[n, n];
            double[,] P = new double[n, n];
            Task q6 = new Task(
           () =>
           {
               J = MulArrVector(Y3_2, Y2);

           });
            Task q7 = new Task(
          () =>
           {
               P = MulArrVector(Y3, y1);
               
           }
           );
            q6.Start();
            q7.Start();
            Task.WaitAll(new Task[]
            {
            q6,q7
            });
            Show(J, n, "J");
            Show(P, n, "P");
            double [,] result = AddArrs(J, P);
            Show(result, n, "result");
            Console.ReadKey();
        }
    }
}
