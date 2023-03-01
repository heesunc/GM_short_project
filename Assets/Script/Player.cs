using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum TurnState
    {
        NONE,
        RIGHT,
        LEFT
    };

    //private
    private void Move();
    private void Turn();
    private void Jump();

    private bool Movefront = true;

    public void ChangeMove() //³Ë¹é¿¡ »ç¿ë
        {
            Movefront = !Movefront;
        }

    // Update is called once per frame
    void Update()
    {
        int count = 0;

        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(count);


        } 
    }
}
