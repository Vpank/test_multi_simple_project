using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f; //сила прыжка (ваш КЭП)
    bool facingRight = true;
    bool grounded = false; //стоит ли объект на земле
    public Transform groundCheck; // должен проверять столкновения с землёй
    public float groundRadius = 0.2f; //радиус круга относительно точки, который будет каждый кадр проверяться
    public LayerMask whatIsGround; // маска слоя, помогающая определить, что будет землёй
    public float score;
    public float move; //может принимать значения от -1 до 1, где -1 - лево, 1 - право.

    private GameObject star;
    // Use this for initialization
    void Start()
    {

    }

    // Эта функция как простой апдэйт, но каждый шаг физики выполняется последовательно, вне завичимости от производительности
    void FixedUpdate()
    {


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); // оверлап сёркл проверяет пересечение некоторого круга с некоторым слоем

        move = Input.GetAxis("Horizontal"); //отсюда мув и получает значения по горизонтальной оси

    }
    //События в простом апдейт выполняются в разных интервалах, в зависимости от производительности в игре
    void Update()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) //если нажато W или вверх, то
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce)); // добавляется сила, нулевая по горизонтали, а по вертикали равная джамп форс
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); //движение влево -вправо

        if (move > 0 && !facingRight) //если объект смотрит влево, а конпка нажата вправо 
            Flip();//спрайт поворачивается (отражается)
        else if (move < 0 && facingRight)
            Flip();


        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }


    }

    void Flip() //что происходит с персонажем, когда он меняет направление оси
    {
        facingRight = !facingRight; // меняется направление взгляда 
        Vector3 theScale = transform.localScale; // значение localScale отвечает за отображение объекта вокруг оси
        theScale.x *= -1;//естественно зеркалим
        transform.localScale = theScale;  //и присваиваем ёпта
    }

    void OnTriggerEnter2D(Collider2D col) //если входим в триггер, то функция принимает объект типа коллайдер 2D 
    {
        if ((col.gameObject.name == "dieCollider") || (col.gameObject.name == "saw")) //
            Application.LoadLevel(Application.loadedLevel);

        if (col.gameObject.name == "burito") 
        {
            score++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.name == "endLevel")
        {
            if (!(GameObject.Find("burito"))) Application.LoadLevel("scene2");
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 100), "Food: " + score);
    }

}
