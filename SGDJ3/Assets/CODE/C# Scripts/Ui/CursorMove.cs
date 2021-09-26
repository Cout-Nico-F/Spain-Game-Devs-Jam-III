using UnityEngine;

public class CursorMove : MonoBehaviour {

    [SerializeField] private Sprite pressedCursor;
    [SerializeField] private Sprite defaultCursor;
    
    private SpriteRenderer cursorRenderer;
    private Camera cam;
    private Vector2 cursorPos;


    private void Start()
    {
        cam = Camera.main;
        cursorRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }


    private void Update()
    {
        cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }


    public void SetCursorDefault()
    {
        cursorRenderer.sprite = defaultCursor;
    }

    public void SetCursorPressed()
    {
        cursorRenderer.sprite = pressedCursor;
    }
}
