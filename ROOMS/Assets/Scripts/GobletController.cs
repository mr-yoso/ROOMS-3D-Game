using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobletController : MonoBehaviour, IInteractable
{
    public float rotationSpeed = 10f;
    private Rigidbody rb;
    public Vector3 initialPosition;
    public float hoverSpeed = 1f; 
    public float hoverHeight = 0.5f; 
    public static Level2GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Calculate the new position using a sine wave
        float newY = initialPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Move the object using Rigidbody.MovePosition to respect the physics system
        Vector3 newPosition = new Vector3(initialPosition.x, newY, initialPosition.z);
        rb.MovePosition(newPosition);
    }

    public void Interact()
    {
        Debug.Log("alloo");
        Level2GameManager.Instance.PickUpObj();
        Destroy(gameObject);
    }
}
