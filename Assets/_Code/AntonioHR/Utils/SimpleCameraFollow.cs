using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace AntonioHR.Utils
{

    [RequireComponent(typeof(Camera))]
    public class SimpleCameraFollow : MonoBehaviour
    {
        private Camera cam;

        public List<Transform> objs;

        //public Vector3 centerReference;

        public Vector3 offset;
        public float minSize = 5;
        public Vector2 border;

        private void Start()
        {
            cam = GetComponent<Camera>();
        }
        private void Update()
        {
            var avg = objs.Aggregate(Vector3.zero, (sum, x) => sum + x.position) / objs.Count;
            transform.position = avg + offset;

            var maxDelta = new Vector2(
                objs.Max(obj => Mathf.Abs(obj.transform.position.x - transform.position.x)),
                objs.Max(obj => Mathf.Abs(obj.transform.position.y - transform.position.y)));
            
            maxDelta += border;

            cam.orthographicSize = Mathf.Max(maxDelta.x/cam.aspect, maxDelta.y, minSize);
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position - offset, .2f);
        }
    }
}