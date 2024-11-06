using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Create NPC")]
public class NPC : ScriptableObject
{
    [SerializeField] string _name;

    [SerializeField] Sprite sprite;

    public string Name {get {return _name;} }
    public Sprite Sprite {get {return sprite;}}
}
