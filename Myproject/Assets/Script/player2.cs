using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player2 : MonoBehaviour
{
    Rigidbody2D rigid;
    public Animator animator;
    public float maxSpeed;
    public float jumpPower;
    public bool isjump = true;
    private bool isSit = false;
    private GameObject chair;
    private GameObject chairChild;
    private GameObject miniPanel;
    private GameObject restartPanel;
    public GameObject Target; //��ư�� ������ ����� ��ü
    public GameObject Btn; //��ư�� �������
    public GameObject StartBtn;
    private GameObject panelController;
    private GameObject missionController;
    private void Start()
    {
        if (GameObject.Find("PanelController"))
        {
            panelController = GameObject.Find("PanelController");
        }
    
    }
   
    void Awake()
    {
        missionController = GameObject.Find("MissionController");
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Debug.Log("isjump"+ isjump);
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

        if (Input.GetKey(KeyCode.W) && isSit == false)
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
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Box")
        {
            isjump = true;
            animator.SetBool("IsJumping", false);
        }
        if (other.gameObject.tag == "Obstacle")
        {
            animator.SetBool("IsDie", true);
            restartPanel.SetActive(true);
        }
        if (other.gameObject.tag == "Btn")
        {
            Btn.SetActive(false);
            Target.SetActive(false);
        }
        if (other.gameObject.CompareTag("ChatNPC"))
        {
            GameObject.Find("Canvas").transform.Find("Chat Back").gameObject.SetActive(true);
            StartBtn.SetActive(true);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isjump = false;
            animator.SetBool("IsJumping", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
     {
        if (other.gameObject.tag == "Leaf")
        {
            missionController.GetComponent<MissonContorller>().leafCount++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Head")
        {
            isjump = true;
            animator.SetBool("IsJumping", false);
        }
        if (other.gameObject.CompareTag("River"))
        {
            GameObject.Find("Canvas").transform.Find("Chat Back").gameObject.SetActive(true);
            StartBtn.SetActive(true);
        }
        if (other.gameObject.CompareTag("Npc6"))
        {
            if (panelController)
            {
                panelController.GetComponent<BtnControl>().panelOn = true;
                panelController.GetComponent<BtnControl>().miniPanel.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("ChatNPC"))
        {
            if (GameObject.Find("Canvas").transform.Find("Chat Back1"))
            {
                if (missionController.GetComponent<MissonContorller>().map5Clear == true)
                {
                    GameObject.Find("Canvas").transform.Find("Chat Back2").gameObject.SetActive(true);
                    missionController.GetComponent<MissonContorller>().dropLeaf = true;
                }
                else
                {
                    GameObject.Find("Canvas").transform.Find("Chat Back1").gameObject.SetActive(true);
                }

                GameObject.Find("PanelController").GetComponent<BtnControl>().panelOn = true;
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("Chat Back").gameObject.SetActive(true);
                GameObject.Find("PanelController").GetComponent<BtnControl>().panelOn = true;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chair"))
        {
            chair = collision.gameObject;
            chair.transform.GetChild(2).gameObject.SetActive(true);
            // Debug.Log("����");
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (isSit == false)
                {
                    chair = collision.gameObject;
                    chairChild = chair.transform.GetChild(0).gameObject;
                    chairChild.SetActive(true);
                    gameObject.transform.position = new Vector3(chair.transform.position.x, chair.transform.position.y + 1f, chair.transform.position.z);
                    chair.GetComponent<BoxCollider2D>().isTrigger = false;
                    animator.SetBool("IsSit", true);
                    isSit = true;
                    Debug.Log("��");
                }
            }
        }
        if (collision.gameObject.CompareTag("ChairUp"))
        {
            //Debug.Log("������");
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (isSit == true)
                {
                    chairChild.SetActive(false);
                    chair.GetComponent<BoxCollider2D>().isTrigger = true;
                    animator.SetBool("IsSit", false);
                    isSit = false;
                    Debug.Log("����");
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Chair"))
        {
            chair = collision.gameObject;
            chair.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}