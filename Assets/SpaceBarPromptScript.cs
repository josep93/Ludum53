using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBarPromptScript : MonoBehaviour
{
    public static SpaceBarPromptScript current;

    [SerializeField] Sprite[] sprites;
    Image spriteRenderer;
    float timer = 0;
    int pressed = 0;
    bool flag = false;
    void Start()
    {
        current = this;
        spriteRenderer = GetComponent<Image>();
        spriteRenderer.enabled = false;
        Invoke("TurnOnPrompt",3);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1)
        {
            pressed ^= 1;
            spriteRenderer.sprite = sprites[pressed];
            timer = 0;
        }
    }

    public void TurnOffPrompt()
    {
        flag = true;
        spriteRenderer.enabled = false;
    }

    void TurnOnPrompt()
    {
        if (flag) return;
        spriteRenderer.enabled = true;
    }
}
