using UnityEngine;
using TMPro;

public class SpellsActivatedTextScript : MonoBehaviour
{
    public GameObject cameraContainer;
    public Vector3 positionOffset = new Vector3(0f, 3f, 2f);

    void Update()
    {
        if (cameraContainer != null)
        {
            transform.SetParent(cameraContainer.transform);
            transform.localPosition = positionOffset;
        }
    }
}
