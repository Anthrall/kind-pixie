using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellsNames
    { Fear,Happy,Human,Animal,Plant,Weather }

public enum CharacterName
{ Grandmother, Aunt, Wife, Husband, Son, Daughter }

public enum ResultSpell
{ Good, Bad, Neutral}

[System.Serializable]
public class Spells 
{
    //public string name; 
    public SpellsNames SpellName;

    
}

[System.Serializable]
public class Illusions
{
    public string name;
    public Spells FirstSpell, SecondSpell;
    
    [TextArea(3, 10)]
    public string Description;

    public Character[] characters;
}

[System.Serializable]
public class Character
{
    public CharacterName name;
    public ResultSpell result;
}
