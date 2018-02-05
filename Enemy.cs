using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject EnemyDeathFX;
    [SerializeField] Transform parent; //variable to set position in game hierarchy
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 3;


    ScoreBoard scoreBoard;

	// Use this for initialization
	void Start ()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
	}

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>(); //adds box collider component to game object
        boxCollider.isTrigger = false; //sets the box collider to non triggered
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hits <= 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit); //sends the integer value of scorePerHit to ScoreHit function
        hits--; //same as "hits = hits - 1;"
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(EnemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
