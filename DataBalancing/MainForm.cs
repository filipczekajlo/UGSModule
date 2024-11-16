using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.Measure;
using LiveChartsCore.Drawing;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InventoryDTO;
using InventoryDTO.Weapons;
using LiveCharts.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;

namespace DamageVisualizer
{
    public class Form1 : Form
    {
        public Form1()
        {
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
                // values.Add(new ObservablePoint(item.ChiCost, item.TotalDamage));
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

        // [STAThread]
        // static void Main()
        // {
        //     Application.SetHighDpiMode(HighDpiMode.SystemAware);
        //     Application.EnableVisualStyles();
        //     Application.SetCompatibleTextRenderingDefault(false);
        //     Application.Run(new Form1());
        // }
    }

    // Base classes from your previous code
    // public abstract class ItemData
    // {
    //     public ItemData()
    //     {
    //         Id = "";
    //         TotalDamage = 0;
    //         ChiCost = 0;
    //         Name = "";
    //         ItemType = "";
    //     }
    //
    //     public string ItemType { get; set; }
    //     public string Id { get; set; }
    //     public string Name { get; set; }
    //     public int TotalDamage { get; set; }
    //     public int ChiCost { get; set; }
    // }
    //
    // public class WeaponData : ItemData { }
    //
    // public class DamageOverTimeData : WeaponData, ICreateDefaultValues
    // {
    //     public ItemData CreateDefaultValues(string itemType, string element)
    //     {
    //         DamageOverTimeData damageOverTimeData = new DamageOverTimeData
    //         {
    //             ItemType = itemType,
    //             Id = itemType + element,
    //             Name = "Damage Over Time Weapon",
    //             TotalDamage = 5,
    //             ChiCost = 5
    //         };
    //
    //         return damageOverTimeData;
    //     }
    // }
    //
    // public interface ICreateDefaultValues
    // {
    //     ItemData CreateDefaultValues(string itemType, string element);
    // }
}
