/*  Legacy code
 *  Depreciated since it is a repeated version of PowerUpScript.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float horizontal = 0.0f;
    public float vertical = 1.0f;
    public float speed = 1.0f;

    public PowerUpEmitter.EmitterDirection enemyDirection;

    public float lifetime;

    // Use this for initialization
    void Start () {
        lifetime = 8.0f;

        switch (enemyDirection)
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
	}

    // Update is called once per frame
    void Update()
    {

        GameObject player = GameObject.FindWithTag("Player");

        float distance_from_player = (transform.position - player.transform.position).sqrMagnitude;

        if (distance_from_player < 10.0f)
        {
            Debug.Log("distance: " + transform.position.x + " " + transform.position.y);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.05f);
        }
        else
        {
            transform.position += new Vector3(horizontal * Time.deltaTime, vertical * Time.deltaTime, 0);
            //transform.position += 2.0f * Vector3.up * Time.deltaTime;
        }

        lifetime -= Time.deltaTime;

        if (lifetime < 0.0f)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            int last_Member = collision.GetComponent<PlayerMovement>().followers.Count - 1;

            if(last_Member >= 0)
            {
                collision.GetComponent<PlayerMovement>().followers[last_Member].SetActive(false);
                Destroy(collision.GetComponent<PlayerMovement>().followers[last_Member]);
                collision.GetComponent<PlayerMovement>().followers.RemoveAt(last_Member);
            }
            else
            {
                //Player dies
            }

            

            Debug.Log("hit");
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
