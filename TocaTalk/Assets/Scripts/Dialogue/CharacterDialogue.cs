using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDialogue 
{
    [TextArea(3, 10)]
    public string sentence;
    public NPC npc;

}
