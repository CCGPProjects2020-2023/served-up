using UnityEngine;


public class PlayerLook : MonoBehaviour
{
    public Camera camera;
    public Transform orientation;

    private float xRotation, yRotation;

    public static float xSensitivity = 30f;

    public static float ySensitivity = 30f;

    void Start()
    {
        camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yRotation = 134.392f;
        UpdateSensitivity();
    }


    public void Look(Vector2 input)
    {
        float mouseX = input.x * Time.fixedDeltaTime * xSensitivity;
        float mouseY = input.y * Time.fixedDeltaTime * ySensitivity;

        /*xRotation -= (mouseY * Time.fixedDeltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.fixedDeltaTime) * xSensitivity);*/

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public static void UpdateSensitivity()
    {
        int sens = PlayerPrefs.GetInt("Sensitivity", 20);
        xSensitivity = sens;
        ySensitivity = sens;
    }
}
