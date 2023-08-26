namespace GridWorld.Data;

public class ConfigurationData
{
    public char[][] Map { get; set; }
    public int MoveCount { get; set; }
    public int PositiveReward { get; set; }
    public int NegativeReward { get; set; }
    public int DelayBetweenPrintMs { get; set; }
    public Vector2 StartPosition { get; set; }
    public Vector2 GoalPosition { get; set; }
}