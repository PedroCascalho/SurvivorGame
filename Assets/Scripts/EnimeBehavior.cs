using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnimeBehavior : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followDistance = 10f; // Dist�ncia de seguir o jogador
    [SerializeField] private float lookRotationSpeed = 2f; // Velocidade de rota��o do olhar
    private Vector3 originalPosition; // Posi��o original do inimigo
    private bool isFollowingPlayer = false; // Flag que indica se o inimigo est� seguindo o jogador

    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        originalPosition = transform.position; // Salva a posi��o original do inimigo
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followDistance)
        {
            // Se o jogador estiver dentro da dist�ncia de seguir, o inimigo come�a a segui-lo
            enemyNavMesh.SetDestination(playerTransform.position);
            isFollowingPlayer = true;
        }
        else if (isFollowingPlayer)
        {
            // Se o jogador estiver fora da dist�ncia de seguir e o inimigo estava seguindo o jogador,
            // o inimigo mant�m sua posi��o atual
            enemyNavMesh.ResetPath(); // Reseta a rota do NavMeshAgent
            isFollowingPlayer = false;
        }

        // Faz o inimigo olhar para o jogador quando estiver longe
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookRotationSpeed * Time.deltaTime);
    }
}
    