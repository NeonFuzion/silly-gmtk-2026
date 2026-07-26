using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Umbrella : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Animator animator;
    ShadowCaster2D shadowCaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        shadowCaster = GetComponent<ShadowCaster2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUmbrella()
    {
        animator.CrossFade("UmbrellaOpen", 0, 0);
    }

    public void CloseUmbrella()
    {
        animator.CrossFade("Idle", 0, 0);
        boxCollider.enabled = false;
        shadowCaster.enabled = false;
    }
}
