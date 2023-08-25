using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Khoang thoi gian giua moi lan ban
    public float fireRate;
    float m_curFireRate;

    // Thiet lap ong ngam
    public GameObject viewFinder;

    bool m_isShooted;
    GameObject m_viewFinderClone;

    private void Awake()
    {
        m_curFireRate = fireRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Thiet lap vi tri ong ngam khi bat dau Game
        if (viewFinder)
        {
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }     
    }

    // Update is called once per frame
    void Update()
    {
        //Chuyen doi toa do con tro chuot sang toa do cua Unity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Chi goi phuong thuc Shot khi nguoi choi nhan chuot va m_isShooted = false
        if (Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            Shot(mousePos);
        }

        // Neu da ban sung thi thiet lap thoi gian nap dan de ban tiep
        if (m_isShooted)
        {
            m_curFireRate -= Time.deltaTime;

            if(m_curFireRate <= 0)
            {
                m_isShooted = false;

                m_curFireRate = fireRate;
            }

            //Cap nhat hieu ung nap dan
            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate);
        }

        //Cap nhat vi tri ong ngam = vi tri con tro chuot
        if (m_viewFinderClone)
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
    }

    void Shot(Vector3 mousePos)
    {
        m_isShooted = true;

        // Toa do Vi tri ban la tu camera tru con tro chuot
        Vector3 shootDir = Camera.main.transform.position - mousePos;

        // Chuyen toa do cua shootDir voi do lai la 1
        shootDir.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Xac dinh chinh xac vi tri doi tuong thi moi co the ban duoc doi tuong
                if (hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();
                    //Neu ban trung thi chim se chet
                    if (bird)
                    {
                        bird.Die();
                    }
                }
            }
        }

        //Tao hieu ung rung man hinh khi ban sung
        CineController.Ins.ShakeTrigger();

        //Tao tieng ban sung
        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }
}
