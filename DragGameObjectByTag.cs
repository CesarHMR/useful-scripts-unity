using UnityEngine;

public class DragGameObjectByTag : MonoBehaviour
{
    bool isDraging;
    TargetJoint2D selectedObject;
    Camera main;
    private void Start()
    {
        main = Camera.main;
    }
    private void Update()
    {
        TouchCheck();
        Drag();
    }

    void TouchCheck()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            CheckTouchPosition(Input.touches[0].position);

        if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled))
            SetDragOff();

        if (Input.GetMouseButtonDown(0))
            CheckTouchPosition(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
            SetDragOff();
    }

    void CheckTouchPosition(Vector3 position)
    {
        Ray rayy = main.ScreenPointToRay(position);
        RaycastHit2D hit = Physics2D.Raycast(rayy.origin, rayy.direction);
        if (hit.collider != null)
        {
            Transform selection = hit.transform;

            if (selection.CompareTag("Use your desired Tag"))
            {
                SetDragOn(selection);
            }
        }
    }

    void SetDragOn(Transform selection)
    {
        selectedObject = selection.gameObject.GetComponent<TargetJoint2D>();
        selectedObject.enabled = true;
        isDraging = true;
    }

    void SetDragOff()
    {
        isDraging = false;
        if (selectedObject == null) return;
        selectedObject.enabled = false;
        selectedObject = null;
    }

    void Drag()
    {
        if (isDraging)
        {
            Vector2 touchPos = Input.touchCount > 0 ? Input.touches[0].position : (Vector2)Input.mousePosition;
            touchPos = main.ScreenToWorldPoint(touchPos);
            selectedObject.target = touchPos;
        }
    }
}