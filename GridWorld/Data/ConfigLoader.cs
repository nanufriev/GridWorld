namespace GridWorld.Data;

public class ConfigLoader
{
    public static ConfigurationData LoadConfig(string filePath)
    {
        var config = new ConfigurationData();
        var lines = File.ReadAllLines(filePath);
        var mapLines = new List<string>();
            
        foreach (var line in lines)
        {
            if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("PositiveReward:"))
            {
                config.PositiveReward = int.Parse(line.Split(':')[1].Trim());
            }
            else if (line.StartsWith("NegativeReward:"))
            {
                config.NegativeReward = int.Parse(line.Split(':')[1].Trim());
            }
            else if (line.StartsWith("MoveCount:"))
            {
                config.MoveCount = int.Parse(line.Split(':')[1].Trim());
            }
            else if (line.StartsWith("DelayBetweenPrintMs:"))
            {
                config.DelayBetweenPrintMs = int.Parse(line.Split(':')[1].Trim());
            }
            else
            {
                mapLines.Add(line);
            }
        }

        config.Map = mapLines.Select(s => s.ToCharArray()).ToArray();
        config.StartPosition = FindPosition('S', config.Map);
        config.GoalPosition = FindPosition('G', config.Map);
        
        return config;
    }
    
    private static Vector2 FindPosition(char target, char[][] map)
    {
        for (var x = 0; x < map.Length; x++)
        {
            for (var y = 0; y < map[x].Length; y++)
            {
                if (map[x][y] == target)
                {
                    return new Vector2(x, y);
                }
            }
        }

        throw new Exception($"Couldn't find target symbol in config: {target}");
    }
}