using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    GameManager manager;
    public Text GetMoneyTextO;
    public Text GetMoneyTextC;
    public Text RestMoneyTextO;
    public Text RestMoneyTextC;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 획득한 돈
        GetMoneyTextO.text="획득한 돈 : 0";
        GetMoneyTextC.text="획득한 돈 : 0";

        // 남은 돈 개수
        int rest = manager.keyFind.Length;
        RestMoneyTextO.text="남은 돈 : "+rest;

        // 획득한 돈 개수
    }
}