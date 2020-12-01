﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource rocketThrust;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody= GetComponent<Rigidbody>();
        rocketThrust= GetComponent<AudioSource>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();

    }

    void Thrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if(!rocketThrust.isPlaying)
            {
                rocketThrust.Play();
            }
       
    }
         else  
        {
            rocketThrust.Stop();
        }

    }

    void Rotate()
    {
        rigidBody.freezeRotation = true;

        if(Input.GetKey(KeyCode.A) )
        {
            transform.Rotate(Vector3.forward);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false;
    }


}
