using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelivererScript : MonoBehaviour
{
    public enum State : byte
    {
        Standby,
        Delivering
    }

    public static DelivererScript current;

    [SerializeField]Sprite[] sprites;
    SpriteRenderer sprite;

    int spriteState = 0;
    float frameTime = 0;

    State state;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        sprite = GetComponent<SpriteRenderer>();
        state = State.Standby;
    }

    public void StateUpdate(State state)
    {
        if (this.state == state) { return; }
        frameTime = 0;
        this.state = state;
        if (state == State.Standby)
        {
            spriteState = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Standby:
                frameTime += Time.deltaTime;
                if (frameTime >= 0.5)
                {
                    spriteState = spriteState==1?0:1;
                    frameTime = 0;
                }
                sprite.sprite = sprites[spriteState];
                return;
            case State.Delivering:
                frameTime += Time.deltaTime;
                if (frameTime >= 0.1)
                {
                    var random = Random.Range(2,6);
                    spriteState = random >= spriteState ? random + 1 : random;
                    frameTime = 0;
                }
                sprite.sprite = sprites[spriteState];
                return;
        }
    }

}
