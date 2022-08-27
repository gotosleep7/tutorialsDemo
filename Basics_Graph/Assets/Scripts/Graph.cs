using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    Transform[] points;

    private void Awake()
    {
        points = new Transform[resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one  * step;
        Vector3 position = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point =  Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            //position.y = position.x * position.x * position.x;

            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform,false);
            points[i] = point;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform point =  points[i];
            Vector3 position =  point.localPosition;
            //position.y = position.x  * position.x * position.x;
            position.y = Mathf.Sin((position.x + Time.time) * Mathf.PI);
            point.localPosition = position; 
        }
    }
}
