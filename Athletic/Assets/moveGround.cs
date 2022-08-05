using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGround : MonoBehaviour
{
    int counter;
    public int threshold;
    public float speed;
    Vector3 p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (gameObject.tag)
        {
            case "upDownLift":
                p = new Vector3(0, speed, 0);
                counter++;
                
                if (counter == threshold)
                {
                    counter = 0;
                    speed *= -1;
                }
            break;
            case "rightLeftLift":
            case "slowRightLeftLift":
                p = new Vector3(speed, 0 , 0);
                counter++;
                
                if (counter == threshold)
                {
                    counter = 0;
                    speed *= -1;
                }
                break;
        }
        transform.Translate(p);

    }
}
