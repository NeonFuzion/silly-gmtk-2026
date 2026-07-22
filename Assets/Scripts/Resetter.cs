using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    [SerializeField] List<Transform> targets;

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

    public void ResetLevel()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].position = positions[i];
            targets[i].eulerAngles = eulerAngles[i];
        }
    }
}
