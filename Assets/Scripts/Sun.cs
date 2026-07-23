using UnityEngine;
using UnityEngine.Events;

public class Sun : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform player, sun;
    [SerializeField] UnityEvent onBurn, onUnburn;

    bool isBurning, isFrozen;
    float resetAngle;

    Vector2 resetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBurning = false;
        isFrozen = false;
        resetAngle = transform.eulerAngles.z;

        resetPosition = transform.position;
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

    public void Unfreeze()
    {
        isFrozen = false;
    }

    public void Move(Transform target)
    {
        resetPosition = new (target.position.x, transform.position.y);
        transform.position = new (resetPosition.x, resetPosition.y, -10);
    }

    public void ResetTransform()
    {
        transform.position = resetPosition;
        transform.eulerAngles = Vector3.forward * resetAngle;
    }

    public void MoveLevel(Transform target)
    {
        Move(target);
        ResetTransform();
        Unfreeze();
    }
}
