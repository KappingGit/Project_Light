using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerTwo : MonoBehaviour
{
    public ParticleSystem bomb;

    public void OnCollisionEnter(Collision collision)
    {
        ParticleSystem Bomb = Instantiate(bomb);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Bomb.transform.position = this.transform.position;
            // Destroy(collision.gameObject); 적이 사라진다.
            Destroy(this.gameObject);
            Destroy(Bomb.gameObject, 0.5f);
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Enemy")
        {
            ParticleSystem Bomb = Instantiate(bomb);
            Debug.Log("현재 충돌되는 오브젝트는" + other.gameObject.tag + "입니다");
            Bomb.transform.position = other.transform.position;
            // Destroy(collision.gameObject); 적이 사라진다.
            //Destroy(this.gameObject);
            Destroy(Bomb.gameObject, 0.5f);
           
        }
    }

}
