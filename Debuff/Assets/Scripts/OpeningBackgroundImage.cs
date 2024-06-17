using System.Collections;
using UnityEngine;

public class OpeningBackgroundImage : MonoBehaviour
{
    Material mat;
    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ofs = mat.mainTextureOffset;
        ofs.y -= (speed * Time.deltaTime);

        mat.mainTextureOffset = ofs;
        
    }
}
