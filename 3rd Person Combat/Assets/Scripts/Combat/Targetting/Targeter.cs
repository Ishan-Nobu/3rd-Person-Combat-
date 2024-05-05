using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{   
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
    private Camera mainCamera;
    private List<Target> targets = new List<Target>();
    public Target CurrentTarget { get; private set; }

    private void Awake() 
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other) 
    {   
        if (!other.TryGetComponent(out Target target)) return; 
        
        targets.Add(target);
        target.DestroyEvent += RemoveTarget;
    }
    private void OnTriggerExit(Collider other) 
    {
        if (!other.TryGetComponent(out Target target)) return; 

        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCentre = viewPos - new Vector2(0.5f, 0.5f);

            if (toCentre.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCentre.sqrMagnitude;
            }
        }

        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
        cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.DestroyEvent -= RemoveTarget;
        targets.Remove(target);
    }

    public void Cancel()
    {   
        if (CurrentTarget == null) { return; }

        cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }
}
