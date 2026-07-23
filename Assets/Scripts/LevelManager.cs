using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] level;

    int currentLevel;
    bool levelComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    public void IncrementLevel()
    {
        currentLevel++;
    }

    public void EndLevel()
    {
        if (!levelComplete) return;
        level[currentLevel].Transition();
        levelComplete = false;
    }

    public void ResetLevel()
    {
        level[currentLevel].ResetLevel();
    }

    public void SetLevelComplete()
    {
        levelComplete = true;
    }

    public void TransitionInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        EndLevel();
    }
}
