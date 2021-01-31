using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public RectTransform BatteryTransform;
    public TextMeshProUGUI BatteryText;
    private const float BATTERY_DURATION = 10;
    private float _initialBatteryWidth;
    private float _batteryAmount = 100f;
    public void Start()
    {
        _initialBatteryWidth = BatteryTransform.rect.width;
    }

    private void Update()
    {
        _batteryAmount -= Time.deltaTime * (100f / BATTERY_DURATION);
        var percentage = _batteryAmount / 100;
        var batterySize = BatteryTransform.sizeDelta;
        batterySize.x = percentage * _initialBatteryWidth;
        BatteryTransform.sizeDelta = batterySize;
        var position = BatteryTransform.anchoredPosition;
        position.x = -1f * (1f - percentage) * _initialBatteryWidth / 2f;
        BatteryTransform.anchoredPosition = position;
        BatteryText.text = $"%{(int) (percentage * 100f)}";
    }
}