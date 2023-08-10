using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Desired offset from target.
    /// </summary>
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 1.0f, 10.0f);
    /// <summary>
    /// Desired vertical offset for LookAt point from target.
    /// </summary>
    [SerializeField]
    private float _lookAtHeightOffset = 1.0f;
    /// <summary>
    /// Factor to regulate how fast camera reaches desired position on horizontal plane.
    /// </summary>
    [SerializeField]
    private float _springFactor = 1.0f;
    /// <summary>
    /// Factor to regulate how fast camera reaches desired vertical position.
    /// </summary>
    [SerializeField]
    private float _springJumpFactor = 1.0f;
    #endregion
    #region references
    /// <summary>
    /// Reference to camera target object.
    /// </summary>
    [SerializeField]
    private GameObject _targetObject;
    /// <summary>
    /// Reference to Transform of camera target object.
    /// </summary>
    private Transform _targetTransform;
    /// <summary>
    /// Reference to local Transform.
    /// </summary>
    private Transform _myTransform;
    #endregion
    #region properties
    private Vector3 _targetLookAtPosition;
    #endregion
    #region methods
    public void setPlayer(GameObject gO)
    {       // Se le pasa a la cámara el jugador que tiene que seguir y se coge su transform.
        _targetObject = gO;
        _targetTransform = _targetObject.GetComponent<Transform>();
    }
    #endregion
    /// <summary>
    /// References initialization
    /// </summary>
    void Start()
    {
        _targetTransform = _targetObject.GetComponent<Transform>();
        _myTransform = GetComponent<Transform>();
        _offset.y = _lookAtHeightOffset;
    }
    /// <summary>
    /// Updates camera position according to target position. Uses linear interpolation for smooth approach.
     /// Updates camera rotation according to target position and LookAt offset.
     /// </summary>
 void LateUpdate()
     {
        if (_targetObject != null)
        {
            _targetLookAtPosition = _targetTransform.position + (_lookAtHeightOffset * _targetTransform.up);
            _myTransform.position = Vector3.Lerp(_myTransform.position, _targetTransform.position + (_offset.z * _targetTransform.forward) + (_offset.y * _targetTransform.up), _springFactor * Time.deltaTime);
            _myTransform.LookAt(_targetLookAtPosition);
        }
    }

}

