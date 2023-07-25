using UnityEngine.Events;

public class LevelController
{
    private static LevelController instance;
    public static LevelController Instance 
    {
        get
        {
            if (instance is null)
            {
                instance = new LevelController();
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public UnityEvent OnPlayerWin;
    public UnityEvent OnPlayerLose;

    private LevelController()
    {
        OnPlayerWin  = new UnityEvent();
        OnPlayerLose = new UnityEvent();
    }
}
