using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;

    public bool onGround = true;//True or False
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    // Start is called before the first frame update
    void Move(bool ifLeft)
    {
        if (ifLeft)
        {
            this.transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            this.transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
    }
    void Jump()
    {
        if (onGround)
        {
            Vector2 velocity;
            velocity = this.GetComponent<Rigidbody2D>().velocity;
            velocity.y = jumpSpeed;
            this.GetComponent<Rigidbody2D>().velocity = velocity;
            onGround = false;
        }
        
    }
    public void TurnOver(bool ifLeft)
    {
        if (ifLeft)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    Vector3 Direction()
    {
        Vector3 mousePosOnScreen = Input.mousePosition;
        mousePosOnScreen.z = 0;
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        return (mousePosInWorld - this.transform.position);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))//往左走
        {
            Move(true);
        }
        if (Input.GetKey(KeyCode.D))//往右走
        {
            Move(false);
        }
        if (Input.GetKeyDown(KeyCode.W))//跳跃
        {
            Jump();
        }
        if (Direction().x < 0)//左转
        {
            TurnOver(true);
        }
        else//右转
        {
            TurnOver(false);
        }
    }
}
