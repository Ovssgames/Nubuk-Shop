using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class BuyerController : MonoBehaviour
{
    public Range range;

    public List<SellShalf> sellShalfs;

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject targetPositionPrefab;
    [SerializeField] BuyerInventory buyerInventory;
    [SerializeField] Animator animator;

    [HideInInspector]
    public int countProduct;
    [HideInInspector]
    public int countProductMax;
    [HideInInspector]
    public List<Transform> exits = new List<Transform>();

    private Dictionary<int, int> route = new Dictionary<int, int>();
    private CashRegister _cashRegister;
    private List<int> idProductsDefault = new List<int>();
    private List<int> idProductsRare = new List<int>();
    private Dictionary<int, Transform> shalfs = new Dictionary<int, Transform>();

    private int _productDefault;
    private int _productRare;

    [HideInInspector]
    public Transform targetPosition;

    private void Awake()
    {
        AwakeValues();
    }

    private void Start()
    {
        FindRoute();
        StartCoroutine(AiBuyer());
    }

    private void Update()
    {
        MoveBuyer();
    }

    private void MoveBuyer()
    {
        navMeshAgent.destination = targetPosition.position;

        var tarPos = new Vector3(targetPosition.position.x, navMeshAgent.destination.y, targetPosition.position.z);
        if (transform.position != tarPos)
        {
            animator.SetBool("IsStep", true);
        }
        else

        {
            animator.SetBool("IsStep", false);
        }

    }

    private void FindRoute()
    {
        for (int i = 0; i < _productDefault; i++)
        {
            if (idProductsDefault.Count == 0)
                break;
            int product = idProductsDefault[(int)Random.Range(0, idProductsDefault.Count)];
            route.Add(i, product);
            idProductsDefault.Remove(product);
        }
        for (int i = _productDefault; i < _productRare + _productDefault; i++)
        {
            if (idProductsRare.Count == 0)
                break;
            int product = idProductsRare[(int)Random.Range(0, idProductsRare.Count)];
            route.Add(i, product);
            idProductsRare.Remove(product);
        }
    }

    private void AwakeValues()
    {
        _productDefault = (int)Random.Range(range.minProductDefault, range.maxProductDefault + 1);
        _productRare = (int)Random.Range(range.minProductRare, range.maxProductRare + 1);

        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>(), GetComponent<Collider>());

        foreach (SellShalf item in sellShalfs)
        {
            if (item.gameObject.activeSelf == true)
            {
                if (item.type.rarely == ScObjFood.Rarely.Default)
                {
                    idProductsDefault.Add(item.type.id);
                }



                else
                {
                    idProductsRare.Add(item.type.id);
                }

                shalfs.Add(item.type.id, item.GetComponentInChildren<FinishShalfPosition>().transform);
            }

        }
        _cashRegister = GameObject.FindGameObjectWithTag("CashRegister").GetComponent<CashRegister>();

        var targetPos = Instantiate(targetPositionPrefab);
        targetPosition = targetPos.transform;

        navMeshAgent.avoidancePriority = Random.Range(55, 99);

        GameObject[] exit = GameObject.FindGameObjectsWithTag("Exit");

        for (int i = 0; i < exit.Length; i++)
        {
            exits.Add(exit[i].transform);
        }
    }

    private IEnumerator AiBuyer()
    {
        Debug.Log("StartBuyer");
        for (int i = 0; i < route.Count; i++)
        {
            foreach (var item in sellShalfs)
            {
                if (item.type.id == route[i])
                {
                    targetPosition.position = shalfs[route[i]].position;
                    buyerInventory.idProduct = route[i];
                    countProductMax = item.type.rarely == ScObjFood.Rarely.Default ? (int)Random
                        .Range(range.minCountDefault, range.maxCountDefault + 1) : (int)Random.Range(range.minCountRare, range.maxCountRare + 1);
                    break;
                }
            }

            while (countProduct < countProductMax)
            {
                yield return null;
            }
            yield return null;







            countProduct = 0;
        }
        yield return null;
        Debug.Log("CashRegisterGoBuyer");

        for (int i = 0; i < _cashRegister.queueBuyers.Count; i++)







        {
            if (_cashRegister.buyers[i] == null)


            {
                _cashRegister.buyers[i] = gameObject;
                targetPosition.position = _cashRegister.queueBuyers[i].position;
                break;
            }
        }
        yield return null;
        while (transform.position.x != exits[0].position.x && transform.position.x != exits[1].position.x)


        {
            yield return null;



        }

        SpawnerBuyers.countBuyers--;

        Destroy(gameObject);
        Destroy(targetPosition.gameObject);
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