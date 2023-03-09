using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void ChangeMove() //�˹�
    {
        move *= -1;
    }

    public void oderTurnR()
    {
        isturn = TurnState.RIGHT;
    }

    public void oderTurnL()
    {
        isturn = TurnState.LEFT;
    }

    public void oderFront()
    {
        move = 1;
    }


    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            rg.AddForce(Vector3.up * f, ForceMode.Impulse);
        }
    }

    public bool seeX;
    public Animator anim;
    
    //private:
    enum TurnState
    {
        NONE,
        RIGHT = 1,
        LEFT = -1
    };

    const int TILE = 7;

    private int move = 1;
    private TurnState isturn = TurnState.NONE;
    private bool isJump = false;
    private int f = 5; //���� �� ����
    public float speed = 7; //�޸��� �ӵ�
    private int mistake = 100; //�������� ����
    private bool s = false; //���� ��ġ�� true, ������ ��ġ�� false
    private bool go = true; //������ ���� �� => ���� ���̴� �뵵
    private float playTime = 0f;

    Rigidbody rg;
    Transform tf;
    Vector3 startPos;

    void Start()
    {
        //seeX = false;
        tf = gameObject.GetComponent<Transform>();
        rg = gameObject.GetComponent<Rigidbody>();
        startPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Swipe�� ��� ��ġ �Է� �ޱ�
        //�ƿ� ��ġ �޴� �κ��� �и��ϰ� �ٸ� ������Ʈ�� �ٿ��� ������Ʈ false�ϴ� �� �� ����� ��
        if (Option.getController() == Controller.SWIPE && Input.touchCount > 0) //ù ��° ��ġ �߻�
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Began) //������ ��ġ�� ����, ���� ��ġ�� �������� �غ�
            {
                startPos = touch(touch0);
            }
            else if (s && (touch0.phase == TouchPhase.Moved)) //���� ��ġ�� ��: �������� ��������� Ȯ��
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

        //ȸ�� ���� Ȯ��
        if (isturn != TurnState.NONE)
        {
            Debug.Log("��ư�� ���� ����");
            float x = tf.position.x;
            float z = tf.position.z;

            if ((seeX && checkPlace(x) && Math.Round(x) % TILE == 0) //x������ �̵� ���� ��� x���� Ÿ�� ������ ��
                || (!seeX && checkPlace(z) && Math.Round(z) % TILE == 0)) //z������ �̵� ���� ��� z���� Ÿ�� ������ �� ȸ��.
            {
                Debug.Log("checkPlace�� �Ǵµ�");
                go = false;

                if (isturn == TurnState.LEFT)
                {
                    StartCoroutine(turnL());
                }
                else //RIGHT
                {
                    StartCoroutine(turnR()); //�׽�Ʈ �غ���
                }
                seeX = !seeX;
                isturn = TurnState.NONE;
            }
        }
        
        //������ �����
        if (go == true)
        {
            tf.Translate(Vector3.forward * speed * move * Time.deltaTime); //������ ������ ��, ���� �ӵ� ����
        }

        //�ð��� ���� ���ӵ�
        //�÷���Ÿ�� ���࿡ �ʿ��ϸ� �� ��° �������� ���� ���� ����.
        playTime += Time.deltaTime;
        if (playTime > 25)
        {
            playTime = 0;
            speedUp();
        }
    }

    private void speedUp()
    {
        speed *= 1.2f;
    }

    private bool checkPlace (float x) //������ �ʹ� ũ�ϱ� ���ݸ� �� ���� ���ƶ�
    {
        float xf = x - (float)Math.Floor(x);
        Debug.Log(xf);

        if (0.75 <= xf || xf <= 0.25)
            return true; //xd + 1
        else 
            return false; //xd
    }

    private Vector3 touch(Touch touch) //��ġ �߻� -> �������� ���������� Ȯ��.
    {
        if (touch.position.x > 960)
        {
            Jump();
            Debug.Log("������ ��ġ");
        }
        else
        {
            s = true;
        }

        return touch.position;
    }

    private void swipe(Touch touch, Vector3 startPos) //���� ��ġ �߻� -> ����� ������������ Ȯ��.
    {
        if (touch.position.x - startPos.x > mistake) //�������ٴ� ȸ�� �켱
        {
            oderTurnR();
            Debug.Log("���������� ��������");
        }
        else if (startPos.x - touch.position.x > mistake)
        {
            isturn = TurnState.LEFT;
            Debug.Log("�������� ��������");
        }
        else if (touch.position.y - startPos.y > mistake)
        {
            oderFront();
            Debug.Log("�������� ��������");
        }
        else
            return;

        s = false;
    }
    
    private void OnCollisionEnter(Collision other) //���� ���� ���� Ȯ��.
    {
        if (other.collider.CompareTag("Floor")) //��ҹ��� Ȯ��
        {
            isJump = false;
        }
        
    }

    private int turnGoal(int r)
    {
        if (r % 90 == 0)
        {
            if (isturn == TurnState.RIGHT)
                return r + 90;
            else
                return r - 90;
        }

        return r;
    }

    IEnumerator turnR() //ȸ�� �޼ҵ�.
    {
        int rY = (int)tf.eulerAngles.y;
        int goal = turnGoal(rY); //��ǥ ����

        while (rY != goal)
        {
            rY += 5;
            rg.MoveRotation(Quaternion.Euler(0, rY, 0));
            yield return null;
        }
        go = true;
    }

    IEnumerator turnL() //ȸ�� �޼ҵ�.
    {
        int rY = (int)tf.eulerAngles.y;
        int goal = turnGoal(rY); //��ǥ ����

        while (rY != goal)
        {
            rY -= 5;
            rg.MoveRotation(Quaternion.Euler(0, rY, 0));
            yield return null;
        }
        go = true;
    }
}
