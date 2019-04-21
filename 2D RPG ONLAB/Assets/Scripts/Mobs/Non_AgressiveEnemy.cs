using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks {
    public class Non_AgressiveEnemy : Mobs
    {


        override public void ManageMovement()
        {
            rigidbody.MovePosition(startPosition);
        }

        protected override void Movement(Vector2 Dir)
        {

        }

        override public void Attack(Vector2 Dir)
        {

        }
    }
}
