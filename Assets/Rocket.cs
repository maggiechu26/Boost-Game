using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    //rcs = reaction controller system
    [SerializeField] float rcsThrust = 100f; //serializeField --> exposes variable in inspector 
    [SerializeField] float mainThrust = 100f;

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
