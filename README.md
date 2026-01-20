✈️ Airport Simulation Assignment
This project is a functional airport simulation built in Unity, featuring dynamic aircraft state management and a waypoint-based navigation system.

🛠️ Chosen Option
I selected Option 2: Airport Simulation. The goal was to create a realistic environment where multiple aircraft manage their own cycles of taxiing, takeoff, and landing while allowing the user to observe the operations from various perspectives.

🎮 Controls
The simulation uses a dual-camera system to give the user full control over the view:

Free Camera:

WASD / Arrow Keys: Pan the camera around the airport.

Right-Click + Drag: Rotate the view.

Mouse Scroll: Zoom in and out.

Interaction & Follow Mode:

Left-Click: Select any aircraft to view its unique ID and current flight state.

Follow View: Once selected, the camera will automatically track that specific aircraft through its flight path.

Simulation Toggle: Use the UI button to start or stop the simulation logic.

⚙️ Movement Logic
The aircraft movement is handled by a custom state machine that transitions based on a waypoint system:

Waypoint Navigation: Each aircraft follows a pre-defined path of 68 waypoints using Vector3.MoveTowards for smooth translation.

State Transitions: The logic checks the currentWaypointIndex to trigger specific behaviors:

Taxiing: Ground movement at steady speeds.

Takeoff: Acceleration initiated at the runway start, with the aircraft pitch increasing as it gains speed.

Landing: Approach markers trigger the descent and landing gear deployment.

Parking: Automatic halt at designated gates with a timer for departure.

Realistic Rotation: All turns use Quaternion.Slerp to ensure the aircraft bank naturally toward their next target rather than snapping instantly.

Acceleration/Deceleration: Speed is never constant; I used Mathf.MoveTowards for all velocity changes to simulate the weight and inertia of a real plane.
