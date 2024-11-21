using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float sensitivityX = 8f;
    [SerializeField] private float sensitivityY = 8f;
    [SerializeField] private GameObject Crosshair;
    private bool lockCursor = true;
    private float mouseX;
    private float mouseY;
    private bool isPaused = false;

    private void Start()
    {
        SetCursorState();
        GameEvents.OnGamePaused += Pause;

        GameEvents.OnGameResumed += Resume;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Crosshair.SetActive(true);
        //playerCamera.localRotation = Quaternion.Euler(0f, 0f, 0f);
        StartCoroutine(ShowCrosshair());
    }

    private void OnDestroy()
    {
        GameEvents.OnGamePaused -= Pause;
        GameEvents.OnGameResumed -= Resume;
    }

    private void SetCursorState()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameScene" || currentScene == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReceiveInput(Vector2 _mouseInput)
    {
        mouseX = _mouseInput.x * sensitivityX;
        mouseY = _mouseInput.y * sensitivityY;
    }

    private void Update()
    {
        if (isPaused | lockCursor)
            return;

        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        //transform.Rotate(Vector3.right * mouseY * Time.deltaTime);
        playerCamera.Rotate(Vector3.left * mouseY * Time.deltaTime);
        //playerCamera.Rotate(Vector3.up * mouseX * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
    }

    private void Pause()
    {
        isPaused = true;
    }

    private void Resume()
    {
        isPaused = false;
    }

    private IEnumerator ShowCrosshair()
    {
        lockCursor = true;
        playerCamera.localRotation = Quaternion.Euler(0f, 0f, 0f);
        mouseY = 0;
        Crosshair.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        lockCursor = false;
        Crosshair.SetActive(true);
    }
}