using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

    // Use this for initialization
    void Start() {
        //get reference to Rigid Body
        rigidBody = GetComponent<Rigidbody>(); //act on components of type Rigidbody
    }
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotating
            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward); //z axis
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward); //z axis
        }

    }
}
