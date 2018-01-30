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
    public  float minSize= 5;

    private void Start()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - centerReference;
    }
    private void Update()
    {
        var avg = objs.Aggregate(Vector3.zero,  (sum, x)=> sum + x.position)/objs.Count;
        transform.position = avg + offset;


        var min = new Vector2(
            objs.Min(x => x.transform.position.x), 
            objs.Min(x => x.transform.position.y));

        var max = new Vector2(
            objs.Max(x => x.transform.position.x),
            objs.Max(x => x.transform.position.y));

        var aspect = new Vector2(cam.aspect, 1);
        var delta = (max - min);
        delta.Scale(aspect);
        

        if(delta.magnitude > 1 )
            cam.orthographicSize = Mathf.Max(delta.x, delta.y, minSize);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(centerReference, .1f);
    }
}
