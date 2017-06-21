using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject[] emitters;
    public float time_limit;

    private float time;


	// Use this for initialization
	void Start () {

        time_limit = 5.0f;
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        try
        {
            foreach (GameObject emitter in emitters)
            {
                if (emitter == null)
                {
                    Debug.Log("error...");
                }

                Debug.Log(emitter.name);

                switch (emitter.GetComponent<PowerUpEmitter>().emitterPosition)
                {
                    case PowerUpEmitter.EmitterDirection.UP:
                    case PowerUpEmitter.EmitterDirection.DOWN:
                        if (emitter.gameObject.transform.position.x >= 8.0f)
                        {
                            emitter.GetComponent<PowerUpEmitter>().horizontal_direction = -1.0f;
                        }
                        else if (emitter.gameObject.transform.position.x <= -8.0f)
                        {
                            emitter.GetComponent<PowerUpEmitter>().horizontal_direction = 1.0f;
                        }

                        break;
                    case PowerUpEmitter.EmitterDirection.LEFT:
                    case PowerUpEmitter.EmitterDirection.RIGHT:
                        if (emitter.gameObject.transform.position.y >= 8.0f)
                        {
                            emitter.GetComponent<PowerUpEmitter>().vertical_direction = -1.0f;
                        }
                        else if (emitter.gameObject.transform.position.y <= -8.0f)
                        {
                            emitter.GetComponent<PowerUpEmitter>().vertical_direction = 1.0f;
                        }

                        break;
                }

                emitter.gameObject.transform.position += new Vector3(
                    emitter.GetComponent<PowerUpEmitter>().horizontal_direction * Time.deltaTime,
                    emitter.GetComponent<PowerUpEmitter>().vertical_direction * Time.deltaTime);
            }
        } catch(NullReferenceException e)
        {
            Debug.Log(e.StackTrace);
        }


        //Time
        time += Time.deltaTime;

        if (time >= time_limit)
        {

        }


    }
}
