using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLifeComponent : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Initial life for the player
    /// </summary>
    [SerializeField]
    private int _maxLife = 3;
    /// <summary>
    /// Life points lost when player receives damage
    /// </summary>
    [SerializeField]
    private int _hitDamage = 1;
    #endregion
    #region properties
    /// <summary>
    /// Stores current life points
    /// </summary>
    private int _currentLife;
    private float _timeElapsed;
    #endregion
    #region methods
    /// <summary>
    /// Called on collision with other objects.
    /// If collided object is an enemy, the player receives damage.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>()!=null)
        {
            if (_timeElapsed > 1.0f)
            {
                this.Damage();
                _timeElapsed = 0.0f;
            }
        }

    }
    void Update()
    {
        _timeElapsed += Time.deltaTime;
    }
    /// <summary>
    /// Method called when player receives damage.
    /// </summary>
    public void Damage()
    {
        _currentLife -= _hitDamage;
        if (_currentLife >= 0)
        {
            gameManager.Instance.OnPlayerDamage(_currentLife);
        }

    }
    #endregion
    /// <summary>
    /// Initializes current life of the player
    /// </summary>
    void Start()
    {
        _currentLife = _maxLife;        //Hecho
    }
}
