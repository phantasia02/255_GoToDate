using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LanKuDot.UnityToolBox;

public enum RenderingMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent,
}

///<summary>
///Afterimage effects
///</summary>
public class AfterImageEffects : MonoBehaviour
{

    public class AfterImage
    {
        //My GameObject
        public GameObject _MyObj;
        //afterimage grid
        public Mesh _Mesh;
        //Afterimage texture
        public Material _Material;
        //Afterimage position
        public Matrix4x4 _Matrix;
        //afterimage retention time
        public float _Duration;
        // Material Property ID
        public int _PropertyToID = -1;

        public Color _StartColor = Color.black;

        public AfterImage(
            GameObject myObj, 
            Mesh mesh, 
            Material material, 
            string PropertyName, 
            Matrix4x4 matrix4x4)
        {
            _MyObj = myObj;
            _Mesh = mesh;
            _Material = material;
            _Matrix = matrix4x4;
            _PropertyToID = Shader.PropertyToID("_BaseColor");
        }

        public void UpdateAfterImage(TweenColorEaseCurve Curve)
        {
            _StartColor = Curve.StartValue;

            Tweener lTempTweener = DOTween.To(
                () => _StartColor, x => _StartColor = x,
                Curve.endValue, Curve.duration)
                .SetEase(Curve.curve)
                .OnUpdate(()=> 
                {
                    if (_Material != null)
                    {
                        if (_Material.HasProperty(_PropertyToID))
                            _Material.SetColor(_PropertyToID, _StartColor);

                        Graphics.DrawMesh(_Mesh, _Matrix, _Material, _MyObj.layer);
                    }
                });
        }
    }

    public enum SurfaceType
    {
        Opaque,
        Transparent
    }
    public enum BlendMode
    {
        Alpha,
        Premultiply,
        Additive,
        Multiply
    }


    public Material MeshMaterial;

    //Open afterimage
    public bool _OpenAfterImage;
    public bool _OpenAfterImageShow = true;

    //The interval between generating afterimages
    public float _IntervalTime = 0.2f;
    public float _Time = 0;
    public string _PropertyToIDName = "";

    [SerializeField]
    private TweenColorEaseCurve _Curve;
    public TweenColorEaseCurve Curve
    {
        set{ _Curve = value;}
        get => _Curve;
    }

    private MeshRenderer _SkinnedMeshRenderer;

    void Awake()
    {
        //_AfterImageList = new List<AfterImage>();
        _SkinnedMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void OpenAfterImage(bool open)
    {
        if (open == _OpenAfterImage)
            return;

        if (open && _OpenAfterImageShow)
            CreateAfterImage();
        else
            _Time = 0.0f;

        _OpenAfterImage = open;
    }

    void Update()
    {
        if (_SkinnedMeshRenderer == null)
        {
            _OpenAfterImage = false;
            return;
        }

        if (_OpenAfterImage)
        {
            _Time += Time.deltaTime;
            //Generate afterimages
            CreateAfterImage();
        }
    }

    ///<summary>
    ///Generate afterimage
    ///</summary>
    void CreateAfterImage()
    {
        //Generate afterimages
        if (_Time >= _IntervalTime)
        {
            _Time = 0;

            Mesh mesh = new Mesh();
           // _SkinnedMeshRenderer.BakeMesh(mesh);
            mesh = _SkinnedMeshRenderer.GetComponent<MeshFilter>().mesh;

            Material material = new Material(MeshMaterial);
            material.SetFloat("_Surface", (float)SurfaceType.Transparent);
            material.SetFloat("_Blend", (float)BlendMode.Additive);
            material.mainTexture = null;
            SetupMaterialBlendMode(material);
            //_SkinnedMeshRenderer.material = material;
            SetMaterialRenderingMode(material, RenderingMode.Fade);

            AfterImage lTempAfterImage = new AfterImage(
                this.gameObject,
                mesh,
                material,
                _PropertyToIDName,
                transform.localToWorldMatrix
                );

            lTempAfterImage.UpdateAfterImage(_Curve);
         
        }
    }
    
    

    ///<summary>
    ///Set texture rendering mode
    ///</summary>
    void SetMaterialRenderingMode(Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
              //  material.("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }

    void SetupMaterialBlendMode(Material material)
    {
        bool alphaClip = material.GetFloat("_AlphaClip") == 1;
        if (alphaClip)
            material.EnableKeyword("_ALPHATEST_ON");
        else
            material.DisableKeyword("_ALPHATEST_ON");
        SurfaceType surfaceType = (SurfaceType)material.GetFloat("_Surface");
        if (surfaceType == 0)
        {
            material.SetOverrideTag("RenderType", "");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
            material.SetShaderPassEnabled("ShadowCaster", true);
        }
        else
        {
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_ZWrite", 0);
            material.SetShaderPassEnabled("ShadowCaster", false);
            BlendMode blendMode = (BlendMode)material.GetFloat("_Blend");
            switch (blendMode)
            {
                case BlendMode.Alpha:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendMode.Premultiply:
                    //material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    //material.SetInt("_ZWrite", 0);
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    //material.SetShaderPassEnabled("ShadowCaster", false);
                    break;
                case BlendMode.Additive:
                   // material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                   // material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    //material.SetShaderPassEnabled("ShadowCaster", false);
                    break;
                case BlendMode.Multiply:
                    //material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                   // material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    //material.SetShaderPassEnabled("ShadowCaster", false);
                    break;
            }
        }
    }
}
