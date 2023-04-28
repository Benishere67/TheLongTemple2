using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mayan_Hole")
        {
            SceneManager.LoadScene("MyScene");
        }
    }






}
