using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] float slideDuration, cooldown;

    float slideTime;
    int index, indexShift;
    bool isMoving;

    Vector2 direction, lastPosition;
    Vector2[] positions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positions = new Vector2[transform.childCount];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i).position;
        }

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            slideTime = Mathf.Min(slideTime + Time.deltaTime, slideDuration);
            float progress = slideTime / slideDuration;
            transform.position = lastPosition + progress * direction;

            if (progress < 1) return;
            transform.position = lastPosition + direction;
            slideTime = 0;
            isMoving = false;
        }
        else
        {
            slideTime = Mathf.Min(slideTime + Time.deltaTime, cooldown);

            if (slideTime < cooldown) return;
            int lastIndex = index;
            int nextIndex = index + indexShift;
            if (nextIndex != Mathf.Clamp(nextIndex, 0, positions.Length - 1)) indexShift = -indexShift;
            index = index + indexShift;

            isMoving = true;
            slideTime = 0;
            lastPosition = positions[lastIndex];
            direction = positions[index] - lastPosition;
        }
    }

    public void Initialize()
    {
        slideTime = 0;
        index = 0;
        indexShift = 1;

        lastPosition = positions[0];
        direction = positions[1] - lastPosition;
        transform.position = lastPosition;
    }
}
