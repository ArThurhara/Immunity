using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    Transform tf;
    AudioSource audS;
    SpriteRenderer sr;
    public GameObject bullet_prefab;
    public GameObject playerAppearence;
    SpriteRenderer player_sr;
    // Start is called before the first frame update
    void Start()
    {
        player_sr = playerAppearence.GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        audS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bullet_prefab, tf.position, tf.rotation);
            Vector2 dir;
            Quaternion brot = bullet.transform.rotation;
            dir = brot * Vector2.right;
            try
            {
                bullet.GetComponent<Bullet>().SetDirection(dir);
                audS.PlayOneShot(audS.clip);
            }
            catch (System.Exception)
            {
                Debug.LogWarning("Bullet prefab does not have a Bullet component");
            }
        }
    }
    void FixedUpdate()
    {
        Vector3 myPos = tf.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir;
        dir = (mousePos - myPos).normalized;
        dir.z = 0;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tf.rotation = Quaternion.Euler(0, 0, ang);
        Vector3 scale = tf.localScale;
        if (dir.x >= 0 && tf.localScale.y < 0) {
            scale.y *= -1;
            tf.localScale = scale;
            tf.localPosition = new Vector3(tf.localPosition.x * -1, tf.localPosition.y, tf.localPosition.z);
            player_sr.flipX = false;
        } else if (dir.x < 0 && tf.localScale.y > 0) {
            scale.y *= -1;
            tf.localScale = scale;
            tf.localPosition = new Vector3(tf.localPosition.x * -1, tf.localPosition.y, tf.localPosition.z);
            player_sr.flipX = true;
        }
    }
}
