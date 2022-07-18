using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchRight = false;
    public bool isTouchLeft = false;

    Animator anim;

    public GameObject goBullet;

    public float curBulletDelay = 0;
    public float maxBulletDelay;

    public int life = 0;

    public GameObject goGameManager;

    public bool isHit = false;

    public bool[] joyControl;
    public bool isControl;

    public ObjectManager objManager;

    public int nScore;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        ReloadBullet();
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
        {
            return;
        }

        if(curBulletDelay < maxBulletDelay)
            return;

        //GameObject bullet = Instantiate(goBullet, transform.position, Quaternion.identity);
        GameObject bullet = objManager.MakeObject("PlayerBullet");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);  //  Impulse 질량에 대한 변화X
        
        curBulletDelay = 0;
    }

    void ReloadBullet()
    {
        curBulletDelay += Time.deltaTime;
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (joyControl[0]) { h = -1; v =  1; }
        if (joyControl[1]) { h =  0; v =  1; }
        if (joyControl[2]) { h =  1; v =  1; }
        if (joyControl[3]) { h = -1; v =  0; }
        if (joyControl[4]) { h =  0; v =  0; }
        if (joyControl[5]) { h =  1; v =  0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h =  0; v = -1; }
        if (joyControl[8]) { h =  1; v = -1; }

        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl)
        {
            h = 0;
        }

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
        {
            v = 0;
        }

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    public void JoyPanel(int type)
    {
        for(int idx = 0; idx < 9; idx++)
        {
            joyControl[idx] = idx == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }

    public void JoyUp()
    {
        isControl = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            switch(col.gameObject.name)
            {
                case "Top":
                    {
                        isTouchTop = true;
                    }
                    break;
                case "Bottom":
                    {
                        isTouchBottom = true;
                    }
                    break;
                case "Right":
                    {
                        isTouchRight = true;
                    }
                    break;
                case "Left":
                    {
                        isTouchLeft = true;
                    }
                    break;
            }
        }
        else if(col.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;

            isHit = true;

            life--;

            GameManager gManager = goGameManager.GetComponent<GameManager>();

            gManager.UpdateLifeIcon(life);

            if (life == 0)
            {
                gManager.GameOver();
            }
            else
            {
                
                gManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            col.gameObject.SetActive(false); //Destroy(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            switch (col.gameObject.name)
            {
                case "Top":
                    {
                        isTouchTop = false;
                    }
                    break;
                case "Bottom":
                    {
                        isTouchBottom = false;
                    }
                    break;
                case "Right":
                    {
                        isTouchRight = false;
                    }
                    break;
                case "Left":
                    {
                        isTouchLeft = false;
                    }
                    break;
            }
        }
    }
}
