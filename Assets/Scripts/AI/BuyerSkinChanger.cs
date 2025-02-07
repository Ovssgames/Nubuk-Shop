using UnityEngine;

public class BuyerSkinChanger : MonoBehaviour
{
    [SerializeField] Texture[] skins;
    [SerializeField] Renderer renderer;


    private void Start()
    {
        int id = Random.Range(0, skins.Length);

        if (renderer!= null)
        {
            renderer.material.SetTexture("_MainTex", skins[id]);
            Debug.Log("SetTexture");
        }
    }
}
