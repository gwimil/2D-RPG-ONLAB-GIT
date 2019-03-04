using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }

    override public void Move(Vector2 position)
    {
        rigidbody.MovePosition(rigidbody.position + position);
    }

    override public void Attack()
    {

    }
    override public void UseSkill(int i)
    {

    }
    override public void AddItemToInventory(Items i)
    {
        inventory.AddItem(i);
    }
}
