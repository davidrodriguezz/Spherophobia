using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Force applied to enemy when shot impacts
    /// </summary>
    [SerializeField]
    private float _impactForce = 100.0f;
    #endregion
    #region references
    /// <summary>
    /// Reference to self Transform
    /// </summary>
    private Transform _myTransform;
    /// <summary>
    /// Reference to object that will act as source of the shots
    /// </summary>
    [SerializeField]
    private GameObject _shotOriginObject;
    /// <summary>
    /// Reference to the transform of the object that will act as source of the shots
    /// </summary>
    private Transform _shotOriginTransform;
    /// <summary>
    /// Reference to Game Camera
    /// </summary>
    private Camera _myCamera;
    #endregion
    #region properties
    /// <summary>
    /// LayerMask used for enemies detection.
    /// </summary>
    private LayerMask _myLayerMask;
    #endregion
    #region methods
    /// <summary>
    /// Tries to shot a target from origin point. Causes damage and applies force to target if successful.
    /// </summary>
    /// <param name="originPoint">Shoot origin point</param>
    /// <param name="targetPoint">Shoot target point</param>
    public void Shoot(Vector3 targetPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(_shotOriginTransform.position, targetPoint, out hit, 100, _myLayerMask))
        {
            hit.collider.GetComponent<Rigidbody>().AddForce((hit.point-_shotOriginTransform.position)*_impactForce);
            hit.transform.gameObject.GetComponent<EnemyLifeComponent>().Damage();
        }
    }
    #endregion
    /// <summary>
    /// LayerMask and Camera initialization
    /// </summary>
    void Start()
    {
        _myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _myLayerMask = LayerMask.GetMask("Enemy");
        _shotOriginTransform = _shotOriginObject.transform;
    }
}

