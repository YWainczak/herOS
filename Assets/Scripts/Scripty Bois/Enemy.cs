using System.Collections;
using UnityEngine;

public class Enemy : ScriptableObject
{
    public Color color;
	public Color accentColor;
	public Color powerColor;

    public Sprite sprite;
    
    public float health;

    public float attack; // Attack stat
    public float attackTime; // Time from telegraph until actual attack
    public float attackCooldownMax; // How long can their cooldown be?
	public float attackCooldownMin; // How short can their cooldown be?
}
