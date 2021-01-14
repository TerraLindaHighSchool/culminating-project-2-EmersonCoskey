using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public int ragDollDuration;
    public int deathDelay;

    private Rigidbody rb;
    private GameObject player;
    private Animator anim;
    private GameObject target;
    private bool isRagDoll;
    private Quaternion startRotation;
    private ParticleSystem explosionParticles;
    private AudioSource explodeSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Referee");
        target = GameObject.Find("Exit");
        anim = transform.Find("EnemyMesh").gameObject.GetComponent<Animator>();
        explodeSound = GetComponent<AudioSource>();
        explosionParticles = transform.Find("FX_Explosion_Smoke").GetComponent<ParticleSystem>();

        anim.SetFloat("Speed_f", Mathf.Lerp(anim.GetFloat("Speed_f"), maxSpeed, 0.1f));
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Vector3.Angle(Vector3.up, transform.up) <= 45 && !isRagDoll)
        {
            anim.SetFloat("Speed_f", Mathf.Lerp(anim.GetFloat("Speed_f"), maxSpeed, 0.1f));
            Run();
        }
        else if (Vector3.Angle(Vector3.up, transform.up) >= 45 && !isRagDoll)
        {
            anim.SetFloat("Speed_f", Mathf.Lerp(anim.GetFloat("Speed_f"), 0, 0.1f));
            StandUp();
        }
        else if (isRagDoll)
        {
            anim.SetFloat("Speed_f", Mathf.Lerp(anim.GetFloat("Speed_f"), 0, 0.2f));
        }
        
    }

    private void Run()
    {
        Vector3 exitDirection = (target.transform.position - transform.position).normalized;
        exitDirection.y = 0;

        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        playerDirection.y = 0;
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);

        Vector3 intendedDirection = playerDistance <= 8 ? (exitDirection - playerDirection * 0.8f).normalized : exitDirection;

        Quaternion lookDirection = Quaternion.LookRotation(intendedDirection, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 0.05f);
    }

    public void RagDoll()
    {
        isRagDoll = true;
        StartCoroutine("WaitForStandUp");
    }

    private void StandUp()
    {
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, startRotation, 0.1f);
        transform.rotation = newRotation;
    }

    IEnumerator WaitForStandUp()
    {
        yield return new WaitForSeconds(ragDollDuration);
        isRagDoll = false;
    }

    public void Die()
    {
        anim.SetBool("Death_b", true);
        explosionParticles.Play();
        explodeSound.Play();
        StartCoroutine(WaitForExplode());
    }

    IEnumerator WaitForExplode()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
