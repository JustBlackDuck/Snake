using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/////
using TMPro;
/////

public class SnakeController : MonoBehaviour
{
    public List <Transform> segments = new List <Transform>(); //Списочек с кусочками тела змейки
    public Transform segmentPrefab; //Префаб кусочка змейки для спавна
    public Vector2 inputDirection = Vector2.right; //Указываю, что изначально буду двигаться вправо
    public int initialSize = 4; //Размер змейки по умолчанию

    public TMP_Text scoreText;
    int score = 0; //счет яблочек

    void Start()
    {
        ResetState();
    }

    
    void Update()
    {
        if(inputDirection.x != 0) //если наше текущее направление х не равно нулю, значит мы уже двигаемся по этой оси х
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                inputDirection = Vector2.up;
            }

            else if(Input.GetKeyDown(KeyCode.S))
            {
                inputDirection = Vector2.down;
            }
        }
        else if(inputDirection.y != 0)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                inputDirection = Vector2.left;
            }

            else if(Input.GetKeyDown(KeyCode.D))
            {
                inputDirection = Vector2.right;
            }
        }
    }

    //В отличие от Update - не привязан к кол - ву кадров и работает по времени
    void FixedUpdate()
    {
        for(int i = segments.Count-1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        //Mathf.Round() - округление
        float x = Mathf.Round(gameObject.transform.position.x) + inputDirection.x; //Берем текущую позицию и затем доюавляем ей направление оси х
        float y = Mathf.Round(gameObject.transform.position.y) + inputDirection.y; //Берем текущую позицию и затем доюавляем ей направление оси у
        gameObject.transform.position = new Vector2(x, y); //присваеваем голове новую позицию с учетом нашего направления
    }
    public void ResetState() //Обнуляем свою игру
        {
            inputDirection = Vector2.right;
            gameObject.transform.position = Vector3.zero;
            score = 0;
            scoreText.text = score.ToString();
            for(int i = 1; i < segments.Count; i++) //для каждого кусочка тела, кроме 0 (головы)
            {
                Destroy(segments[i].gameObject); //удаляем объект со сцены
            }
            segments.Clear(); //очищаем список ЦЕЛИКОМ
            segments.Add(gameObject.transform); //добавляем список ЦЕЛИКОМ

            for(int i = 0; i< initialSize - 1; i++)
            {
                Grow();
            }
        
        }

    public void Grow()//Функция для роста змейки
    {
        Transform newSegment = Instantiate(segmentPrefab); //Создаем копию сегмента и кладем его в переменную
        newSegment.position = segments[segments.Count - 1].position; // В позицию нового сегмента назначаем координаты текущего последнего кусочка тела
        segments.Add(newSegment); //Новый сегмент в списочек всех сегментов (кусочков тела змейки)
    }  

    private void OnTriggerEnter2D(Collider2D collision)
         { 
            if(collision.gameObject.CompareTag("Obstacle"))
            {
                ResetState();
            }

        if (collision.gameObject.CompareTag("Food")) {
            Grow();
        }
        }
    private void OnTriggerEnter2D2(Collider2D other)
    {
        if(other.gameObject.CompareTag("Food")) {
            score++;
            scoreText.text = score.ToString();
            Grow();
        }
    }
}


    
