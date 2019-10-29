using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{
    public LayerMask Agent_mask;
    public LayerMask Raycast_mask;
    public Camera camera;
    private float radius = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        radius = camera.farClipPlane / 2;
    }

    // Update is called once per frame
    void Update()
    {
        radius = camera.farClipPlane/2;

        Collider[] Colliders = Physics.OverlapSphere(transform.position + transform.forward*(radius + camera.nearClipPlane), radius, Agent_mask);

        for (int i = 0; i < Colliders.Length; ++i)
        {
            GameObject GO = Colliders[i].gameObject;

            // --- Do not evaluate ourselves ---
            if (GO == gameObject)
                continue;

            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), GO.GetComponent<BoxCollider>().bounds))
            {
                RaycastHit ray_hit;

                if (Physics.Raycast(transform.position + transform.forward*camera.nearClipPlane, GO.transform.position - transform.position + transform.forward * camera.nearClipPlane, out ray_hit, camera.farClipPlane, Raycast_mask))
                {
                    if(ray_hit.collider.tag == "Visual Emitter")
                    Debug.Log("Tank on sight");
                }

            }

        }
    }

    private void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * (radius + camera.nearClipPlane), radius);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position + transform.forward * radius, radius);
    }

}
