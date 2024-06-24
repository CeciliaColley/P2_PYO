using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SprinkleBehaviour : MonoBehaviour
{
    // This variable is set by the intializer depending on where the clicker is.
    // // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference.
    public static Vector3 startingPosition = new Vector3();

    // These variables are supposed to be set up from the engine.
    [SerializeField] private float clickForceUp = 0.0f;
    [SerializeField] private float clickForceLateral = 0.0f;
    
    // This variable is used internally by the game object.
    private Rigidbody2D _rigidbody2D;

    // When the sprinkle is enabled, it moves to the starting position, which is set from the initializer as the position of the button.
    // Then a random diagonal upwards force is applied using the the click force up and sideways parameters that are set up from the engine.
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
