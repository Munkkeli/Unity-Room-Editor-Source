using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {
    public float speed = 1, zoom = 1;

    private float height = 10;

    void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && height > 5)
            height -= (zoom * Time.deltaTime) * height;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && height < 30)
            height += (zoom * Time.deltaTime) * height;
        height = Mathf.Clamp(height, 5, 30);

        Vector3 position = transform.position + ((new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * (speed + height)) * Time.deltaTime);
        position.y = height;
        transform.position = position;
    }
}
