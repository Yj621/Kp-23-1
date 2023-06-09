using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map3Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    public float jumpPower;
    private bool isJump = true;
    // private bool isDie = false;
    public bool isBtn1 = false;
    private GameObject missionController;
    public Animator animator;
    public GameObject BlueObs; 
    public GameObject BlueBtn; 
    public GameObject OrgBtn;
    public GameObject OrgBtn2;

    public GameObject map3;

    private void Start()
    {
        missionController = GameObject.Find("MissionController");
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isJump /* && !Animation.GetBool("isJumping")*/) //�����̽��ٸ� ������, ĳ���Ͱ� ���� �ִٸ�
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = false;

        }
        if (Input.GetButtonDown("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //점프
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("IsJumping", true);
        }
        else animator.SetBool("IsJumping", false);
        //걷기
        if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("IsWalking", true);
        }
        else animator.SetBool("IsWalking", false);

    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
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
            isJump = true;
            animator.SetBool("IsJumping", false);
        }
        //장애물 닿아 죽을 때
        if (other.gameObject.tag == "Obstacle")
        {
            animator.SetBool("IsDie", true);
        }
        //파랑 버튼
        if (other.gameObject.tag == "BlueBtn")
        {
            BlueObs.SetActive(false);
            BlueBtn.SetActive(false);
        }
        //주황버튼
        if (other.gameObject.tag == "OrBtn")
        {
            OrgBtn.SetActive(false);
            GameObject.Find("Orange").transform.Find("Org").gameObject.SetActive(true);
            OrgBtn2.SetActive(true);
        }

        if (other.gameObject.tag == "Blind")
        {
            Destroy(other.gameObject);
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Head")
        {
            isJump = true;
            animator.SetBool("IsJumping", false);
        }
        //나뭇잎 먹을 때
        if (other.gameObject.tag == "Leaf")
        {
            map3.GetComponent<Map3>().getLeaf = true;
            missionController.GetComponent<MissonContorller>().leafCount++;
            Destroy(other.gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        //버튼 누를 때
        if (other.gameObject.tag == "Btn1")
        {
            isBtn1 = true;
            missionController.GetComponent<MissonContorller>().map3Btn1 = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isBtn1 = false;
        missionController.GetComponent<MissonContorller>().map3Btn1 = false;
    }
}
