using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public bool inFallEnemyArea;
    float savedPosX,savedPosY,savedPosZ;
    bool isStrongGravity = false;
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
        if(this.isStrongGravity){
            this.increaseGravity();
        }
    }
    void OnCollisionStay(Collision collision){
        Debug.Log(collision);
    }
    void OnTriggerEnter(Collider other) {

        switch(other.tag){
            case "flag":
                SavePosition();
                hitEffect_flag();
                break;
            case "enemy":
                Dead();
                break;
            case "goal line":
                SavePosition();
                break;
        }
    }
    void OnTriggerStay(Collider other) {
        switch(other.tag){
            case "fallEnemyArea":
                inFallEnemyArea = true;
                break;
            case "strongGravity":
                this.isStrongGravity = true;
                break;
        }
    }
    void OnTriggerExit(Collider other) {
        switch(other.tag){
            case "fallEnemyArea":
                inFallEnemyArea = false;
                break;
            case "strongGravity":
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.isStrongGravity = false;
                break;
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
    void increaseGravity(){
        Vector3 grav = new Vector3(0,11f,0);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().AddForce(grav, ForceMode.Acceleration);
    }
        void hitEffect_flag(){
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
