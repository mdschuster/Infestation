using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public DoorTrigger connectedDoor;
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        connectedDoor.doorTriggered.AddListener(spawn);
    }

    public void spawn()
    {
        Instantiate(spawnObject, this.transform.position, Quaternion.identity);
        connectedDoor.doorTriggered.RemoveListener(spawn);
    }

}
