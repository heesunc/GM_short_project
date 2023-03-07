using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    [Serializable]
    public class Array2D //2���� �迭 Ŭ����
    {
        public GameObject [] Keys; 
    }
    public Array2D [] outsideGroup; //�ٱ��� Ű
    public Array2D [] insideGroup; //���� Ű
    public int[] outCount;
    public int[] inCount;
    public int index;

    public GameObject bossUI; //��ֹ�
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
        if (bossUI.activeSelf) //�Ǵ���� Ȱ��ȭ
        {
            if (other.CompareTag("Smoke") || other.CompareTag("Mail") || other.CompareTag("Bomper"))
                manager.GameOver();
        }

        if (other.CompareTag("Mail") || other.CompareTag("Smoke") || other.CompareTag("Bomb")) //��ֹ� �浹ó��
        {
            ObstacleCollision(other);
        }

        if(other.CompareTag("Key"))
        {
            CheckKeys(other);

            RemoveInside();
        }
    }


    private void CheckKeys(Collider other) //�浹�� ������Ʈ�� ���� Ű���� �׵θ� Ű���� Ȯ�� �� Count ����
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
       if(outCount[index] == outsideGroup[index].Keys.Length) //�ش� �ٱ��� �׷��� ���� �� �������� �ε�����
        {
            if (inCount[index] > 0) //�ش� ���� �׷��� ���谡 �ϳ��� ������� ���
                return; 
            for(int j = 0; j < insideGroup[index].Keys.Length; j++) //�ƴ϶��, �ش� ���� �׷��� ���� ��� ��Ȱ��ȭ �� keyCount ����
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
