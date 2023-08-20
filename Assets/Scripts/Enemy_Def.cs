using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Def", menuName = "Enemy_Def", order = 1)]
public class Enemy_Def : ScriptableObject
{
    public float health;
    public enum EnemyType {Human, Zombie, Alien}
    public EnemyMoveType moveType;
    public Gun_Def weapon;
    
}
