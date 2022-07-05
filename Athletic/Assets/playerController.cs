using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    float savedPosX,savedPosY,savedPosZ;
    // Start is called before the first frame update
    void Start()
    {
        LoadPosition();//初回ロード
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < -7){//画面外に出たらロードしなおし
            LoadPosition();
        }
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
    }

    void LoadPosition(){
        savedPosX = PlayerPrefs.GetFloat("savedPosX", -35f);
        savedPosY = PlayerPrefs.GetFloat("savedPosY", 1.37f);
        savedPosZ = PlayerPrefs.GetFloat("savedPosZ", -3f);
        transform.position = new Vector3(savedPosX,savedPosY,savedPosZ);
    }
}
