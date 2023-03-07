using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
    GameObject[] keyFind; //Scene에 존재하는 키의 수
=======
    public GameObject[] keyFind; //Scene에 존재하는 키의 수
>>>>>>> fd341e66d8e686b0234de83dc4711afa8304db9a

    public int keyCount; //획득한 키의 수
    public GameObject keyCountUI;
    private Text keyCountText;

    public bool isClear;
    public bool isOver;
    public GameObject GameOver_UI;
    public GameObject GameClear_UI;
<<<<<<< HEAD
    

    
    // Start is called before the first frame update
=======

>>>>>>> fd341e66d8e686b0234de83dc4711afa8304db9a
    void Start()
    {
        keyCount = 0; //player가 획득한 key 개수 0으로 초기화
        keyFind = GameObject.FindGameObjectsWithTag("Key"); //Scene 전체의 키 찾기
        keyCountText = keyCountUI.GetComponentInChildren<Text>(); //keyCountUI의 자식 keyCountText의 Text 컴포넌트 get
    }

    void Update()
    {
        
        keyCountText.text = keyCount.ToString(); //KeyCount UI

        if (keyFind.Length == keyCount)
        {
            GameClear();
<<<<<<< HEAD
        }     
=======
        }

>>>>>>> fd341e66d8e686b0234de83dc4711afa8304db9a
    }

    public void GameOver()
    {
        if (isOver || isClear)
        {
            return;
        }
        isOver = true;
        Debug.Log("GameOver!");
        Time.timeScale = 0;
        GameOver_UI.SetActive(true);
        keyCountUI.SetActive(false);
        
    }

    public void GameClear()
    {
        if (isClear || isOver)
        {
            return;
        }
        isClear = true;
        Debug.Log("GameClear!");
        Time.timeScale = 0;
        GameClear_UI.SetActive(true);
        keyCountUI.SetActive(false);
        
    }

}
