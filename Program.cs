using System;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== TEST COLLECTIONS (Standard vs Immutable vs Sorted) ===");

            TestCollections test = new TestCollections(20000);
            test.MeasureSearch();

            Console.WriteLine("\n=== MAGAZINE COLLECTION ===");

            MagazineCollection mc = new MagazineCollection();
            mc.AddDefaults();

            Console.WriteLine("\n--- DEFAULT DATA ---");
            Console.WriteLine(mc.ToString());

            Magazine m = new Magazine("My Magazine", Frequency.Monthly, DateTime.Now, 500);

            m.AddEditors(new Person("Іван", "Іванов", DateTime.Now));
            m.AddArticles(new Article(new Person("Петро", "Петров", DateTime.Now), "Test Article", 5));

            mc.AddMagazines(m);

            Console.WriteLine("\n--- AFTER ADDING DATA ---");
            Console.WriteLine(mc.ToString());
        }
    }
}