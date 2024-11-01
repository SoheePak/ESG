using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float speed = 15f;

    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    void Update()
    {
        transform.position += Time.deltaTime * transform.forward * speed;
    }

}
