using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public struct ParallaxLayer
    {
        public Transform layerTransform;
        public float parallaxFactor;
        public Vector2 offset;
    }

    public ParallaxLayer[] layers;

    public Vector3 pivotPoint;

    private void Start()
    {
        pivotPoint = transform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = transform.position - pivotPoint;

        foreach (ParallaxLayer layer in layers)
        {
            float parallaxFactor = layer.parallaxFactor;
            Vector2 offset = layer.offset;

            Vector3 layerDeltaMovement = deltaMovement * parallaxFactor;

            Vector3 newPosition = pivotPoint + layerDeltaMovement + (Vector3)offset;

            layer.layerTransform.position = newPosition;
        }

    }
}
