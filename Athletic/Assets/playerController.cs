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
            Dead();
        }
    }
    void OnTriggerEnter(Collider other) {

        switch(other.tag){
            case "flag":
                SavePosition();
                break;
            case "enemy":
                Dead();
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
        transform.localScale = Vector3.one;
        PlayerPrefs.SetFloat("savedPosX", -35f);
        PlayerPrefs.SetFloat("savedPosY", 1.37f);
        PlayerPrefs.SetFloat("savedPosZ", -3f);
    }
    void debugInit(){
        transform.localScale = Vector3.one;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
        
    }
    void Dead(){
        Time.timeScale = 0;
        transform.localScale = Vector3.zero;
        StartCoroutine(Dying());
        
    }
    IEnumerator Dying() 
    {
        //2秒待つ
        yield return new WaitForSecondsRealtime(2);
        reLoad();
        //再開してから実行したい処理を書く
        //例：敵オブジェクトを破壊
    } 
}
