using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class WeatherDisplay : MonoBehaviour
{
    public string serverURL = "https://net-subprime-pardon.ngrok-free.dev/";
    public TMP_Text weatherText;

    void Start()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        UnityWebRequest request = UnityWebRequest.Get(serverURL);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            WeatherData data = JsonUtility.FromJson<WeatherData>(json);

            weatherText.text =
                "Air temperature: " + data.ILMA + " °C\n" +
                "Average wind: " + data.KESKITUULI + " m/s\n" +
                "Air humidity: " + data.ILMAN_KOSTEUS + " %\n" +
                "Precipitation: " + data.SADE;
        }
        else weatherText.text = "Error: " + request.error;        
    }
}

[System.Serializable]
public class WeatherData
{
    public float ILMA;
    public float KESKITUULI;
    public float ILMAN_KOSTEUS;
    public float SADE;
}
