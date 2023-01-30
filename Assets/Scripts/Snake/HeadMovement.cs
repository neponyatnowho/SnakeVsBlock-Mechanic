using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Snake))]
public class HeadMovement : MonoBehaviour
{
    [SerializeField] private float _forwadrSpeed = 5;
    [SerializeField] private float _sensitivity = 50;
    [SerializeField] private float _tailSpringiness = 1;


    private SnakeHead _head;
    private List<Segment> _tail;
    private Snake _snake;
    private Rigidbody2D _headRigitbody;
    private Vector2 touchLastPos;
    private Camera mainCamera;
    private float sidewaysSpeed;

    private void Start()
    {
        mainCamera = Camera.main;
        _head = GetComponentInChildren<SnakeHead>();
        _headRigitbody = _head.GetComponent<Rigidbody2D>();
        _snake = GetComponent<Snake>();
        _tail = _snake.Tail; 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * _sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        _headRigitbody.velocity = new Vector2(sidewaysSpeed * 5, _forwadrSpeed);
        sidewaysSpeed = 0;
        MoveTailToHead();
    }

    private void MoveTailToHead()
    {
        Vector3 previosPosition = _head.transform.position;

        foreach (var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            previosPosition.y -= segment.transform.localScale.y;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previosPosition, _tailSpringiness);
            previosPosition = tempPosition;
        }
    }
}


