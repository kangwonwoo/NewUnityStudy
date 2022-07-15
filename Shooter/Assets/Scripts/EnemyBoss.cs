using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float speed;
    public float health;

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    Rigidbody2D rd;

    public float curBulletDelay = 0.0f;
    public float maxBulletDelay;

    public GameObject goBullet;
    public GameObject goPlayer;

    // Start is called before the first frame update
    void Start()  //  Scene에 로드될 때
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //rd = GetComponent<Rigidbody2D>();
        //rd.velocity = Vector2.down * speed;  //  velocity는 속도(방향과 스피드)를 의미

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        ReloadBullet();
    }

    void Fire()
    {
        if (curBulletDelay < maxBulletDelay)
            return;

        GameObject createBullet = Instantiate(goBullet, transform.position, Quaternion.identity);
        Rigidbody2D rd = createBullet.GetComponent<Rigidbody2D>();

        Vector3 dirVec = goPlayer.transform.position - transform.position;

        rd.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        curBulletDelay = 0.0f;
    }

    void ReloadBullet()
    {
        curBulletDelay += Time.deltaTime;
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = col.gameObject.GetComponent<Bullet>();
            OnHit(bullet.power);

            Destroy(col.gameObject);
        }
    }

    void OnHit(float playerBulletPower)
    {
        health -= playerBulletPower;

        spriteRenderer.sprite = sprites[1];

        Invoke("ReturnSprite", 0.1f);  //  StartCouroutine으로도 사용가능

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
