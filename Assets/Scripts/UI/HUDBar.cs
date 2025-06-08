using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBar : MonoBehaviour
{
    public RectMask2D Mask;
    public float FillLevel {
        get => fillLevel;
        set {
            fillLevel = value;
            Mask.padding = new Vector4(0, 0, (1 - fillLevel) * Mask.rectTransform.rect.width, 0);
        }
    }

    private float fillLevel;
}
