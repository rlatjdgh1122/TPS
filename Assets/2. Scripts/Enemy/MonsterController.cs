using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public enum State
{
    IDLE,
    TRACE,
    ATTACK,
    DIE
}

public class MonsterController : PoolableMono
{

    //더 빠름
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDie = Animator.StringToHash("Death");

    private Animator anim;
    private Transform playerTr;
    private NavMeshAgent agent;

    public State state = State.IDLE;

    [Header("스탯 설정")]
    public float traceDistance = 10f;
    public float attackDistance = 2f;

    public bool isDie = false;

    public UnityEvent OnDamageCast;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void Start()
    {
        agent.destination = playerTr.position;
    }
    private void OnEnable()
    {
        isDie = false;
        state = State.IDLE;
        StartCoroutine(checkMonsterState());
        StartCoroutine(MonsterAction());
    }

    public void OnAnimationHit()
    {
        Debug.Log("공격");
        OnDamageCast?.Invoke();
    }
    private IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;
                case State.DIE:
                    anim.SetTrigger(hashDie);
                    isDie = true;
                    agent.isStopped = true;
                    yield return new WaitForSeconds(1);
                    PoolManager.Instance.Push(this);
                    break;
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    private bool CheckPlayer()
    {
        Vector3 blas = transform.forward;
        Vector3 pos = transform.position;
        pos.y += 1;

        for (int i = -60; i <= 60; i += 10)
        {
            Vector3 dir = Quaternion.Euler(0, i, 0) * blas;
            Ray ray = new Ray(pos, dir.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, traceDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator checkMonsterState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;
            float dis = (playerTr.position - transform.position).sqrMagnitude;

            if (dis <= attackDistance * attackDistance)
                state = State.ATTACK;

            else if ((dis <= traceDistance * traceDistance))
            {
                state = State.TRACE;
            }

            else state = State.IDLE;

            yield return new WaitForSeconds(.3f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        pos.y += 1;
        for (int i = -60; i <= 60; i += 10)
        {
            Vector3 dir = Quaternion.Euler(0, i, 0) * transform.forward;
            Gizmos.DrawRay(pos, dir * traceDistance);
        }
    }

    public override void Reset()
    {
        LivingEntity living = this.GetComponent<LivingEntity>();
        living.initHealth = 100;
    }
}
