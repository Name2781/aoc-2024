using System.ComponentModel.Design;

public class Program
{
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);

        List<List<int>> data = [];

        foreach (var line in lines)
        {
            List<int> ints = [];

            foreach (var parse in line.Split(' '))
                ints.Add(int.Parse(parse));

            data.Add(ints);
        }

        Console.WriteLine(args[1] == "p1" ? Part1(data) : Part2(data));
    }

    public static int Part1(List<List<int>> data)
    {
        int totalSafe = 0;
        foreach (var lineData in data)
        {
            if (CheckList(lineData)) totalSafe++;
        }

        return totalSafe;
    }

    public static int Part2(List<List<int>> data)
    {
        int totalSafe = 0;
        foreach (var lineData in data)
        {
            for (int i = 0; i < lineData.Count; i++)
            {
                var data2 = new List<int>();
                for (int j = 0; j < lineData.Count; j++)
                {
                    if (i == j) continue;

                    data2.Add(lineData[j]);
                }
                if (CheckList(data2))
                {
                    totalSafe++;
                    break;
                }
            }
        }

        return totalSafe;
    }

    public static bool CheckList(List<int> data)
    {
        int last = data[0];
        var safe = true;
        var ascending = false;

        for (int i = 1; i < data.Count; i++)
        {
            var number = data[i];

            var diff = Math.Abs(last - number);
            if (diff > 3 || diff == 0) 
            {
                safe = false;
                break;
            }

            if (i == 1)
                ascending = last < number;
            else
            {
                if ((ascending && last > number) || (!ascending && last < number))
                {
                    safe = false;
                    break;
                }
            }

            last = number;
        }

        return safe;
    }
}