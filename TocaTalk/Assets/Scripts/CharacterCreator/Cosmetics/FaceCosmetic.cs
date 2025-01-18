using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Face", menuName = "Create Customization/Face")]
public class FaceCosmetic : CosmeticItem
{
    [SerializeField] private FaceType faceType;
    
    public FaceType Type {get {return faceType;}}
    public enum FaceType {
        Clean, Beard, Goatee, Mustache, Sideburns
    }
}
