using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterData", menuName = "ScriptableObjects/Spawn Fighter Data", order = 1)]
public class FighterData : ScriptableObject
{
    [Range(50,1000)]
    public int Health = 100;
    [Range(1, 1000)]
    public int Damage = 10;
}
