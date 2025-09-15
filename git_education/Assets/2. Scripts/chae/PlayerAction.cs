using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public GameManager manager;
    
    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizontalMove;

    Vector2 dirVec = Vector2.down;
    GameObject scanObject;

    void Start()
    {
       anim = GetComponent<Animator>();
       rigid = GetComponent<Rigidbody2D>();
    }
  
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (hDown || vUp)
            isHorizontalMove = true;
        else if (vDown || hUp)
            isHorizontalMove = false;

        anim.SetInteger("hAxis", (int)h);
        anim.SetInteger("vAxis", (int)v);

        //방향
        if (vDown && v == 1)
            dirVec = Vector2.up;
        else if (vDown && v == -1)
            dirVec = Vector2.down;
        else if (hDown && h == -1)
            dirVec = Vector2.left;
        else if (hDown && h == 1)
            dirVec = Vector2.right;

        //사물인지

        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null&& manager != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {

        Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        //앞쪽 사물 스캔
        Vector2 origin = rigid.position;
        Debug.DrawRay(origin, dirVec * 1.0f, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(origin, dirVec, 1.0f, LayerMask.GetMask("object"));
        scanObject = (rayHit.collider != null) ? rayHit.collider.gameObject : null;
      
    }
}
