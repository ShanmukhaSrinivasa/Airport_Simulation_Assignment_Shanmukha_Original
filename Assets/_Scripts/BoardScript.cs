using UnityEngine;
using TMPro;

public class AirportBoard : MonoBehaviour
{
    public AircraftController[] allPlanes; // Drag your 3 planes here
    public TextMeshProUGUI boardText; // The big list display

    void Update()
    {
        string table = "<b>AIRPORT LIVE DATA</b>\n\n";
        table += "FLIGHT ID      |      STATUS\n";
        table += "------------------------------\n";

        foreach (var plane in allPlanes)
        {
            // Use the status color logic we built earlier
            string sColor = GetColor(plane.currentStatus);
            table += $"{plane.aircraftID}         |      <color={sColor}>{plane.currentStatus}</color>\n";
        }

        boardText.text = table;
    }

    private string GetColor(AircraftController.FlightState state)
    {
        switch (state)
        {
            case AircraftController.FlightState.Takeoff: return "#00FF00";
            case AircraftController.FlightState.Landing: return "#FFFF00";
            case AircraftController.FlightState.Parking: return "#FF0000";
            default: return "#FFFFFF";
        }
    }
}
