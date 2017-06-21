﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public float horizontal = -0.0f;
    public float vertical = 1.0f;
    public float speed = 1.0f;

    public PowerUpEmitter.EmitterType powerUpType;
    public PowerUpEmitter.EmitterDirection powerupDirection;

    private float lifetime;

    // Use this for initialization
    void Start () {
        lifetime = 48.0f;

        switch (powerupDirection)
        {
            case PowerUpEmitter.EmitterDirection.UP:
                vertical = 1.0f;
                horizontal = 0.0f;
                break;
            case PowerUpEmitter.EmitterDirection.DOWN:
                vertical = -1.0f;
                horizontal = 0.0f;
                break;
            case PowerUpEmitter.EmitterDirection.LEFT:
                vertical = 0.0f;
                horizontal = -1.0f;
                break;
            case PowerUpEmitter.EmitterDirection.RIGHT:
                vertical = 0.0f;
                horizontal = 1.0f;
                break;
        }

        vertical *= speed;
        horizontal *= speed;

        if(powerUpType == PowerUpEmitter.EmitterType.ENEMY)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
	
	// Update is called once per frame
	void Update () {

        GameObject player = GameObject.FindWithTag("Player");

        float distance_from_player = (transform.position - player.transform.position).sqrMagnitude;

        if (distance_from_player < 10.0f)
        {
            Debug.Log("distance: " + transform.position.x + " " + transform.position.y);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        }
        else
        {
            transform.position += new Vector3(horizontal * Time.deltaTime, vertical * Time.deltaTime, 0);
        }

        lifetime -= Time.deltaTime;

        if(lifetime < 0.0f)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            if(powerUpType == PowerUpEmitter.EmitterType.ENEMY)
            {
                int last_Member = collision.GetComponent<PlayerMovement>().followers.Count - 1;

                if (last_Member >= 0)
                {
                    collision.GetComponent<PlayerMovement>().followers[last_Member].SetActive(false);
                    Destroy(collision.GetComponent<PlayerMovement>().followers[last_Member]);
                    collision.GetComponent<PlayerMovement>().followers.RemoveAt(last_Member);
                }
                else
                {
                    //Player dies
                }
            }

            Debug.Log("hit");
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}