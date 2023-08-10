using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacterAttackController : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    private EnemyController eC;
    #endregion
    #region parameters
    [SerializeField]
    private float _impactForce = 100.0f;
    #endregion

    void Start()
    {//Cogemos su transform
        _myTransform = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        eC = collision.GetComponent<EnemyController>();     //Comrpobamos que lo que ha entrado en el trigger sea un enemigo
        if (eC != null)
        {
            RaycastHit hit;             //lanzamos rayo para saber hacia donde tiene que salir el enemigo disparado.
            if (Physics.Raycast(_myTransform.position, collision.transform.position, out hit))
            {           //Quitamos vida y empujamos
                collision.GetComponent<Rigidbody>().AddForce((hit.point - _myTransform.position) * _impactForce);
                collision.GetComponent<EnemyLifeComponent>().Damage();
            }
        }
    }
}
