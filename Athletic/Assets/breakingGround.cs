using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakingGround : MonoBehaviour
{
    bool flag;
    public bool canFall;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canFall){
            StartCoroutine(DelayFall());
        }
        if(flag){
            flag = true;
            transform.Translate(0,-0.1f,0);
        }
    }
    IEnumerator DelayFall()
    {
        // 3秒間待つ

        yield return new WaitForSeconds(1);
        flag = true;

    }
}
