using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveMageEnemy : Mobs
{
    public Projectile m_EnemyFireBall;

    override public void ManageMovement()
    {
        GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Hero");
        for (int i = 0; i < playersToFind.Length; i++)
        {
            Vector2 playerPos = playersToFind[i].transform.position;
            Vector2 currPos = this.transform.position;
            float distance = Vector2.Distance(currPos, playerPos);
            Debug.Log("Distance: " + distance);
            Vector2 normalizedMovement = (playerPos - currPos).normalized;
            if (distance < 16 && distance >8) {
                Movement(normalizedMovement);
            }
            else if (distance <= 8 && M_AttackCooldown <= AttackCooldownATM)
            {
                Attack(normalizedMovement);
            }
        }
    }



    protected override void Movement(Vector2 Dir)
    {
        Debug.Log("MoveTo: " + rigidbody.position + Dir*m_MovementSpeed);
        rigidbody.MovePosition(rigidbody.position + Dir*m_MovementSpeed);
    }

    override public void Attack(Vector2 Dir)
    {
        Projectile fb = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
        fb.setDirection(Dir);
        fb.m_damage += m_Level * 5;
        AttackCooldownATM = 0;
    }

    override public void Die()
    {
        GameObject droppedBag = Instantiate(m_Drop, transform.position, Quaternion.Euler(0, 0, 0));
        List<Items> items = itemManager.DropItemsFromEnemies();
        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], droppedBag.gameObject.transform);
        }


        Destroy(this.gameObject);
    }
}
