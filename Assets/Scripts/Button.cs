using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject sprite;
    [SerializeField] UnityEvent onActivate;

    Animator animator;
    BoxCollider2D boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateButton()
    {
        onActivate?.Invoke();
        animator.CrossFade("ButtonInteract", 0, 0);
    }

    public void ResetButton()
    {
        animator.CrossFade("Idle", 0, 0);
        boxCollider.enabled = true;
        sprite.transform.localScale = Vector3.one;
    }
}
