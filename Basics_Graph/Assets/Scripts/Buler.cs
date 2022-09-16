using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Buler : MonoBehaviour
{
    [Range(1, 4)]
    public int iteration = 1;   // ��������
    [Range(1, 8)]
    public int downSample = 2;  // ����������
    [Range(0.2f, 3.0f)]
    public float blurSpread = 2f;  // ģ����Χ


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
        // ���н����ô���
        // Screen.widthΪ��Ļ���, Screen.heightΪ��Ļ�߶�
        // ����ʵ����Ŀ����
        int rtW = Screen.width / downSample;
        int rtH = Screen.height / downSample;

        // ���ָ���������Ⱦ����Ļ����
        if (texture == null)
        {
            texture = RenderTexture.GetTemporary(rtW, rtH, 24);
        }
        RenderTexture source = RenderTexture.GetTemporary(rtW, rtH, 24);
        source.filterMode = FilterMode.Bilinear;
        targetCamera.targetTexture = source;  // targetCamera ����Ҫʹ�õ������
        RenderTexture.active = source;
        targetCamera.Render();
        RenderTexture buffer0 = RenderTexture.GetTemporary(rtW, rtH, 24);
        buffer0.filterMode = FilterMode.Bilinear;
        Graphics.Blit(source, buffer0);
        // ����ģ������,����ע����i�Ǵ�1��
        for (int i = 1; i < iteration; ++i)
        {
            material.SetFloat("_BlurSize", 1.0f + i * blurSpread);
            RenderTexture buffer1 = RenderTexture.GetTemporary(rtW, rtH, 24);
            Graphics.Blit(buffer0, buffer1, material, 0);  // materail��ʹ�ö�Ӧblurshader�Ĳ���
            RenderTexture.ReleaseTemporary(buffer0);  // �����˾�Ҫ�ͷ�
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
