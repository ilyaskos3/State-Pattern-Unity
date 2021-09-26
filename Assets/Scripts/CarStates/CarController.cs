using UnityEngine;

public class CarController : StateMachineParent
{
    public IdleState IdleState { get; private set; }
    public MovingState SearchState { get; private set; }
    public ReachedDestinationState FoundState { get; private set; }
    public CollectingStarState CollectingStarState { get; private set; }
    
    [SerializeField] Rigidbody _rb;
    
    [SerializeField] float _rotateSpeed = 5;
    public float RotateSpeed => _rotateSpeed;
    [SerializeField] float _moveSpeed = 5;
    public float MoveSpeed => _moveSpeed;
    
    [SerializeField] MeshRenderer _boxMesh;
    
    public Vector3 TargetPosition { get; set; }

    private void Awake()
    {
        IdleState = new IdleState(this, _boxMesh.material);
        SearchState = new MovingState(this, _boxMesh.material, _rb);
        FoundState = new ReachedDestinationState(this, _boxMesh.material);
        CollectingStarState = new CollectingStarState(this, _boxMesh.material);
    }

    private void OnEnable()
    {
        MouseClickInput.TargetUpdated += OnTargetUpdated;
    }

    private void OnDisable()
    {
        MouseClickInput.TargetUpdated -= OnTargetUpdated;
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    void OnTargetUpdated(Vector3 newTarget)
    {
        Debug.Log("Get Player Input: " + newTarget);
        TargetPosition = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            Destroy(other.gameObject);
            ChangeState(CollectingStarState);
        }

    }
}


