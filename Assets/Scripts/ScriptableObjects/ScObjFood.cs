using UnityEngine;

[CreateAssetMenu(fileName ="New SpawnThing", menuName ="ScriptableObjeckt/SpawnThing")]
public class ScObjFood : ScriptableObject
{
    public int id;
    public Rarely rarely;
    public GameObject model;
    public Transform sellShalf;

    public enum Rarely { Default, Rare }
}
