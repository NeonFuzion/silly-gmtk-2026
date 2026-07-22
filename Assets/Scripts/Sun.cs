using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
    }
}
