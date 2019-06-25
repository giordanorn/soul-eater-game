using UnityEngine;

public class CameraController : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The object to follow.</summary>
    private GameObject target;


    /***** Private Fields *****/

    /// <summary>The offset to be maintained from the target.</summary>
    private Vector3 offset;


    /***** Unity Methods *****/

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}