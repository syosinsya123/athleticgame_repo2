using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallEnemyController : MonoBehaviour
{
    GameObject target;
    float moveY;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        moveY = -0.3f;
        target = GameObject.Find("unitychan_dynamic");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flag || target.GetComponent<playerController>().inFallEnemyArea == true) {
            flag = true;
            Vector3 p = new Vector3(0, moveY, 0);
            transform.Translate(p);
        }
    }
}
