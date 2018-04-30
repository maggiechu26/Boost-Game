﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    //rcs = reaction controller system
    [SerializeField] float rcsThrust = 100f; //serializeField --> exposes variable in inspector 
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending } //of type value "State"
    State state = State.Alive;

    // Use this for initialization
    void Start() {
        //get reference to Rigid Body
        rigidBody = GetComponent<Rigidbody>(); //act on components of type Rigidbody
        audioSource = GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {
        //todo: somewhere stop sound on death
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }

	}

    void OnCollisionEnter(Collision collision) { //as long as it has collider on sub-objects, then it gets called when hits something
        if (state != State.Alive) { return; } //stop execution at this point 
        switch (collision.gameObject.tag) { //look at the gameObject you are colliding with and read its "tag"
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f); //invoke the method after x second
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstScene", 1f); // parameterise time
                break;

        }
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); //todo allow for more than 2 levels
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotation
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
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
        float rotationThisFrame = rcsThrust * Time.deltaTime;//rotation speed based on thrust and multiply by frame time... frame rate independant
        if (Input.GetKey(KeyCode.A)) {
            //see Multiplying Vectors
            transform.Rotate(Vector3.forward * rotationThisFrame); //z axis clockwise
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame); //z axis counterclockwise
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }


}
