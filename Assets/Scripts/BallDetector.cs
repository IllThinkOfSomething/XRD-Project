using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    private GameObject RedFoam, BlueFoam;
    public GameObject Ball;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
       if(other.gameObject.CompareTag("BlueFoam"))
       {
        RedFoam = other.GetComponentInParent(other.GetType()).gameObject;
        ScoreManager.Instance.IncreasedRedScore(1);
        Destroy(Ball);
        Destroy(RedFoam);
       }
       else if (other.gameObject.CompareTag("Foam"))
       {
           BlueFoam = other.GetComponentInParent(other.GetType()).gameObject;
           ScoreManager.Instance.IncreasedBlueScore(1);
           Destroy(Ball);
           Destroy(BlueFoam);
       }
    }

    
    

    
}
