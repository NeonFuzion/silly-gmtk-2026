using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [SerializeField] UnityEvent onPlayerRespawn, onPlayerDeath;

    public static EventManager Instance;

    public UnityEvent OnPlayerRespawn => onPlayerRespawn;
    public UnityEvent OnPlayerDeath => onPlayerDeath;

    void Awake()
    {
        if (Instance) Destroy(Instance);
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeOnPlayerRespawn() => onPlayerRespawn?.Invoke();
    public void InvokeOnPlayerDeath() => onPlayerDeath?.Invoke();
}
