using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI infoDisplay;
    public LayerMask selectableLayer;
    private GameObject aircraftHighlighter;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, selectableLayer))
            {
                if (aircraftHighlighter != null)
                {
                    SetHighLight(aircraftHighlighter, false);
                }

                aircraftHighlighter = hit.collider.gameObject;
                SetHighLight(aircraftHighlighter, true);

                AircraftController aircraft = hit.collider.GetComponentInParent<AircraftController>();
                if (aircraft != null)
                {
                    // Access the aircraft's properties from script
                    UpdateUIText(aircraft);
                }
            }
        }
    }

    private void SetHighLight(GameObject obj, bool active)
    {
        Transform ring = obj.transform.Find("Aircraft_Highlighter");
        if (ring != null)
        {
            ring.gameObject.SetActive(active);
        }
    }

    private void UpdateUIText(AircraftController aircraft)
    {
        string statusColor = GetStatusColor(aircraft.currentStatus);
        infoDisplay.text = $"<b>ID:</b> {aircraft.aircraftID}\n" +
                           $"<b>Status:</b> <color={statusColor}>{aircraft.currentStatus}</color>\n" +
                           $"<b>Taxi Speed:</b> {aircraft.taxiSpeed} units/s";
    }

    private string GetStatusColor(AircraftController.FlightState state)
    {
        switch (state)
        {
            case AircraftController.FlightState.Takeoff:
                return "#00FF00"; // Bright Green
            case AircraftController.FlightState.Landing:
                return "#FFFF00"; // Bright Yellow
            case AircraftController.FlightState.Parking:
                return "#FF0000"; // Bright Red
            default:
                return "#FFFFFF"; // White
        }
    }
}
