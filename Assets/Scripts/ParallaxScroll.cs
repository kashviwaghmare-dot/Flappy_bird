using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxScroll : MonoBehaviour
{
    [Header("Scrolling Settings")]
    [Tooltip("Speed of scrolling. Higher values scroll faster to the left.")]
    public float scrollSpeed = 2f;

    [Tooltip("If checked, the layer will scroll automatically.")]
    public bool isScrolling = true;

    private float spriteWidth;
    private Transform[] layers = new Transform[2];
    private Vector3 startPosition;

    void Start()
    {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError($"[ParallaxScroll] No sprite assigned to SpriteRenderer on {gameObject.name}!");
            enabled = false;
            return;
        }

        spriteWidth = spriteRenderer.bounds.size.x;
        startPosition = transform.position;

        layers[0] = transform;

        GameObject clone = new GameObject(gameObject.name + "_Clone");
        clone.transform.parent = transform.parent;
        clone.transform.localScale = transform.localScale;
        clone.transform.position = transform.position + new Vector3(spriteWidth, 0, 0);

        SpriteRenderer cloneRenderer = clone.AddComponent<SpriteRenderer>();
        cloneRenderer.sprite = spriteRenderer.sprite;
        cloneRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        cloneRenderer.sortingOrder = spriteRenderer.sortingOrder;
        cloneRenderer.color = spriteRenderer.color;

        layers[1] = clone.transform;
    }

    void Update()
    {
        if (!isScrolling) return;

        float movement = scrollSpeed * Time.deltaTime;

        layers[0].Translate(Vector3.left * movement);
        layers[1].Translate(Vector3.left * movement);

        if (layers[0].position.x <= startPosition.x - spriteWidth)
        {

            float gapAdjustment = layers[0].position.x - (startPosition.x - spriteWidth);

            layers[0].position = new Vector3(layers[1].position.x + spriteWidth + gapAdjustment, layers[0].position.y, layers[0].position.z);

            Transform temp = layers[0];
            layers[0] = layers[1];
            layers[1] = temp;
        }
    }

    public void SetScrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
