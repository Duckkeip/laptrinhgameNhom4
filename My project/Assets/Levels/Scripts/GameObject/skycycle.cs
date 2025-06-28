using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkyCycle : MonoBehaviour
{
    public GameObject skyDay;
    public GameObject skyDawn;
    public GameObject skyNight;

    public Light2D globalLight; // Light 2D
    
    public Light2D[] nightLights;


    public float cycleDuration = 10f; // giây mỗi trạng thái

    private float timer;
    private int state = 0; // 0 = Day, 1 = Dawn, 2 = Night

    [Header("Light Settings")]
    public float intensityDay = 1f;
    public float intensityDawn = 0.5f;
    public float intensityNight = 0.1f;

    public Color colorDay = new Color(1f, 0.95f, 0.8f);
    public Color colorDawn = new Color(1f, 0.7f, 0.4f);
    public Color colorNight = new Color(0.2f, 0.3f, 0.5f);

    void Start()
    {
        SetSkyState(0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cycleDuration)
        {
            timer = 0f;
            state = (state + 1) % 3;
            SetSkyState(state);
        }
    }

    void SetSkyState(int state)
    {
        // Kích hoạt bầu trời
        skyDay.SetActive(state == 0);
        skyDawn.SetActive(state == 1);
        skyNight.SetActive(state == 2);

        globalLight.intensity = (state == 2) ? 0.1f : 1f;

        foreach (var light in nightLights)
        {
            light.enabled = (state == 2); // chỉ bật khi trời đêm
        }

        // Điều chỉnh ánh sáng
        switch (state)
        {
            case 0: // Day
                globalLight.intensity = intensityDay;
                globalLight.color = colorDay;
                break;
            case 1: // Dawn
                globalLight.intensity = intensityDawn;
                globalLight.color = colorDawn;
                break;
            case 2: // Night
                globalLight.intensity = intensityNight;
                globalLight.color = colorNight;
                break;
        }
    }
}
