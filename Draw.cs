using UnityEditor;
using UnityEngine;

    public class Draw : MonoBehaviour
    {
        public Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            if (!Input.GetMouseButton(0))
                return;

            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
                meshCollider == null)
                return;
            Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            tex.SetPixel((int) pixelUV.x, (int) pixelUV.y, Color.black);
            tex.Apply();
        }
 
        [ContextMenu("New")]
        public void NewPlane()
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            obj.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            var render = obj.GetComponent<MeshRenderer>();
            render.materials = new Material[]
            {
                new Material(Shader.Find("Draw"))
            };
            var t = new Texture2D(20, 20);
            t.wrapMode = TextureWrapMode.Clamp;
            t.anisoLevel = 0;
            t.filterMode = FilterMode.Point;
            render.sharedMaterial.mainTexture = t;
        }
    }