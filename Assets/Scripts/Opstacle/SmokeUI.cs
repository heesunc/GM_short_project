using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeUI : MonoBehaviour
{
    private float smokeTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smokeTimer += Time.deltaTime; //스모크 타이머
        if (smokeTimer >= 10.0f)
        {
            gameObject.SetActive(false);
            smokeTimer = 0.0f;
        }
    }
}
