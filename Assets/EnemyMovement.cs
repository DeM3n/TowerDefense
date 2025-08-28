using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("reference")]
    [SerializeField] Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    private Transform target;
    private int pathIndex = 0;
    private void Start()
    {
        target = LevelManager.main.path[0];

    }
     private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        // Hướng từ vị trí hiện tại tới target
        Vector2 dir = ((Vector2)target.position - rb.position).normalized;

        // Di chuyển enemy
        rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);

        // Nếu đã gần target thì đổi target
        if (Vector2.Distance(rb.position, target.position) < 0.1f)
        {
            GetNextTarget();
        }
    }

    private void GetNextTarget()
    {
        if (pathIndex + 1 < LevelManager.main.path.Length)
        {
            pathIndex++;
            target = LevelManager.main.path[pathIndex];
        }
        else
        {
            // Enemy đã đi hết đường
            Debug.Log("Enemy reached the end!");
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject); // hoặc trừ máu base
        }
    }
}
