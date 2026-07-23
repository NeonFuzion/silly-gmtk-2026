using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Transition : MonoBehaviour
{
    [SerializeField] UnityEvent onTransition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransitionLevel()
    {
        if (!enabled) return;
        onTransition?.Invoke();
    }
}
