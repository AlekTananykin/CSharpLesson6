using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

/* Танынкин
 * 
 * Модифицировать программу нахождения минимума функции так, чтобы можно было 
 * передавать функцию в виде делегата. 
а) Сделать меню с различными функциями и представить пользователю выбор, для 
 *какой функции и на каком отрезке находить минимум. Использовать массив (или 
 *список) делегатов, в котором хранятся различные функции.
б) *Переделать функцию Load, чтобы она возвращала массив считанных значений. 
Пусть она возвращает минимум через параметр (с использованием модификатора out). 
 */
namespace Task2
{
    delegate double Func(double x);
    class Program
    {
        public static double XXMinus50XPlus10(double x)
        {
            return x * x - 50.0 * x + 10;
        }

        public static double XXPlusCoshX(double x)
        {
            return x * x + Math.Cosh(x);
        }

        public static double SinX(double x)
        {
            return Math.Sin(x);
        }

        public static void SaveFunc(Func func,
            double a, double b, double h, string fileName)
        {
            FileStream fs = new FileStream(
                fileName, FileMode.Create, FileAccess.Write);

            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;

            while (x <= b)
            {
                bw.Write(func(x));
                x += h;
            }
            bw.Close();
            fs.Close();
        }

        public static double[] Load(string fileName, out double min)
        {
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(
                    fileName, FileMode.Open, FileAccess.Read);

                br = new BinaryReader(fs);
                min = double.MaxValue;
                long pointsCount = fs.Length / sizeof(double);
                if (0 == pointsCount)
                    throw new FunctionException("Ошибка считывания данных");

                double[] fun = new double[pointsCount];
                double d;
                for (int i = 0; i < fs.Length / sizeof(double); ++i)
                {
                    fun[i] = d = br.ReadDouble();
                    if (d < min) min = d;
                }
                return fun;
            }
            catch (Exception ex)
            {
                throw new FunctionException(
                    "Ошибка считывания данных из файла.", ex);
            }
            finally
            {
                if (null != br)
                    br.Close();

                if (null != fs)
                    fs.Close();
            }
        }

        private struct FunctionData
        {
            public int _functionNumber;
            public double _beginInterval;
            public double _endInterval;
            public double _step;
        }

        static void Main(string[] args)
        {
            Func[] functions = new Func[3];

            functions[0] = new Func(XXMinus50XPlus10);
            functions[1] = new Func(XXPlusCoshX);
            functions[2] = new Func(SinX);

            FunctionData functionData;
            SelectFunctionAndInterval(out functionData);

            string filename = "data.bin";

            SaveFunc(functions[functionData._functionNumber], 
                functionData._beginInterval, 
                functionData._endInterval, functionData._step, filename);

            double min;
            double[] functionValues = Load(filename, out min);


            PrintFunctionValues(functionValues);
            Console.WriteLine("\nМинимальное значение функции: {0:f2}", min);

            Console.WriteLine("Нажмите любую клавишу для завершения программы.");
            Console.ReadKey();
        }

        private static void PrintFunctionValues(double[] functionValues)
        {
            Console.WriteLine("Значения выбранной функции, расчитанные по заданной сетке:");
            for (int i = 0; i < functionValues.Length; ++i)
                Console.Write("{0:f2}\t", functionValues[i]);
        }
        private static void SelectFunctionAndInterval(out FunctionData functionData)
        {
            int func;
            do
            {
                Console.Write("\nВвыберете функцию: \n" +
                    "1) f(x) = x * x - 50.0 * x + 10;\n" +
                    "2) f(x) = x * x + Math.Cosh(x)\n" +
                    "3) f(x) = sin(x)\nВведите число от 1 до 3:");

                if (!int.TryParse(Console.ReadLine(), out func))
                    continue;
            }
            while (1 > func || func > 3);
            functionData._functionNumber = func - 1;

            while (true)
            {
                functionData._beginInterval = ReadDouble("\nВведите начало интервала: ");
                functionData._endInterval = ReadDouble("Введите конец интервала: ");
                if (functionData._beginInterval >= functionData._endInterval)
                {
                    Console.WriteLine("Начало интервала должно быть меньше конца интервала. ");
                    continue;
                }
                break;
            }

            while(true)
            {
                functionData._step = ReadDouble("Введите шаг расчёта функции: ");
                if (0 >= functionData._step)
                {
                    Console.WriteLine("Шаг не может быть нулевым или меньше нуля. ");
                    continue;
                }
                break;
            }
        }

        private static double ReadDouble(string message)
        {
            double result;
            do
            {
                Console.Write(message);
            }
            while (!double.TryParse(Console.ReadLine(), out result));
            return result;
        }
        
    }
}
