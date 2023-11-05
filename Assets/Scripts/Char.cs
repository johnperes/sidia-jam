using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    int maxHp = 100;
    [HideInInspector]
    public int hp;
    public Transform hpbar;
    public BulletSpawn.BulletSpawnTypes type;
    public Animator animator;
    public bool dead = false;

    public Color criticalColor;
    public Color normalColor;
    public Color reducedColor;
    public ParticleSystem hitParticle;

    public BulletSpawn bulletSpawnEnemy;

    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        hpbar.localScale = new Vector3((float) hp / 100, hpbar.localScale.y, hpbar.localScale.z);
        if (hp == 0)
        {
            animator.SetBool("dead", true);
        } 
        else
        {
            animator.SetBool("dead", false);
        }
    }

    public void Heal(int value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public bool IsAlive()
    {
        return (hp > 0);
    }

    void GetHit(int dmg, Color color)
    {
        hp -= dmg;
        hitParticle.startColor = color;
        hitParticle.Play();
        if (GameConfig.Instance.lifesteal)
        {
            bulletSpawnEnemy.currentObj.GetComponent<Char>().hp += (int) dmg / 2;
            if (bulletSpawnEnemy.currentObj.GetComponent<Char>().hp > 100)
            {
                bulletSpawnEnemy.currentObj.GetComponent<Char>().hp = 100;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet.bulletSpawnTypes == BulletSpawn.BulletSpawnTypes.Rock)
        {
            if (type == BulletSpawn.BulletSpawnTypes.Scissors)
            {
                GetHit(GameConfig.Instance.criticalDamage, criticalColor);
            }
            else if (type == BulletSpawn.BulletSpawnTypes.Rock)
            {
                GetHit(GameConfig.Instance.normalDamage, normalColor);
            }
            else if (type == BulletSpawn.BulletSpawnTypes.Paper)
            {
                GetHit(GameConfig.Instance.reducedDamage, reducedColor);
            }
        }
        if (bullet.bulletSpawnTypes == BulletSpawn.BulletSpawnTypes.Paper)
        {
            if (type == BulletSpawn.BulletSpawnTypes.Scissors)
            {
                GetHit(GameConfig.Instance.reducedDamage, reducedColor);
            }
            else if (type == BulletSpawn.BulletSpawnTypes.Rock)
            {
                GetHit(GameConfig.Instance.criticalDamage, criticalColor);
            }
            else if (type == BulletSpawn.BulletSpawnTypes.Paper)
            {
                GetHit(GameConfig.Instance.normalDamage, normalColor);
            }
        }
        if (bullet.bulletSpawnTypes == BulletSpawn.BulletSpawnTypes.Scissors)
        {
            if (type == BulletSpawn.BulletSpawnTypes.Scissors)
            {
                GetHit(GameConfig.Instance.normalDamage, normalColor);
            }
            else if (type == BulletSpawn.BulletSpawnTypes.Rock)
            {
                GetHit(GameConfig.Instance.reducedDamage, reducedColor);
            }
            else if (type == BulletSpawn.BulletSpawnTypes.Paper)
            {
                GetHit(GameConfig.Instance.criticalDamage, criticalColor);
            }
        }
        if (hp <= 0)
        {
            AudioManager.Instance.PlayDie();
            hp = 0;
        }
        else
        {
            AudioManager.Instance.PlayHit();
        }
        Destroy(other.gameObject);
    }
}
