namespace GridWorld.Agent;

public interface IAgent
{
    event Action OnInvalidMove;
    Vector2 Position { get; }
    void Move(Vector2 goalPosition, Func<Vector2, bool> isMoveValid);
    void ResetPosition();
}