using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button pauseBtn;
    public GameObject PauseUI;
    void Update()
    {
        Debug.Log("Update");
        transform.Translate(Time.deltaTime, 0, 0);
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }

    public void OnTogglePauseButton()
    {
        if (Time.timeScale == 0) //멈춰있으면
        {
            Time.timeScale = 1f; //시작
        }
        else //움직이면
        {
            Time.timeScale = 0; //멈추기
            PauseUI.SetActive(true);
        }
    }
}
