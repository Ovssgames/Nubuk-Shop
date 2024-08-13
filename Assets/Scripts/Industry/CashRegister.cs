using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public List<Prise> products;
    public List<Transform> queueBuyers;


}

[System.Serializable]
public class Prise
{
    public ScObjFood product;
    public int prise;
}