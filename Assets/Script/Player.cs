using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public:
    public void ChangeMove() //넉백에서 호출하여 사용 -> 테스트 필요.
    {
        move *= -1;
    }

    //private:
    enum TurnState
    {
        NONE,
        RIGHT = 1,
        LEFT = -1
    };

    private int move = 1;
    private TurnState isturn = TurnState.NONE;
    private bool isJump = false;
    private int f = 5; //점프 힘 조절
    private int speed = 5; //달리기 속도
    private int mistake = 100; //스와이프 길이
    private bool s = false; //오른쪽 터치면 true, 왼쪽 터치면 false

    Rigidbody rg;
    Vector3 startPos;

    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody>();
        startPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) //첫 번째 터치 발생
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Began) //왼쪽 터치면 점프, 오른쪽 터치면 스와이프 준비
            {
                startPos = touch(touch0);
            }
            else if (s && (touch0.phase == TouchPhase.Moved)) //오른쪽 터치일 때: 스와이프 어느쪽인지 확인
            {
                swipe(touch0, startPos);
            }

            if (Input.touchCount > 1) //두 번째 터치 발생 시 첫번째 터치와 같은 방식으로 처리.
            {
                Touch touch1 = Input.GetTouch(1);

                if (touch1.phase == TouchPhase.Began)
                {
                    startPos = touch(touch1);
                }
                else if (s && (touch1.phase == TouchPhase.Moved))
                {
                    swipe(touch1, startPos);
                }
            }
        }

        if (isturn != TurnState.NONE) //회전 명령 확인.
        {
            if (isturn == TurnState.LEFT)
            {

            }
            else //RIGHT
            {
                StartCoroutine(turn()); //테스트 해보기
            }
        }

        rg.velocity = new Vector3(move * speed, 0, 0);

    }

    private Vector3 touch(Touch touch) //터치 발생 -> 왼쪽인지 오른쪽인지 확인.
    {
        if (touch.position.x < 960)
        {
            Jump();
            Debug.Log("왼쪽 터치");
        }
        else
        {
            s = true;
        }

        return touch.position;
    }


    private void swipe(Touch touch, Vector3 startPos) //오른쪽 터치 발생 -> 어느쪽 스와이프인지 확인.
    {
        if (touch.position.x - startPos.x > mistake) //직진보다는 회전 우선
        {
            isturn = TurnState.RIGHT;
            Debug.Log("오른쪽으로 스와이프");
        }
        else if (startPos.x - touch.position.x > mistake)
        {
            isturn = TurnState.LEFT;
            Debug.Log("왼쪽으로 스와이프");
        }
        else if (touch.position.y - startPos.y > mistake)
        {
            move = 1; //앞으로 가도록 함.
            Debug.Log("위쪽으로 스와이프");
        }
        else
            return;

        s = false;
    }
    

    private void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            Debug.Log(isJump);
            rg.AddForce(Vector3.up * f, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other) //점프 가능 상태 확인.
    {
        if (other.collider.CompareTag("Floor")) //대소문자 확인
        {
            isJump = false;
        }
    }

    IEnumerator turn() //회전 메소드.
    {
        int rY = (int)rg.rotation.y;
        while (rY < 90)
        {
            rY += 5;
            rg.MoveRotation(Quaternion.Euler(0, (int)isturn * rY, 0));
            yield return null;
        }
    }




}
