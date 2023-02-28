public class Timer
{
    public float CurrentTime { get; private set; }

    public bool IsFinished => CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }

    public void Start(float startTime)
    {
        CurrentTime = startTime;
    }

    public void RemoveTime(float deltaTime)
    {
        if (CurrentTime <= 0) return;
        CurrentTime -= deltaTime;
    }
}
