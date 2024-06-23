using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SprinkleBehaviour : MonoBehaviour
{
    // This variable is set byt the intializer depending on where the clicker is.
    // // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference.
    public static Vector3 startingPosition = new Vector3();

    private Rigidbody2D _rigidbody2D;
    public float clickForceUp = 0.0f;
    public float clickForceLateral = 0.0f;

    // The 
    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        gameObject.transform.position = startingPosition;
        
        if (_rigidbody2D != null)
        {
            float forceSideways = Random.Range(-clickForceLateral, clickForceLateral);
            _rigidbody2D.AddForce(new Vector2(forceSideways, clickForceUp), ForceMode2D.Impulse);
        }
    }
}
