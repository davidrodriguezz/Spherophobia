using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoController : MonoBehaviour
{
    #region references
    /// <summary>
    /// Reference to Transform of Main Camera.
    /// </summary>
    private Transform _cameraTransform;
    /// <summary>
    /// Reference to own Transform.
    /// </summary>
    private Transform _myTransform;
    #endregion
    #region properties
    /// <summary>
    /// Store own initial rotation.
    /// </summary>
    Quaternion _initialRotation;
    #endregion

    /// <summary>
    /// Finds Main Camera and initializes references.
    /// </summary>
    void Start()
    {
        _myTransform = GetComponent<RectTransform>();
        _cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        _initialRotation = _cameraTransform.rotation;
    }
    /// <summary>
    /// Positions life text in front of own object, according to camera.
    /// Uses lookat method to make it look at camera.
    /// </summary>
    void Update()
    {
        Vector3 FinalPos = _myTransform.parent.localPosition;       //Mantiene la posición del padre, no hemos podido hacer que se mueva
        _myTransform.position = FinalPos;
        _myTransform.LookAt(_cameraTransform);
    }
}

