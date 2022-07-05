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
        init();//イニシャライズ
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
        switch(other.tag){
            case "flag":
                SavePosition();
                break;
        }
    }
    void init(){
        PlayerPrefs.SetFloat("savedPosX", -35f);
        PlayerPrefs.SetFloat("savedPosY", 1.37f);
        PlayerPrefs.SetFloat("savedPosZ", -3f);
    }
    void SavePosition(){
        PlayerPrefs.SetFloat("savedPosX", transform.position.x);
        PlayerPrefs.SetFloat("savedPosY", transform.position.y);
        PlayerPrefs.SetFloat("savedPosZ", transform.position.z);
    }
    void LoadPosition(){
        savedPosX = PlayerPrefs.GetFloat("savedPosX");
        savedPosY = PlayerPrefs.GetFloat("savedPosY");
        savedPosZ = PlayerPrefs.GetFloat("savedPosZ");
        transform.position = new Vector3(savedPosX,savedPosY,savedPosZ);
    }
}
