using System;
using UnityEngine;

public class MouseClickInput : MonoBehaviour
{
    public static event Action<Vector3> TargetUpdated = delegate { };
    
    [SerializeField] private GameObject _destinationIndicatorPrefab;
    private Camera _camera;
    
    private GameObject _destinationIndicator;
    private RaycastHit _hitInfo;

    private void Awake()
    {
        _camera = Camera.main;

        _destinationIndicator = Instantiate(_destinationIndicatorPrefab, _hitInfo.point, Quaternion.identity);
        _destinationIndicator.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetNewMouseHit(_camera);
            SetNewTargetPoint(_hitInfo.point);
        }
    }

    void GetNewMouseHit(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hitInfo, 200))
        {
            Debug.Log("Hitted: " + _hitInfo.transform.name + " to: " + _hitInfo.point);
        }
    }

    void SetNewTargetPoint(Vector3 position)
    {
        _destinationIndicator.SetActive(true);
        _destinationIndicator.transform.position = position;
        
        TargetUpdated.Invoke(position);
    }
}