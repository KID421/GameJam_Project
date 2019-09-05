using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.5f;
    public AudioClip soundProp, soundDoor;

    private Rigidbody2D rig;
    private Animator ani;
    private SpriteRenderer sr;
    private AudioSource aud;

    public int propCount = 0;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();

        propCount = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.stop) return;
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        sr.flipX = h > 0.1f;
        ani.SetBool("移動", Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
        rig.AddForce((h * transform.right + v * transform.up) * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "金蛋")
        {
            aud.PlayOneShot(soundProp, 0.7f);
            propCount++;
            Destroy(collision.gameObject);

            GameManager.instance.StartCoroutine(GameManager.instance.CameraShake());

            if (propCount == GameManager.instance.propCount) GameManager.instance.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "門")
        {
            aud.PlayOneShot(soundDoor, 0.7f);
            GameManager.instance.Invoke("Pass", 0.5f);
        }
    }
}
