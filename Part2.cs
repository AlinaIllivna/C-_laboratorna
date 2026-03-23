using System;



namespace Lab1
{
    class Part2
    {
        public static void Run()
        {
            // Edition comparison
            Edition e1 = new ("Magazine", new DateTime(2024, 1, 1), 1000);
            Edition e2 = new ("Magazine",  new DateTime(2024, 1, 1) , 1000);

            Console.WriteLine("Reference equals: " + ReferenceEquals(e1, e2));
            Console.WriteLine("Objects equal: " + e1.Equals(e2));
            Console.WriteLine("Hash1: " + e1.GetHashCode());
            Console.WriteLine("Hash2: " + e2.GetHashCode());

            //   try/catch
            try
            {
                e1.Circulation = -3;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            //   Magazine створення
            Magazine magazine = new Magazine();

            Console.WriteLine("\nShort info:");
            Console.WriteLine(magazine.ToShortString());

            Console.WriteLine("\nFrequency check:");
            Console.WriteLine($"Weekly: {magazine[Frequency.Weekly]}");
            Console.WriteLine($"Monthly: {magazine[Frequency.Monthly]}");
            Console.WriteLine($"Yearly: {magazine[Frequency.Yearly]}");

            //  редактори
            Person p1 = new Person("Alina", "Illivna", DateTime.Now);
            Person p2 = new Person("Test", "User", DateTime.Now);
            Person p3 = new Person("NoArticle", "Editor", DateTime.Now);

            magazine.AddEditors(p1, p2, p3);

            //  статті
            magazine.AddArticles(
                new Article(p1, "C# Basics", 4.5),
                new Article(p2, "OOP in C#", 5.0),
                new Article(new Person("Other", "Author", DateTime.Now), "Java", 3.5)
            );

            Console.WriteLine("\n--- Full magazine info ---");
            Console.WriteLine(magazine);

            //   Edition property
            Console.WriteLine("\n--- Edition from Magazine ---");
            Console.WriteLine(magazine.Edition);

            //  DeepCopy
            Console.WriteLine("\n--- DeepCopy test ---");

            Magazine copy = (Magazine)magazine.DeepCopy();

            // змінюємо оригінал
            magazine.AddArticles(new Article(new Person(), "NEW ARTICLE", 1));

            Console.WriteLine("Original:");
            Console.WriteLine(magazine);

            Console.WriteLine("Copy (must stay unchanged):");
            Console.WriteLine(copy);

            Console.WriteLine($"Same object? {ReferenceEquals(magazine, copy)}");

            //   foreach (double)
            Console.WriteLine("\n--- Articles with rating > 4 ---");

            foreach (Article article in magazine.GetArticlesWithRatingGreaterThan(4))
            {
                Console.WriteLine(article);
            }

            //   foreach (string)
            Console.WriteLine("\n--- Articles with 'C#' in title ---");

            foreach (Article article in magazine.GetArticlesWithTitleContaining("C#"))
            {
                Console.WriteLine(article);
            }

            Console.WriteLine("\n--- Articles where author is NOT editor ---");

            foreach (var item in magazine)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n--- Articles by editors ---");

            foreach (Article article in magazine.GetArticlesByEditors())
            {
                Console.WriteLine(article);
            }

            Console.WriteLine("\n--- Editors without articles ---");

            foreach (Person editor in magazine.GetEditorsWithoutArticles())
            {
                Console.WriteLine(editor);
            }
        }
    }
}