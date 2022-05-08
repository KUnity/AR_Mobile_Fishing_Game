using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
  using UnityEditor;
#endif

using System.IO;
public class CreateWaterMesh : MonoBehaviour
{

    GameObject plane;

    public int widthSegments = 1;       //Number of pieces for dividing plane vertically
    public int heightSegments = 1;      //Number of pieces for dividing plane horizontally
    public static string assetSaveLocation = "Assets/Plane Meshes/";
    public Material material;    
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //         if (!Directory.Exists(assetSaveLocation))
    //     {
    //         Directory.CreateDirectory(assetSaveLocation);
    //     }
    //     material = Resources.Load("Materials/LowPolyWaterMaterial", typeof(Material)) as Material;
    //     Debug.Log(material);
    // }

    public void CreatePlane(float planeWidth,float planeHeight, Vector3 center ){

        //Create an empty gamobject
        plane = new GameObject();   
        plane.name = "WaterPlane";
        
        //Create Mesh Filter and Mesh Renderer components
        MeshFilter meshFilter = plane.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer meshRenderer = plane.AddComponent((typeof(MeshRenderer))) as MeshRenderer;
        meshRenderer.sharedMaterial = material;
    
        //Generate a name for the mesh that will be created
        string planeMeshAssetName = plane.name + widthSegments + "x" + heightSegments
                                    + "W" + planeWidth + "H" + planeHeight + ".asset";
            
        Mesh m = new Mesh();
        m.name = plane.name;

        int hCount2 = widthSegments + 1;
        int vCount2 = heightSegments + 1;
        int numTriangles = widthSegments * heightSegments * 6;
        int numVertices = hCount2 * vCount2;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uvs = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];
        Vector4[] tangents = new Vector4[numVertices];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        Vector2 anchorOffset = Vector2.zero;

        int index = 0;
        float uvFactorX = 1.0f / widthSegments;
        float uvFactorY = 1.0f / heightSegments;
        float scaleX = planeWidth / widthSegments;
        float scaleY = planeHeight / heightSegments;

        //Generate the vertices
        for (float y = 0.0f; y < vCount2; y++)
        {
            for (float x = 0.0f; x < hCount2; x++)
            {
                vertices[index] = new Vector3(x * scaleX - planeWidth / 2f - anchorOffset.x, 0.0f, y * scaleY - planeHeight / 2f - anchorOffset.y);

                tangents[index] = tangent;
                uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
            }
        }

        //Reset the index and generate triangles
        index = 0;
        for (int y = 0; y < heightSegments; y++)
        {
            for (int x = 0; x < widthSegments; x++)
            {
                triangles[index] = (y * hCount2) + x;
                triangles[index + 1] = ((y + 1) * hCount2) + x;
                triangles[index + 2] = (y * hCount2) + x + 1;

                triangles[index + 3] = ((y + 1) * hCount2) + x;
                triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                triangles[index + 5] = (y * hCount2) + x + 1;
                index += 6;
            }
        }

        //Update the mesh properties (vertices, UVs, triangles, normals etc.)
        m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;
        m.tangents = tangents;
        m.RecalculateNormals();
 
        //Update mesh
        meshFilter.sharedMesh = m;
        // meshFilter.material = material;
        m.RecalculateBounds();

        //Add LowPolyWater as component
        plane.AddComponent<LowPolyWater.LowPolyWater>();
        plane.transform.position =center;
    }

    public void RemovePlane(){
        Destroy(plane);
    }



}