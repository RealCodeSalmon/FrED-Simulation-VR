using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParts : MonoBehaviour
{
    //Rotating Parts
    public GameObject[] RotatingParts;

    //Rotating Velocity
    private float rotVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rotVelocity = GetComponent<FilamentSpoolingControl>().RVelocity;
        for (int i = 0; i < RotatingParts.Length; i++)
        {
            RotatingParts[i].transform.Rotate(0f, 0f, rotVelocity * Time.deltaTime, Space.Self);
        }
    }
}
