using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [Header("Enemy setting")]
    public float hitPoints = 100f;
    public float turnSpeed = 15f;
    public Transform target;
    public float chaseRange;
    private NavMeshAgent agent;
    private float DistancetoTarget;
    private float DistancetoDefault;
    private Animator anim;
    Vector3 defaultPosition;

    private bool isDead = false; // tambahan sendiri

    [Header("Enemy SFX")]
    public AudioClip GetHitAudio;
    public AudioClip stepAudio;
    public AudioClip AttackSwingAudio;
    public AudioClip AttackConnectAudio;
    public AudioClip DeathAudio;
    AudioSource EnemyAudio;

    [Header("Enemy VFX")]
    public ParticleSystem SlashEffect;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerLogic>().transform; // baru
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        anim.SetFloat("Hitpoint", hitPoints);
        EnemyAudio = this.GetComponent<AudioSource>();
        defaultPosition = this.transform.position;
    }

    private void Update()
    {

        if (isDead) return;// tambahan sendiri

        DistancetoTarget = Vector3.Distance(target.position, transform.position);
        DistancetoDefault = Vector3.Distance(defaultPosition, transform.position);

        if(DistancetoTarget <= chaseRange  && hitPoints !=0)
        {
            FaceTarget(target.position);
            if(DistancetoTarget > agent.stoppingDistance + 2f)
            {
                chaseTarget();
                SlashEffect.Stop();
            }
            else if(DistancetoTarget <= agent.stoppingDistance)
            {
                Attack();
            }
        }
        else if(DistancetoTarget >= chaseRange * 2)
        {
            agent.SetDestination(defaultPosition);
            FaceTarget(defaultPosition);
            if(DistancetoDefault <= agent.stoppingDistance)
            {
                //Debug.Log("time to stop");
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
            }
        }
    }

    public void SlachEffectToggleOn()
    {
        SlashEffect.Play();
    }

    public void Step()
    {
        EnemyAudio.clip = stepAudio;
        EnemyAudio.Play();
    }

    public void hitConnect()
    {
        EnemyAudio.clip = AttackSwingAudio;
        EnemyAudio.Play();
        if(DistancetoTarget <= agent.stoppingDistance)
        {
            EnemyAudio.clip = AttackConnectAudio;
            EnemyAudio.Play();
            target.GetComponent<PlayerLogic>().playerGetHit(50f);
        }
    }
    
    private void FaceTarget(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void Attack()
    {
        //Debug.Log("attack");
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
    }

    public void chaseTarget()
    {
        agent.SetDestination(target.position);
        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void takeDamage(float damage)
    {
        EnemyAudio.clip = GetHitAudio;
        EnemyAudio.Play();
        hitPoints -= damage;
        anim.SetTrigger("GetHit");
        anim.SetFloat("Hitpoint", hitPoints);
        if(hitPoints <= 0)
        {
            EnemyAudio.clip = DeathAudio;
            EnemyAudio.Play();
            Destroy(gameObject,5f);
        }
    }
}
