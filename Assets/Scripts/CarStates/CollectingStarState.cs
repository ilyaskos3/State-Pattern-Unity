using UnityEngine;

public class CollectingStarState : IState
{
    private CarController _carController;
    private Material _boxMat;
    
    private float _collectingDuration = 2f;
    private Vector3 scaleFactor = Vector3.one * 0.2f; 
    private Color _boxColor = Color.blue;
    
    private float _elapsedTime;
    private bool _timerActive;
    private Color _startingBoxColor;
    

    public CollectingStarState(CarController carController, Material boxMat)
    {
        _carController = carController;
        _boxMat = boxMat;
    }
    public void Enter()
    {
        Debug.Log("STATE - Collecting Star");
        
        StartTimer();
        _startingBoxColor = _boxMat.color;
        _boxMat.color = _boxColor;
    }

    public void UpdateState()
    {
        if (_timerActive)
        {
            _carController.transform.localScale += (Time.deltaTime * scaleFactor);
            _elapsedTime += Time.deltaTime;
        }
        
        if (_elapsedTime > _collectingDuration)
        {
            StopTimer();
            _carController.ChangeState(_carController.SearchState);
        }
    }

    public void FixedUpdateState()
    {

    }

    public void Exit()
    {
        _boxMat.color = _startingBoxColor;
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
