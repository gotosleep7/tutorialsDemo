using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField]
    Transform cubePrefab;

    Transform cubeTransform;
    Transform[] points;
    [SerializeField, Range(1, 100)]
    int rate;

    [SerializeField,Range(10,100)]
    int resolution = 10;

    private void Awake()
    {
        points = new Transform[resolution];
        float step = 2f / resolution;
        //float scale = 
        for (int i = 0; i < points.Length; i++)
        {

            points[i] = Instantiate(cubePrefab);
            points[i].localScale = Vector3.one / 10f ;
        }
    }

    void Start()
    {
        //Transform t = Instantiate(cubePrefab);
        //t.SetParent(transform, false);
        //cubeTransform = t;
        //t.localScale = Vector3.one * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {

        //float step = 
        for (int i = 0; i < points.Length; i++)
        {
            float time = Time.time;
            Vector3 position;
            position.x = ( i + 0.5f) / 10f - 1f;
            //position.x = (i / 5f + 0.1f) - 1f;
            position.y = Mathf.Sin(Mathf.PI + time);
            position.z = 0;
            points[i].position = position;  
        }
        //Vector3 position = cubeTransform.localPosition;
        


        
    }
}
