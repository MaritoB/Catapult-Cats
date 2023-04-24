using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform[] LayersPositions;
    public float[] LayersVelocity;
    public float[] LayersXOffset;
    public float[] LayersYOffset;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < LayersPositions.Length; ++i)
        {
            LayersPositions[i].position = new Vector3(LayersXOffset[i] + cameraPosition.position.x * LayersVelocity[i], LayersPositions[i].position.y, LayersPositions[i].position.z);

        }

    }
}
