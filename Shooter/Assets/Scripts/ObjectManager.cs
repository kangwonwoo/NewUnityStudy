using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    GameObject[] goEnemies;
    public GameObject enemyPrefab;

    GameObject[] goEnemies1;
    public GameObject enemyPrefab1;

    GameObject[] goBulletPlayer;
    public GameObject bulletPlayer;

    GameObject[] goBulletEnemy;
    public GameObject bulletEnemy;

    GameObject[] goTargetPool;

    // Start is called before the first frame update
    void Start()
    {
        goEnemies = new GameObject[10];

        goEnemies1 = new GameObject[10];

        goBulletEnemy = new GameObject[100];

        goBulletPlayer = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < goEnemies.Length; i++)
        {
            goEnemies[i] = Instantiate(enemyPrefab);
            goEnemies[i].SetActive(false);
        }

        for (int i = 0; i < goEnemies.Length; i++)
        {
            goEnemies1[i] = Instantiate(enemyPrefab1);
            goEnemies1[i].SetActive(false);
        }

        for (int i = 0; i < goBulletEnemy.Length; i++)
        {
            goBulletEnemy[i] = Instantiate(bulletEnemy);
            goBulletEnemy[i].SetActive(false);
        }

        for (int i = 0; i < goBulletPlayer.Length; i++)
        {
            goBulletPlayer[i] = Instantiate(bulletPlayer);
            goBulletPlayer[i].SetActive(false);
        }
    }

    public GameObject MakeObject(string objType)
    {
        switch (objType)
        {
            case "A":
                {
                    goTargetPool = goEnemies;
                }
                break;
            case "B":
                {
                    goTargetPool = goEnemies1;
                }
                break;
            case "EnemyBullet":
                {
                    goTargetPool = goBulletEnemy;
                }
                break;
            case "PlayerBullet":
                {
                    goTargetPool = goBulletPlayer;
                }
                break;
        }

        for (int i = 0; i < goTargetPool.Length; i++)
        {
            if (goTargetPool[i].activeSelf == false)
            {
                goTargetPool[i].SetActive(true);
                return goTargetPool[i];
            }
        }

        return null;
    }
}


