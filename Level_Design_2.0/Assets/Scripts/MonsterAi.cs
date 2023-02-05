using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    bool sichtbar;
    [SerializeField] GameObject Monster;


    //boxcast
    [SerializeField] GameObject _player;
    bool m_HitDetect;

    Collider m_Collider;
    RaycastHit m_Hit;
    public Camera _camera;

    //navmesh
    NavMeshAgent _navMeshAgent;
    [SerializeField] Transform _BehindPlayer;
    Vector3 _moveDirection;

    int randNumber;
    [SerializeField] int minRandomNumber;
    [SerializeField] int maxRandomNumber;
    [SerializeField] int _wahrscheinlichkeit;
    private void Awake()
    {
        //navmesh
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetMovementSpeed(5f);
        //raycast
        m_Collider = _player.GetComponent<Collider>();

        //eigen
        sichtbar = false;
        Monster.SetActive(false);
    }


    //navmesh
    private void SetMovementSpeed(float speed)
    {
        _navMeshAgent.speed = speed;
    }
    public void FindDirection(Vector3 target)
    {
        _moveDirection = target;
    }

    private void Chase()
    {

        sichtbar = false;
        FindDirection(_BehindPlayer.position);
        _navMeshAgent.SetDestination(_moveDirection);

    }

    //eigen
    private void TaucheAuf()
    {
        sichtbar = true;
        Monster.SetActive(true);
        _navMeshAgent.isStopped = true;
    }

    //boxcast
    public void BoxCast()
    {
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, _player.transform.localScale, _camera.transform.forward, out m_Hit, _player.transform.rotation);
        if (m_HitDetect)
        {
            if (m_Hit.collider.tag == "Monster")
            {
                StartAgain();
            }
            
        }
    }


    private void Update()
    {
        if (!sichtbar)
        {
        //mit einer bestimmten Wahrscheinlichkeit soll der Gegner den Spieler zufällig jagen 
        randNumber = Random.Range(minRandomNumber, maxRandomNumber);
         if (randNumber <= _wahrscheinlichkeit)
            {

                TaucheAuf();
            }
            else
            {
            Chase();
            }
        }
        else
        {
            
        }
        
    }

    public void StartAgain() {
        StartCoroutine("Wait");
        Monster.SetActive(false);
        sichtbar=false;
        _navMeshAgent.isStopped = false;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
