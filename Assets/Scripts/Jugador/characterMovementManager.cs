using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovementManager : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Character movement speed
    /// </summary>
    [SerializeField]
    private float _speed = 1.0f;
    /// <summary>
    /// Character vertical jump speed
    /// </summary>
    [SerializeField]
    private float _jumpSpeed = 1.0f;
    /// <summary>
    /// Gravity value applied to player
    /// </summary>
    [SerializeField]
    private float _gravity = -10.0f;
    /// <summary>
    /// Maximum falling speed
    /// </summary>
    [SerializeField]
    private float _fallSpeed = 10.0f;
    /// <summary>
    /// Speed for rotation
    /// </summary>
    [SerializeField]
    private float _rotationSpeed = 1.0f;
    #endregion
    #region references
    /// <summary>
    /// Reference to local transform component
    /// </summary>
    private Transform _myTransform;
    /// <summary>
    /// Reference to local CharacterController component
    /// </summary>
    private CharacterController _myCharacterController;
    #endregion
    #region properties
    /// <summary>
    /// Stores desired movement direction
    /// </summary>
    private Vector3 _movementDirection;
    /// <summary>
    /// Stores desired rotation factor, determined by CharacterInputManager
    /// </summary>
    private float _rotationFactor;
    /// <summary>
    /// Stores current vertical speed value
    /// </summary>
    private float _verticalSpeed;
    #endregion
    #region methods
    /// <summary>
    /// Sets desired direction for player.
    /// </summary>
    /// <param name="horizontal">Right component for desired direction</param>
    /// <param name="vertical">Forward component for desired direction</param>
    public void SetMovementDirection(float horizontal, float vertical)
    {
        _movementDirection = _myTransform.right * horizontal + _myTransform.forward * vertical;
    }
    /// <summary>
    /// Sets desired rotation for the player
    /// </summary>
    /// <param name="rotation">Desired rotation</param>
    public void SetMovementRotation(float rotation)
    {
        _rotationFactor = rotation * 100;
    }

    /// <summary>
    /// Sets _verticalSpeed to _jumpSpeed,
    /// only if Character is grounded.
    /// </summary>
    public void JumpRequest()
    {
        if (_myCharacterController.isGrounded)
        {
            _verticalSpeed = _jumpSpeed;
        }
    }
    #endregion
    /// <summary>
    /// Initializes references to local components
    /// </summary>
    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _myCharacterController = GetComponent<CharacterController>();
        _rotationFactor = 0;
    }
    /// <summary>
    /// Manages player movement, including translation and gravity
    /// </summary>
    void Update()
    {
        _movementDirection *= _speed;
        if (_fallSpeed > _verticalSpeed)
        {
            _verticalSpeed -= _gravity * Time.deltaTime;
            _movementDirection.y = _verticalSpeed;
        }
        _myCharacterController.Move(_movementDirection * Time.deltaTime);

        if (_rotationFactor != 0)
        {
            _myTransform.Rotate(0, _rotationSpeed * _rotationFactor * Time.deltaTime, 0);
            _rotationFactor = 0;
        }
    }
}
