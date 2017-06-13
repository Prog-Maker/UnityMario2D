using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    public bool fallAnimationNeed;
    public bool shootAnimationInAirNeed;
    public bool blockAnimationNeed;

    [Tooltip("Нужно ли персонажу изменять состяние (например большой/маленький)")]
    public bool changeStateNeed;

    private InputState inputState;
    private Walk walkBehavior;
    private Animator animator;
    private CollisionState collisionState;
    private Duck duckBehavior;
    private Block blockBehavoir;
    private FireProjectile fireBehavior;
    private ChangeState changeStateBevavior;

    void Awake ()
    {
        inputState = GetComponent<InputState> ();
        walkBehavior = GetComponent<Walk> ();
        animator = GetComponent<Animator> ();
        collisionState = GetComponent<CollisionState> ();
        duckBehavior = GetComponent<Duck> ();
        if (blockAnimationNeed) blockBehavoir = GetComponent<Block> ();
        if (shootAnimationInAirNeed) fireBehavior = GetComponent<FireProjectile> ();
        if (changeStateNeed) changeStateBevavior = GetComponent<ChangeState> ();
    }


    void Update ()
    {
        if (collisionState.standing && !changeStateBevavior.state)
        {
            ChangeAnimationState (0); // анимация стоя
        }

        if (inputState.absVelX > 0 && collisionState.standing)
        {
            ChangeAnimationState (1); // анимация бега (ходьбы)
        }

        if (inputState.VelY > 0 && !collisionState.standing)
        {
            ChangeAnimationState (2); // анимация прыжка
        }

        if (fireBehavior)
        {
            if (inputState.VelY > 0.0f && !collisionState.standing && fireBehavior.shooting && shootAnimationInAirNeed)
            {
                ChangeAnimationState (5); // анимация стрельбы в прыжке
            }
        }

        if (inputState.VelY < 0.0f && !collisionState.standing && fallAnimationNeed)
        {
            ChangeAnimationState (7); // анимация падения
        }

        if (fireBehavior)
        {
            if (inputState.VelY < 0.0f && !collisionState.standing && fallAnimationNeed && fireBehavior.shooting && shootAnimationInAirNeed)
            {
                ChangeAnimationState (5); // анимация стрельбы в падении
            }
        }

        animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;

        if (duckBehavior.ducking && collisionState.standing)
        {
            ChangeAnimationState (3); // анимация приседания (лежания)
        }

        if (!collisionState.standing && collisionState.onWall)
        {
            ChangeAnimationState (4); // анимация связааная когда персонаж на стене
        }

        if (fireBehavior)
        {
            if (fireBehavior.shooting && shootAnimationInAirNeed)
            {
                ChangeAnimationState (5); // анимация стрельбы
            }
        }

        if (blockBehavoir)
        {
            if (blockBehavoir.blocking && blockAnimationNeed)
            {
                ChangeAnimationState (6); // анимация блока
            }
        }

        if (changeStateBevavior)
        {
            if (changeStateBevavior.state)
            {
                ChangeAnimationState (9); // анимация изменения состояния персонажа
            }
        }

    }

    void ChangeAnimationState (int value)
    {
        animator.SetInteger ("AnimState", value);
    }
}
