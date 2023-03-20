using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public float NhayCao;
    public float VanToc = 5f;
    private float speed;
    private bool DuoiDat = true;
    private bool ChuyenHuong = false;
    private bool QuayPhai = true;
    private Animator animator;
    private Rigidbody2D r2d;
    public int CapDo = 0;
    public bool BienHinh = false;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", speed);
        animator.SetBool("DuoiDat", DuoiDat);
        animator.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
    }
    void FixedUpdate()
    {
        DiChuyen();
        
    }

    void DiChuyen()
    {
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc * PhimNhanPhaiTrai, r2d.velocity.y);
        speed = Mathf.Abs(VanToc * PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) XoayMat();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) XoayMat();
        //float PhimNhanTrenDuoi = Input.GetAxis("Vertical");
        //if(PhimNhanTrenDuoi > 0)
        //{
        //    DuoiDat = false;
        //    r2d.velocity = new Vector2(r2d.velocity.x, PhimNhanTrenDuoi * VanToc);
        //}
        //if(r2d.velocity.y == 0)
        //{
        //    DuoiDat = true;
        //}
    }
    void XoayMat()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
        if (speed > 0) StartCoroutine(MarioChuyenHuong());
    }
    void NhayLen()
    {
        if(Input.GetKeyDown(KeyCode.W) && DuoiDat == true)
        {
            r2d.AddForce(Vector2.up * NhayCao);
            DuoiDat = false;
        }
        //if(!DuoiDat && r2d.velocity.y == 0)
        //{
        //    DuoiDat = true;
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
         if(collision.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay is called");
        if(collision.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    IEnumerator MarioChuyenHuong()
    {
        Debug.Log("Mario chuyen huong");
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.1f);
        ChuyenHuong = false;
    }

    //IEnumerable MarioAnNam()
    //{
    //    float DoTre = 0.1f;

    //}
}
