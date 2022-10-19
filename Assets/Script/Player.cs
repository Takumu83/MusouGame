using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //移動速度
    [SerializeField] public float mae;
    [SerializeField] public float yoko;
    //ジャンプ力の設定
    [SerializeField] public float jumppower;
    //プレイヤー移動速度
    Vector3 velocity;
    //rigidbody取得
    private Rigidbody rigidbody;
    //キャラクターコントローラー
    private CharacterController controller;

    //マウス視点移動
    float old_mouse_y;
    bool old_mouse_y_flag;
    float ang_y;


    //初期設定
    private void Awake()
    {
        //キャラクターコントローラー取得
        this.controller = this.GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        jumppower = jumppower;
        //コンポーメントをキャッシュしておく
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //スペースでジャンプ
        if (Input.GetKeyDown("space") || Input.GetButtonDown("Fire"))
        {
            bool isGrouded = Physics.Linecast(transform.position, transform.position - transform.up * 1.1f);
            if (isGrouded == true)
            {
                rigidbody.AddForce(Vector3.up * jumppower);
            }
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        //pad移動
        Vector3 vec = this.velocity;
        vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void FixedUpdate()
    {
        //視点移動
        if(old_mouse_y_flag == false)
        {
            old_mouse_y = Input.mousePosition.y;
            old_mouse_y_flag = true;
        }
        ang_y += old_mouse_y - Input.mousePosition.y;
        if (ang_y < -40)
            ang_y = -40;
        if (ang_y > 40)
            ang_y = 40;
        old_mouse_y = Input.mousePosition.y;
        transform.rotation = Quaternion.Euler(ang_y, Input.mousePosition.x, 0);


        //移動
        float x;
        float z;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        velocity = new Vector3(x*yoko, 0, z*mae);
        velocity = transform.TransformDirection(velocity);
        transform.localPosition += velocity * Time.fixedDeltaTime;
    }
}
