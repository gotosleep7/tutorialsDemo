using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FunctionLibrary;
using UnityEngine.UIElements;
using static UnityEngine.Mathf;
public class FunctionLibrary
{


    //public delegate float Function(float x,float z,float t);
    public delegate Vector3 Function(float x, float z, float t);
    public enum FunctionName { Wave,MultiWave,Ripple,Sphere, Torus }

    static Function[] functions = { Wave, MultiWave, Ripple , Sphere ,Torus};

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static Vector3 Wave(float u,float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        float y = Sin(PI * (u+t * 0.5f));
        
        y += Sin(2f * PI * (v + t )) *0.5f;
        y += Sin(PI * (u + v + t * 0.25f));
        p.y = y * (1 / 2.5f);


        p.z = v;
        // 性能上，优先使用乘法。
        // 在这里(2f/3f)是常量表达式，常量表达式会在编译阶段被编译器处理成1.5 ，所以最后执行的是一个乘法，
        return p;
    }

    public static Vector3 Ripple(float u , float v, float t) {
        Vector3 p;
        p.x = u;
        

        float d = Sqrt(u * u +v * v);
        float y =  Sin(PI * 4f  * d - t);
        p.y = y / (1f + 10f * d);
        p.z = v;
        return p ;
    }
    public static Vector3 Sphere(float u, float v, float t) {


        Vector3 p;
        
        //float r = 0.5f + 0.5f * Sin(PI*t);
        float r = 0.9f + 0.1f * Sin(PI * (6f * v + 4f * u + t)); 
        float s = r * Cos(PI * v * 0.5f);
        p.x = s *Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }


    public static Vector3 Torus(float u,float v,float t) {

        Vector3 p;
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f *Sin(PI * (8f * u + 4f * v + 2f *t));
        float r = 1f;
        float s = r1 + r2 * Cos(PI * v);
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v); 
        p.z = s * Cos(PI * u);
        return p;
    }
}
