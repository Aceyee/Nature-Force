using UnityEngine;
using System.Collections.Generic;

public class Select : MonoBehaviour
{
    // Use this for initialization
    private Dictionary<Color, string> dic = new Dictionary<Color, string>();
    void Start()
    {
        dic.Add(new Color((float)1, (float)0, (float)0), "North America");
        dic.Add(new Color(0, 1, 0), "South America");
        dic.Add(new Color(0, 0, 1), "Africa");
        dic.Add(new Color(1, 1, 0), "Europe");
        dic.Add(new Color(1, 0, 1), "Asia");
        dic.Add(new Color(0, 1, 1), "Australia");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("You selected the " + hit.transform); // ensure you picked right object
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                return;

            Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            Color c = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
            //Debug.Log(pixelUV);
            if (dic.ContainsKey(c))
            {
                //Debug.Log(dic[c]);
            }
        }

    }
}
