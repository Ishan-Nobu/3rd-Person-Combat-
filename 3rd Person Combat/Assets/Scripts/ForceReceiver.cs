using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{   
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private float drag = 0.3f; // drag = smoothTime
    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    private void Update()
    {
        if (verticalVelocity > 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }   
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
        
        if (agent != null)
        {
            if (impact.sqrMagnitude <= 0.2f * 0.2f)
            {   
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }       
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}
