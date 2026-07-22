using UnityEngine;
using UnityEngine.InputSystem;

public class Presenter : MonoBehaviour
{
    [SerializeField] GameObject prefabPresent;

    Vector2 mousePosition;

    int presentIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MousePositionInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        mousePosition = input;
    }

    public void MouseClickInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
    }

    public void SelectPresent(int index)
    {
        presentIndex = index;
    }
}
