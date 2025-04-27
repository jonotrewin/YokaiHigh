using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScroll : MonoBehaviour
{
    [SerializeField]float maxYPosition;
    [SerializeField] float speed = 1;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < maxYPosition)
        {
            transform.position += transform.up * speed;
        }
    }
}
