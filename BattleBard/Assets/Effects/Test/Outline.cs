using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
{
    public Shader outlineShader;
    public RawImage outlineImage;
    RenderTexture outlineRT;

    Camera outlineCam;

    void Awake() 
    {
        outlineRT = RenderTexture.GetTemporary(Screen.width, Screen.height, 24);
        outlineRT.name = "OutlineObjects";
        outlineCam = Camera.main.transform.GetChild(0).GetComponent<Camera>();

        outlineCam.CopyFrom(Camera.main);
        outlineCam.clearFlags = CameraClearFlags.Color;
        outlineCam.backgroundColor = new Color(0,0,0,0);
        outlineCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");
        outlineCam.depth = 1;
         outlineCam.SetReplacementShader(outlineShader, "");


        outlineCam.gameObject.SetActive(true);
        outlineCam.enabled = true;
        outlineCam.targetTexture = outlineRT;

        outlineImage.texture = outlineRT;
        outlineImage.gameObject.SetActive(true);

    }
}
