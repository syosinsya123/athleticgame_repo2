using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeTitleController : MonoBehaviour
{
    public AudioClip changeSound;
    AudioSource audioSource;
    bool isCurrentTileA;
    // Start is called before the first frame update
    void Start()
    {
        this.isCurrentTileA = true;
        transform.localScale = Vector3.zero;
        StartCoroutine(ShowTile());
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Debug.Log(this.isCurrentTileA);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    IEnumerator ShowTile() 
    {
        //終わるまで待ってほしい処理を書く
        //例：敵が倒れるアニメーションを開始
        if(this.isCurrentTileA){
            if(gameObject.tag == "tileA"){
                transform.localScale = Vector3.one;
            }else{
                transform.localScale = Vector3.zero;
            }
        }else{
            if(gameObject.tag == "tileB"){
                transform.localScale = Vector3.one;
            }else{
                transform.localScale = Vector3.zero;
            }
        }
        //2秒待つ
        yield return new WaitForSeconds(3.5f);
        audioSource.PlayOneShot(changeSound);
        yield return new WaitForSeconds(0.5f);

        this.isCurrentTileA = !this.isCurrentTileA;
        StartCoroutine(ShowTile());
        //再開してから実行したい処理を書く
        //例：敵オブジェクトを破壊
    }

}
