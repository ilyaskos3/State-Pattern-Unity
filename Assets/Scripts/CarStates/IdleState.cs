using UnityEngine;

public class IdleState : IState
    {
        private CarController _carController;
        private Material _boxMat;

        private Color _startingBoxColor;
        private Color _boxColor = Color.black;

        public IdleState(CarController carController, Material boxMat)
        {
            _carController = carController;
            _boxMat = boxMat;
        }
        
        public void Enter()
        {
            Debug.Log("STATE - Idle");
            _startingBoxColor = _boxMat.color;
            _boxMat.color = _boxColor;
            MouseClickInput.TargetUpdated += OnTargetUpdated;
        }

        public void Exit()
        {
            _boxMat.color = _startingBoxColor;
            MouseClickInput.TargetUpdated -= OnTargetUpdated;
        }

        public void FixedUpdateState()
        {

        }

        public void UpdateState()
        {

        }

        void OnTargetUpdated(Vector3 newPosition)
        {
            _carController.ChangeState(_carController.SearchState);
        }
    }


