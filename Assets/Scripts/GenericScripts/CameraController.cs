using UnityEngine;

public class CameraController : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The object to follow.</summary>
    public GameObject target;


    /***** Private Fields *****/

    /// <summary>The offset to be maintained from the target.</summary>
    private Vector3 offset;


    /***** Unity Methods *****/

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}