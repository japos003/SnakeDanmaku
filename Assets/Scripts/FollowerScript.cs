using UnityEngine;
using System.Collections;

public class FollowerScript : MonoBehaviour {

    private GameObject player;
    private Vector3 player_location;
    private int ordinal_position;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");

        ordinal_position = player.GetComponent<PlayerMovement>().getFollowerNumber();

        gameObject.transform.position = player.transform.position; 
       


	}
	
	// Update is called once per frame
	void Update () {

        ordinal_position = player.GetComponent<PlayerMovement>().getFollowerNumber(gameObject) + 1;

        player = GameObject.Find("Player");

        gameObject.transform.position = Vector3.Lerp(transform.position, player.transform.position, 1.0f / (3.5f * ordinal_position*ordinal_position) );
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PowerUpScript>().DisappearAfterContact();

        if (other.GetComponent<PowerUpScript>().powerUpType == PowerUpEmitter.EmitterType.ENEMY)
        {
            player.GetComponent<PlayerMovement>().loseFollower(this.gameObject);
        }

        if(other.GetComponent<PowerUpScript>().powerUpType == PowerUpEmitter.EmitterType.POWERUP)
        {
            player.GetComponent<PlayerMovement>().addFollower();
        }
    }
}
