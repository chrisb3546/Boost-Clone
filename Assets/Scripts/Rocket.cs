using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip victoryChime;
    [SerializeField] ParticleSystem mainEngineEffect;
    [SerializeField] ParticleSystem deathExplosionEffect;
    [SerializeField] ParticleSystem victoryEffect;
    Rigidbody rigidBody;
    AudioSource audioSource ;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody= GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
        

    }

        void OnCollisionEnter(Collision collision)
        {
            if(state != State.Alive) {return;}
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                break;
                case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                victoryEffect.Play();
                audioSource.PlayOneShot(victoryChime);
                Invoke("LoadNextScene", 1f);
                break;
                default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathExplosion);
                deathExplosionEffect.Play();
                Invoke("LoadFirstScene", 1f);
                break;
                
            }
        }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame *Time.deltaTime);
            if(!audioSource .isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                mainEngineEffect.Play();
            }
       
    }
         else  
        {
            audioSource.Stop();
            mainEngineEffect.Stop();
        }

    }

    private void Rotate()
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
