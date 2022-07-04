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
        LoadPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7){
            SceneManager.LoadScene("GameScene");
        }
    }
    void LoadPosition(){
        savedPosX = PlayerPrefs.GetFloat("savedPosX", -20f);
        savedPosY = PlayerPrefs.GetFloat("savedPosY", 1.37f);
        savedPosZ = PlayerPrefs.GetFloat("savedPosZ", -3f);
        transform.position = new Vector3(posX,posY,posZ);
    }
}
