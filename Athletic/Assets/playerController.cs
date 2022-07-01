using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    Rigidbody rb;
    int horizonKey,verticalKey;
    float speedMagnification = 40.0f;
    Vector3 movingDirection,movingForce,movingVelocity;
    bool jumpNow;
    float jumpPower = 30;
    float gravityPower = -75;
    // Vector3 latestPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Debug.Log(x);
        Debug.Log(z);
        movingDirection = new Vector3(x, 0, z);
        movingDirection.Normalize();
        movingVelocity = movingDirection * speedMagnification;
        Debug.Log(movingVelocity);
        // Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        // latestPos = transform.position;  //前回のPositionの更新

        // //ベクトルの大きさが0.01以上の時に向きを変える処理をする
        // if (diff.magnitude > 0.01f)
        // {
        //     transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
        // }

    }


    void Gravity() {
        if (jumpNow == true) {
            rb.AddForce(new Vector3(0, gravityPower, 0));
        }
    }
    void FixedUpdate() {
        Gravity();
        if (jumpNow == true) return;
        rb.AddForce(movingVelocity, ForceMode.Force);
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("hoge");
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            jumpNow = true;
        }
    }
    void OnCollisionEnter(Collision other) {
        if (jumpNow == true) {
            if (other.gameObject.CompareTag("Ground")) {
                jumpNow = false;
            }
        }
    }

}
