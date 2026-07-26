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
            lastPosition += direction;
            transform.position = lastPosition;
            slideTime = 0;
            isMoving = false;
        }
        else
        {
            slideTime = Mathf.Min(slideTime + Time.deltaTime, cooldown);

            if (slideTime < cooldown) return;
            index += indexShift;

            isMoving = true;
            slideTime = 0;
            direction = positions[index] - lastPosition;

            if (index > 0 && index < positions.Length - 1) return;
            indexShift = -indexShift;
        }
    }

    public void Initialize()
    {
        slideTime = 0;
        index = 0;
        indexShift = 1;
        isMoving = true;

        lastPosition = positions[0];
        direction = positions[1] - lastPosition;
        transform.position = lastPosition;
    }
}
