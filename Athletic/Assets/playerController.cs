using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public bool inFallEnemyArea;
    float savedPosX,savedPosY,savedPosZ;
    bool isStrongGravity = false;
    bool isPlayable;
    public GameObject GameOverUI;
    [SerializeField]
	[Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;
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
    void Update()
    {
        if(!isPlayable){
            GameOverUI.SetActive(true);

        }
        if(Input.anyKeyDown && !isPlayable){
            isPlayable = true;
            Time.timeScale = 1;
            reLoad();
        }
    }
    void OnTriggerEnter(Collider other) {

        switch(other.tag){
            case "flag":
                SavePosition();
                PlaySaveParticle();
                break;
            case "enemy":
                Dead();
                break;
            case "goal line":
                SavePosition();
                PlaySaveParticle();
                break;
        }
    }
    void OnTriggerStay(Collider other) {
        switch(other.tag){
            case "fallEnemyArea":
                inFallEnemyArea = true;
                break;
            case "rightLeftLift":
                transform.SetParent(other.transform);
                break;
            case "upDownLift":
                transform.SetParent(other.transform);
                break;
            case "rotationGround":
                transform.SetParent(other.transform);
                break;

        }
    }
    void OnTriggerExit(Collider other) {
        switch(other.tag){
            case "fallEnemyArea":
                inFallEnemyArea = false;
                break;
            case "rightLeftLift":
                transform.SetParent(null);
                break;
            case "upDownLift":
                transform.SetParent(null);
                break;
            case "rotationGround":
                transform.SetParent(null);
                break;
        }
    }
    void init(){
        isPlayable = true;
        transform.localScale = Vector3.one;
        PlayerPrefs.SetFloat("savedPosX", -35f);
        PlayerPrefs.SetFloat("savedPosY", 1.37f);
        PlayerPrefs.SetFloat("savedPosZ", -3f);
    }
    void debugInit(){
        isPlayable = true;
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
        Debug.Log("saved");
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
        isPlayable = false;
        
        
    } 

    void PlaySaveParticle(){
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(particle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        Vector3 pos = this.transform.position;
        pos.y += 1;
        newParticle.transform.position = pos;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 2.0f);
    }
}
