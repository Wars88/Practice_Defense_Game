using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] Waypoints { get; private set; }

    private void Awake()
    {
        Waypoints = gameObject.GetComponentsInChildren<Transform>();
    }

}
