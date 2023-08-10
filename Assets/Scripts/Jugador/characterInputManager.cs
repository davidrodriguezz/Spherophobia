using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterInputManager : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Maximum distance for shots to reach target
    /// </summary>
    [SerializeField]
    private float _shotDistance;
    #endregion
    #region references
    /// <summary>
    /// Reference to local component CharacterMovementManager
    /// </summary>
    private characterMovementManager _myCharacterMovementManager;
    /// <summary>
    /// Reference to local component CharacterAttackController
    /// </summary>
    private CharacterAttackController _myCharacterAttackController;
    /// <summary>
    /// Reference to Game Camera
    /// </summary>
    private Camera _myCamera;
    #endregion
    #region properties
    /// <summary>
    /// Stores horizontal input
    /// </summary>
    private float _horizontalInput;
    /// <summary>
    /// Stores vertical input
    /// </summary>
    private float _verticalInput;
    /// <summary>
    /// Stores mouse input
    /// </summary>
    private float _mouseInput;
    #endregion
    #region methods
    /// <summary>
    /// Returns the world point corresponding to desired screen point at 
    /// </summary>
    /// <param name="screenPoint"></param>
    /// <returns></returns>
    private Vector3 GetWorldPoint(Vector2 screenPoint, float distanceFromCamera)
    {
        return _myCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, distanceFromCamera-_myCamera.transform.position.z));
    }
    #endregion
    /// <summary>
    /// Initializes references
    /// </summary>
    void Start()
    {
        _myCharacterMovementManager = GetComponent<characterMovementManager>();
        _myCharacterAttackController = GetComponent<CharacterAttackController>();
        _myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
 /// <summary>
 /// Get input and calls required methods
 /// from components CharacterMovementManager and CharacterAttackController
 /// </summary>
    void Update()
    {
        _horizontalInput = 0f;
        _verticalInput = 0f;
        _mouseInput = 0f;

        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            _myCharacterMovementManager.JumpRequest();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 aux = GetWorldPoint(Input.mousePosition, _shotDistance);
            _myCharacterAttackController.Shoot(aux);
        }
        else if (Input.GetMouseButton(1))
        {
            _mouseInput= Input.GetAxis("Mouse X");
        }
        _myCharacterMovementManager.SetMovementDirection(_horizontalInput, _verticalInput);
        _myCharacterMovementManager.SetMovementRotation(_mouseInput);
    }
}


