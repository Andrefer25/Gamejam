using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private GameObject NoRecBulletPrefab1;
    [SerializeField] private GameObject NoRecBulletPrefab2;
    [SerializeField] private GameObject NoRecBulletPrefab3;
    [SerializeField] private GameObject NoRecBulletPrefab4;
    [SerializeField] private GameObject NoRecBulletPrefab5;
    [SerializeField] private GameObject RecBulletPrefab1;
    [SerializeField] private GameObject RecBulletPrefab2;
    [SerializeField] private GameObject RecBulletPrefab3;
    [SerializeField] private GameObject RecBulletPrefab4;
    [SerializeField] private GameObject RecBulletPrefab5;
    private PlayerStats playerStats;
    private Animator animator;
    private List<GameObject> bulletRecList;
    private List<GameObject> bulletNoRecList;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        bulletRecList = new List<GameObject>
        {
            RecBulletPrefab1,
            RecBulletPrefab2,
            RecBulletPrefab3,
            RecBulletPrefab4,
            RecBulletPrefab5
        };
        bulletNoRecList = new List<GameObject>
        {
            NoRecBulletPrefab1,
            NoRecBulletPrefab2,
            NoRecBulletPrefab3,
            NoRecBulletPrefab4,
            NoRecBulletPrefab5
        };
    }

    public void Shoot()
    {
        playerStats.RedAmmoA(1);
        int idx = Random.Range(0, bulletNoRecList.Count);
        Vector3 direction;
        if (animator.GetBool("isLookingUp"))
        {
            direction = Vector2.up;
        }
        else if(animator.GetBool("isLookingDiag"))
        {
            if (transform.localScale.x == 1.0f)
                direction = new Vector2(1, 1);
            else direction = new Vector2(-1, 1);
        }
        else if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(bulletNoRecList[idx], transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetDirection(direction);
    }

    public void OtherShoot()
    {
        playerStats.RedAmmoB(1);
        int idx = Random.Range(0, bulletRecList.Count);
        Vector3 direction;
        if (animator.GetBool("isLookingUp"))
        {
            direction = Vector2.up;
        }
        else if (animator.GetBool("isLookingDiag"))
        {
            if (transform.localScale.x == 1.0f)
                direction = new Vector2(1, 1);
            else direction = new Vector2(-1, 1);
        }
        else if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(bulletRecList[idx], transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<OtherBulletController>().SetDirection(direction);
    }
}
