using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transition[] transitions;

    int currentLevel;

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
        SetLevel(currentLevel + 1);
    }

    public void EndLevel()
    {
        transitions[currentLevel].TransitionLevel();
    }

    public void TransitionInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        EndLevel();
    }
}
