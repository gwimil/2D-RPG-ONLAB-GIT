using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
  public class FireClosestEnemyBot : MonoBehaviour
  {

    public Projectile m_ProjectileToFire;
    public float m_health;
    // Start is called before the first frame update
    void Start()
    {
      InvokeRepeating("ShootClosestEnemy", 1.0f, 1.0f);
    }

    private void ShootClosestEnemy()
    {
      GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Enemy");
      GameObject closestEnemy = null;
      float closestDistance = 7;
      Vector2 closestNormalizedMovement = new Vector2(0, 0);
      for (int i = 0; i < playersToFind.Length; i++)
      {
        Vector2 playerPos = playersToFind[i].transform.position;
        Vector2 currPos = this.transform.position;
        float distance = Vector2.Distance(currPos, playerPos);
        Vector2 normalizedMovement = (playerPos - currPos).normalized;
        if (distance < 7)
        {
          if (closestDistance > distance)
          {
            closestEnemy = playersToFind[i];
            closestDistance = distance;
            closestNormalizedMovement = normalizedMovement;
          }
        }
      }
      if (closestEnemy != null)
      {
        Projectile fb = Instantiate(m_ProjectileToFire, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
        fb.setDirection(closestNormalizedMovement);
      }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
      m_health -= damage;

      if (m_health <= 0.0f)
      {
        Die();
      }
    }

    private void Die()
    {
      Destroy(this.gameObject);
    }


  }

}
