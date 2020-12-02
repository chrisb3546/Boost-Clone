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
    [SerializeField] float levelLoadDelay = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource ;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;
    [SerializeField] bool collisionsEnabled = true;
     int currentSceneIndex;

    

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
        
        if (Debug.isDebugBuild)
        {
            DevTool();
            
        }
        
        print(currentSceneIndex);
        
        
        
        

    }

        void OnCollisionEnter(Collision collision)
        {
            if(state != State.Alive || !collisionsEnabled ) {return;}
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                break;
                case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                victoryEffect.Play();
                audioSource.PlayOneShot(victoryChime);
                Invoke("LoadNextScene", levelLoadDelay);
                break;
                default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathExplosion);
                deathExplosionEffect.Play();
                Invoke("LoadFirstScene", levelLoadDelay);
                break;
                
            }
        }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; 
        }
        SceneManager.LoadScene(nextSceneIndex);
        
        
       
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

    private void DevTool()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        }
        else if( Input.GetKey(KeyCode.C))
        {
            collisionsEnabled = !collisionsEnabled ;
            
            
           
        }
       
    }


}
