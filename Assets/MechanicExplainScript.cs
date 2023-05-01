using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MechanicExplainScript : MonoBehaviour
{
    InputSystem inputActions;
    [SerializeField] TextMeshProUGUI prompt;
    [SerializeField] private GameObject obstacleController;
    private void Start()
    {
        inputActions = new InputSystem();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package")
        {
            Time.timeScale = 0;
            inputActions.Air.Enable();
            inputActions.Air.Parry.performed += EndTimeStop;
            MusicScript.current.SetMusicLevel(0.2f);
            prompt.enabled = true;
        }
    }

    void EndTimeStop(InputAction.CallbackContext ctx)
    {
        Time.timeScale = 1;
        inputActions.Air.Parry.performed -= EndTimeStop;
        MusicScript.current.SetMusicLevel(1f);
        prompt.enabled = false;
        obstacleController.SetActive(true);
        //ObstacleControllerScript.instance.StartObstacle();
    }
}
