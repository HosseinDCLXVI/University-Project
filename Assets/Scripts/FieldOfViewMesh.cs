using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewMesh : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask WallAndGroundLayer;
    [SerializeField] private int NumberOfRays;//{get{return NumberOfRays * 2 + 1; }}//actual number of rays are numberofrays *2 +1
    [SerializeField] private float FovAngle;
    [SerializeField] private int FovDistance;
    [SerializeField] private ProgressManager ProgressManagerScript;
    [SerializeField] private EnemyController EnemyControllerScript ;
    [SerializeField] SortingLayer sortingLayer;
      private MeshRenderer meshRenderer;

    bool CharacterDirection, Right = true, Left = false;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sortingLayerName = "Character";
        meshRenderer.sortingOrder = 0;

    }

    private void Update()
    {
        Vector2[] FieldOfViewRange = CalculateFieldOfViewRange();
        RaycastHit2D[] FieldOfViewRaycastHit2D = CreateFieldOfViewRaycast(FieldOfViewRange,WallAndGroundLayer);
        Vector3[] Vertices = FieldOfViewMeshVerticesCalculator(FieldOfViewRange, FieldOfViewRaycastHit2D);
        Vector2[] UV = FieldOfViewMeshUvCalculator(FieldOfViewRange, FieldOfViewRaycastHit2D);
        int[] Triangles = FieldOfViewMeshTrianglesCalculator(Vertices);
        CreateMesh(Vertices, Triangles, UV);

        if (ProgressManagerScript!=null)
        {
            CharacterDirection =ProgressManagerScript.CharacterDirection;
            transform.position = ProgressManagerScript.GetComponent<Transform>().position;
            CreateMesh(Vertices, Triangles, UV);


        }
        else if(EnemyControllerScript!=null)
        {
            CharacterDirection = EnemyControllerScript.EnemyDirection;
            transform.position = EnemyControllerScript.GetComponent<Transform>().position;
            EnemyEye(CreateFieldOfViewRaycast(FieldOfViewRange, PlayerLayer));

            if(EnemyControllerScript.EnemyCurrentHealth<0||!EnemyControllerScript.EnemyIsAwake)
            {
                meshRenderer.enabled=false ;
            }
            else
            {
                meshRenderer.enabled=true ;
            }
        }
        else if(EnemyControllerScript==null&&ProgressManagerScript==null)
        {
            meshRenderer.enabled = false ;
        }



    }

    Vector2[] CalculateFieldOfViewRange()
    {
        Vector2[] FieldOfViewRange = new Vector2[NumberOfRays * 2 + 1];
        float AngularDistanceBetweenRays = FovAngle / NumberOfRays;

        int x = 0;
 
            if (CharacterDirection)
            {
                for (float i = FovAngle; i >= -FovAngle; i -= AngularDistanceBetweenRays)
                {
                    FieldOfViewRange[x] = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * FovDistance, Mathf.Sin(i * Mathf.Deg2Rad) * FovDistance);
                    x++;
                }
            }
            else
            {
                for (float i = FovAngle; i >= -FovAngle; i -= AngularDistanceBetweenRays)
                {
                    FieldOfViewRange[x] = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * -FovDistance, Mathf.Sin(i * Mathf.Deg2Rad) * FovDistance);
                    x++;
                }
                Array.Reverse(FieldOfViewRange);
            }
        return FieldOfViewRange;
    }

    RaycastHit2D[] CreateFieldOfViewRaycast(Vector2[] FieldOfViewRange,LayerMask layer)
    {
        RaycastHit2D[] raycastHit2D = new RaycastHit2D[FieldOfViewRange.Length];

        for (int i = 0; i < FieldOfViewRange.Length; i++)
        {
            raycastHit2D[i] = Physics2D.Raycast(transform.position, FieldOfViewRange[i], FovDistance, layer);
            //Debug.DrawRay(transform.position,RayEndPossition[i],Color.red);
        }

        return raycastHit2D;
    }

    void EnemyEye(RaycastHit2D[] EnemyFieldOfView)
    {
        for(int i = 0; i < EnemyFieldOfView.Length; i++)
        if (EnemyFieldOfView[i])
            if (EnemyFieldOfView[i].collider.GetComponent<ProgressManager>().PlayerIsVisible)
            {

                EnemyControllerScript.EnemyCanSeeThePlayer = true;
                EnemyControllerScript.EnemyIsAwareOfThePlayer = true;
               EnemyControllerScript.GetComponent<EnemyPatrol>().StelthMission(EnemyFieldOfView[i]);
            }
            else
            {
                EnemyControllerScript.EnemyCanSeeThePlayer = false;
            }
    }
    Vector3[] FieldOfViewMeshVerticesCalculator(Vector2[] FieldOfViewRange, RaycastHit2D[] FieldOfViewRaycast = null)
    {
        Vector3[] Vertices = new Vector3[NumberOfRays * 2 + 2];
        Vertices[0] = Vector3.zero;
        for (int i = 0; i < FieldOfViewRaycast.Length; i++)
        {
            if (FieldOfViewRaycast[i])
            {
                Vertices[i + 1] = FieldOfViewRaycast[i].point - new Vector2(transform.position.x, transform.position.y);
            }
            else
            {
                Vertices[i + 1] = FieldOfViewRange[i];
            }
        }

        return Vertices;
    }

    Vector2[] FieldOfViewMeshUvCalculator(Vector2[] FieldOfViewRange, RaycastHit2D[] FieldOfViewRaycast = null)
    {
        Vector2[] UV = new Vector2[NumberOfRays * 2 + 2];
        UV[0] = Vector2.zero;
        for (int i = 0; i < FieldOfViewRaycast.Length; i++)
        {
            if (FieldOfViewRaycast[i])
            {
                UV[i + 1] = (FieldOfViewRaycast[i].point - new Vector2(transform.position.x, transform.position.y)) / new Vector2(FovDistance, Mathf.Sin(FovAngle * Mathf.Deg2Rad) * FovDistance);
            }
            else
            {
                UV[i + 1] = FieldOfViewRange[i] / new Vector2(FovDistance, Mathf.Sin(FovAngle * Mathf.Deg2Rad) * FovDistance);
            }
        }
        return UV;
    }

    int[] FieldOfViewMeshTrianglesCalculator(Vector3[] Vertices)
    {
        int[] Triangles = new int[(Vertices.Length - 2) * 3];
            Triangles[0] = 0;
            Triangles[1] = 1;
            Triangles[2] = 2;
            for (int i = 3; i < Triangles.Length; i++)
            {
                if (Triangles[i - 3] != 0)
                {
                    Triangles[i] = Triangles[i - 3] + 1;
                }
                else
                {
                    Triangles[i] = 0;
                }
            }
        return Triangles;
    }

    void CreateMesh(Vector3[] Vertices, int[] Triangles, Vector2[] UV=null)
    {
        Mesh FovMesh = new Mesh();
        FovMesh.vertices = Vertices;
        FovMesh.uv = UV;
        FovMesh.triangles = Triangles;
        GetComponent<MeshFilter>().mesh = FovMesh;
    }
}