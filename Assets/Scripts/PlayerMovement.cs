using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    private Vector3 pos;
    private bool noFollowers = true;
    public GameObject follower;
    public GameController controller;


    public List<GameObject> followers;

	// Use this for initialization
	void Start () {
        pos = new Vector3();
        followers = new List<GameObject>();
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
        other.GetComponent<PowerUpScript>().DisappearAfterContact();

        if (other.GetComponent<PowerUpScript>().powerUpType == PowerUpEmitter.EmitterType.POWERUP)
        {
            addFollower();
        }
        
    }

    public void addFollower()
    {
        noFollowers = false;
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

        controller.UpdateScore(10);

    }

    public void loseLastFollower()
    {
        int last_Member = followers.Count - 1;

        followers[last_Member].SetActive(false);
        Destroy(followers[last_Member]);
        followers.RemoveAt(last_Member);
        controller.UpdateScore(-5);

        if (followers.Count == 0)
            noFollowers = true;
    }

    public void loseFollower(GameObject follower)
    {
        int curr_pos = followers.IndexOf(follower);
        follower.SetActive(false);
        followers.Remove(follower);
        controller.UpdateScore(-5);

        if (followers.Count == 0)
            noFollowers = true;
    }

    public int getFollowerNumber()
    {
        return followers.Count;
    }

    public int getFollowerNumber(GameObject follower)
    {
        return followers.IndexOf(follower);
    }

    public GameObject getFollower(int order)
    {
        return followers[order];
    }
}
