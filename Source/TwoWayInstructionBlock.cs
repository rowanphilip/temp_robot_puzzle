using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

enum RobotInstruction {
    None,
    MoveForward1,
    MoveForward10,
    TurnRight,
    TurnLeft
}

public class TwoWayInstructionBlock : MonoBehaviour
{
    public RobotControl robot;
    
    public static Vector3 connectionOffset;

    public RobotInstruction instruction;

    private bool childBlockSet = false;
    private TwoWayInstructionBlock childBlock;
    private bool beingDragged = false;
    private Vector3 clickOffset;
    private Vector3 screenPoint;
    private bool inControl = false;
    private bool inCollision = false;
    private TwoWayInstructionBlock collidedBlock;

    void OnMouseDown()
    {
        float canvasDistance = 10.0f;
        Vector3 clickLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, canvasDistance));
        this.clickOffset = gameObject.transform.position - clickLocation;
    }

    void OnMouseDrag()
    {
        float canvasDistance = 10.0f;
        Vector3 clickLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, canvasDistance));
        transform.position = clickLocation - this.clickOffset;
    }

    void OnMouseUp()
    {
        if(this.inCollision)
        {
            // Add self as child object to colliding object
            collidedBlock.SetChildBlock(this);
            // Change own position to position of colliding object connection
            this.transform.position = collidedBlock.GetChildConnectionPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        this.inCollision = true;
        this.collidedBlock = col.gameObject.GetComponent<TwoWayInstructionBlock>;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        this.inCollision = false;
    }

    Vector3 GetChildConnectionPosition()
    {
        if (childBlockSet)
        {
            return this.childBlock.GetChildConnectionPosition();
        }
        else
        {
            return this.transform.position + connectionOffset;
        }
    }

    void SetChildBlock(TwoWayInstructionBlock childBlock)
    {
        if (this.childBlockSet)
        {
            this.childBlock.SetChildBlock(childBlock);
        }
        else
        {
            this.childBlock = childBlock;
        }
    }

    void GiveControl()
    {
        this.inControl = true;
        this.robot.SendCommand(this, this.instruction);
    }

    void PassControl()
    {
        childBlock.GiveControl();
        this.inControl = false;
    }

}