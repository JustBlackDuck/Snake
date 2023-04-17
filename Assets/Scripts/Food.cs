using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int xmin = 0;
    public int xmax = 0;
    public int ymin = 0;
    public int ymax = 0;


    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition() //перемещает корм в случайную точку
    {
        float x = Random.Range(xmin, xmax);
        float y = Random.Range(ymin, ymax);
        //округляем координаты
        x = Mathf.Round(x);
        y = Mathf.Round(y);


        gameObject.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        RandomizePosition();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
