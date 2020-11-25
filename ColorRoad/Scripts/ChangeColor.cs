using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] Color colorRet;
    [SerializeField] bool floorMat = false;
    [SerializeField] Vector2 speed;
    [SerializeField] Texture2D texture;

    Material mat;

    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Start()
    {
        mat.color = colorRet;

        if(floorMat)
            SetFloorMat();
    }

    void SetFloorMat()
    {
        mat.SetColor("Color_FBD0185B", colorRet);
        mat.SetVector("Vector2_22CDCB21", speed);
        mat.SetTexture("Texture2D_792AD589", texture);
    }

}
