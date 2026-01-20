using UnityEngine;

public class AircraftController : MonoBehaviour
{

    public enum FlightState { Parking, Taxiing, Takeoff, Landing }

    [Header("Identity")]
    public string aircraftID;
    public FlightState currentStatus = FlightState.Parking;

    [Header("Movement")]
    public Transform[] waypoints;
    public float taxiSpeed = 2f;
    public float takeoffSpeed = 8f;
    public float rotationSpeed = 2f;

    [Header("Reverse Logic")]
    public int reverseUntilIndex = 5;

    [Header("Parking Settings")]
    public float parkTimeAtGate = 10f; // Stay at gate for 10 seconds
    private float parkTimer = 0f;
    private bool isParkingAtGate = false;

    [Header("Mid-Path Parking")]
    public int midPathParkIndex;

    private int currentWaypointIndex = 0;
    private float currentMoveSpeed;

    [Header("State Markers")]
    public int takeoffStartIndex;
    public int landingStartIndex;
    public int taxiBackStartIndex;

    [Header("Propellers")]
    private Transform propL;
    private Transform propR;

    [Header("Looping")]
    public bool shouldLoop = true;

    [Header("Navigation Lights")]
    public GameObject redLight; // Assign in Inspector (Left Wing)
    public GameObject greenLight; // Assign in Inspector (Right Wing)

    private void Start()
    {
        propL = transform.Find("PropellerL");
        propR = transform.Find("PropellerR");

        // Snap to start
        if (waypoints.Length > 0) transform.position = waypoints[0].position;

        isParkingAtGate = true;
        currentStatus = FlightState.Parking;
    }

    void Update()
    {
        if (propL) propL.Rotate(Vector3.forward * 1000f * Time.deltaTime);
        if (propR) propR.Rotate(Vector3.forward * 1000f * Time.deltaTime);

        if (waypoints.Length == 0)
        {
            return;
        }

        if (isParkingAtGate)
        {
            if(!shouldLoop)
            {
                currentStatus = FlightState.Parking;
                return;
            }

            parkTimer += Time.deltaTime;
            if (parkTimer >= parkTimeAtGate)
            {
                parkTimer = 0f;
                isParkingAtGate = false;
            }
            return;
        }

        UpdateStateAndSpeed();
        MoveTowardsTarget();
    }

    private void UpdateStateAndSpeed()
    {
        // 1. TAKEOFF / FLIGHT PHASE
        if (currentWaypointIndex > takeoffStartIndex && currentWaypointIndex < landingStartIndex)
        {
            currentStatus = FlightState.Takeoff;
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, takeoffSpeed, 5f * Time.deltaTime);

            if (transform.position.y > 5f) TogglelandingGear(false);

            ToggleNavLights(true);
        }
        // 2. LANDING PHASE (Descending and Touchdown)
        else if (currentWaypointIndex >= landingStartIndex && currentWaypointIndex < taxiBackStartIndex)
        {
            currentStatus = FlightState.Landing;
            // Moderate landing speed
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, taxiSpeed * 2f, 5f * Time.deltaTime);

            TogglelandingGear(true);

            ToggleNavLights(true);
        }
        // 3. TAXIING PHASE (On the ground only)
        else
        {
            currentStatus = FlightState.Taxiing;
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, taxiSpeed, 5f * Time.deltaTime);

            TogglelandingGear(true); // Keep gear down while taxiing

            ToggleNavLights(false);
        }

    }

    private void MoveTowardsTarget()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = waypoints.Length - 1;
        }

        Transform target = waypoints[currentWaypointIndex];

        // 1. Smoothly rotate to look at the next Waypoint
        Vector3 direction = target.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation;
            if (currentWaypointIndex < reverseUntilIndex)
            {
                targetRotation = Quaternion.LookRotation(-direction);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(direction);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        // 2. Move forward smoothly (No Teleporting)
        transform.position = Vector3.MoveTowards(transform.position, target.position, currentMoveSpeed  * Time.deltaTime);

        // 3. Check if we reached the point to move to the next one
        if (Vector3.Distance(target.position, transform.position) < 0.5f)
        {
            if (currentWaypointIndex == midPathParkIndex || currentWaypointIndex == waypoints.Length - 1)
            {
                isParkingAtGate = true;
                currentStatus = FlightState.Parking;

                // If we just reached the VERY LAST waypoint
                if (currentWaypointIndex == waypoints.Length - 1)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    currentWaypointIndex++;
                }
            }
            else
            {
                currentWaypointIndex++;
            }
        }
    }

    private void TogglelandingGear(bool show)
    {
        Transform gear = transform.Find("LandingGear");
        if (gear != null)
        {
            gear.gameObject.SetActive(show);
        }
    }

    private void ToggleNavLights(bool active)
    {
        if (redLight != null) redLight.SetActive(active);
        if (greenLight != null) greenLight.SetActive(active);
    }
}