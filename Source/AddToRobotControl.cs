public enum RobotState {
    eOk,
    eFail,
    eSuccess
}

public enum BlockState {
    eNotOver,
    eOver,
    eHeld
}

private RobotState state = eOk;
private Vector3D start_position;
private Vector3D start_rotation;
private BlockState block_state = eNotOVer;
private MovableBlock block;


Start() {
    this.start_position = Transform.position();
    this.start_rotation = Transform.rotation();
}

public ResetRobot() {
    Transform.position = start_position;
    Transform.rotation = start_rotation;
    this.state = eOk;
}

OnCollisionEnter2D() {
    this.state = eNotOk; // ADD CHECKS WHEN MOVING THE ROBOT FOR THE ROBOT STATE
    // Instantiate popup
}

OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.name == "Goal" && this.block_state = held) {
        this.state = eSuccess;
        // Instantiate popup
    }
    else if (col.gameObject.name == "Block") {
        if (this.block_state != eHeld) {
            this.block_state = eOver;
        }
    }
}

OnTriggerExit2D(Collider2D col) {
    if (col.gameObject.name == "Block") {
        if (this.block_state != eHeld) {
            this.block_state = eNotOver;
        }
    }
}

private Pickup() {
    if (this.block_state == eOver) {
        this.block_state = eHeld;
    }
}

// ADD BLOCK MOVEMENT TO ROBOT MOVEMENT FUNCTIONS