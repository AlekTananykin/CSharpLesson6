using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * **Считайте файл различными способами. Смотрите “Пример записи файла 
 * различными способами”. Создайте методы, которые возвращают массив 
 * byte (FileStream, BufferedStream), строку для StreamReader и 
 * массив int для BinaryReader.
 */
namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            BufferedStreamTest();
            FileStreamTest();
            BinaryStreamTest();

            Console.WriteLine("\nНажмите любую клавишу для завершения программы");
            Console.ReadKey();
        }

        private static void BufferedStreamTest()
        {
            Console.WriteLine("а) Чтение массива byte(FileStream, BufferedStream)");

            byte[] byteArray = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            const string filename = "byteArray.dat";
            WriteByteArray(byteArray, filename);
            byte[] bytesFromFile = ReadByteArray(filename);

            PrintArrayComparation(byteArray, bytesFromFile);
        }

        private static void WriteByteArray(byte[] array, string filename)
        {
            using (FileStream fs = new FileStream(
                filename, FileMode.Create, FileAccess.Write))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                bs.Write(array, 0, array.Length);
            }
        }

        private static byte[] ReadByteArray(string fileName)
        {
            byte[] byteArray = null;
            using (FileStream fs = new FileStream(
                fileName, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                int arraySize = (int)fs.Length;
                byteArray = new byte[arraySize];
                bs.Read(byteArray, 0, arraySize);
            }
            return byteArray;
        }

        private static void WriteString(string str, string filename)
        {
            using (FileStream fs = new FileStream(
                filename, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(str);
            }
        }
        private static string ReadString(string filename)
        {
            using (FileStream fs = new FileStream(
                filename, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                return sr.ReadLine();
            }
        }

        private static void FileStreamTest()
        {
            Console.WriteLine("\nб) Чтение строки (StreamReader)");

            const string filename = "stringArray.txt";
            string testString = "1234567890";
            WriteString(testString, filename);
            string fileString = ReadString(filename);
            Console.WriteLine("Исходная строка: " + testString);
            Console.WriteLine("Cтрока из файла: " + fileString);

            if (fileString == testString)
                Console.WriteLine("Строки одинаковы");
            else
                Console.WriteLine("Строки не одинаковы");
        }

        private static void WriteIntArray(int[] array, string filename)
        {
            using (FileStream fs = new FileStream(
                filename, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                for (int i = 0; i < array.Length; ++ i)
                    bw.Write(array[i]);
            }
        }

        private static int[] RreadIntArray(string filename)
        {
            int[] array = null;
            using (FileStream fs = new FileStream(
                filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                array = new int[fs.Length / 4];
                for (int i = 0; i < array.Length; ++ i)
                   array[i] = br.ReadInt32();
            }
            return array;
        }

        private static void BinaryStreamTest()
        {
            Console.WriteLine("\nв) Чтение массива массива int для (BinaryReader)");

            int[] intArray = new int[] { 
                1, -22, 333, -4444, 55555, 
                -666666, 7777777, -88888888 };

            const string filename = "intArray.dat";
            WriteIntArray(intArray, filename);
            int[] intsFromFile = RreadIntArray(filename);

            PrintArrayComparation(intArray, intsFromFile);
        }

        static private void PrintArray<T>(T[] array)
        {
            for (int i = 0; i < array.Length; ++ i)
                Console.Write(" " +  array[i]);
        }

        static private void PrintArrayComparation<T>(
            T[]origArray, T[]fileArray) where T: IComparable<T>
        {
            Console.Write("Исходный  массив: ");
            PrintArray(origArray);
            Console.WriteLine();
            Console.Write("Считанный массив: ");
            PrintArray(fileArray);

            bool arraysAreEqual = true;
            for (int i = 0; i < origArray.Length; ++i)
            {
                if (0 != fileArray[i].CompareTo(origArray[i]))
                {
                    arraysAreEqual = false;
                    break;
                }
            }

            if (arraysAreEqual)
                Console.WriteLine("\nМассивы одинаковы");
            else
                Console.WriteLine("\nМассивы не одинаковы");
        }
    }
}
