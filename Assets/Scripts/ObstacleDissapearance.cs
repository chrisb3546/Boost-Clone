using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDissapearance : MonoBehaviour
{
    [SerializeField] GameObject theObstacle;
    // Start is called before the first frame update
    void Start()
    {
        ObstacleOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ObstacleOut()
    {
        StartCoroutine(InAndOut());
    }

    IEnumerator InAndOut()
    {
        theObstacle.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        theObstacle.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        ObstacleOut();
    }
}
