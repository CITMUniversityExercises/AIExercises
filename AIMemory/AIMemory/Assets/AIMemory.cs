using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// TODO 1: Create a simple class to contain one entry in the blackboard
// should at least contain the gameobject, position, timestamp and a bool
// to know if it is in the past memory
public class Memory
{
    public GameObject GO;
    public Vector3 position;
    public float timestamp = Time.time;
    public bool in_memory = false;
}

public class AIMemory : MonoBehaviour {


	public GameObject Cube;
	public Text Output;

	// TODO 2: Declare and allocate a dictionary with a string as a key and
	// your previous class as value
    Dictionary<string,Memory> mem;

	// TODO 3: Capture perception events and add an entry if the player is detected
	// if the player stop from being seen, the entry should be "in the past memory"
    public void AddToMem(PerceptionEvent Pevent)
    {
        Memory tmpmem = new Memory();

        mem.TryGetValue(Pevent.go.name, out tmpmem);

        if (tmpmem == null)
        {
            tmpmem = new Memory();

            tmpmem.GO = Pevent.go;
            tmpmem.position = Pevent.go.transform.position;
            tmpmem.timestamp = Time.time;
            mem.Add(Pevent.go.name, tmpmem);

        }
        else if (Pevent.type == PerceptionEvent.types.LOST)
        {
            mem[tmpmem.GO.name].position = Pevent.go.transform.position;
            mem[tmpmem.GO.name].in_memory = true;
            mem[tmpmem.GO.name].timestamp = Time.time;
        }

    }

	// Use this for initialization
	void Start () {
        mem = new Dictionary<string, Memory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 4: Add text output to the bottom-left panel with the information
        // of the elements in the Knowledge base
        string tmp;
  
        foreach (KeyValuePair<string, Memory> e in mem)
        {
            Cube.transform.position = e.Value.position;

            tmp = "< -- database display -- >";
            tmp.Insert(tmp.Length, "Name:");
            tmp.Insert(tmp.Length, e.Value.GO.name);
            tmp.Insert(tmp.Length, "Timestamp:");
            tmp.Insert(tmp.Length, e.Value.timestamp.ToString()); 
            tmp.Insert(tmp.Length, "Position:");
            tmp.Insert(tmp.Length, e.Value.position.ToString());
            tmp.Insert(tmp.Length, "InMemory:");
            tmp.Insert(tmp.Length, e.Value.in_memory.ToString());
        
            Output.text =  tmp;
        }

    }

}
