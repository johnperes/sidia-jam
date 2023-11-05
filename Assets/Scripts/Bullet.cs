using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public bool directionRight = true;
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    
    public BulletSpawn.BulletSpawnTypes bulletSpawnTypes = BulletSpawn.BulletSpawnTypes.Rock;

    public Sprite bulletSpriteRock;
    public Sprite bulletSpritePaper;
    public Sprite bulletSpriteScissors;

    public void UpdateSprite()
    {
        if (bulletSpawnTypes == BulletSpawn.BulletSpawnTypes.Rock) {
            spriteRenderer.sprite = bulletSpriteRock;
        }
        if (bulletSpawnTypes == BulletSpawn.BulletSpawnTypes.Paper) {
            spriteRenderer.sprite = bulletSpritePaper;
        }
        if (bulletSpawnTypes == BulletSpawn.BulletSpawnTypes.Scissors) {
            spriteRenderer.sprite = bulletSpriteScissors;
        }
    }

    void Update() 
    {
        transform.Translate((transform.right * (directionRight ? 1 : -1) * speed * Time.deltaTime));
    }
}
