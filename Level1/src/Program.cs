public class Level1
{
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        
        List<int> Map1 = [];
        List<int> Map2 = [];

        foreach (var line in lines)
        {
            var split = line.Split("   ");

            Map1.Add(int.Parse(split[0]));
            Map2.Add(int.Parse(split[1]));
        }

        if (args[1] == "p1")
        {
            Map1.Sort();
            Map2.Sort();

            var total = 0;
            for (int i = 0; i < Map1.Count; i++)
            {
                total += Math.Abs(Map1[i] - Map2[i]);
            }

            Console.WriteLine(total);
        }
        else if (args[1] == "p2")
        {
            var similarity = 0;
            for (int i = 0; i < Map1.Count; i++)
            {
                similarity += Map1[i] * Map2.Where(x => x == Map1[i]).Count();
            }

            Console.WriteLine(similarity);
        }
    }
}