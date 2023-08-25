using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //Toc do bay cua chim
    public float xSpeed;
    public float minYSpeed;
    public float maxYSpeed;

    public GameObject DeathVfx;

    Rigidbody2D m_rb;
    bool m_moveLeftOnStart;
    bool m_isDead;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomMovingDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Thiet lap toc do bay va huong bay cua chim
        //Neu chim xuat hien ben phai thi bay sang trai, nguoc lai bay sang phai
        m_rb.velocity = m_moveLeftOnStart ?
            new Vector2(-xSpeed, Random.Range(minYSpeed, maxYSpeed))
            : new Vector2(xSpeed, Random.Range(minYSpeed, maxYSpeed));
        Flip();
    }

    //Kiem tra vi tri xuat hien cua con chim la ben trai hay phai
    //Neu ben phai la true nguoc lai la false
    public void RandomMovingDirection()
    {
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;
    }

    void Flip()
    {
        //Kiem tra xem chim bay tu huong nao thi doi scale.x cua no
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.lossyScale.y, transform.lossyScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.lossyScale.y, transform.lossyScale.z);
        }
    }

    public void Die()
    {
        m_isDead = true;

        GameManager.Ins.BirdKilled++;

        if (xSpeed >= 12) GameManager.Ins.Score = GameManager.Ins.Score + 10;
        else if(xSpeed >=9) GameManager.Ins.Score = GameManager.Ins.Score + 8;
        else if(xSpeed >= 3) GameManager.Ins.Score = GameManager.Ins.Score + 5;
        else GameManager.Ins.Score = GameManager.Ins.Score + 2;

        Destroy(gameObject);

        //Thiet lap hieu ung tia mau' tai vi tri con chim bi ban chet
        if (DeathVfx)
            Instantiate(DeathVfx, transform.position, Quaternion.identity);

        //Cap nhat so chim bi ban ha len TextKilled
        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
        //Cap nhat so diem
        GameGUIManager.Ins.ScorePlay(GameManager.Ins.Score);
    }
}
