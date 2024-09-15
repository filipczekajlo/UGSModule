using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using InventoryDTO;
using InventoryDTO.Weapons;
using LiveCharts.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;

namespace DataBalancing;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        CreateChart();
    }

    private void CreateChart()
    {
        // Create a list of ItemData objects
        List<ItemData> items = new List<ItemData>
        {
            new DamageOverTimeData().CreateDefaultValues("Fire", "Flame"),
            new DamageOverTimeData().CreateDefaultValues("Ice", "Frost"),
            new DamageOverTimeData().CreateDefaultValues("Poison", "Toxin")
        };

        // Prepare data for the chart
        var values = new List<ObservablePoint>();

        foreach (var item in items)
        {
            values.Add(new ObservablePoint(item.ChiCost, item.TotalDamage));
        }

        // Create the series for the chart
        var series = new ScatterSeries<ObservablePoint>
        {
            Values = values,
            Fill = new SolidColorPaint(SKColors.Red), // Set the fill color
            Stroke = new SolidColorPaint(SKColors.Blue) // Set the stroke color
        };

        // Create the cartesian chart
        var cartesianChart = new CartesianChart
        {
            Series = new ISeries[] { series },
            XAxes = new Axis[]
            {
                new Axis { Name = "Chi Cost", LabelsRotation = 15 }
            },
            YAxes = new Axis[]
            {
                new Axis { Name = "Total Damage" }
            }
        };

        // Add the chart to the form
        cartesianChart.Dock = DockStyle.Fill;
        this.Controls.Add(cartesianChart);
    }
    }