using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject slashPrefab;
    public Transform shotPos;

  

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Bullet = Instantiate(bulletPrefab);
            GameObject Slash = Instantiate(slashPrefab);

            Bullet.transform.position = shotPos.transform.position;
            Slash.transform.position = shotPos.transform.position;
            Destroy(Bullet.gameObject, 1.4f);
            Destroy(Slash.gameObject, 0.5f);

            

            Bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 25), ForceMode.Impulse);
        }
        
    }
}
