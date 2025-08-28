using UnityEngine;
using UnityEditor;
public class GunTurret : MonoBehaviour

{
    [Header("Reference")]
    [SerializeField] private Transform RotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] firingPoints;
    [SerializeField] private float bps = 1f; // Bullets per second
    [Header("Attribute")]
    [SerializeField] private float TargetRange = 4f;
    [SerializeField] private float rotationSpeed = 4f;
    private Transform target;
    private float timeUntilFire; 
         private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, TargetRange);
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, TargetRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    private void RotateTowardTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        RotationPoint.rotation = Quaternion.RotateTowards(RotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardTarget();
        if (!checkTargetInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1 / bps)
            {
                shoot();
                timeUntilFire = 0f;
            }
        }
    }
    private void shoot()
    {
      foreach (Transform firePoint in firingPoints)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }
    }
    private bool checkTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= TargetRange;
    }
}
