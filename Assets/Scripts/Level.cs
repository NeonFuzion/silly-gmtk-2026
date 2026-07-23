using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] List<Transform> targets;
    [SerializeField] UnityEvent onReset, onTransition;

    Vector3[] positions, eulerAngles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positions = targets.Select(target => target.position).ToArray();
        eulerAngles = targets.Select(target => target.eulerAngles).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition()
    {
        if (!gameObject.activeInHierarchy) return;
        onTransition?.Invoke();
    }

    public void ResetLevel()
    {
        if (!gameObject.activeInHierarchy) return;
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].position = positions[i];
            targets[i].eulerAngles = eulerAngles[i];
        }

        onReset?.Invoke();
    }
}
