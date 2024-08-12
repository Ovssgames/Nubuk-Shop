using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BuyerController : MonoBehaviour
{
    public Range range;
    public UnityEvent<bool> OnMoveInventory;

    public List<SellShalf> sellShalfs;

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject targetPositionPrefab;
    [SerializeField] BuyerInventory buyerInventory;

    private List<int> idProductsDefault = new List<int>();
    private List<int> idProductsRare = new List<int>();
    private Dictionary<int, int> route = new Dictionary<int, int>();

    private int productDefault;
    private int productRare;
    private int _countProduct;

    private Transform _targetPosition;

    private void Awake()
    {
        AwakeValues();
    }

    private void Start()
    {
        FindRoute();
    }

    private void Update()
    {


        navMeshAgent.destination = _targetPosition.position;
    }

    private void FindRoute()
    {
        for (int i = 0; i < productDefault; i++)
        {
            int product = idProductsDefault[(int)Random.Range(0, idProductsDefault.Count)];
            route.Add(i, product);
            idProductsDefault.Remove(product);
        }
        for (int i = productDefault; i < productRare + productDefault; i++)
        {
            int product = idProductsRare[(int)Random.Range(0, idProductsRare.Count)];
            route.Add(i, product);
            idProductsRare.Remove(product);
        }
    }

    private void AwakeValues()
    {
        productDefault = (int)Random.Range(range.minProductDefault, range.maxProductDefault + 1);
        productRare = (int)Random.Range(range.minProductRare, range.maxProductRare + 1);

        foreach (SellShalf item in sellShalfs)
        {
            if (item.type.rarely == ScObjFood.Rarely.Default)
            {
                idProductsDefault.Add(item.type.id);
            }
            else
            {
                idProductsRare.Add(item.type.id);
            }
        }

        var targetPos = Instantiate(targetPositionPrefab);
        _targetPosition = targetPos.transform;
    }

    private IEnumerator AiBuyer()
    {
        for (int i = 0; i < route.Count; i++)
        {
            foreach (var item in sellShalfs)
            {
                if (item.type.id == route[i])
                {
                    _targetPosition.position = item.type.sellShalf.position;
                    buyerInventory.idProduct = route[i];
                    _countProduct = Random.Range(0, item.type.rarely == ScObjFood.Rarely.Default ? (int)Random
                        .Range(range.minCountDefault, range.maxCountDefault + 1) : (int)Random.Range(range.minCountRare, range.maxCountRare + 1 ));
                    break;
                }
            }

            while (transform.position == transform.position)
            {
                yield return null;
            }
        }
    }

}
[System.Serializable]
public class Range
{
    [Header("Default Product")]
    [Header("   Count Products")]
    public int minProductDefault;
    public int maxProductDefault;
    [Header("    Count Things")]
    public int minCountDefault;
    public int maxCountDefault;
    [Header("Rare Product")]
    [Header("   Count Products")]
    public int minProductRare;
    public int maxProductRare;
    [Header("   Count Things")]
    public int minCountRare;
    public int maxCountRare;
}