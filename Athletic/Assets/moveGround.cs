using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGround : MonoBehaviour
{
    int counter = 0;
    float moveY = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 p = new Vector3(0, moveY, 0);
        transform.Translate(p);
        counter++;
        
        if (counter == 50)
        {
            counter = 0;
            moveY *= -1;
        }
    }
}
