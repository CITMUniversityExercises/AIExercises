using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDetection : MonoBehaviour
{
    public LayerMask Agent_mask;
    public LayerMask Raycast_mask;
    public Camera camera;
    private float radius = 0;
    List<GameObject> detected;

    public class PerceptionEvent
    {
        public enum senses { VISION, SOUND };
        public enum types { NEW, LOST };
        public GameObject go;
        public senses sense;
        public types type;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        radius = camera.farClipPlane / 2;
        detected = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        radius = camera.farClipPlane/2;

        Collider[] Colliders = Physics.OverlapSphere(transform.position + transform.forward*(radius + camera.nearClipPlane), radius, Agent_mask);
        List<GameObject> new_detected = new List<GameObject>();

        for (int i = 0; i < Colliders.Length; ++i)
        {
            GameObject GO = Colliders[i].gameObject;
            new_detected.Add(GO);

            // --- Do not evaluate ourselves ---
            if (GO == gameObject)
                continue;

            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), GO.GetComponent<BoxCollider>().bounds))
            {
                RaycastHit ray_hit;

                if (Physics.Raycast(transform.position + transform.forward*camera.nearClipPlane, GO.transform.position - transform.position + transform.forward * camera.nearClipPlane, out ray_hit, camera.farClipPlane, Raycast_mask))
                {
                    //if(ray_hit.collider.tag == "Visual Emitter")
                    //Debug.Log("Tank on sight");

                    int index = detected.IndexOf(ray_hit.collider.gameObject);

                    PerceptionEvent @event = new PerceptionEvent();

                    @event.go = ray_hit.collider.gameObject;
                    @event.sense = PerceptionEvent.senses.VISION;

                    if (index == -1 && @event.go.tag == "Visual Emitter")
                    {
                        detected.Add(ray_hit.collider.gameObject);
                        @event.type = PerceptionEvent.types.NEW;
                        gameObject.SendMessage("HandleEvents", @event);
                    }

                }

            }

        }
        // --- Compare previous frame list with current list ---

        //if (new_detected.Count > 0)
        //{
        //    for (int i = 0; i < new_detected.Count; ++i)
        //    {
        //        int index = detected.IndexOf(new_detected[i]);

        //        PerceptionEvent @event = new PerceptionEvent();

        //        @event.go = new_detected[i];
        //        @event.sense = PerceptionEvent.senses.VISION;

        //        if (index == -1 && @event.go.tag == "Visual Emitter")
        //        {
        //            @event.type = PerceptionEvent.types.NEW;
        //            gameObject.SendMessage("HandleEvents", @event);
        //        }

        //    }

        //}

        if (detected.Count > 0)
        {
            for (int i = 0; i < detected.Count; ++i)
            {
                int index = new_detected.IndexOf(detected[i]);

                PerceptionEvent @event = new PerceptionEvent();

                @event.go = detected[i];
                @event.sense = PerceptionEvent.senses.VISION;

                if (index == -1 && @event.go.tag == "Visual Emitter")
                {
                    detected.Remove(detected[i]);
                    @event.type = PerceptionEvent.types.LOST;
                    gameObject.SendMessage("HandleEvents", @event);
                }

            }
        }


        //detected.AddRange(new_detected);
        //detected.Clear();
        new_detected.Clear();
    }

    private void HandleEvents (PerceptionEvent @event)
    {
        Debug.Log(@event.go.ToString());
        Debug.Log(@event.type.ToString());
        Debug.Log(@event.sense.ToString());
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
