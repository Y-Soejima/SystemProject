using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityList
{
    Normal,
    Rare,
    SuperRare,
}
public class CharacterBase : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] int Id;
    public RarityList Rarity;
}
