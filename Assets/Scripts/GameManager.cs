using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] keyFind; //Scene�� �����ϴ� Ű�� ��
    
    public int keyCount; //ȹ���� Ű�� ��
    public bool isClear;
    public bool isOver;
    // Start is called before the first frame update
    void Start()
    {
        keyCount = 0; //player�� ȹ���� key ���� 0���� �ʱ�ȭ
        keyFind = GameObject.FindGameObjectsWithTag("Key"); //Scene ��ü�� Ű ã��
        
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
    }

    public void GameClear()
    {
        if (isClear || isOver)
        {
            return;
        }
        isClear = true;
        Debug.Log("GameClear!");
    }
}
