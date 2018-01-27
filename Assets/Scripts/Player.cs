using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player stats

public class Player : MonoBehaviour {

    public float health;
    public float healthMax;

    public float attack;
    public float attackCoolDown;

    public float defense;
    public float defenseCoolDown;
    public float defenseTimer;

    public float exp;
    public float expMax;

    public float money;


	void Start ()
    {
        health = healthMax;
        attack = 1f;
        attackCoolDown = 1f;
        defense = 1f;
        defenseCoolDown = 1f;
        defenseTimer = 1f;
        exp = 0f;
        expMax = 10f;
        money = 0f;
	}
}
