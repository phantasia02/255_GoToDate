using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamGLDraw : MonoBehaviour
{

    [SerializeField] protected Material m_2DLineMat = null;

    protected bool m_OpenDrewGL = false;
    public bool OpenDrewGL
    {
        set => m_OpenDrewGL = value;
        get => m_OpenDrewGL;
    }

    protected Vector3 m_startVertex = Vector3.zero;
    public Vector3 startVertex
    {
        set => m_startVertex = value;
        get => m_startVertex;
    }


    void Start()
    {
        
    }

    void Update()
    {
       
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawLine(Vector3.zero, Vector3.one * 2.0f);

        OnPostRender();
    }

    void OnPostRender()
    {

        if (!m_OpenDrewGL)
            return;

        GL.PushMatrix();
        m_2DLineMat.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        Debug.Log($"startVertex = {startVertex}");
        //GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0));
        //GL.Vertex(Vector3.one * 200.0f);
        GL.End();

        GL.PopMatrix();
    }
}
