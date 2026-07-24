using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] MainCamera mainCamera;
    [SerializeField] Sun sun;
    [SerializeField] Player player;
    [SerializeField] Level[] level;

    

    int levelIndex;
    bool levelComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GetComponent<MainCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(int level)
    {
        levelIndex = level;
    }

    public void IncrementLevel()
    {
        levelIndex++;
    }

    public void EndLevel()
    {
        if (!levelComplete) return;
        IncrementLevel();
        Level currentLevel = level[levelIndex];
        sun.MoveLevel(currentLevel.transform);
        currentLevel.SetNewRespawn(player);
        mainCamera.SetTarget(currentLevel.transform);
        levelComplete = false;
    }

    public void ResetLevel()
    {
        player.RespawnPlayer();
        sun.ResetTransform();
        level[levelIndex].ResetLevel();
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
