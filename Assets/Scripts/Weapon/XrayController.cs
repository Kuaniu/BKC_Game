using Unity.Mathematics;
using UnityEngine;

public class XrayController : MonoBehaviour
{
    public static XrayController Instance;
    public GameObject HittedGameObject { get; private set; } = null;
    [SerializeField]
    private Color xrayColor = Color.red;
    [SerializeField]
    private int xrayLength = 10;
    [SerializeField]
    private float xraySpeed = 1.0f;
    [SerializeField]
    private float xrayWidth = 1.0f;

    private LineRenderer lineRenderer;
    private Transform startVFX;
    private Transform endVFX;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        startVFX = transform.GetChild(1);
        endVFX = transform.GetChild(2);

        UpdateEndPosition();
    }

    private void Update()
    {
        UpdateEndPosition();
    }

    private void UpdateEndPosition()
    {
        Vector2 position = transform.position;
        float rotation = transform.rotation.eulerAngles.z;
        rotation *= Mathf.Deg2Rad;

        var direction = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));

        var hit = Physics2D.Raycast(position, direction.normalized);

        float length = xrayLength;
        float xrayEndRotation = 180;

        if (hit)
        {
            HittedGameObject = hit.collider.gameObject;

            length = (hit.point - position).magnitude;

            xrayEndRotation = Vector2.Angle(direction, hit.normal);
        }
        else
        {
            HittedGameObject = null;
        }

        lineRenderer.SetPosition(1, new Vector2(length, 0));

        Vector2 endPositon = position + direction * length;
        startVFX.position = position;
        endVFX.position = endPositon;
        endVFX.rotation = quaternion.Euler(0, 0, xrayEndRotation);
    }
}
