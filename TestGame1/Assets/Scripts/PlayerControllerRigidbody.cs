using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerRigidbody : MonoBehaviour
{
    public PlaygroundManager manager;

    Rigidbody rb;
    public float speed = 2f;
    public float rotSpeed = 20f;
    public float jumpPower = 1f;
    float newRotY = 0;

    public bool hasGun = false;
    public GameObject prefabBullet;
    public GameObject GunPosition;
    public float gunPower = 15f;
    public float GunCooldown = 1f;
    public float GunCooldownCount = 0;
    public int bulletCount = 0;
    public int coinCount = 0;
    public AudioSource audioCoin;
    public AudioSource audioFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (manager == null)
        {
            manager = FindObjectOfType<PlaygroundManager>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if (Input.GetKey(KeyCode.UpArrow))
         {
             rb.AddForce(0, 0, speed,ForceMode.VelocityChange);  
         }
         if (Input.GetKey(KeyCode.DownArrow))
         {
             rb.AddForce(0, 0, -speed, ForceMode.VelocityChange); 
         }
         if (Input.GetKey(KeyCode.RightArrow))
         {
             rb.AddForce(speed, 0, 0, ForceMode.VelocityChange);  
         }
         if (Input.GetKey(KeyCode.LeftArrow))
         {
             rb.AddForce(-speed, 0, 0, ForceMode.VelocityChange);
         }
        */

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        
        if(horizontal > 0)
        {
            newRotY = 90;
        }
        if(horizontal < 0)
        {
            newRotY = -90;
        }
        if(vertical > 0)
        {
            newRotY = 0;
        }
        else if (vertical < 0)
        {
            newRotY = 180;
        }

        rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);
        transform.rotation = Quaternion.Lerp(
                                                Quaternion.Euler(0, newRotY, 0)
                                                , transform.rotation
                                                , Time.deltaTime * rotSpeed
                                            );
    }

    private void Update()
    {
         if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        }

        if(Input.GetButtonDown("Fire1") && bulletCount > 0 && (GunCooldownCount >= GunCooldown) )
        {
            GunCooldownCount = 0;
            bulletCount--;
            manager.SetTextBullet(bulletCount); //บอก manager ให้เเสดงกระสุน
            audioFire.Play();

            GameObject bullet = Instantiate(prefabBullet,GunPosition.transform.position,GunPosition.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * gunPower, ForceMode.Impulse);
            //Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            // bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);
            Destroy(bullet, 3f);
        }
        GunCooldownCount += Time.fixedDeltaTime;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Collentable")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collentable")
        {
            Destroy(other.gameObject);
            coinCount++;
            manager.SetTextCoin(coinCount);
            audioCoin.Play();
        }

        if (other.gameObject.name == "GunTrigger")
        {
            hasGun = true;
            bulletCount += 10;
            Destroy(other.gameObject);
            manager.SetTextBullet(bulletCount);
        }
    }
}
