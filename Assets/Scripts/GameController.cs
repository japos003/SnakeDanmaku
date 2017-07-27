using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] emitters;
    public float time_limit;
    public GameObject player;

    public Text cGameUI;

    public int score;

    private float total_time;
    private float time;


	// Use this for initialization
	void Start () {

        time_limit = 4.5f;
        time = 0.0f;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {

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


        //Time
        time += Time.deltaTime;
        total_time += Time.deltaTime;

        if (total_time >= 10.0f && total_time <= 20.0f)
            time_limit = 3.0f;
        if (total_time >= 20.0f)
            time_limit = 2.0f;

        if (time >= time_limit)
        {
            foreach(GameObject emitter in emitters)
            {
                emitter.gameObject.GetComponent<PowerUpEmitter>().EmitPowerUp();
            }

            time = 0.0f;

            score = player.GetComponent<PlayerMovement>().followers.Count > 0 ?
                (score + player.GetComponent<PlayerMovement>().followers.Count) : (score + 1);

            cGameUI.text = "Score: " + score;
            
        }


    }

    public void UpdateScore(int _score)
    {
        score += _score;
        cGameUI.text = "Score: " + score;
    }
}
