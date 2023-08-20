using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Def", menuName = "Character_Def", order = 1)]
public class Character_Def : ScriptableObject
{
    public float health;
    public enum CharacterType {Human, Bug, Alien, Robot}
    public Gun_Def weapon;
    public float speed;
    
}
