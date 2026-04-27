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

            Console.WriteLine("\n=== MAGAZINE COLLECTION WITH EVENTS ===");

            MagazineCollection collection1 = new MagazineCollection();
            MagazineCollection collection2 = new MagazineCollection();

            collection1.CollectionName = "Collection 1";
            collection2.CollectionName = "Collection 2";

            Listener listener1 = new Listener();
            Listener listener2 = new Listener();

            // listener1 тільки для collection1
            collection1.MagazineAdded += listener1.OnMagazineAdded;
            collection1.MagazineReplaced += listener1.OnMagazineReplaced;

            // listener2 для обох
            collection1.MagazineAdded += listener2.OnMagazineAdded;
            collection1.MagazineReplaced += listener2.OnMagazineReplaced;

            collection2.MagazineAdded += listener2.OnMagazineAdded;
            collection2.MagazineReplaced += listener2.OnMagazineReplaced;

            // ДОДАВАННЯ
            collection1.AddDefaults();
            collection2.AddDefaults();
            collection1.RemoveAt(1);

            // ДОДАЄМО СВІЙ ЖУРНАЛ
            Magazine m = new Magazine("My Magazine", Frequency.Monthly, DateTime.Now, 500);

            m.AddEditors(new Person("Іван", "Іванов", DateTime.Now));
            m.AddArticles(new Article(new Person("Петро", "Петров", DateTime.Now), "Test Article", 5));

            collection1.AddMagazines(m);

            // ЗАМІНА через Replace
            collection1.Replace(0, new Magazine("Replaced Mag", Frequency.Monthly, DateTime.Now, 1000));

            // ЗАМІНА через індексатор
            collection2[1] = new Magazine("Indexed Mag", Frequency.Weekly, DateTime.Now, 2000);

            // ВИВІД
            Console.WriteLine("\n--- LISTENER 1 ---");
            Console.WriteLine(listener1.ToString());

            Console.WriteLine("\n--- LISTENER 2 ---");
            Console.WriteLine(listener2.ToString());

                  Console.WriteLine("\n=== TEST SERIALIZATION ===");

Magazine mag = new Magazine("Test Magazine", Frequency.Monthly, DateTime.Now, 1000);

mag.AddEditors(new Person("Іван", "Іванов", DateTime.Now));
mag.AddArticles(new Article(new Person("Петро", "Петров", DateTime.Now), "Article 1", 4.5));

Console.WriteLine("\n--- ORIGINAL ---");
Console.WriteLine(mag);

// SAVE
mag.Save("mag.dat");

// LOAD
Magazine loaded = new Magazine();
loaded.Load("mag.dat");

Console.WriteLine("\n--- LOADED ---");
Console.WriteLine(loaded);

// DEEP COPY
Magazine copy = (Magazine)mag.DeepCopy();

Console.WriteLine("\n--- COPY ---");
Console.WriteLine(copy);

// ADD FROM CONSOLE
Console.WriteLine("\n=== ADD FROM CONSOLE ===");
mag.AddFromConsole();

Console.WriteLine("\n--- AFTER ADD ---");
Console.WriteLine(mag);
        }
    }
}