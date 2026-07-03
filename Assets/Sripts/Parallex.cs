using UnityEngine;
using UnityEngine.UI;

public class Parallex : MonoBehaviour
{
    private Image image;
    private Material materialInstance;
    public float animationSpeed = 1f;

    private void Awake()
    {
        image = GetComponent<Image>();
        materialInstance = new Material(image.material);
        image.material = materialInstance;
    }

    private void Update()
    {
        materialInstance.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}