using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private List<GameObject> Bullet = new List<GameObject>();

    private List<GameObject> Bot = new List<GameObject>();
    private Vector3 currentPos;
    private GameObject bot;
    private float horizontal, vertical;
    private float speed = 500;
    private int HP = 1;
    private int amountBullet = 1;
    private string currentAnim;
    private void Start()
    {
        HP = 1;
        amountBullet = 1;
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        if(HP > 0)
        {
            Move();
        }
            DestroyBullet(currentPos);
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            rb.velocity = new Vector3(horizontal, 0, vertical).normalized * speed * Time.deltaTime;
            transform.forward = rb.velocity;
            ResetBullet();

            ChangeAnim("run");
        }
        else
        {
            Shot();
            amountBullet = 0;

            ChangeAnim("idle");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Bot")
            Bot.Add(other.gameObject);
    }
    private bool CheckBot()
    {
        foreach(GameObject bot in Bot)
        {
            if (Vector3.Distance(transform.position, bot.transform.position) <= 7f) return true;
            else
            {
                Bot.Remove(bot);
            }
        }
        return false;
    }

    private void DefineBot()
    {
        float[] dis = new float[Bot.Count];
        int i = 0;
        foreach(GameObject bot in Bot)
        {
            dis[i] = Vector3.Distance(bot.transform.position, transform.position);
        }
        foreach (GameObject _bot in Bot)
        {
            if(Vector3.Distance(_bot.transform.position, transform.position) == dis.Min())
                bot = _bot;
        }
    }

    private void Shot()
    {
        DefineBot();
        if(amountBullet > 0 && CheckBot())
        {
            Vector3 direction = bot.transform.position - transform.position;
            direction.Normalize();
            GameObject bullet = Instantiate(weapon, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(direction * 500);
            Bullet.Add(bullet);
            currentPos = transform.position;
        }
    }

    private void ResetBullet()
    {
        amountBullet = 1;
    }

    private void DestroyBullet(Vector3 currentPos)
    {
        foreach(GameObject bullet in Bullet)
        {
            if(Vector3.Distance(bullet.transform.position, currentPos) > 7f)
            {
                bullet.IsDestroyed();
                Bullet.Remove(bullet);
            }
        }
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
