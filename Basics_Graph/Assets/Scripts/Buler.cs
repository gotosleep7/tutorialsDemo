using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Buler : MonoBehaviour
{
    [Range(1, 4)]
    public int iteration = 1;   // 迭代次数
    [Range(1, 8)]
    public int downSample = 2;  // 降采样比例
    [Range(0.2f, 3.0f)]
    public float blurSpread = 2f;  // 模糊范围


    RenderTexture texture;

    public    Camera targetCamera;
    Material material;

    public RenderTexture originTexture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Cap();
        }
    }

    public void Cap()
    {
        // 进行降采用处理
        // Screen.width为屏幕宽度, Screen.height为屏幕高度
        // 根据实际项目调整
        int rtW = Screen.width / downSample;
        int rtH = Screen.height / downSample;

        // 获得指定摄像机渲染的屏幕像素
        if (texture == null)
        {
            texture = RenderTexture.GetTemporary(rtW, rtH, 24);
        }
        RenderTexture source = RenderTexture.GetTemporary(rtW, rtH, 24);
        source.filterMode = FilterMode.Bilinear;
        targetCamera.targetTexture = source;  // targetCamera 是需要使用的摄像机
        RenderTexture.active = source;
        targetCamera.Render();
        RenderTexture buffer0 = RenderTexture.GetTemporary(rtW, rtH, 24);
        buffer0.filterMode = FilterMode.Bilinear;
        Graphics.Blit(source, buffer0);
        // 进行模糊迭代,这里注意是i是从1起
        for (int i = 1; i < iteration; ++i)
        {
            material.SetFloat("_BlurSize", 1.0f + i * blurSpread);
            RenderTexture buffer1 = RenderTexture.GetTemporary(rtW, rtH, 24);
            Graphics.Blit(buffer0, buffer1, material, 0);  // materail是使用对应blurshader的材质
            RenderTexture.ReleaseTemporary(buffer0);  // 申请了就要释放
            buffer0 = buffer1;
        }
        Graphics.Blit(buffer0, texture);

        RenderTexture.ReleaseTemporary(buffer0);
        RenderTexture.ReleaseTemporary(source);
        targetCamera.targetTexture = originTexture;
        RenderTexture.active = null;
        material.SetTexture("_MainTex", texture);
    }
}
