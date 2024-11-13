using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Create NPC")]
public class NPCBase : ScriptableObject
{
    [SerializeField] string _name;

    [SerializeField] Sprite _sprite;

    public string Name {get {return _name;} }
    public Sprite Sprite {get {return _sprite;}}
}
