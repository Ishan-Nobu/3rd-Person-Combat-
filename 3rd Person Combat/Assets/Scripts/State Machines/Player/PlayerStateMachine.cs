using UnityEngine;

public class PlayerStateMachine : StateMachine
{   
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetedMovementSpeed { get; private set; }
    [field: SerializeField] public float SmoothRotationValue { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeDistance { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field : SerializeField] public Health Health { get; private set; }
    [field : SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTranform { get; private set; }
    private void Start() 
    {   
        MainCameraTranform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }

    private void OnEnable() 
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDeath;
    }
    
    private void OnDisable() 
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDeath;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }
}