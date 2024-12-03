using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualBasic;

public class Program
{
    public static void Main(string[] args)
    {
        var data = File.ReadAllText(args[0]);

        Console.WriteLine(args[1] == "p1" ? Part1(data) : Part2(data));
    }

    public static int Part1(string data)
    {
        var regex = new Regex(@"(mul\()\d*,\d*\)");
        var res = regex.Matches(data);

        var total = 0;
        foreach (var item in res)
        {
            var str = item.ToString();
            var final = str[4..(str.Length - 1)];
            var split = final.Split(',');
            total += int.Parse(split[0]) * int.Parse(split[1]);
        }

        return total;
    }

    public static int Part2(string data)
    {
        while (true)
        {
            var previous = data;
            var firstDont = data.IndexOf("don't()");
            var firstDo = data.IndexOf("do()");

            if (firstDont == -1)
                break;

            var distance = firstDo - firstDont;

            if (firstDo < firstDont)
            {
                data = data[..firstDo] + data[(firstDo + 4)..];
                continue;
            }

            data = data.Replace(data.Substring(firstDont, distance + 4), "");

            if (previous == data)
                break;
        }

        return Part1(data);
    }
}