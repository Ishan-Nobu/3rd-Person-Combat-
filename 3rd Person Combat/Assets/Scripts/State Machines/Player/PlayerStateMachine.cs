using UnityEngine;

public class PlayerStateMachine : StateMachine
{   
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float SmoothRotationValue { get; private set; }
    public Transform MainCameraTranform { get; private set; }
    private void Start() 
    {   
        MainCameraTranform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
