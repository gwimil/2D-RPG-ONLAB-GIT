using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
  public class Fireball4Ways : MonoBehaviour
  {
    public Projectile m_EnemyFireBall;

    public bool attack;

    public bool fullPower;


    private Vector2 vectorMultiplier;
    private Vector2 firstAngle;
    private Vector2 secondAngle;
    private Vector2 thirdAngle;
    private Vector2 fourthAngle;

    public float rotateSpeed;
    public int rotateDirection; // 1 or -1



    // Start is called before the first frame update
    void Start()
    {
      fullPower = false;
      rotateSpeed = 5;
      rotateDirection = 1;
      attack = true;
      vectorMultiplier = new Vector2(0.1f, 0.1f);
      firstAngle = new Vector2(1, 0);
      secondAngle = new Vector2(0, 1);
      thirdAngle = new Vector2(-1, 0);
      fourthAngle = new Vector2(0, -1);
      InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
    }


    public void startAttacking(bool b)
    {
      attack = b;
    }

    void LaunchProjectile()
    {
      if (attack)
      {
        if (fullPower)
        {
          Projectile fb1 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
          fb1.setDirection(firstAngle);
        }


        Projectile fb2 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
        fb2.setDirection(secondAngle);

        if (fullPower)
        {
          Projectile fb3 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
          fb3.setDirection(thirdAngle);
        }


        Projectile fb4 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
        fb4.setDirection(fourthAngle);

        firstAngle = RotateVector2(firstAngle, rotateSpeed * rotateDirection);
        secondAngle = RotateVector2(secondAngle, rotateSpeed * rotateDirection);
        thirdAngle = RotateVector2(thirdAngle, rotateSpeed * rotateDirection);
        fourthAngle = RotateVector2(fourthAngle, rotateSpeed * rotateDirection);

      }
      else
      {
        firstAngle = new Vector2(1, 0);
        secondAngle = new Vector2(0, 1);
        thirdAngle = new Vector2(-1, 0);
        fourthAngle = new Vector2(0, -1);
      }
    }

    private Vector2 RotateVector2(Vector2 v, float degrees)
    {
      float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
      float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
      float tx = v.x;
      float ty = v.y;
      v.x = (cos * tx) - (sin * ty);
      v.y = (sin * tx) + (cos * ty);
      return v;
    }

  }
}