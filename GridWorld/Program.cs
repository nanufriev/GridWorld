using GridWorld.Agent;
using GridWorld.Data;

namespace GridWorld;

public class Program
{
    private const string PATH_TO_CONFIG = "Resources\\Config.txt";

    private static ConfigurationData _config;
    private static int _movesLeft;
    private static int _currentReward;
    private static ConsoleKeyInfo _inputKey;
    private static IAgent _agent;

    private static void Main()
    {
        LoadConfig();
        _agent = new Character(_config.StartPosition);
        _agent.OnInvalidMove += () => _currentReward -= _config.NegativeReward;

        while (true)
        {
            ResetValues();

            while (_movesLeft > 0)
            {
                PrintMap(_config.Map, _agent.Position);

                if (_config.DelayBetweenPrintMs > 0)
                    Thread.Sleep(_config.DelayBetweenPrintMs);

                _agent.Move(_config.GoalPosition, IsValidMove);

                if (_agent.Position.Equals(_config.GoalPosition))
                {
                    _currentReward += _config.PositiveReward;
                    PrintMap(_config.Map, _agent.Position);
                    Console.WriteLine(
                        $"Reached the goal! Reward: {_currentReward}, moves count: {_config.MoveCount - _movesLeft}");
                    break;
                }

                _movesLeft--;
            }

            if (_movesLeft == 0)
            {
                Console.WriteLine($"Out of moves! Reward: {_currentReward}");
            }

            Console.WriteLine("Press any key to restart the simulation or Escape to exit.");
            _inputKey = Console.ReadKey();
            if (_inputKey.Key == ConsoleKey.Spacebar)
            {
                Environment.Exit(0);
            }
        }
    }

    private static void LoadConfig()
    {
        _config = ConfigLoader.LoadConfig(PATH_TO_CONFIG);
    }

    private static void ResetValues()
    {
        _movesLeft = _config.MoveCount;
        _currentReward = 0;
        _agent.ResetPosition();
    }

    private static bool IsValidMove(Vector2 move)
    {
        if (move.X < 0 || move.Y < 0 || move.X >= _config.Map.Length || move.Y >= _config.Map[0].Length)
        {
            return false;
        }

        return _config.Map[move.X][move.Y] != 'W';
    }

    private static void PrintMap(char[][] map, Vector2 playerPosition)
    {
        Console.Clear();
        Console.WriteLine($"Moves left: {_movesLeft}");

        for (var x = 0; x < map.Length; x++)
        {
            for (var y = 0; y < map[x].Length; y++)
            {
                if (playerPosition.X == x && playerPosition.Y == y)
                {
                    Console.Write('P');
                }
                else
                {
                    Console.Write(map[x][y]);
                }
            }

            Console.WriteLine();
        }
    }
}