﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start() {
        //get reference to Rigid Body
        rigidBody = GetComponent<Rigidbody>(); //act on components of type Rigidbody
        audioSource = GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotation
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying) { //so it doesn't layer on top of each other
                audioSource.Play();
            }

        }
        else {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward); //z axis clockwise
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward); //z axis counterclockwise
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }

}
