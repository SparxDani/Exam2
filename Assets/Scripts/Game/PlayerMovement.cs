using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    private Rigidbody2D myRB;
    [SerializeField]
    private float speed;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 3;
    public PlayerInput _playerInput;
    [SerializeField] private AudioClip hitSound; // AudioClip para el sonido de golpe
    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido
    // Start is called before the first frame update
    void Start()
    {
        if (up == KeyCode.None) up = KeyCode.UpArrow;
        if (down == KeyCode.None) down = KeyCode.DownArrow;
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();
        
            myRB.velocity = movementInput * speed;
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.tag == "Candy" )
        {
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
        }
        if (other.tag == "Enemy")
        {
            audioSource.PlayOneShot(hitSound);
            EnemyGenerator.instance.ManageEnemy(other.gameObject.GetComponent<EnemyController>(), this);
        }
    }
}
