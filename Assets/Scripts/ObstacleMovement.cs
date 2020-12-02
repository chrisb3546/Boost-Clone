using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObstacleMovement : MonoBehaviour
{
    // Start is called before the first frame update
[SerializeField] Vector3 movementVector = new Vector3(0f,10f,0f);
[SerializeField] float period = 2f;
[Range(-1,1)] [SerializeField] float movementFactor;

Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave/2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
        
    }
}
