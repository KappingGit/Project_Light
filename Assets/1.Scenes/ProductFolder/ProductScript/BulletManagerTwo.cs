using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerTwo : MonoBehaviour
{
    public ParticleSystem bomb;

    public void OnCollisionEnter(Collision collision)
    {
        ParticleSystem Wind = Instantiate(bomb);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Wind.transform.position = this.transform.position;
            // Destroy(collision.gameObject); 적이 사라진다.
            Destroy(this.gameObject);
            Destroy(Wind.gameObject, 0.5f);
        }
        
    }
   
}
