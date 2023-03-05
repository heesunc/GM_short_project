using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public GameObject bossUI;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            bossUI.SetActive(true);
       
        }
    }
}
