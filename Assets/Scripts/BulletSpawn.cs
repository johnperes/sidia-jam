using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public enum BulletSpawnTypes
    {
        Rock,
        Paper,
        Scissors
    }

    public GameObject bulletPrefab;
    public float attackDelay = 1f;
    float attackTimer = 0f;
    public BulletSpawnTypes bulletSpawnTypes = BulletSpawnTypes.Rock;

    public KeyCode rockKey;
    public KeyCode paperKey;
    public KeyCode scissorsKey;

    [HideInInspector]
    public Char currentObj;
    public Char rockObj;
    public Char paperObj;
    public Char scissorsObj;

    public GameObject swapKeyRock;
    public GameObject swapKeyPaper;
    public GameObject swapKeyScissors;

    public bool cpuPlace = false;
    public BulletSpawn otherPlayer;

    bool canSwap = false;

    float swapTimer;
    public Transform swapFill;
    public bool gameOver = false;

    float cpuDelay = 0.75f;
    float cpuTimer = 0f;

    float healDelay = 0.25f;
    float healTimer = 0f;

    void Start()
    {
        currentObj = rockObj;
        swapTimer = GameConfig.Instance.swapDelay;
        rockObj.animator.SetBool("atk", true);
        if (cpuPlace && GameConfig.Instance.cpuGame)
        {
            swapKeyRock.SetActive(false);
            swapKeyPaper.SetActive(false);
            swapKeyScissors.SetActive(false);
        }
    }

    void Update()
    {
        if (gameOver) return;

        if (GameConfig.Instance.heal)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= healDelay)
            {
                healTimer = 0;
                rockObj.Heal(1);
                paperObj.Heal(1);
                scissorsObj.Heal(1);
            }
        }


        attackTimer += Time.deltaTime;
        swapTimer -= Time.deltaTime;
        if (swapTimer <= 0)
        {
            canSwap = true;
        }
        else
        {
            swapFill.localScale = new Vector3((float) swapTimer / GameConfig.Instance.swapDelay, swapFill.localScale.y, swapFill.localScale.z);
        }

        if (canSwap)
        {
            if (cpuPlace && GameConfig.Instance.cpuGame && GameConfig.Instance.cpuLevel == 0)
            {
                CpuLevel0();
            }
            else if (cpuPlace && GameConfig.Instance.cpuGame && GameConfig.Instance.cpuLevel == 1)
            {
                CpuLevel1();
            }
            else
            {
                if (Input.GetKeyDown(rockKey) && bulletSpawnTypes != BulletSpawnTypes.Rock && rockObj.IsAlive())
                {
                    Swap(rockObj, BulletSpawnTypes.Rock, true);
                }
                else if (Input.GetKeyDown(paperKey) && bulletSpawnTypes != BulletSpawnTypes.Paper && paperObj.IsAlive())
                {
                    Swap(paperObj, BulletSpawnTypes.Paper, true);
                }
                else if (Input.GetKeyDown(scissorsKey) && bulletSpawnTypes != BulletSpawnTypes.Scissors && scissorsObj.IsAlive())
                {
                    Swap(scissorsObj, BulletSpawnTypes.Scissors, true);
                }
            }
        }

        if (!rockObj.IsAlive() && !paperObj.IsAlive() && !scissorsObj.IsAlive())
        {
            gameOver = true;
            GameConfig.Instance.GetBirdEvent().SetActive(false);
        }
        else
        {
            if (attackTimer >= attackDelay)
            {
                AudioManager.Instance.PlayShoot();
                GameObject obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                obj.GetComponent<Bullet>().bulletSpawnTypes = bulletSpawnTypes;
                obj.GetComponent<Bullet>().UpdateSprite();
                attackTimer = 0f;
            }

            if (currentObj.GetComponent<Char>().hp <= 0)
            {
                if (!rockObj.IsAlive() && paperObj.IsAlive())
                {
                    Swap(paperObj, BulletSpawnTypes.Paper, false);
                }
                if (!paperObj.IsAlive() && scissorsObj.IsAlive())
                {
                    Swap(scissorsObj, BulletSpawnTypes.Scissors, false);
                }
                if (!scissorsObj.IsAlive() && rockObj.IsAlive())
                {
                    Swap(rockObj, BulletSpawnTypes.Rock, false);
                }
            }
        }
    }

    void CpuLevel0()
    {
        cpuTimer += Time.deltaTime;
        if (cpuTimer >= cpuDelay)
        {
            if (Random.Range(0, 10) > 8)
            {
                if (otherPlayer.currentObj == otherPlayer.rockObj)
                {
                    Swap(paperObj, BulletSpawnTypes.Paper, true);
                }
                else if (otherPlayer.currentObj == otherPlayer.paperObj)
                {
                    Swap(scissorsObj, BulletSpawnTypes.Scissors, true);
                }
                else if (otherPlayer.currentObj == otherPlayer.scissorsObj)
                {
                    Swap(rockObj, BulletSpawnTypes.Rock, true);
                }
            }
            cpuTimer = 0f;
        }
    }

    void CpuLevel1()
    {
        if (otherPlayer.currentObj == otherPlayer.rockObj)
        {
            Swap(paperObj, BulletSpawnTypes.Paper, true);
        }
        else if (otherPlayer.currentObj == otherPlayer.paperObj)
        {
            Swap(scissorsObj, BulletSpawnTypes.Scissors, true);
        }
        else if (otherPlayer.currentObj == otherPlayer.scissorsObj)
        {
            Swap(rockObj, BulletSpawnTypes.Rock,  true);
        }
    }

    public void Swap(Char toChar, BulletSpawnTypes toType, bool resetTimer)
    {
        if (currentObj == toChar) return;
        AudioManager.Instance.PlaySwap();
        bulletSpawnTypes = toType;
        Vector3 oldPosition = toChar.transform.position;
        toChar.transform.position = currentObj.transform.position;
        toChar.animator.SetBool("atk", true);
        currentObj.transform.position = oldPosition;
        currentObj.animator.SetBool("atk", false);
        currentObj = toChar;
        if (resetTimer)
        {
            canSwap = false;
            swapTimer = GameConfig.Instance.swapDelay;
        }
    }
}
