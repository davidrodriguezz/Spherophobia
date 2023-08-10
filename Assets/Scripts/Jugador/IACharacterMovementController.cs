using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacterMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float changeTime = 3.0f;
    private float elapsedTime;
    private Vector3 desiredDir;
    private float _rotationFactor;
    private float _rotationSpeed = 2.0f;
    #endregion
    #region references
    private CharacterController _myCharacterController;
    private Transform _myTransform;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myTransform = GetComponent<Transform>();
        elapsedTime = 0.0f;
        desiredDir = new Vector3(Random.Range(-1.0f, 1.0f), -1.0f, Random.Range(-1.0f, 1.0f));
        desiredDir.Normalize();
        _rotationFactor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= changeTime)
        {
            desiredDir = new Vector3(Random.Range(-1.0f, 1.0f), -1.0f, Random.Range(-1.0f, 1.0f));
            desiredDir.Normalize();
            _rotationFactor = Random.Range(-20.0f, 20.0f);
            elapsedTime = 0.0f;
        }
        _myCharacterController.Move(3.0f*desiredDir * Time.deltaTime);
        _myTransform.Rotate(0, _rotationSpeed * _rotationFactor * Time.deltaTime, 0);
    }
}
