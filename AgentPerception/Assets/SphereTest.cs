using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{
    public LayerMask mask;
    public float search_radius = 5.0f;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        // 1- Find other agents in the vicinity (use a layer for all agents)
        Collider[] Colliders = Physics.OverlapSphere(transform.position, search_radius, mask);

        for (int i = 0; i < Colliders.Length; ++i)
        {
            GameObject GO = Colliders[i].gameObject;

            // --- Do not evaluate ourselves ---
            if (GO == gameObject)
                continue;

            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), GO.GetComponent<BoxCollider>().bounds))
            {
                //RaycastHit ray_hit;



                //if (Physics.Raycast(transform.position + GeometryUtility.CalculateFrustumPlanes(camera)[0], destination, out ray_hit, maxDistance, mask))
                //{

                    Debug.Log("Tank on sight");
                //}
            }

        }
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, search_radius);
    }

}
