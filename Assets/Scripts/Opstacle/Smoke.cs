using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public GameObject smokeUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) //����ũ UI Ȱ��ȭ
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            smokeUI.SetActive(true);

        }
    }
}
