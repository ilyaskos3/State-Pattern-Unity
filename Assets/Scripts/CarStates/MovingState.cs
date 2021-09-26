using UnityEngine;

public class MovingState : IState
{
    private CarController _carController;
    private Rigidbody _rb;
    private Material _boxMat;
    
    private Color _startingBoxColor;
    private Color _boxColor = Color.red;
    private float minDistance = 2.5f;
    
    public MovingState(CarController carController, Material boxMat, Rigidbody rb)
    {
        _carController = carController;
        _boxMat = boxMat;
        _rb = rb;
    }

    public void Enter()
    {
        Debug.Log("STATE - Move");
        _startingBoxColor = _boxMat.color;
        _boxMat.color = _boxColor;
    }

    public void Exit()
    {
        _boxMat.color = _startingBoxColor;
    }

    public void UpdateState()
    {

    }

    public void FixedUpdateState()
    {
        float distanceFromTarget = Vector3.Distance(_carController.TargetPosition, _rb.position);
        
        if (distanceFromTarget < minDistance)
        {
            _carController.ChangeState(_carController.FoundState);
        }else
        {
            RotateTowardsTarget();
            MoveTowardsTarget();
        }
    }

    void RotateTowardsTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_carController.TargetPosition - _rb.position);
        lookRotation = Quaternion.Slerp(_rb.rotation, lookRotation, _carController.RotateSpeed * Time.deltaTime);
        _rb.MoveRotation(lookRotation);
    }

    void MoveTowardsTarget()
    {
        Vector3 moveOffset = _carController.transform.forward * _carController.MoveSpeed;
        _rb.MovePosition(_rb.position + moveOffset);
    }
}


