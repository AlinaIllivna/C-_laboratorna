using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab1
{
    public class TestCollections
    {
        private readonly List<Edition> _editions=new();
        private readonly List<string> _strings=new();
        private readonly Dictionary<Edition, Magazine> _editionDict=new();
        private readonly Dictionary<string, Magazine> _stringDict=new();

        public TestCollections(int n)
        {
        

            for (int i = 0; i < n; i++)
            {
                Magazine m = GenerateElement(i);

                _editions.Add(m);
                _strings.Add(m.Name);

                _editionDict.Add(m, m);
                _stringDict.Add(m.Name, m);
            }
        }

        //  генерація елементів
        public static Magazine GenerateElement(int i) => new Magazine( $"Magazine {i}",Frequency.Monthly, DateTime.Now.AddDays(-i),1000 + i);
        

        // метод вимірювання часу
        public void MeasureSearch()
        {
            if (_editions.Count == 0) return;

            Edition first = GenerateElement(0);
            Edition middle = GenerateElement(_editions.Count / 2);
            Edition last = GenerateElement(_editions.Count - 1);
            Edition notExist = new Edition("None", DateTime.Now, 1);

            Stopwatch sw = new Stopwatch();

            Console.WriteLine("=== LIST<Edition> ===");

            sw.Start();
            _editions.Contains(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            _editions.Contains(middle);
            sw.Stop();
            Console.WriteLine($"Middle: {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            _editions.Contains(last);
            sw.Stop();
            Console.WriteLine($"Last: {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            _editions.Contains(notExist);
            sw.Stop();
            Console.WriteLine($"Not exist: {sw.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine("\n=== DICTIONARY KEY ===");

            sw.Restart();
            _editionDict.ContainsKey(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            _editionDict.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine($"Middle: {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            _editionDict.ContainsKey(last);
            sw.Stop();
            Console.WriteLine($"Last: {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            _editionDict.ContainsKey(notExist);
            sw.Stop();
            Console.WriteLine($"Not exist: {sw.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine("\n=== DICTIONARY VALUE ===");

            sw.Restart();
            _editionDict.ContainsValue((Magazine)first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds} ms");
        }
    }
}