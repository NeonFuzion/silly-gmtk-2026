using UnityEngine;
using UnityEngine.Events;

public class Sun : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform player, sun;
    [SerializeField] UnityEvent onBurn, onUnburn;

    bool isBurning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBurning = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;

        Burn();
    }

    void Burn()
    {
        Vector2 distanceVector = player.position - sun.position;
        if (Physics2D.Raycast(sun.position, distanceVector, distanceVector.magnitude + 1, LayerMask.GetMask("Player")))
        {
            if (isBurning) return;
            isBurning = true;
            onBurn?.Invoke();
        }
        else
        {
            if (!isBurning) return;
            isBurning = false;
            onUnburn?.Invoke();
        }
    }
}
