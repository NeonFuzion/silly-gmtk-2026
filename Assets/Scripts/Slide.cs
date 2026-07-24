using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] float slideDuration, cooldown;

    float slideTime;
    int index;
    bool isMoving;

    Vector2 direction, lastPosition;
    Vector2[] positions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slideTime = 0;
        index = 0;

        positions = new Vector2[transform.childCount];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i).position;
        }
        direction = positions[1] - positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            slideTime = Mathf.Min(slideTime + Time.deltaTime, slideDuration);
            float progress = slideTime / slideDuration;
            transform.position = lastPosition + progress * direction;

            if (slideTime < slideDuration) return;
            isMoving = false;
        }
        else
        {
            slideTime = Mathf.Min(slideTime + Time.deltaTime, cooldown);

            if (slideTime < cooldown) return;
            isMoving = true;
            direction = positions[index + 1] - positions[index++];
        }
    }
}
