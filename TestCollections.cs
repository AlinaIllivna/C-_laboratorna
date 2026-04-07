using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Lab1
{
    public class TestCollections
    {
        //  Standard
        private List<Edition> _editions = new();
        private Dictionary<Edition, Magazine> _editionDict = new();

        // Immutable
        private ImmutableList<Edition> _imList = ImmutableList<Edition>.Empty;
        private ImmutableDictionary<Edition, Magazine> _imDict = ImmutableDictionary<Edition, Magazine>.Empty;

        //  Sorted
        private SortedList<Edition, Magazine> _sortedList = new();
        private SortedDictionary<Edition, Magazine> _sortedDict = new();

        public TestCollections(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Magazine m = GenerateElement(i);

                // Standard
                _editions.Add(m);
                _editionDict.Add(m, m);

                // Immutable
                _imList = _imList.Add(m);
                _imDict = _imDict.Add(m, m);

                // Sorted
                _sortedList.Add(m, m);
                _sortedDict.Add(m, m);
            }
        }


      
    
        public static Magazine GenerateElement(int i) => new Magazine($"Magazine {i}", Frequency.Monthly, DateTime.Now.AddDays(-i), 1000 + i);

        public void MeasureSearch()
        {
            Edition first = GenerateElement(0);
            Edition middle = GenerateElement(_editions.Count / 2);
            Edition last = GenerateElement(_editions.Count - 1);
            Edition notExist = new Edition("None", DateTime.Now, 1);

            Stopwatch sw = new Stopwatch();

            Console.WriteLine("===== LIST =====");

            sw.Start();
            _editions.Contains(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();
            _editions.Contains(middle);
            sw.Stop();
            Console.WriteLine($"Middle: {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();
            _editions.Contains(last);
            sw.Stop();
            Console.WriteLine($"Last: {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();
            _editions.Contains(notExist);
            sw.Stop();
            Console.WriteLine($"Not exist: {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("\n===== DICTIONARY =====");

            sw.Restart();
            _editionDict.ContainsKey(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();
            _editionDict.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine($"Middle: {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();
            _editionDict.ContainsKey(last);
            sw.Stop();
            Console.WriteLine($"Last: {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("\n===== IMMUTABLE LIST =====");

            sw.Restart();
            _imList.Contains(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("\n===== IMMUTABLE DICTIONARY =====");

            sw.Restart();
            _imDict.ContainsKey(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("\n===== SORTED LIST =====");

            sw.Restart();
            _sortedList.ContainsKey(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("\n===== SORTED DICTIONARY =====");

            sw.Restart();
            _sortedDict.ContainsKey(first);
            sw.Stop();
            Console.WriteLine($"First: {sw.Elapsed.TotalMilliseconds}");
        }
    }
}