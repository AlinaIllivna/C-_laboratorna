using System;
using System.Text;


 enum Frequency
{
    Weekly,
    Monthly,
    Yearly
}

class Article
{
    public Person Author { get; init; }
    public string Title { get; init; }
    public double Rating { get; init; }

    //  Конструктор з параметрами
    public Article(Person author, string title, double rating)
    {
        Author = author;
        Title = title;
        Rating = rating;
    }

    //  Конструктор без параметрів
    public Article(): this(author: new Person(), title : "Title", rating:0){}
    

    public override string ToString()=> $"Author: {Author.ToShortString()}, Title: {Title}, Rating: {Rating}";
    
}

class Magazine
{
    private string _name=null!;
    private Frequency _frequency;
    private DateTime _releaseDate;
    private int _circulation;
    private Article[] _articles=null!;

    //  Конструктор з параметрами
    public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation)
    {
        Name = name;
        Frequency = frequency;
        ReleaseDate = releaseDate;
        Circulation = circulation;
        Articles = Array.Empty<Article>();
    }
     //  Конструктор без  параметрів
    public Magazine():this(name :"Default Magazine", frequency : Frequency.Monthly, releaseDate: DateTime.Now, circulation :1000){}
      
    public string Name
    {
        get => _name;
        init => _name = value;
    }

    public Frequency Frequency
    {
        get => _frequency;
        init => _frequency = value;
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        init => _releaseDate = value;
    }

    public int Circulation
    {
        get => _circulation;
        init => _circulation = value;
    }
    
    public Article[] Articles
    {
        get => _articles;
        init => _articles = value;
    }


    // Середній рейтинг статей
    public double AverageRating
    {
        get
        {
            if (_articles.Length == 0)
                return 0;

            double sum = 0;

            foreach (var article in _articles)
                sum += article.Rating;

            return sum / _articles.Length;
        }
    }
     
    // Індексатор
    public bool this[Frequency freq] => Frequency == freq;
    



     //Додавання нових статтей
    public void AddArticles(params Article[] newArticles)
    {  
        if(newArticles is null|| newArticles.Length==0) return;
        if(_articles is null|| _articles.Length==0)
         {
            _articles=newArticles;
            return;
        }

        int oldLength = _articles.Length;

        Array.Resize(ref _articles, oldLength + newArticles.Length);

        for (int i = 0; i < newArticles.Length; i++)
        {
            _articles[oldLength + i] = newArticles[i];
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("ABC", 100);
        sb.Append($"Magazine: {_name}\nFrequency: {_frequency}\nDate: {_releaseDate}\nCirculation: {_circulation}\nArticles:\n");
            

        foreach (var article in _articles)
        { 
            sb.AppendLine(article.ToString());
            
           
        }

        return sb.ToString();
    }

    public virtual string ToShortString()=> $"Magazine: {Name}, Frequency: {Frequency}, Average rating: {AverageRating}";
    
}

class Part2
{
    public static void Run()
    {
        Magazine magazine = new Magazine();

        Console.WriteLine("Short info:");
        Console.WriteLine(magazine.ToShortString());

        Console.WriteLine("\nFrequency check:");

        Console.WriteLine($"Weekly: {magazine[Frequency.Weekly]}");
        Console.WriteLine($"Monthly: {magazine[Frequency.Monthly]}");
        Console.WriteLine($"Yearly: {magazine[Frequency.Yearly]}");

        Article a1 = new Article(new Person(), "C# Basics", 4.5);
        Article a2 = new Article(new Person(), "OOP in C#", 5.0);

        magazine.AddArticles(a1, a2);

        Console.WriteLine("\nFull magazine info:");
        Console.WriteLine(magazine.ToString());

        Console.WriteLine("\n--- Time comparison for Article arrays ---");
        Console.WriteLine("Enter nRows and nColumns separated by space:");

        string? input = Console.ReadLine();
          if (input == null)
          return;

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int nRows = int.Parse(parts[0]);
        int nColumns = int.Parse(parts[1]);
        int total = nRows * nColumns;

        Article[] array1D = new Article[total];
        Article[,] array2D = new Article[nRows, nColumns];

        int t = 0, r = 0;

        do
        {
            r++;
            t += r;
            }
        while (t < total);

        Article[][] jagged = new Article[r][];

        //  створення jagged
        for (int i = 0; i < r-1; i++)
        {
            jagged[i] = new Article[i + 1];
            
            }
        jagged[r-1] = new Article[r  - (t - total)];

        // заповнення jagged
        for (int i= 0; i < jagged.Length; i++)
            for (int j = 0; j < jagged[i].Length; j++)
                jagged[i][j] = new Article();

        // заповнення одновимірного
        for (int i = 0; i < array1D.Length; i++)
            array1D[i] = new Article();
       
        // заповнення двовимірного
        for (int i = 0; i < nRows; i++)
            for (int j = 0; j < nColumns; j++)
                array2D[i, j] = new Article();


int start, end;

// 1D array
start = Environment.TickCount;

for (int i = 0; i < array1D.Length; i++)
{
     _ = array1D[i].Rating;
}

end = Environment.TickCount;
Console.WriteLine($"Time for 1D array: {end - start} ms");


// 2D array
start = Environment.TickCount;

for (int i = 0; i < nRows; i++)
    for (int j = 0; j < nColumns; j++)
    {
        _ = array2D[i, j].Rating;
    }

end = Environment.TickCount;
Console.WriteLine($"Time for 2D array: {end - start} ms");


// Jagged array
start = Environment.TickCount;

for (int i = 0; i < jagged.Length; i++)
{
    var row = jagged[i];

    for (int j = 0; j < row.Length; j++)
    {
        _ = row[j].Rating;
    }
}

end = Environment.TickCount;
Console.WriteLine($"Time for jagged array: {end - start} ms");
    }
}