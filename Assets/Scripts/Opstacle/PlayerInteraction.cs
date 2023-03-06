using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    [Serializable]
    public class Array2D //2차원 배열 클래스
    {
        public GameObject [] Keys; 
    }
    public Array2D [] outsideGroup; //바깥쪽 키
    public Array2D [] insideGroup; //안쪽 키
    public int[] outCount;
    public int[] inCount;
    public int index;

    public GameObject bossUI; //장애물
    public GameObject smokeUI;
    GameObject obstacle; 
    
    GameManager manager;

    

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

       
            
    }

    void OnTriggerEnter(Collider other) 
    {
        if (bossUI.activeSelf) //악덕상사 활성화
        {
            if (other.CompareTag("Smoke") || other.CompareTag("Mail") || other.CompareTag("Bomper"))
                manager.GameOver();
        }

        if (other.CompareTag("Mail") || other.CompareTag("Smoke") || other.CompareTag("Bomb")) //장애물 충돌처리
        {
            ObstacleCollision(other);
        }
 

        CheckKeys(other);
       
        RemoveInside();
       
    }


    private void CheckKeys(Collider other) //충돌한 오브젝트가 안쪽 키인지 테두리 키인지 확인 및 Count 증가
    {
        for(index = 0; index < outsideGroup.Length; index++)
        {
            for(int j = 0; j < outsideGroup[index].Keys.Length; j++)
            {
                if (other.name == outsideGroup[index].Keys[j].name)
                {
                    outCount[index]++;
                    return;
                }
            }
                
        }

        for (index = 0; index < insideGroup.Length; index++)
        {
            for (int j = 0; j < insideGroup[index].Keys.Length; j++)
            {
                if (other.name == insideGroup[index].Keys[j].name)
                {
                    inCount[index]++;
                }
            }

        }
    }

    private void RemoveInside()
    {
       if(outCount[index] == outsideGroup[index].Keys.Length) //해당 바깥쪽 그룹의 열쇠 중 마지막과 부딪히면
        {
            if (inCount[index] > 0) //해당 안쪽 그룹의 열쇠가 하나라도 사라졌을 경우
                return; 
            for(int j = 0; j < insideGroup[index].Keys.Length; j++) //아니라면, 해당 안쪽 그룹의 열쇠 모두 비활성화 및 keyCount 증가
            {
                insideGroup[index].Keys[j].SetActive(false);
                manager.keyCount++;
            }
        }
    }

    public void ObstacleCollision(Collider obstacle)
    {
        obstacle.gameObject.SetActive(false);

        if(obstacle.CompareTag("Mail"))
        {
            bossUI.SetActive(true);
        }
        else if (obstacle.CompareTag("Smoke"))
        {
            smokeUI.SetActive(true);
        }
        else if (obstacle.CompareTag("Bomb"))
        {
            manager.GameOver();
        }

    }

}
