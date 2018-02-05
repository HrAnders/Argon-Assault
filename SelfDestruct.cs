using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("SelfDestruction", 5f);
	}

    void SelfDestruction()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
