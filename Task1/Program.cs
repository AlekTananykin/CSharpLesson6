using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Танынкин
 * 
 * Изменить программу вывода таблицы функции так, чтобы можно было передавать 
 * функции типа double (double, double). Продемонстрировать работу на функции 
 * с функцией a*x^2 и функцией a*sin(x).
 */
namespace Task1
{
    public delegate double FunArgParam(double x, double param);
    class Program
    {

        public static void Table(FunArgParam F, double param, double x, double b)
        {
            Console.WriteLine("----- X -------- Y ---");
            while (x <= b)
            {
                Console.WriteLine("| {0, 8:0.000} | {1, 8:0.000}|", x, F(x, param));
                x += 1.0;
            }
            Console.WriteLine("----------------------");
        }

        public static double ASinX(double x, double param)
        {
            return param * Math.Sin(x);
        }

        public static double AXPow2(double x, double param)
        {
            return param * x * x;
        }

        static void Main(string[] args)
        {
            const double param = 0.5;
            Console.WriteLine("Таблица функции {0}*sin(x):", param);
            Table(new FunArgParam(ASinX), param, -2, 2);

            Console.WriteLine("\nТаблица функции {0}*x^2:", param);
            Table(AXPow2, param , -2, 2);

            Console.WriteLine("Нажмите любую клавишу для завершения программы.");
            Console.ReadKey();
        }
       
    }
}
