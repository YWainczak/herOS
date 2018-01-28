using System.Collections;
using UnityEngine;

public class Enemy : ScriptableObject
{
    public Color color;
	public Color accentColor;
    public Sprite sprite;
    
    public float health;

    public float attack; // Attack stat
    public float attackTime; // Time from telegraph until actual attack
    public float attackTimeMax; // Max time for the attack timer
    public float defense; // Defense stat
    public float defenseTime; // Time for defense to be held
    public float defenseTimeMax; //Max time for defense to be held
}
