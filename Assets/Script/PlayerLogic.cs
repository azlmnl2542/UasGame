using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Player setting")]
    public Transform PlayerOrientation;
    public Animator anim;
    public CameraLogic camlogic;
    public float walkspeed, runspeed, jumppower, fallspeed, airMultiplier, hitPoint;
    private bool isDead = false; // tambahan sendiri

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    private Rigidbody rb;
    bool grounded = true, aerialboost = true, AimMode = false, TPSMode = true;

    public float MaxHealth;                 //
    public UIGameplayLogic UIGameplay;      //

    [Header("SFX")]
    public AudioClip ShootAudio;
    public AudioClip StepAudio;
    public AudioClip DeathAudio;
    public AudioClip GetHitAudio;
    AudioSource PlayerAudio;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerAudio = this.GetComponent<AudioSource>();
        MaxHealth = hitPoint;
        UIGameplay.UpdateHealthBar(hitPoint, MaxHealth);

    }

    void Update()
    {
        Movement();
        Jump();
        ShootLogic();
        AimModeAdjuster();

        if(Input.GetKey(KeyCode.F))
        {
            playerGetHit(100f);
        }
    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumppower, ForceMode.Impulse);
            grounded = false;
            anim.SetBool("Jump", true);
        }
        else if (!grounded)
        {
            rb.AddForce(Vector3.down * fallspeed * rb.mass, ForceMode.Force);
            if (aerialboost)
            {
                rb.AddForce(moveDirection.normalized * walkspeed * 10f * airMultiplier, ForceMode.Impulse);
                aerialboost = false;
            }
        }
    }

    public void groundchanger()
    {
        grounded = true;
        aerialboost = true;
        anim.SetBool("Jump", false);
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;

        if (grounded && moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }
    }

    public void AimModeAdjuster()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (AimMode)
            {
                TPSMode = true;
                AimMode = false;
                anim.SetBool("AimMode", false);
            }
            else if(TPSMode)
            {
                TPSMode = false;
                AimMode = true;
                anim.SetBool("AimMode", true);
            }
            camlogic.CameraModeChanger(TPSMode, AimMode);
        }
    }

    private void ShootLogic()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PlayerAudio.clip = ShootAudio;
            PlayerAudio.Play();
            if(moveDirection.normalized != Vector3.zero)
            {
                anim.SetBool("WalkShoot", true);
                anim.SetBool("IdleShoot", false);
            }
            else
            {
                anim.SetBool("WalkShoot", false);
                anim.SetBool("IdleShoot", true);
            }
        }
        else
        {
            anim.SetBool("WalkShoot", false);
            anim.SetBool("IdleShoot", false);
        }
    }

    public void playerGetHit(float damage)
    {
        if (isDead) return; // tambahan sendiri

        PlayerAudio.clip = GetHitAudio;
        PlayerAudio.Play();

        Debug.Log("Player damage -" + damage);
        hitPoint -= damage;
        UIGameplay.UpdateHealthBar(hitPoint, MaxHealth);

        if (hitPoint <= 0f)
        {
            isDead = true; // tandai player sudah mati
            hitPoint = 0f; // jaga biar gak negatif
            PlayerAudio.clip = DeathAudio;
            PlayerAudio.Play();
            anim.SetBool("Death", true);
        }
        else
        {
            anim.SetTrigger("GetHit");
        }
    }

    public void Step()
    {
        PlayerAudio.clip = StepAudio;
        PlayerAudio.Play();
    }
}
