using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] MainCamera mainCamera;
    [SerializeField] Sun sun;
    [SerializeField] Player player;
    [SerializeField] Level[] level;
    [SerializeField] UnityEvent onLevelComplete;

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

    public void PrepareLevel()
    {
        if (levelIndex >= level.Length) return;
        Level currentLevel = level[levelIndex];
        sun.MoveLevel(currentLevel.transform);
        currentLevel.SetNewRespawn(player);
        mainCamera.SetTarget(currentLevel.transform);
    }

    public void SetLevel(int level)
    {
        levelIndex = level;
    }

    public void EndLevel()
    {
        if (!levelComplete) return;
        if (levelIndex >= level.Length - 1) return;
        levelIndex++;
        levelComplete = false;
        onLevelComplete?.Invoke();

        PrepareLevel();
    }

    public void ResetLevel()
    {
        player.RespawnPlayer();
        sun.ResetTransform();
        level[levelIndex].ResetLevel();
    }

    public void TransitionInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        EndLevel();
    }
}
