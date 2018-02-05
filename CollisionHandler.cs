using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("Implement the explosion component")][SerializeField] GameObject deathfx;

	// Use this for initialization
	void Start ()
    {
        
	}

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathfx.SetActive(true);
        Invoke("RestartLevel", levelLoadDelay);

    }

     void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
