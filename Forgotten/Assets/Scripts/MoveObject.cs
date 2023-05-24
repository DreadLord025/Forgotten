using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 2.0f; // скорость движения объекта
    public float startPosition = -10.0f; // начальная позиция объекта
    public float endPosition = 10.0f; // конечная позиция объекта
    public GameObject clone; // префаб копии объекта

    private float currentPosition; // текущая позиция объекта
    private GameObject clone1; // копия объекта ниже

    private bool moveNext = true; // флаг для движения следующего объекта
    private bool isMoving = false; // флаг для проверки движения объекта
    public List<GameObject> clonesList;

    void Start()
    {
        // создание копий объекта
        clone1 = Instantiate(clone, transform.position - new Vector3(0, 14.4f, 1), Quaternion.identity);
        clonesList.Add(clone1); // добавляем ссылку на созданную копию в список
    }

    void Update()
    {
        if (!isMoving) // если объект не движется
        {
            if (moveNext) // если нужно двигать следующий объект
            {
                moveNext = false;
                StartCoroutine(MoveObjectCoroutine()); // запускаем корутину движения объекта
            }
        }

        // движение копий объекта за главным объектом
        clone1.transform.position = new Vector3(transform.position.x, currentPosition - 14.4f, transform.position.z);
    }

    IEnumerator MoveObjectCoroutine()
    {
        isMoving = true; // устанавливаем флаг движения объекта

        // движение объекта вверх
        while (currentPosition < endPosition)
        {
            currentPosition += Time.deltaTime * speed;
            transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
            yield return null;
        }

        // перемещение объекта на начальную позицию
        transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
        currentPosition = startPosition;

        isMoving = false; // сбрасываем флаг движения объекта
        moveNext = true; // устанавливаем флаг для движения следующего объекта
    }
}
