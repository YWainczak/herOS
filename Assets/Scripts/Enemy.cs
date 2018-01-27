using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color color;
    public Sprite sprite;
    
    public float health;
    public float healthMax;

    public float attack; // Attack stat
    public float attackTime; // Time from telegraph until actual attack
    public float attackTimeMax; // Max time for the attack timer
    public float defense; // Defense stat
    public float defenseTime; // Time for defense to be held
    public float defenseTimeMax; //Max time for defense to be held
}
