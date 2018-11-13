using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour {

    private Collider2D col;


    
    private bool collided; 


	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool Collided
    {
        get { return collided; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collided = true; 
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        collided = false; 
    }
}
