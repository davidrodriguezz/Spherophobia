using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region references
    /// <summary>
    /// Reference to local RigidBody.
    /// </summary>
    private Rigidbody _myRigidBody;
    #endregion
    #region methods
    /// <summary>
    /// Deactivate gravity on enemies.
    /// </summary>
    public void StopEnemy()
    {
        _myRigidBody.useGravity = false;
    }
    /// <summary>
    /// Activates enemy and sets random initial velocity for RigidBody.
    /// </summary>
    public void StartEnemy()
    {
        this.gameObject.SetActive(true);
        _myRigidBody.useGravity = true;
        _myRigidBody.velocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
    }
    #endregion
    /// <summary>
    /// Initialization includes:
    /// - Registration of Enemy on GameManager.
    /// - Random initial translation.
    /// - References initialization.
    /// - Stopping enemy.
    /// </summary>
    void Start()
    {
        gameManager.Instance.RegisterEnemy(this);
        _myRigidBody = GetComponent<Rigidbody>();
        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(5.0f, 13.0f), Random.Range(-10.0f, 10.0f));
        StopEnemy();
    }
}

