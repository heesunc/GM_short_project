using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public Collider player, bomper, smoke, mail;
    private GameManager manager;

    private float bossTimer;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        player = GameObject.FindWithTag("Player").GetComponent<Collider>(); //tag로 오브젝트 콜라이더 받기
        bomper = GameObject.FindWithTag("Bomper").GetComponent<Collider>();
        smoke = GameObject.FindWithTag("Smoke").GetComponent<Collider>();
        mail = GameObject.FindWithTag("Mail").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        bossTimer += Time.deltaTime; //보스UI 타이머
        if(bossTimer >= 10.0f)
        {
            gameObject.SetActive(false);
            bossTimer = 0.0f;
        }

        if (player.bounds.Intersects(bomper.bounds) || player.bounds.Intersects(smoke.bounds) || player.bounds.Intersects(mail.bounds)) //오브젝트 콜라이더의 경계가 겹치는 경우
        {
            manager.GameOver();
        }


    }
}
