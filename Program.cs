using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] texts = new string[1000];
            RandomTextGenerator(texts);
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Restart();
            RadixSort((string[])texts.Clone());
            stopwatch.Stop();
            Console.WriteLine("Radix " + stopwatch.ElapsedMilliseconds + "ms");

            Console.ReadKey();
        }

        static void RadixSort(string[] texts)
        {
            int maxIndex = 0;

            //Figure out maximum length
            foreach (string text in texts)
            {
                if (maxIndex < text.Length - 1)
                    maxIndex = text.Length - 1;
            }

            //Do counting sort for every letter
            for (int i = maxIndex; i >= 0; i--)
            {
                CountingSort(texts, i);
            }

            //PrintText(texts);
        }

        static void CountingSort(string[] texts, int index)
        {
            string[] output = new string[texts.Length];
            int[] count = new int[256];

            //count each occurrence
            foreach (string text in texts)
            {
                if (text.Length - 1 < index)
                    count[0]++;
                else
                {
                    count[text[index]]++;
                }
            }

            //add previous counts to the next so that they represent indexes
            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            //Build output array
            for (int i = texts.Length - 1; i >= 0; i--)
            {
                if (texts[i].Length - 1 < index)
                {
                    output[count[0] - 1] = texts[i];
                    count[0]--;
                }
                else
                {
                    output[count[texts[i][index]] - 1] = texts[i];
                    count[texts[i][index]]--;
                }
            }

            //Copy output to array so that it contains sorted texts
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = output[i];
            }
        }

        static void RandomTextGenerator(string[] texts)
        {
            Random random = new Random();
            for (int i = 0; i < texts.Length; i++)
            {
                int randomLength = random.Next(1, 20);
                string randomText = "";
                for (int j = 0; j < randomLength; j++)
                {
                    randomText += (char)random.Next(97, 123);
                }
                texts[i] = randomText;
            }
        }

        static void PrintText(string[] texts)
        {
            foreach (string item in texts)
            {
                Console.Write(item + ", ");
            }
        }
    }
}
