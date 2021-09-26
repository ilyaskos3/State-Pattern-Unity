
using UnityEngine;


public class ReachedDestinationState : IState
{
    private CarController _carController;
    private Material _boxMat;

    private Color _startingBoxColor;
    private Color _boxColor = Color.green;

    private float _reachedDelayDuration = 0.5f;
    private float _elapsedTime;
    private bool _timerActive;

    public ReachedDestinationState(CarController carController, Material boxMat)
    {
        _carController = carController;
        _boxMat = boxMat;
    }

    public void Enter()
    {
        Debug.Log("STATE - Reached");
        
        _startingBoxColor = _boxMat.color;
        _boxMat.color = _boxColor;
        
        StartTimer();
    }

    public void Exit()
    {

        _boxMat.color = _startingBoxColor;
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        if (_timerActive)
        {
            _elapsedTime += Time.deltaTime;
        }
        
        if (_elapsedTime > _reachedDelayDuration)
        {
            StopTimer();
            _carController.ChangeState(_carController.IdleState);
        }
    }

    void StartTimer()
    {
        _timerActive = true;
        _elapsedTime = 0;
    }

    void StopTimer()
    {
        _timerActive = false;
    }
}


