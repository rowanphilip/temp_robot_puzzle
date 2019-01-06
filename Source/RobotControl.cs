using UnityEngine;

// Script for robot movement behaviour
public class RobotControl : MonoBehaviour
{
    // Action state can take the values:
    // 0: No action
    // 1: Forward 1 step
    // 2: Forward 10 steps
    // 3: Turning left
    // 4: Turning right
    private RobotInstruction instruction;
    private TwoWayInstructionBlock masterBlock;
    private Vector3 targetPosition;
    private Quaternion targetDirection;
    public float stepSize;
    public float moveSpeed;
    public float turnSpeed;

    void Update()
    {
        switch(this.instruction)
        {
            case None:
                break;
            case MoveForward1:
                MoveForward();
                break;
            case MoveForward10:
                MoveForward();
                break;
            case TurnRight:
                Turn();
                break;
            case TurnLeft:
                Turn();
                break;
        }
    }

    void MoveForward()
    {
        if (transform.position == this.targetPosition)
        {
            this.instruction = None;
            masterBlock.PassControl();
        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, this.targetPosition, step);
        }
    }

    void Turn()
    {
        // Is this how to compare two quaternions?
        if (transform.rotation == targetDirection) {
            this.instruction = None;
            masterBlock.PassControl();
        }
        else
        {
            float step = turnSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, this.targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    void SendCommand(TwoWayInstructionBlock masterBlock, int instruction)
    {
        this.masterBlock = masterBlock;
        this.instruction = instruction;

        switch (instruction)
        {
            case None:
                break;
            case MoveForward1:
                this.targetPosition = transform.position + transform.forward * stepSize;
                break;
            case MoveForward10:
                this.targetPosition = transform.position + transform.forward * 10 * stepSize;
                break;
            case TurnRight:
                this.targetDirection = transform.rotation * Quaternion.Euler(0, 90.0f, 0);
                break;
            case TurnLeft:
                this.targetDirection = transform.rotation * Quaternion.Euler(0, -90.0f, 0);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        this.instruction = None;
    }
}