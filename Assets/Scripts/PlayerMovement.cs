using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    private Vector3 pos;
    private bool noFollowers = true;

    public bool isDead = false;
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

        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            pos = mousePosition;
            gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - gameObject.transform.position);
        }

        gameObject.transform.position = Vector3.Lerp(transform.position, pos, 0.05f);

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
