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

        gameObject.transform.position = new Vector3(
                player.transform.position.x - (2.0f * ordinal_position - (1.0f-ordinal_position)/ordinal_position),
                player.transform.position.y,
                0
                );


	}
	
	// Update is called once per frame
	void Update () {

        ordinal_position = player.GetComponent<PlayerMovement>().getFollowerNumber(gameObject) + 1;

        player = GameObject.Find("Player");

        player_location = new Vector3(
            player.transform.position.x - (2.0f * ordinal_position - (1.0f - ordinal_position) / ordinal_position),
            player.transform.position.y,
            0
        );
        gameObject.transform.position = Vector3.Lerp(transform.position, player_location, 1.0f / (ordinal_position*ordinal_position) );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerUpScript>().powerUpType == PowerUpEmitter.EmitterType.ENEMY)
        {
            int curr_pos = player.GetComponent<PlayerMovement>().followers.IndexOf(this.gameObject);
            player.GetComponent<PlayerMovement>().followers.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }

        if(other.GetComponent<PowerUpScript>().powerUpType == PowerUpEmitter.EmitterType.POWERUP)
        {
            player.GetComponent<PlayerMovement>().addFollower();
        }
    }
}
