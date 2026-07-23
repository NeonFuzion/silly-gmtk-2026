using UnityEngine;
using UnityEngine.Events;

public class Sun : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform player, sun;
    [SerializeField] UnityEvent onBurn, onUnburn;

    bool isBurning, isFrozen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBurning = false;
        isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen) return;
        transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;

        Burn();
    }

    void Burn()
    {
        Vector2 distanceVector = player.position - sun.position;
        if (Physics2D.Raycast(sun.position, distanceVector, distanceVector.magnitude, LayerMask.GetMask("Ground")))
        {
            if (!isBurning) return;
            isBurning = false;
            onUnburn?.Invoke();
        }
        else
        {
            if (isBurning) return;
            isBurning = true;
            onBurn?.Invoke();
        }
    }

    public void Freeze()
    {
        isFrozen = true;
    }
}
