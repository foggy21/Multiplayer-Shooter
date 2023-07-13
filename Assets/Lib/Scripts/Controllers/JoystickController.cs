using System;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private GameObject joystickFrame;
    [SerializeField] private GameObject joystickButton;
    public Action<Vector2> OnPlayerMove;
    private bool touchStart;
    private Vector2 joystickStartPosition;
    private Vector2 joystickEndPosition;
    private Vector2 joystickOriginalPosition;

    private void Start()
    {
        joystickOriginalPosition = joystickFrame.transform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                joystickStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                joystickFrame.transform.position = joystickStartPosition;
                joystickButton.transform.position = joystickStartPosition;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                touchStart = true;
                joystickEndPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchStart = false;
                joystickFrame.transform.position = joystickOriginalPosition;
                joystickButton.transform.position = joystickOriginalPosition;
            }
        }


    }

    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = joystickEndPosition - joystickStartPosition;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            joystickButton.transform.position = new Vector2(joystickStartPosition.x + direction.x, joystickStartPosition.y + direction.y);
            OnPlayerMove.Invoke(direction);
        }
    }
}
