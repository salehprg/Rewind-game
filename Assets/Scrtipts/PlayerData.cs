using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float maxspeed;
    public float speed;
    
    [Header("Jump")]
    
    public float jumpHeight;
    public float jumpcut_multiplier;
    public bool falling , rising , climbing;
}