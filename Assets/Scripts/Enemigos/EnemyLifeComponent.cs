using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Life parameter for enemy.
    /// </summary>
    [SerializeField]
    private int _maxLife = 3;
    /// <summary>
    /// Life points lost when character receives damage.
    /// </summary>
    [SerializeField]
    private int _hitDamage = 1;
    /// <summary>
    /// Vertical line for death of enemies.
    /// </summary>
    [SerializeField]
    private float _deadLineHeight = -10.0f;
    #endregion
    #region references
    /// <summary>
    /// Reference to local EnemyController.
    /// </summary>
    private EnemyController _myEnemyController;
    /// <summary>
    /// Reference to local Transform.
    /// </summary>
    private Transform _myTransform;
    /// <summary>
    /// Reference to local Text, where enemy lifepoints will be displayed.
    /// </summary>
    [SerializeField]
    private Text _myText;
    #endregion
    #region properties
    /// <summary>
    /// Stores player's current life points.
    /// </summary>
    private int _currentLife;
    #endregion
    #region methods
 /// <summary>
 /// Called when enemy collides with other object.
 /// If collided object is an enemy, local enemy receives damage.
 /// </summary>
 /// <param name="collision"></param>
 private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            Damage();
        }
    }
    /// <summary>
    /// Called when enemy receives damage.
    /// Updates life points of the enemy and corresponding points display.
    /// Is life points are lower than or equal to zero, enemy dies.
    /// </summary>
    public void Damage()
    {
        _currentLife -= _hitDamage;
        _myText.text = _currentLife.ToString();
        if (_currentLife <= 0)
        {
            Die();
        }
    }
    /// <summary>
    /// Called when enemy dies.
    /// Calls corresponding method on GameManager and destroys object.
    /// </summary>
    public void Die()
    {
        gameManager.Instance.OnEnemyDies(_myEnemyController);
    }
    #endregion

    /// <summary>
    /// Initialization of properties and references.
    /// </summary>
    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _myEnemyController = GetComponent<EnemyController>();
        _currentLife = _maxLife;
        _myText.text = _currentLife.ToString();
    }
    /// <summary>
    /// Checks vertical position against Dead Line.
    /// </summary>
    void Update()
    {
        if (_myTransform.position.y <= _deadLineHeight)
        {
            Die();
        }
    }
}

