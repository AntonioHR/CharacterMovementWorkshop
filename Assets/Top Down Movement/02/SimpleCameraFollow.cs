using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class SimpleCameraFollow : MonoBehaviour
{
    private Camera cam;

    public List<Transform> objs;

    public Vector3 centerReference;

    private Vector3 offset;


    private void Start()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - centerReference;
    }
    private void Update()
    {
        var avg = objs.Aggregate(Vector3.zero,  (sum, x)=> sum + x.position)/objs.Count;
        transform.position = avg + offset;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(centerReference, .1f);
    }
}
