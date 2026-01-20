# ✈️ Airport Simulation Assignment

This project is a functional airport simulation built in **Unity**, featuring dynamic aircraft state management and a waypoint-based navigation system.

## 🛠️ Project Selection

I chose **Option 2: Airport Simulation**.
The project focuses on simulating a living airport environment where aircraft transition through various flight phases autonomously, while providing a free-look system for the user to observe the operations.

## 🎮 Controls

The simulation allows the user to navigate the environment and interact with the aircraft:

* **Free Camera Navigation**:
* **WASD / Arrow Keys**: Pan around the airport and city.
* **Right-Click + Drag**: Rotate the camera view.
* **Mouse Scroll**: Zoom in/out to get closer to the runways.


* **Interaction**:
* **Left-Click**: Select an aircraft to display its unique ID and current status (e.g., Taxiing, Taking Off).


* **Simulation Management**:
* **Start/Stop Button**: Toggle the simulation logic to pause or resume aircraft movement.



## ⚙️ Movement Logic

The aircraft movement is controlled by a custom state machine that transitions based on a waypoint system:

1. **Waypoint Navigation**: Aircraft follow a sequence of 68 markers using `Vector3.MoveTowards` to ensure smooth, non-teleporting translation.
2. **Phase Transitions**: The logic monitors the `currentWaypointIndex` to trigger specific behaviors:
* **Taxiing**: Standard ground movement at a controlled speed.
* **Takeoff**: Acceleration starts at the runway threshold, with the aircraft pitching up once it hits rotation speed.
* **Landing**: Approach markers trigger the descent, speed reduction, and gear deployment.
* **Parking**: Aircraft return to gates and halt using a `parkTimer` before the next cycle.


3. **Rotation & Banking**: I used `Quaternion.Slerp` for all heading changes, allowing the planes to bank naturally into turns rather than snapping instantly to the next target.
4. **Inertia**: To avoid "robotic" movement, `Mathf.MoveTowards` is used for all speed changes, simulating the time it takes for a heavy aircraft to accelerate or decelerate.

---
