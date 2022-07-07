using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public bool inFallEnemyArea;
    float savedPosX,savedPosY,savedPosZ;
    // Start is called before the first frame update
    void Start()
    {
        // init();//イニシャライズ
        debugInit();//デバッグ用
        LoadPosition();//初回ロード
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < -7){//画面外に出たらロードしなおし
            reLoad();
        }
    }
    void OnTriggerEnter(Collider other) {

        switch(other.tag){
            case "flag":
                SavePosition();
                break;
            case "enemy":
                reLoad();
                break;
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "fallEnemyArea" )
        {
            inFallEnemyArea = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "fallEnemyArea" )
        {
            inFallEnemyArea = false;
        }
    }
    void init(){
        PlayerPrefs.SetFloat("savedPosX", -35f);
        PlayerPrefs.SetFloat("savedPosY", 1.37f);
        PlayerPrefs.SetFloat("savedPosZ", -3f);
    }
    void debugInit(){
        PlayerPrefs.SetFloat("savedPosX", transform.position.x);
        PlayerPrefs.SetFloat("savedPosY", transform.position.y);
        PlayerPrefs.SetFloat("savedPosZ", transform.position.z);
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
    void reLoad(){
        SceneManager.LoadScene("GameScene");
    }
}
