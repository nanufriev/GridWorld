namespace GridWorld.Agent;

public class Character : IAgent
{
    public event Action? OnInvalidMove;
    public Vector2 Position { get; private set; }

    private readonly Vector2 _initPosition;
    private readonly Random _rnd = new();
    private readonly List<Vector2> _previousInvalidMoves = new();

    private List<double> _weights = new();
    private double _randomValue;
    private double _cumulativeWeight;
    private int _chosenIndex;
    private Vector2 _chosenMove;
    
    public Character(Vector2 startPosition)
    {
        _initPosition = startPosition;
        Position = _initPosition;
    }

    public void Move(Vector2 goalPosition, Func<Vector2, bool> isMoveValid)
    {
        var possibleMoves = new List<Vector2>
        {
            new(Position.X - 1, Position.Y),
            new(Position.X + 1, Position.Y),
            new(Position.X, Position.Y - 1),
            new(Position.X, Position.Y + 1)
        };

        _weights = new List<double>();
        foreach (var move in possibleMoves)
        {
            double weight = 0;

            if (_previousInvalidMoves.Contains(move))
                weight = 0;
            else
                weight = 1 / (move.DistanceTo(goalPosition) + 1);

            _weights.Add(weight);
        }

        var totalWeight = _weights.Sum();
        if (totalWeight > 0)
        {
            for (var i = 0; i < _weights.Count; i++)
            {
                _weights[i] /= totalWeight;
            }
        }

        _randomValue = _rnd.NextDouble();
        _cumulativeWeight = 0.0;
        _chosenIndex = 0;

        for (int i = 0; i < _weights.Count; i++)
        {
            _cumulativeWeight += _weights[i];
            if (_randomValue < _cumulativeWeight)
            {
                _chosenIndex = i;
                break;
            }
        }

        _chosenMove = possibleMoves[_chosenIndex];

        if (isMoveValid(_chosenMove))
        {
            Position = _chosenMove;
        }
        else
        {
            _previousInvalidMoves.Add(_chosenMove);
            OnInvalidMove?.Invoke();
        }
    }

    public void ResetPosition()
    {
        Position = _initPosition;
    }
}