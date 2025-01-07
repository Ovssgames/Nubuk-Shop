using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float speed;

    private Vector2 value;

    private void Update()
    {
        value += new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);
        material.mainTextureOffset = value;
        if (value.x > 100)
        {
            value.x = 0; 
            value.y = 0;
        }
    }
}
