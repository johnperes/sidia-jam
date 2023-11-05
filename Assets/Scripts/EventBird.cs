using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBird : MonoBehaviour
{
    public float speed = 10;
    bool eventTrigger = false;
    bool firstPart = false;
    bool secondPart = false;

    float eventDelay = 6f;
    float eventTimer = 0f;

    float birdDelay = 1f;
    float birdTimer = 0f;

    public GameObject dmgUpIcon;
    public GameObject fastIcon;
    public GameObject lifeStealIcon;
    public GameObject healIcon;

    int mode;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        mode = Random.Range(0, 4);
    }

    void Update()
    {
        birdTimer += Time.deltaTime;

        if (birdTimer > birdDelay && !firstPart && !secondPart)
        {
            firstPart = true;
        }
        else if (birdTimer > birdDelay && secondPart)
        {
            eventTimer = 0f;
            birdTimer = 0f;
            eventTrigger = false;
            firstPart = false;
            secondPart = false;
            mode = Random.Range(0, 4);
            transform.position = startPosition;
        }

        if ((transform.position.x < 0 && firstPart) || secondPart)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else if (firstPart)
        {
            firstPart = false;
            eventTrigger = true;

            switch (mode)
            {
                case 0:
                    GameConfig.Instance.IncreaseDmg();
                    dmgUpIcon.SetActive(true);
                    break;
                case 1:
                    GameConfig.Instance.swapDelay = 0.1f;
                    fastIcon.SetActive(true);
                    break;
                case 2:
                    GameConfig.Instance.lifesteal = true;
                    lifeStealIcon.SetActive(true);
                    break;
                case 3:
                    GameConfig.Instance.heal = true;
                    healIcon.SetActive(true);
                    break;
            }
        }
        if (eventTrigger)
        {
            eventTimer += Time.deltaTime;

            if (eventTimer > eventDelay)
            {
                eventTrigger = false;
                secondPart = true;
                birdTimer = 0f;

                switch (mode)
                {
                    case 0:
                        GameConfig.Instance.ResetDmg();
                        dmgUpIcon.SetActive(false);
                        break;
                    case 1:
                        GameConfig.Instance.swapDelay = 1f;
                        fastIcon.SetActive(false);
                        break;
                    case 2:
                        GameConfig.Instance.lifesteal = false;
                        lifeStealIcon.SetActive(false);
                        break;
                    case 3:
                        GameConfig.Instance.heal = false;
                        healIcon.SetActive(false);
                        break;
                }
            }
        }
        
    }
}
