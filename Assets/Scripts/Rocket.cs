using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1000f;
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

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                break;
                case "Finish":
                print("hit finish");
                SceneManager.LoadScene(1);
                break;
                default:
                print("die");
                SceneManager.LoadScene(0);
                break;
                
            }
        }
    void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
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
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if(Input.GetKey(KeyCode.A) )
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }


}
