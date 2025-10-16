using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace eBar.KitchenDisplay
{
    public partial class OrderControl : UserControl
    {
        public static readonly DependencyProperty LeftTargetProperty =
            DependencyProperty.Register(nameof(LeftTarget), typeof(StackPanel), typeof(OrderControl));

        public static readonly DependencyProperty RightTargetProperty =
            DependencyProperty.Register(nameof(RightTarget), typeof(StackPanel), typeof(OrderControl));

        public StackPanel LeftTarget
        {
            get => (StackPanel)GetValue(LeftTargetProperty);
            set => SetValue(LeftTargetProperty, value);
        }

        public StackPanel RightTarget
        {
            get => (StackPanel)GetValue(RightTargetProperty);
            set => SetValue(RightTargetProperty, value);
        }

        class OrderEntry
        {
            public CheckBox Checkbox;
            public FrameworkElement LeftElement;
            public FrameworkElement RightElement;
            public DispatcherTimer Timer;
            public string OrderNumber;
        }

        private int orderCounter = 1;
        private readonly List<OrderEntry> activeOrders = new();

        public OrderControl()
        {
            InitializeComponent();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftTarget == null || RightTarget == null) return;

            string orderNumber = orderCounter.ToString("D3");

            var orderCheckbox = new CheckBox { VerticalAlignment = VerticalAlignment.Center};
            var leftContainer = new StackPanel { Orientation = Orientation.Horizontal};
            leftContainer.Children.Add(orderCheckbox);
            leftContainer.Children.Add(new TextBlock { Text = $"Заказ с номером №{orderNumber} создан" });
            LeftTarget.Children.Add(leftContainer);

            var timerText = new TextBlock { Text = "30:00"};
            var rightPanel = new StackPanel { Orientation = Orientation.Horizontal};
            rightPanel.Children.Add(timerText);
            RightTarget.Children.Add(rightPanel);

            TimeSpan remaining = TimeSpan.FromMinutes(30);
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, args) =>
            {
                remaining = remaining.Subtract(TimeSpan.FromSeconds(1));
                if (remaining < TimeSpan.Zero) remaining = TimeSpan.Zero;
                timerText.Text = $"{(int)remaining.TotalMinutes:D2}:{remaining.Seconds:D2}";
                if (remaining == TimeSpan.Zero) timer.Stop();
            };

            var entry = new OrderEntry
            {
                Checkbox = orderCheckbox,
                LeftElement = leftContainer,
                RightElement = rightPanel,
                Timer = timer,
                OrderNumber = orderNumber
            };
            activeOrders.Add(entry);
            timer.Start();
            orderCounter++;
        }

        private void DeleteOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var toRemove = new List<OrderEntry>();
            foreach (var entry in activeOrders)
            {
                if (entry.Checkbox.IsChecked == true) toRemove.Add(entry);
            }

            if (toRemove.Count == 0)
            {
                MessageBox.Show("Выберите заказы для удаления.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var entry in toRemove)
            {
                try
                {
                    entry.Timer.Stop();
                }
                catch
                {
                    
                }
                LeftTarget?.Children.Remove(entry.LeftElement);
                RightTarget?.Children.Remove(entry.RightElement);
                activeOrders.Remove(entry);
            }
        }
    }
}
