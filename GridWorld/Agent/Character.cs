namespace GridWorld.Agent;

public class Character : IAgent
{
    private const int BACKWARD_MOVE_PENALTY = 10;
    
    public event Action? OnInvalidMove;
    public Vector2 Position { get; private set; }

    private readonly Vector2 _initPosition;
    private readonly Random _rnd = new();
    private readonly List<Vector2> _invalidMoves = new();
    private List<Vector2> _possibleMoves;
    private List<double> _weights;
    private double _randomValue;
    private double _cumulativeWeight;
    private Vector2 _chosenMove;
    private Vector2 _lastPosition;

    public Character(Vector2 startPosition)
    {
        _initPosition = startPosition;
        _lastPosition = _initPosition;
        Position = _initPosition;
    }

    public void Move(Vector2 goalPosition, Func<Vector2, bool> isMoveValid)
    {
        RefreshPossibleMoves();
        CheckForDeadEnd(isMoveValid);
        SetWeights(goalPosition);

        _chosenMove = GetNextMove();

        if (isMoveValid(_chosenMove))
        {
            _lastPosition = Position;
            Position = _chosenMove;
        }
        else
        {
            _invalidMoves.Add(_chosenMove);
            OnInvalidMove?.Invoke();
        }
    }

    public void ResetPosition()
    {
        Position = _initPosition;
    }

    private void RefreshPossibleMoves()
    {
        _possibleMoves = new List<Vector2>
        {
            new(Position.X - 1, Position.Y),
            new(Position.X + 1, Position.Y),
            new(Position.X, Position.Y - 1),
            new(Position.X, Position.Y + 1)
        };
    }

    private void SetWeights(Vector2 goalPosition)
    {
        _weights = new List<double>();
        foreach (var move in _possibleMoves)
        {
            double weight;

            if (_invalidMoves.Contains(move))
            {
                weight = 0;
            }
            else
            {
                weight = 1 / (move.DistanceTo(goalPosition) + 1);

                if (move.Equals(_lastPosition))
                    weight /= BACKWARD_MOVE_PENALTY;
            }

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
    }

    private Vector2 GetNextMove()
    {
        _randomValue = _rnd.NextDouble();
        _cumulativeWeight = 0.0;

        for (var i = 0; i < _weights.Count; i++)
        {
            _cumulativeWeight += _weights[i];
            if (_randomValue < _cumulativeWeight)
            {
                return _possibleMoves[i];
            }
        }

        throw new Exception("Cant find next move");
    }

    private void CheckForDeadEnd(Func<Vector2, bool> isMoveValid)
    {
        if (Position.Equals(_initPosition)) 
            return;
        
        var isDeadEnd = true;

        foreach (var move in _possibleMoves)
        {
            if (isMoveValid(move) 
                && !_invalidMoves.Contains(move) 
                && !move.Equals(_lastPosition))
            {
                isDeadEnd = false;
                break;
            }
        }

        if (isDeadEnd)
        {
            _invalidMoves.Add(Position);
        }
    }
}