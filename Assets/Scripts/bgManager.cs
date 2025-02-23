using UnityEngine;

public class bgManager : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = new Vector2(scrollSpeed, 0);
    }
    void Update()
    {
        mat.mainTextureOffset += offset * Time.deltaTime;
    }
}