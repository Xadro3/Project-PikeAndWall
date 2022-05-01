using UnityEngine;

public class UnitSelector : MonoBehaviour
{

    SelectedUnitsDictionary selectedUnitsDictionary;
    RaycastHit raycastHit;
    RaycastHit[] raycastHits;

    bool isDragging;

    Vector3 mouseDownPosition;
    Vector3 mouseDragPosition;

    Vector2[] corners;
    Vector3[] vertices;
    Vector3[] vectors;

    Mesh selectionMesh;
    MeshCollider selectionBox;

    // Start is called before the first frame update
    void Start()
    {

        selectedUnitsDictionary = GetComponent<SelectedUnitsDictionary>();
        isDragging = false;


    }

    // Update is called once per frame
    void Update()
    {
        MouseInputHandler();
    }

    void MouseInputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if ((mouseDownPosition - Input.mousePosition).magnitude > 20)
            {
                isDragging = true;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging == false)
            {
                Ray singleSelectionRay = Camera.main.ScreenPointToRay(mouseDownPosition);
                raycastHits = Physics.RaycastAll(singleSelectionRay, 50000f);

                foreach (RaycastHit raycast in raycastHits)
                {
                    // if ((raycast.collider.gameObject.tag == "Unit") && (Input.GetKey(KeyCode.LeftControl)))
                    // {
                    //     Debug.Log("I got here at least ");
                    //     selectedUnitsDictionary.AddSelectedUnits(raycast.collider.gameObject);
                    //     break;
                    // }


                    if (raycast.collider.gameObject.tag == "Unit")
                    {


                        selectedUnitsDictionary.RemoveAllUnitsFromSelection();
                        selectedUnitsDictionary.AddSelectedUnits(raycast.collider.gameObject);
                        break;
                    }
                    else
                    {
                        selectedUnitsDictionary.RemoveAllUnitsFromSelection();
                    }


                }



            }
            else
            {
                vertices = new Vector3[4];
                vectors = new Vector3[4];

                int i = 0;

                mouseDragPosition = Input.mousePosition;
                corners = getBoundingBox(mouseDownPosition, mouseDragPosition);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out raycastHit, 50000f, (1 << 3)))
                    {
                        vertices[i] = new Vector3(raycastHit.point.x, 0, raycastHit.point.z);
                        vectors[i] = ray.origin - raycastHit.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), raycastHit.point, Color.cyan, 1);
                    }
                    i++;
                }

                selectionMesh = generateSelectionMesh(vertices, vectors);
                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selectedUnitsDictionary.RemoveAllUnitsFromSelection();
                }
                Destroy(selectionBox, 0.02f);
            }
            isDragging = false;
        }

    }

    void OnGUI()
    {
        if (isDragging == true)
        {
            var rectangle = DrawSelectionRectangle.GetScreenRect(mouseDownPosition, Input.mousePosition);
            DrawSelectionRectangle.DrawScreenRect(rectangle, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            DrawSelectionRectangle.DrawScreenRectBorder(rectangle, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        Vector2 newP1;
        Vector2 newP2;
        Vector2 newP3;
        Vector2 newP4;

        if (p1.x < p2.x) //if p1 is to the left of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = p1;
                newP2 = new Vector2(p2.x, p1.y);
                newP3 = new Vector2(p1.x, p2.y);
                newP4 = p2;
            }
            else //if p1 is below p2
            {
                newP1 = new Vector2(p1.x, p2.y);
                newP2 = p2;
                newP3 = p1;
                newP4 = new Vector2(p2.x, p1.y);
            }
        }
        else //if p1 is to the right of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = new Vector2(p2.x, p1.y);
                newP2 = p1;
                newP3 = p2;
                newP4 = new Vector2(p1.x, p2.y);
            }
            else //if p1 is below p2
            {
                newP1 = p2;
                newP2 = new Vector2(p1.x, p2.y);
                newP3 = new Vector2(p2.x, p1.y);
                newP4 = p1;
            }

        }

        Vector2[] corners = { newP1, newP2, newP3, newP4 };
        return corners;

    }

    //generate a mesh from the 4 bottom points
    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Objects hit: " + other.tag);

        if (other.tag == "Unit")
        {

            selectedUnitsDictionary.AddSelectedUnits(other.gameObject);
        }


    }
}
