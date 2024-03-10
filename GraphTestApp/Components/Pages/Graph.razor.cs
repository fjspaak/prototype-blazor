using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GraphTestApp.Components.Pages;

public class GraphBase : ComponentBase, IDisposable
{
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    private Random rnd = new Random();
    private int baseHeartRate = 75; // Starting heart rate
    private System.Threading.Timer timer;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("heartRateChartInterop.createChart", "heartRateCanvas");
            timer = new System.Threading.Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }
    }

    private void Callback(object state)
    {
        int fluctuation = rnd.Next(-5, 6);
        baseHeartRate += fluctuation;

        if (baseHeartRate < 60) baseHeartRate = 60;
        if (baseHeartRate > 160) baseHeartRate = 160;

        if (rnd.Next(0, 10) > 8)
        { // Simulate spikes
            baseHeartRate = rnd.Next(100, 161);
        }

        InvokeAsync(() => UpdateChart(baseHeartRate));
    }

    private async Task UpdateChart(int heartRate)
    {
        var now = DateTime.Now;
        var label = $"{now.Hour}:{now.Minute}:{now.Second}";
        await JSRuntime.InvokeVoidAsync("heartRateChartInterop.addData", label, heartRate);
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}