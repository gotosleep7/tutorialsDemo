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
    [SerializeField]
    FunctionLibrary.FunctionName function;
    Transform[] points;

    private void Awake()
    {
        points = new Transform[resolution * resolution];
        var step = 2f / resolution;
        var scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {

            Transform point = points[i] = Instantiate(pointPrefab);
            point.SetParent(transform, false);
            points[i].localScale = scale;
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
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float step = 2f / resolution;
        float time = Time.time;

        float v = 0.5f * step - 1f;
        for (int i = 0,x=0,z=0; i < points.Length; i++,x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }

            float u = (x + 0.5f) * step - 1f;
            //float v = (z + 0.5f) * step - 1f;
            Transform point = points[i];
            Vector3 position = point.localPosition;
            //position.y = position.x  * position.x * position.x;
            position = f(u,v, time);
            point.localPosition = position;
        }
    }
}
