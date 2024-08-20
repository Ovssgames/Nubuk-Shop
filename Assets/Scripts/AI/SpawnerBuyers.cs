using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SpawnerBuyers : MonoBehaviour
{
    [SerializeField] int maxCountBuyers;

    [SerializeField] float timeToSpawn;
    [SerializeField] float prosentTime;

    [SerializeField] GameObject buyerPrefab;
    [SerializeField] List<Transform> spawners;

    [SerializeField] UnityEvent OnTimeToWait;
    public static int countBuyers;

    private float _timer;
    private float _randomProsent;

    private void Start()
    {
        _randomProsent = Random.Range(-prosentTime, prosentTime);
    }

    private void Update()
    {
        Timer();

        if (_timer > timeToSpawn + _randomProsent && countBuyers < maxCountBuyers)
        {
            OnTimeToWait.Invoke();
            _timer = 0;
        }
    }

    private void Timer()
    {
        _timer += Time.deltaTime;
    }

    public void SpawnBuyer()
    {
        Debug.Log("SpawnBuyer");
        countBuyers++;
        _randomProsent = Random.Range(-prosentTime, prosentTime);

        int index = (int)Random.Range(0, spawners.Count);

        var buyer = Instantiate(buyerPrefab);
        buyer.transform.position = spawners[index].position;
        buyer.GetComponent<BuyerController>().enabled = true;
        buyer.GetComponent<NavMeshAgent>().enabled = true;
    }
}
