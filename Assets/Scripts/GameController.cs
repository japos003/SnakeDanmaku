using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] moving_emitters;
    public GameObject[] stationary_emitters;
    public float time_limit;
    public GameObject player;

    public Text gameUI;
    public Text gameOverText;

    public int score;

    private float total_time;
    private float time;
    private bool canDisplayStageStatus;
    private bool StationaryEmittersAreOn;


	// Use this for initialization
	void Start () {

        time_limit = 4.5f;
        time = 0.0f;
        score = 0;
        gameOverText.text = "";
        canDisplayStageStatus = false;
        StationaryEmittersAreOn = false;
        StartCoroutine(DisplayStageStatus(1));
	}
	
	// Update is called once per frame
	void Update () {

        UpdateEmitters();

        //Time
        time += Time.deltaTime;
        total_time += Time.deltaTime;


        if (total_time >= 30.0f && total_time <= 60.0f)
        {
            if (total_time < 33.0f)
            {
                StartCoroutine(DisplayStageStatus(2));
            }
            
            time_limit = 4.0f;
        }
            
        if (total_time >= 60.0f && total_time <= 90.0f)
        {
            time_limit = 3.0f;
            if (total_time < 63.0f)
            {
                StartCoroutine(DisplayStageStatus(3));
                StationaryEmittersAreOn = true;
            }
        }
            
        if (total_time >= 90.0f && total_time <= 120.0f)
        {
            if (total_time < 93.0f)
            {
                StartCoroutine(DisplayStageStatus(4));
            }
            time_limit = 2.0f;
        }

        if (total_time >= 120.0f && total_time <= 150.0f){
            if (total_time < 123.0f)
            {
                StartCoroutine(DisplayStageStatus(5));
            }
            time_limit = 1.5f;
        }
            
        if (total_time > 150.0f)
        {
            if (total_time < 153.0f)
            {
                StartCoroutine(DisplayStageStatus(6));
            }
            time_limit = 1.0f;
        }
            


        if (time >= time_limit)
        {
            foreach(GameObject emitter in moving_emitters)
            {
                emitter.gameObject.GetComponent<PowerUpEmitter>().EmitPowerUp();
                emitter.gameObject.GetComponent<PowerUpEmitter>().ChangeType();
            }

            if(StationaryEmittersAreOn)
                TurnOnStationeryEmitters((int)total_time);

            time = 0.0f;

            if (!player.GetComponent<PlayerMovement>().isDead)
                UpdateScore(player.GetComponent<PlayerMovement>().followers.Count > 0 ?
                    player.GetComponent<PlayerMovement>().followers.Count : 1);
            gameUI.text = "Score: " + score;
            
        }

        CheckPlayerDeath();

    }

    private void TurnOnStationeryEmitters(int time)
    {
        System.Random random = new System.Random();
        foreach (GameObject emitter in stationary_emitters)
        {
            Debug.Log("stationary_emitters length: " + stationary_emitters.Length);
            emitter.gameObject.GetComponent<PowerUpEmitter>().SwitchBulletDirection(random.Next(0, (int)total_time) % 4);
            emitter.gameObject.GetComponent<PowerUpEmitter>().ChangeType();
            emitter.gameObject.GetComponent<PowerUpEmitter>().EmitPowerUp();

        }
    }

    private void UpdateEmitters()
    {
        foreach (GameObject emitter in moving_emitters)
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
    }

    private void CheckPlayerDeath()
    {
        if (player.GetComponent<PlayerMovement>().isDead)
        {
            Debug.Log("Game Over!");
            gameOverText.text = "GAME OVER";
            player.GetComponent<PlayerMovement>().gameObject.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(ReturnToTitleScreen());
        }
    }

    public void UpdateScore(int _score)
    {
        if(!player.GetComponent<PlayerMovement>().isDead)
            score += _score;
        gameUI.text = "Score: " + score;
    }

    private IEnumerator ReturnToTitleScreen()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SnakeDanmakuTitleScreen");
    }

    private IEnumerator DisplayStageStatus(int stage)
    {
        gameOverText.text = "Stage " + stage.ToString();
        yield return new WaitForSeconds(3f);
        gameOverText.text = "";

    }
}
