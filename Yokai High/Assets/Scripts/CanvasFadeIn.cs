using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFadeIn : MonoBehaviour
{
    CanvasGroup cg;
    [SerializeField]float fadeSpeed=0.05f;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        cg.alpha-=fadeSpeed;
    }
}
