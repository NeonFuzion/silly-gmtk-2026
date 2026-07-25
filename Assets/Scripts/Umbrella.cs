using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [SerializeField] GameObject shade;
    
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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
        shade.gameObject.SetActive(false);
    }
}
