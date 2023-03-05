using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject[] keyFind; //Scene에 존재하는 키의 수
    
    public int keyCount; //획득한 키의 수
    public bool isClear;
    public bool isOver;
    public GameObject GameOver_UI;
    public GameObject GameClear_UI;
 
    
    // Start is called before the first frame update
    void Start()
    {
        keyCount = 0; //player가 획득한 key 개수 0으로 초기화
        keyFind = GameObject.FindGameObjectsWithTag("Key"); //Scene 전체의 키 찾기
    }

    // Update is called once per frame
    void Update()
    {
        if (keyFind.Length == keyCount)
        {
            GameClear();
        }   

        
            
    }

    public void GameOver()
    {
        if (isOver || isClear)
        {
            return;
        }
        isOver = true;
        Debug.Log("GameOver!");
        GameOver_UI.SetActive(true);
    }

    public void GameClear()
    {
        if (isClear || isOver)
        {
            return;
        }
        isClear = true;
        Debug.Log("GameClear!");
        GameClear_UI.SetActive(true);
    }

}
