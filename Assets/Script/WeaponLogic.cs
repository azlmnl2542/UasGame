using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] Camera shootCamera;
    [SerializeField] float range = 1000f;
    public ParticleSystem MuzzleFlash;
    public GameObject HitEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MuzzleFlash.Play();
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(shootCamera.transform.position, shootCamera.transform.forward, out hit, range))
        {
            Debug.Log("tembak si " + hit.transform.name);
            CreateHitImpact(hit);
            if (hit.transform.tag.Equals("Enemy"))
            {
                EnemyLogic target = hit.transform.GetComponent<EnemyLogic>();
                target.takeDamage(50);
            }
        }
        else
        {
            return;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = shootCamera.transform.TransformDirection(Vector3.forward) * range;
        Gizmos.DrawRay(shootCamera.transform.position, direction);
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
