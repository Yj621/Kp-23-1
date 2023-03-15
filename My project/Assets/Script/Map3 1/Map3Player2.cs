using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Player2 : MonoBehaviour
{
    Rigidbody2D rigid;
    public Animator animator;
    public float maxSpeed;
    public float jumpPower;
    private bool isjump = true;
    private bool isJump = true;
    private bool isDie = false;
    public GameObject RedObs; //��ư�� ������ ����� ��ü
    public GameObject RedBtn; //��ư�� �������

    public GameObject BlackObs; 
    public GameObject BlackBtn; 

    public GameObject GreenObs; 
    public GameObject GreenBtn;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && isjump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isjump = false;
        }
        //���⶧ �ӵ�
        if (Input.GetButtonDown("Left Right Arrow"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("IsJumping", true);
        }
        else animator.SetBool("IsJumping", false);

        if (Input.GetButton("Left Right Arrow"))
        {
            animator.SetBool("IsWalking", true);
        }
        else animator.SetBool("IsWalking", false);

    }


    void FixedUpdate()
    {
        //�����϶� �ӵ�
        float h = Input.GetAxisRaw("Left Right Arrow");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isjump = true;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            animator.SetBool("IsDie", true);
        }

        if (other.gameObject.tag == "RedBtn")
        {
            RedObs.SetActive(false);
            RedBtn.SetActive(false);
        }
        if (other.gameObject.tag == "BlackBtn")
        {
            BlackObs.SetActive(false);
            BlackBtn.SetActive(false);
        }
        if (other.gameObject.tag == "GreenBtn")
        {
            GreenObs.SetActive(false);
            GreenBtn.SetActive(false);
        }


        if (other.gameObject.tag == "Blind")
        {
            Destroy(other.gameObject);
        }
    }
}