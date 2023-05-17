using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projetil projetil;
    [SerializeField] private float damage;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private AudioClip fireSound;

    public void Fire()
    {
        //EXECUTAR ANIMAÇÕES, SONS E EFEITOS DE TIRO DA ARMA
        Instantiate(projetil, bulletSpawn.position, bulletSpawn.rotation);
    }
}
