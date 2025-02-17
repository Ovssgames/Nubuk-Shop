using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class NPSController : MonoBehaviour
{
    [Header("NPS Setings")]
    public int idNps;
    [SerializeField] points firstPosition;
    [SerializeField] float distanseToFinish = 0.05f;
    [SerializeField] Animator animator;

    [SerializeField] NavMeshAgent navMeshAgent;

    [Header("Points")]
    [SerializeField] Transform shopPoint;
    [SerializeField] Transform abroadPoint;

    private Transform _player;
    private SphereCollider _capsuleCollider;
    private bool _isPlace = false;
    private string _keySave;

    private enum points { Shop, Abroad }

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _capsuleCollider = GetComponent<SphereCollider>();
        navMeshAgent.enabled = false;

        _keySave = "Nps" + idNps.ToString();

        if (PlayerPrefs.HasKey(_keySave))
        {
            var stateNps = PlayerPrefs.GetInt(_keySave);

            if (stateNps == 1)
                StartAbroad();
            else
                StartShop();
        }
        else
        {
            if (firstPosition == points.Abroad)
                StartAbroad();
            else
                StartShop();
        }
    }

    private void Update()
    {
        LookPlayer();
    }

    private void StartAbroad()
    {
        transform.position = abroadPoint.position;
        _capsuleCollider.enabled = false;
        navMeshAgent.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void StartShop()
    {
        _isPlace = true;
        transform.position = shopPoint.position;
        _capsuleCollider.enabled = true;
        navMeshAgent.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }


    public void MoveAbroad()
    {
        animator.SetBool("IsStep", true);
        PlayerPrefs.SetInt(_keySave, 1);
        _isPlace = false;
        _capsuleCollider.enabled = false;
        navMeshAgent.enabled = true;
        navMeshAgent.destination = abroadPoint.position;
        StartCoroutine(Abroad());
    }

    private IEnumerator Abroad()
    {
        float distanse = Vector3.Distance(transform.position, abroadPoint.position);
        while (distanse > distanseToFinish)
        {
            distanse = Vector3.Distance(transform.position, abroadPoint.position);
            yield return null;
        }

        transform.GetChild(0).gameObject.SetActive(false);
        navMeshAgent.enabled = false;
        Debug.Log("FinishAbroad");
    }

    private void LookPlayer()
    {
        if (_isPlace)
        {
            Vector3 direction = new Vector3(_player.position.x, 0, _player.position.z);

            transform.LookAt(direction);
        }
    }
}
