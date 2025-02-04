using UnityEngine;

public class Ragdoll : MonoBehaviour
{   
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private Collider[] allColliders;
    private Rigidbody[] allRigidbodies;
    void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRigidbodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in allColliders)
        {   
            if (collider.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }
        foreach (Rigidbody rigidbody in allRigidbodies)
        {   
            if (rigidbody.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }
        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}