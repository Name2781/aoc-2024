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
            int last = lineData[0];
            var fails = 0;
            var ascending = false;
            List<int> newList = [];

            var diff = Math.Abs(last - lineData[1]);
            if (diff < 3 && diff != 0) 
                newList.Add(last);
            else
                fails++;

            for (int i = 1; i < lineData.Count; i++)
            {
                var failed = false;
                var number = lineData[i];

                diff = Math.Abs(last - number);
                if (diff > 3 || diff == 0) 
                {
                    fails++;
                    failed = true;
                    if ((i + 1) != lineData.Count)
                    {
                        i++;
                        newList.Add(lineData[i]);
                        last = lineData[i];
                    }
                }

                if (i == 1)
                    ascending = last < number;
                else
                {
                    if ((ascending && last > number) || (!ascending && last < number))
                    {
                        if (!failed)
                        {
                            fails++;
                            failed = true;
                            if ((i + 1) != lineData.Count)
                            {
                                i++;
                                newList.Add(lineData[i]);
                                last = lineData[i];
                            }
                        }
                    }
                }

                if (!failed)
                {
                    last = number;
                    newList.Add(number);
                }
            }

            Console.WriteLine(fails);
            Console.WriteLine(string.Join(", ", lineData));
            Console.WriteLine(string.Join(", ", newList));

            if (fails == 0) totalSafe++;
            else if (fails == 1)
                if (CheckList(newList)) totalSafe++;
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