using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairScript : MonoBehaviour
{

    public Color InnerCircleColor;
    public float OuterCircleColorAlpha;
    [SerializeField]private Image OuterCircleImage;
    [SerializeField]private Image InnerCircleImage;

    void Update()
    {
        OuterCircleImage.color=new Vector4(OuterCircleImage.color.r, OuterCircleImage.color.g, OuterCircleImage.color.b, OuterCircleColorAlpha);
        InnerCircleImage.color = InnerCircleColor;
    }


}
