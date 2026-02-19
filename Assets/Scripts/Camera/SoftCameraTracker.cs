using UnityEngine;

public class SoftCameraTracker : MonoBehaviour, ICameraTracker
{
    //  амера - статична€, но интерполирует значени€ с позицией курсора мыши
    private Vector3 startPosition;
    public Vector3 StartPosition
    {
        get => startPosition; 
        set => startPosition = value;
    }

    [Header("“о, насколько плавно будет движение камеры")]
    [SerializeField] protected float _speed = 5f;

    protected Vector3 TargetPosition => GetMousePosition();

    private void Start()
    {
        startPosition = Camera.main.transform.position;
    }

    protected Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public virtual Vector3 GetCurrentPosition(Vector3 targetPosition)
    {
        return Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
