using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_AgressiveEnemy : Mobs
{
   

    override public void ManageMovement()
    {

    }

    override public void Movement()
    {

    }

    override public void Attack()
    {

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
