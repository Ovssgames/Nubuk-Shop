using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerController : MonoBehaviour
{
    public Range range;
    public List<SellShalf> sellShalfs;

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject targetPositionPrefab;
    [SerializeField] BuyerInventory buyerInventory;
    [SerializeField] Animator animator;

    [HideInInspector] public int countProduct;
    [HideInInspector] public int countProductMax;
    [HideInInspector] public List<Transform> exits = new List<Transform>();
    [HideInInspector] public Transform targetPosition;

    private Dictionary<int, int> route = new Dictionary<int, int>();
    private CashRegister cashRegister;
    private List<int> idProductsDefault = new List<int>();
    private List<int> idProductsRare = new List<int>();
    private Dictionary<int, Transform> shelves = new Dictionary<int, Transform>();

    private int productDefaultCount;
    private int productRareCount;

    private void Awake()
    {
        InitializeValues();
    }

    private void Start()
    {
        GenerateRoute();
        StartCoroutine(BuyerBehavior());
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (targetPosition == null) return;

        navMeshAgent.destination = targetPosition.position;
        bool isMoving = Vector3.Distance(transform.position, targetPosition.position) > 0.5f;
        animator.SetBool("IsStep", isMoving);
    }

    private void GenerateRoute()
    {
        AddProductsToRoute(idProductsDefault, productDefaultCount);
        AddProductsToRoute(idProductsRare, productRareCount);
        Debug.Log($"Route count: {route.Count}");
    }

    private void AddProductsToRoute(List<int> productList, int productCount)
    {
        for (int i = 0; i < productCount && productList.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, productList.Count);
            route.Add(i, productList[randomIndex]);
            productList.RemoveAt(randomIndex);
        }
    }

    private void InitializeValues()
    {
        productDefaultCount = Random.Range(range.minProductDefault, range.maxProductDefault + 1);
        productRareCount = Random.Range(range.minProductRare, range.maxProductRare + 1);

        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>(), GetComponent<Collider>());

        foreach (SellShalf shelf in sellShalfs)
        {
            if (!shelf.gameObject.activeSelf) continue;

            var productType = shelf.type;
            if (productType.rarely == ScObjFood.Rarely.Default)
                idProductsDefault.Add(productType.id);
            else
                idProductsRare.Add(productType.id);

            shelves[productType.id] = shelf.GetComponentInChildren<FinishShalfPosition>().transform;
        }

        cashRegister = GameObject.FindGameObjectWithTag("CashRegister").GetComponent<CashRegister>();

        var targetObj = Instantiate(targetPositionPrefab);
        targetPosition = targetObj.transform;

        navMeshAgent.avoidancePriority = Random.Range(55, 99);

        foreach (GameObject exit in GameObject.FindGameObjectsWithTag("Exit"))
        {
            exits.Add(exit.transform);
        }
    }

    private IEnumerator BuyerBehavior()
    {
        Debug.Log("Buyer started");

        foreach (var step in route)
        {
            if (!shelves.TryGetValue(step.Value, out Transform shelfPosition)) continue;

            targetPosition.position = shelfPosition.position;
            buyerInventory.idProduct = step.Value;
            countProductMax = Random.Range(
                route[step.Key] < productDefaultCount ? range.minCountDefault : range.minCountRare,
                route[step.Key] < productDefaultCount ? range.maxCountDefault : range.maxCountRare
            );

            yield return new WaitUntil(() => countProduct >= countProductMax);
            countProduct = 0;
        }

        MoveToCashRegister();

        yield return new WaitUntil(() => IsAtExit());
        SpawnerBuyers.countBuyers--;
        Cleanup();
    }

    private void MoveToCashRegister()
    {
        Debug.Log("MovingToCashRegister");

        for (int i = 0; i < cashRegister.queueBuyers.Count; i++)
        {
            if (cashRegister.buyers[i] != null) continue;

            cashRegister.buyers[i] = gameObject;
            targetPosition.position = cashRegister.queueBuyers[i].position;
            break;
        }
    }

    private bool IsAtExit()
    {
        foreach (var exit in exits)
        {
            if (Vector3.Distance(transform.position, exit.position) <= 0.1f)
                return true;
        }
        return false;
    }

    private void Cleanup()
    {
        Destroy(gameObject);
        Destroy(targetPosition.gameObject);
    }
}

[System.Serializable]
public class Range
{
    [Header("Default Product")]
    public int minProductDefault;
    public int maxProductDefault;
    public int minCountDefault;
    public int maxCountDefault;

    [Header("Rare Product")]
    public int minProductRare;
    public int maxProductRare;
    public int minCountRare;
    public int maxCountRare;
}