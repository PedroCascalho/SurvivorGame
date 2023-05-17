using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private AudioClip hitSound;

    private void Start()
    {
        StartCoroutine(LifeSpan());
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * bulletVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(this.gameObject);
    }
}
