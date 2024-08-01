using UnityEngine;

[CreateAssetMenu(fileName ="New SpawnThing", menuName ="ScriptableObjeckt/Spawn/SpawnThing")]
public class ScObjFood : ScriptableObject
{
    public int id;
    public string name;
    public GameObject model;
}
