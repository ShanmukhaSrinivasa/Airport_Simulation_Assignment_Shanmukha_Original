# ✈️ Airport Simulation 3D

**Objective:** Create a near-realistic 3D simulation focused on world creation, smooth movement, and UI interaction.
**Scenario Chosen:** Option 2 - Airport Simulation

---

## 🎮 Controls & Interaction

* **W/A/S/D**: Pan the camera freely around the airport and city.
* **Right-Click + Mouse**: Rotate the camera view.
* **Mouse Scroll**: Zoom in and out.
* **Left-Click (On Aircraft)**:
* Selects the aircraft and displays its **ID, Status, and Speed** in the UI.
* Activates a **Visual Highlight (Cylinder)** at the base of the plane.
* **Auto-Follow**: The camera will automatically transition to follow the selected aircraft in flight.


* **UI Controls**:
* **Start/Stop Simulation**: A master toggle to pause or resume the entire airport environment.
* **Live Board**: A real-time Arrival/Departure board showing the status of all active flights.



---

## 🚀 Key Features & Implementation

### 1. Realistic Movement & State Machine

The aircraft utilize a robust waypoint-based system. Each plane transitions through four logical states based on its progress along the path:

* **Parking**: Stationary at the gate with a countdown timer before departure.
* **Taxiing**: Low-speed movement on taxiways.
* **Takeoff**: Rapid acceleration on the runway followed by a climb into the sky.
* **Landing**: Approach from the city, touchdown, and deceleration.
* **Smoothness**: All speed changes use `Mathf.MoveTowards` for natural acceleration, and rotations use `Quaternion.Slerp` for smooth turns.

### 2. Animations & Visuals

* **Propellers**: Real-time rotation synced to the simulation speed.
* **Landing Gear**: Fully functional logic that retracts the gear after takeoff (height > 5 units) and extends it during the landing approach.
* **Navigation Lights**: Red (Left) and Green (Right) wingtip point lights that activate automatically during flight phases.

### 3. Dynamic UI System

* **Selection Manager**: Uses Raycasting to detect aircraft on a specific physics layer.
* **Live Airport Board**: A centralized UI panel that loops through all aircraft data to provide a "Terminal View" of the simulation.
* **Color-Coded Status**: Uses Rich Text (Hex codes) to highlight flight statuses (e.g., Green for Takeoff, Red for Parking).

---

## 🛠️ Technical Setup

* **Engine**: Unity 3D
* **Language**: C#
* **UI System**: TextMeshPro (for high-fidelity text and rich-color support).
* **Camera**: Dual-mode (Free Look and Smooth Follow) using `unscaledDeltaTime` to ensure the user can still navigate while the simulation is paused.

---

## 📂 Project Structure

* `AircraftController.cs`: Handles waypoint navigation, speed, and flight states.
* `SelectionManager.cs`: Manages mouse interaction, highlighting, and UI updates.
* `SimulationManager.cs`: Controls the master `Time.timeScale`.
* `FreeCameraController.cs`: Handles the complex Free-Look and Smooth-Follow logic.
* `AirportBoard.cs`: Generates the live terminal data list.

---

📂 Movement Logic & State Machine
The simulation uses a State-Driven Waypoint System to ensure realistic behavior:

Waypoint Navigation: Aircraft move between Transform nodes using Vector3.MoveTowards. This prevents "teleporting" and ensures the plane follows the taxiway and flight paths accurately.

Rotation: Turning is handled via Quaternion.Slerp directed at the next waypoint. A reverseUntilIndex is used to allow planes to push back from the gate realistically.

State Transitions: The AircraftController monitors the currentWaypointIndex. Based on user-defined markers (e.g., takeoffStartIndex), the plane automatically switches its behavior:

Taxiing: Fixed speed, landing gear down.

Takeoff: Speed increases using Mathf.MoveTowards, and landing gear retracts once the altitude is above 5 units.

Landing: Speed is moderated for approach, and landing gear is deployed.

Parking: When reaching the gate (or a mid-path waypoint), the plane halts, and a parkTimer begins before the next loop.
