using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLinealGuideScript : MonoBehaviour
{
    public GameObject controlsManager;

    private float rVelocity;
    [SerializeField] public float lineVelocity;

    public Transform leftBound, rightBound;

    private bool moveLeft;

    // Update is called once per frame
    void Start()
    {
        moveLeft = true;
    }

    void Update()
    {
        rVelocity = controlsManager.GetComponent<FilamentSpoolingControl>().RVelocity;

        lineVelocity = 0.075f / 360 * rVelocity;
        
        if (moveLeft)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - lineVelocity * Time.deltaTime);
            
            if (this.transform.position.z < leftBound.position.z)
            {
                moveLeft = false;
            }
        }        
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + lineVelocity * Time.deltaTime);

            if (this.transform.position.z > rightBound.position.z)
            {
                moveLeft = true;
            }
        }

    }
}
