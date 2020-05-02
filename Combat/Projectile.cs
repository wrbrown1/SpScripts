using SP.Combat;
using SP.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Health target = null;
    [SerializeField] float speed = .5f;

    float damage = 0f;

    private void Update()
    {
        if (!target) return;
        transform.LookAt(AimPosition());
        transform.Translate(Time.deltaTime * speed * Vector3.forward);
    }

    public Vector3 AimPosition()
    {
        CapsuleCollider collider = target.GetComponent<CapsuleCollider>();
        if (!collider) return target.transform.position;
        return target.transform.position + (Vector3.up * collider.height / 2.3f);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.GetComponent<Health>();
        if (!targetHealth) return;
        targetHealth.TakeDamage(damage);
        other.GetComponent<Combatant>().PlayAudioWithTag("Bow_Impact");
        GetComponent<DestroyParticles>().Detatch();
        Destroy(gameObject);
    }
}
