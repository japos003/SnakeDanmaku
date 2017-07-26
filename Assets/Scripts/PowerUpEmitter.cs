using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEmitter : MonoBehaviour {

    public enum EmitterType
    {
        POWERUP,
        ENEMY
    }

    public enum EmitterDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public GameObject powerup;

    public EmitterType typeOfEmitter;
    public EmitterDirection emitterPosition;

    public float objectSpeed = 1.0f;

    private Quaternion orientation;
    private Vector3 direction;

    public float vertical_direction;
    public float horizontal_direction;
    public float time_limit;

    private float time;
	// Use this for initialization
	void Start () {

        orientation = gameObject.transform.rotation;

        switch (emitterPosition)
        {
            case EmitterDirection.UP:
                //orientation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                direction = Vector3.up;
                horizontal_direction = 1.0f;
                break;
            case EmitterDirection.DOWN:
                //orientation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
                direction = Vector3.down;
                horizontal_direction = -1.0f;
                break;

            case EmitterDirection.LEFT:
                //orientation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
                direction = Vector3.left;
                vertical_direction = 1.0f;
                break;

            case EmitterDirection.RIGHT:
                //orientation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
                direction = Vector3.right;
                vertical_direction = -1.0f;
                break;
        }

        time = 0.0f;
	}

    public void EmitPowerUp()
    {
        GameObject emittedObject = Instantiate(powerup, transform.position, orientation);
        emittedObject.GetComponent<PowerUpScript>().powerupDirection = emitterPosition;
        emittedObject.GetComponent<PowerUpScript>().speed = objectSpeed;
        emittedObject.GetComponent<PowerUpScript>().powerUpType = typeOfEmitter;
    }
	
}
