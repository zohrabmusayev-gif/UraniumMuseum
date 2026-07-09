using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlaceExhibitHere : MonoBehaviour
{
    [SerializeField] private ObjectSpawner spawner;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private float placeDistance = 1f;

    // Wire this to a hand menu button's On Click ().
    public void PlaceHere()
    {
        if (spawner == null)
        {
            Debug.LogWarning("PlaceExhibitHere: no ObjectSpawner assigned.", this);
            return;
        }

        Transform aim = aimTransform != null ? aimTransform : Camera.main.transform;
        Vector3 point = aim.position + aim.forward * placeDistance;
        spawner.SpawnObject(point, Vector3.up);
    }
}
