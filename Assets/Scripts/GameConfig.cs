using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
	public static GameConfig Instance;
	public bool cpuGame = false;
	public int cpuLevel = 0;

	[HideInInspector]
	public int criticalDamage = 6;
	[HideInInspector]
	public int normalDamage = 3;
	[HideInInspector]
	public int reducedDamage = 1;

	public float swapDelay = 1f;
	public bool lifesteal = false;
	public bool heal = false;

	GameObject birdEvent;

	void Awake()
	{
		if (Instance == null)
        {
			DontDestroyOnLoad(this);
			Instance = this;
		}
		else
        {
			Destroy(gameObject);
		}
	}

	public void IncreaseDmg()
    {
		criticalDamage = 12;
		normalDamage = 6;
		reducedDamage = 2;
	}

	public void ResetDmg()
	{
		criticalDamage = 6;
		normalDamage = 3;
		reducedDamage = 1;
	}

	public GameObject GetBirdEvent()
    {
		if (birdEvent == null)
        {
			birdEvent = GameObject.Find("Bird");
		}
		return birdEvent;
	}
}
