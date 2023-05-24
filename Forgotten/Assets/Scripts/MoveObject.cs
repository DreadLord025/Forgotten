using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 2.0f; // �������� �������� �������
    public float startPosition = -10.0f; // ��������� ������� �������
    public float endPosition = 10.0f; // �������� ������� �������
    public GameObject clone; // ������ ����� �������

    private float currentPosition; // ������� ������� �������
    private GameObject clone1; // ����� ������� ����

    private bool moveNext = true; // ���� ��� �������� ���������� �������
    private bool isMoving = false; // ���� ��� �������� �������� �������
    public List<GameObject> clonesList;

    void Start()
    {
        // �������� ����� �������
        clone1 = Instantiate(clone, transform.position - new Vector3(0, 14.4f, 1), Quaternion.identity);
        clonesList.Add(clone1); // ��������� ������ �� ��������� ����� � ������
    }

    void Update()
    {
        if (!isMoving) // ���� ������ �� ��������
        {
            if (moveNext) // ���� ����� ������� ��������� ������
            {
                moveNext = false;
                StartCoroutine(MoveObjectCoroutine()); // ��������� �������� �������� �������
            }
        }

        // �������� ����� ������� �� ������� ��������
        clone1.transform.position = new Vector3(transform.position.x, currentPosition - 14.4f, transform.position.z);
    }

    IEnumerator MoveObjectCoroutine()
    {
        isMoving = true; // ������������� ���� �������� �������

        // �������� ������� �����
        while (currentPosition < endPosition)
        {
            currentPosition += Time.deltaTime * speed;
            transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
            yield return null;
        }

        // ����������� ������� �� ��������� �������
        transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
        currentPosition = startPosition;

        isMoving = false; // ���������� ���� �������� �������
        moveNext = true; // ������������� ���� ��� �������� ���������� �������
    }
}
