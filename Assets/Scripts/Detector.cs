using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] UnityEvent onEnter, onExit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionLayer = LayerMask.LayerToName(collision.gameObject.layer);

        if (LayerMask.GetMask(collisionLayer) != layerMask) return;
        onEnter?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        string collisionLayer = LayerMask.LayerToName(collision.gameObject.layer);

        if (LayerMask.GetMask(collisionLayer) != layerMask) return;
        onExit?.Invoke();
    }
}
