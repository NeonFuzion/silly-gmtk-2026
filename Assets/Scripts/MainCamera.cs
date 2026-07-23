using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] float dampening;
    [SerializeField] Transform target;

    Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, dampening);
        transform.position = new (position.x, position.y, -10);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
