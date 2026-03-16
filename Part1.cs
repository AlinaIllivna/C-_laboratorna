using System;


class Part1
{
    static void SetBirthYear(Person person) => person.BirthYear = 1990;

    public static void Run()
    {
        Console.WriteLine("Enter nRows and nColumns separated by space:");

        string? input = Console.ReadLine();
          if (input == null)
          return;

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int nRows = int.Parse(parts[0]);
        int nColumns = int.Parse(parts[1]);

        int total = nRows * nColumns;
        int t=0,r=0;
 
        Person[] array1D = new Person[total];
        Person[,] array2D = new Person[nRows, nColumns];
      
        
        do
        {
            r++;
            t+=r;
        } while(t<total);

        Person[][] jagged = new Person[r][];

        //  створення jagged
        for (int i = 0; i < r-1; i++)
        {
            jagged[i] = new Person[i + 1];
            
            }
        jagged[r-1] = new Person[r  - (t - total)];
        
        // заповнення jagged
        for (int i= 0; i < jagged.Length; i++)
            for (int j = 0; j < jagged[i].Length; j++)
                jagged[i][j] = new Person();

        // заповнення одновимірного
        for (int i = 0; i < array1D.Length; i++)
            array1D[i] = new Person();
       
        // заповнення двовимірного
        for (int i = 0; i < nRows; i++)
            for (int j = 0; j < nColumns; j++)
                array2D[i, j] = new Person();

        int start, end;

        start = Environment.TickCount;

        for (int i = 0; i < array1D.Length; i++)
            SetBirthYear(array1D[i]);

        end = Environment.TickCount;
        Console.WriteLine($"Time for 1D array: {end - start} ms");

        start = Environment.TickCount;

        for (int i = 0; i < nRows; i++)
            for (int j = 0; j < nColumns; j++)
                SetBirthYear(array2D[i, j]);

        end = Environment.TickCount;
        Console.WriteLine($"Time for 2D rectangular array: {end - start} ms");

        start = Environment.TickCount;

        for (int i = 0; i < jagged.Length; i++)
        {
            var row = jagged[i];

            for (int j = 0; j < row.Length; j++)
                SetBirthYear(row[j]);
        }

        end = Environment.TickCount;
        Console.WriteLine($"Time for jagged array: {end - start} ms");
    }
}