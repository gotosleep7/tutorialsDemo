using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{

    [SerializeField]
    Transform cubePrefab;

    Transform[] points;
    [SerializeField, Range(1, 100)]
    int rate;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionsType functionsType;


    private void Awake()
    {
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
           Transform point  = points[i] = Instantiate(cubePrefab);
            point.SetParent(transform, false);
            
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

        Functions.Function f =  Functions.GetFunction(functionsType);
        //Debug.Log(Time.time);
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        for (int i = 0,x = 0,z = 0; i < points.Length; i++,x ++)
        {   
            if(x == resolution)
            {
                x = 0;
                z += 1;
            }
            float time = Time.time;
            Vector3 position = points[i].localPosition;
            position.x = (x + 0.5f) * step - 1f;
           
            position.z = (z + 0.5f) * step - 1f;
            position.y = f(position.x, position.z,time);
            points[i].localScale = scale;

            points[i].position = position;
        }
    }
}
public enum FunctionsType
{
    Wave,MultiWave, Ripple
}



public class Functions
{

    public delegate float Function(float x, float z,float y);

    static Function[] arr = {Wave , MultiWave ,Ripple};

    public static Function GetFunction(FunctionsType name) {

        return arr[(int)name];
    }

    public static float Wave(float x,float z,float t)
    {
        return Mathf.Sin(Mathf.PI * (x +t ));
    }

    public static float MultiWave(float x, float z, float t)
    {
        // 取值范围是-1到1
        float y = Mathf.Sin(Mathf.PI * (x + t));

        // 取值范围加了0.5  现在是 -1.5 到 1.5
        //y += Mathf.Sin(2f * Mathf.PI * (x + t)) / 2f;
        y += Mathf.Sin(2f * Mathf.PI * (x + t))  * 0.5f;

        // 控制在-1到1 之间
        //y = y / 1.5f;
        y = y * (2f/3f);// 2f/3f 不能完全用十进制表示，所以继续用倒数表示
        
        return y;
    }

    public static float Ripple(float x, float z, float t)
    {
        float d = Mathf.Abs(x);

        float y = Mathf.Sin(  Mathf.PI * (4f * d - t));
        return y / (1f + 10f * d);
    }
}