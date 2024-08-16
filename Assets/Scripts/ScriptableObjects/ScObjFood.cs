using UnityEngine;

[CreateAssetMenu(fileName ="New SpawnThing", menuName ="ScriptableObjeckt/SpawnThing")]
public class ScObjFood : ScriptableObject
{
    public int id;
    public Rarely rarely;
    public int prise;
    public GameObject model;

    public enum Rarely { Default, Rare }
}
