using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public:
    public void ChangeMove() //�˹鿡�� ȣ���Ͽ� ��� -> �׽�Ʈ �ʿ�.
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
    private int f = 5; //���� �� ����
    private int speed = 5; //�޸��� �ӵ�
    private int mistake = 100; //�������� ����
    private bool s = false; //������ ��ġ�� true, ���� ��ġ�� false

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
        if (Input.touchCount > 0) //ù ��° ��ġ �߻�
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Began) //���� ��ġ�� ����, ������ ��ġ�� �������� �غ�
            {
                startPos = touch(touch0);
            }
            else if (s && (touch0.phase == TouchPhase.Moved)) //������ ��ġ�� ��: �������� ��������� Ȯ��
            {
                swipe(touch0, startPos);
            }

            if (Input.touchCount > 1) //�� ��° ��ġ �߻� �� ù��° ��ġ�� ���� ������� ó��.
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

        if (isturn != TurnState.NONE) //ȸ�� ��� Ȯ��.
        {
            if (isturn == TurnState.LEFT)
            {

            }
            else //RIGHT
            {
                StartCoroutine(turn()); //�׽�Ʈ �غ���
            }
        }

        rg.velocity = new Vector3(move * speed, 0, 0);

    }

    private Vector3 touch(Touch touch) //��ġ �߻� -> �������� ���������� Ȯ��.
    {
        if (touch.position.x < 960)
        {
            Jump();
            Debug.Log("���� ��ġ");
        }
        else
        {
            s = true;
        }

        return touch.position;
    }


    private void swipe(Touch touch, Vector3 startPos) //������ ��ġ �߻� -> ����� ������������ Ȯ��.
    {
        if (touch.position.x - startPos.x > mistake) //�������ٴ� ȸ�� �켱
        {
            isturn = TurnState.RIGHT;
            Debug.Log("���������� ��������");
        }
        else if (startPos.x - touch.position.x > mistake)
        {
            isturn = TurnState.LEFT;
            Debug.Log("�������� ��������");
        }
        else if (touch.position.y - startPos.y > mistake)
        {
            move = 1; //������ ������ ��.
            Debug.Log("�������� ��������");
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

    void OnCollisionEnter(Collision other) //���� ���� ���� Ȯ��.
    {
        if (other.collider.CompareTag("Floor")) //��ҹ��� Ȯ��
        {
            isJump = false;
        }
    }

    IEnumerator turn() //ȸ�� �޼ҵ�.
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
