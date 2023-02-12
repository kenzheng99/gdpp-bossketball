public class Timer {

    private float timeRemaining;

    public Timer(float durationSeconds) {
        timeRemaining = durationSeconds;
    }

    public void Tick(float deltaTimeSeconds) {
        timeRemaining -= deltaTimeSeconds;
        if (timeRemaining <= 0) {
            timeRemaining = 0;
        }
    }

    public bool Done() {
        return timeRemaining == 0;
    }
}