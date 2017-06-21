using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    private Vector3 pos;
    public GameObject follower;

    private int num_of_followers;


    public List<GameObject> followers;

	// Use this for initialization
	void Start () {
        pos = new Vector3();
        followers = new List<GameObject>();
        num_of_followers = 0;
    }
	
	// Update is called once per frame
	void Update () {

        bool button_down = false;

        //Movement
        //if(Input.GetKeyDown(KeyCode.UpArrow))
        //       gameObject.transform.Translate(new Vector3(0.0f, 1.0f, 0));
        //   if (Input.GetKeyDown(KeyCode.DownArrow))
        //       gameObject.transform.Translate(new Vector3(0.0f, -1.0f, 0));
        //   if (Input.GetKeyDown(KeyCode.RightArrow))
        //       gameObject.transform.Translate(new Vector3(1.0f, 0, 0));
        //   if (Input.GetKeyDown(KeyCode.LeftArrow))
        //       gameObject.transform.Translate(new Vector3(-1.0f, 0, 0));

        


        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            pos = mousePosition;
        }

        gameObject.transform.position = Vector3.Lerp(transform.position, pos, 0.05f);

        //Input.mousePosition

        //Changes color of object
         if (Input.GetKey(KeyCode.Space))
        {
            
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            if (Input.GetKeyDown(KeyCode.Space) && button_down == false)
            {
                button_down = true;
            }
            
        }       
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }

        button_down = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PowerUpScript>().powerUpType == PowerUpEmitter.EmitterType.POWERUP)
        {
            addFollower();
        }
    }

    public void addFollower()
    {
        if (followers.Count < 5)
        {
            followers.Add(
                    Instantiate(follower,
                    new Vector3(
                        gameObject.transform.position.x - 2.0f, gameObject.transform.position.y, 0
                        ),
                    transform.rotation
                    )
                );
        }
    }

    public int getFollowerNumber()
    {
        return followers.Count;
    }

    public int getFollowerNumber(GameObject follower)
    {
        return followers.IndexOf(follower);
    }

}
