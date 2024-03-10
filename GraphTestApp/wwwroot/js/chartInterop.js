window.heartRateChartInterop = {
    createChart: function (canvasId) {
        var ctx = document.getElementById(canvasId).getContext('2d');
        window.myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: [],
                datasets: [{
                    label: 'Heart Rate',
                    data: [],
                    borderColor: 'rgb(75, 192, 192)',
                    tension: 0.1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: false,
                        suggestedMin: 50,
                        suggestedMax: 160,
                    }
                }
            }
        });
    },
    addData: function (label, data) {
        if (window.myChart.data.labels.length > 20) {
            window.myChart.data.labels.shift();
            window.myChart.data.datasets.forEach((dataset) => {
                dataset.data.shift();
            });
        }
        window.myChart.data.labels.push(label);
        window.myChart.data.datasets.forEach((dataset) => {
            dataset.data.push(data);
        });
        window.myChart.update();
    }
};