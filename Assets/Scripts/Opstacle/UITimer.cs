using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    // Start is called before the first frame update
    private float uiTimer;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        uiTimer += Time.deltaTime; //UI Å¸ÀÌ¸Ó
        if (uiTimer >= 5.0f)
        {
            gameObject.SetActive(false);
            uiTimer = 0.0f;
        }

    }
}
