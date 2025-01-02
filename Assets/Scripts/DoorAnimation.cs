using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int number;
    private int count;

    private void Start()
    {
        animator.SetInteger("Index", number);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (count == 0)
        {
            animator.SetBool("IsCollision", true);
            count++;
        }
        else
        {
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (count == 1)
        {
            animator.SetBool("IsCollision", false);
            count--;
        }
        else
        {
            count--;
        }
    }

}
