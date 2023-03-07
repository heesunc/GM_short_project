using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    [Serializable]
    public class Array2D //2占쏙옙占쏙옙 占썼열 클占쏙옙占쏙옙
    {
        public GameObject [] Keys; 
    }
    public Array2D [] outsideGroup; //占쌕깍옙占쏙옙 키
    public Array2D [] insideGroup; //占쏙옙占쏙옙 키
    public int[] outCount;
    public int[] inCount;
    public int index;

    public GameObject bossUI; //占쏙옙岺占
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
        if (other.CompareTag("Smoke") || other.CompareTag("Mail") || other.CompareTag("Bomb")) //Bomper 수정 필요
        {
            if (!bossUI.activeSelf) //악덕상사 활성화X
                ObstacleCollision(other);
            else //상사 UI 활성화라면, 충돌처리 X
            {
                manager.GameOver(); 
            }
        }
        if(other.CompareTag("Key"))
        {
            CheckKeys(other);

            RemoveInside();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomper")) //Bomper 수정 필요
        {

            if (!bossUI.activeSelf) //악덕상사 활성화X
                ObstacleCollision(collision.gameObject.GetComponent<Collider>());
            else //상사 UI 활성화라면, 충돌처리 X
            {
                manager.GameOver();
            }
        }
    }

    private void CheckKeys(Collider other) //占썸돌占쏙옙 占쏙옙占쏙옙占쏙옙트占쏙옙 占쏙옙占쏙옙 키占쏙옙占쏙옙 占쌓두몌옙 키占쏙옙占쏙옙 확占쏙옙 占쏙옙 Count 占쏙옙占쏙옙
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
                    return;
                }
            }

        }
    }

    private void RemoveInside()
    {
       if(outCount[index] == outsideGroup[index].Keys.Length) //占쌔댐옙 占쌕깍옙占쏙옙 占쌓뤄옙占쏙옙 占쏙옙占쏙옙 占쏙옙 占쏙옙占쏙옙占쏙옙占쏙옙 占싸듸옙占쏙옙占쏙옙
        {
            if (inCount[index] > 0) //占쌔댐옙 占쏙옙占쏙옙 占쌓뤄옙占쏙옙 占쏙옙占썼가 占싹놂옙占쏙옙 占쏙옙占쏙옙占쏙옙占 占쏙옙占
                return; 
            for(int j = 0; j < insideGroup[index].Keys.Length; j++) //占싣니띰옙占, 占쌔댐옙 占쏙옙占쏙옙 占쌓뤄옙占쏙옙 占쏙옙占쏙옙 占쏙옙占 占쏙옙활占쏙옙화 占쏙옙 keyCount 占쏙옙占쏙옙
            {
                insideGroup[index].Keys[j].SetActive(false);
                manager.keyCount++;
            }
        }
    }

    public void ObstacleCollision(Collider obstacle)
    {

        if (obstacle.CompareTag("Mail"))
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
        else if(obstacle.CompareTag("Bomper"))
        {
            Debug.Log("Bomper Collision"); //Bomper 기능 구현
        }

    }

}
