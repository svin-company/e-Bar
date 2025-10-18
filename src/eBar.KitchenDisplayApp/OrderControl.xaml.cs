using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace eBar.KitchenDisplayApp
{
    public partial class OrderControl : UserControl
    {
        public static readonly DependencyProperty LeftTargetProperty =
            DependencyProperty.Register(nameof(LeftTarget), typeof(StackPanel), typeof(OrderControl));

        public static readonly DependencyProperty RightTargetProperty =
            DependencyProperty.Register(nameof(RightTarget), typeof(StackPanel), typeof(OrderControl));
        
        public static readonly DependencyProperty MiddleTargetProperty = 
            DependencyProperty.Register(nameof(MiddleTarget), typeof(StackPanel), typeof(OrderControl));

        public StackPanel LeftTarget
        {
            get => (StackPanel)GetValue(LeftTargetProperty);
            set => SetValue(LeftTargetProperty, value);
        }

        public StackPanel MiddleTarget
        {
            get => (StackPanel)GetValue(MiddleTargetProperty);
            set => SetValue(MiddleTargetProperty, value);
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
            public FrameworkElement MiddleElement;
            public FrameworkElement RightElement;
            public DispatcherTimer Timer;
            public string OrderNumber;
        }

        private int orderCounter = 1;
        private readonly List<OrderEntry> activeOrders = new List<OrderEntry>();

        public OrderControl()
        {
            InitializeComponent();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftTarget == null || RightTarget == null) return;
            FrameworkElement middleElement = null;
            string orderNumber = orderCounter.ToString("D3");

            var orderCheckbox = new CheckBox { VerticalAlignment = VerticalAlignment.Center };
            var leftContainer = new StackPanel { Orientation = Orientation.Horizontal };
            leftContainer.Children.Add(orderCheckbox);
            leftContainer.Children.Add(new TextBlock
            {
                Foreground = System.Windows.Media.Brushes.White,
                Margin = new System.Windows.Thickness(5, 2, 0, 6),
                Text = $"Заказ с номером №{orderNumber} создан"
            });
            LeftTarget.Children.Add(leftContainer);

            var timerText = new TextBlock
            {
                Foreground = System.Windows.Media.Brushes.White,
                Margin = new System.Windows.Thickness(0, 2, 0, 6),
                Text = "30:00"
            };
            var rightPanel = new StackPanel { Orientation = Orientation.Horizontal };
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

            if (MiddleTarget != null)
            {
                var rnd = new Random();
                string[] words = new[] {
                    "суп","стейк","салат","паста","суши","ролл","бутерброд","пицца",
                    "десерт","кофе","чай","вино","вода","сок","соус","хлеб",
                    "масло","картофель","рис","рыба","курица","говядина","овощи","фрукты"
                };
                int targetLength = rnd.Next(8, 25);
                var sb = new System.Text.StringBuilder();
                bool first = true;
                while (true)
                {
                    string w = words[rnd.Next(words.Length)];
                    string num = rnd.Next(1, 10).ToString();
                    string token = num + " шт. " + w;
                    int addLen = first ? token.Length : 2 + token.Length;
                    if (sb.Length + addLen > targetLength)
                    {
                        if (first && targetLength - sb.Length > 0)
                        {
                            int remain = targetLength - sb.Length;
                            sb.Append(token.Substring(0, Math.Min(token.Length, remain)));
                        }
                        break;
                    }
                    if (!first) sb.Append(", ");
                    sb.Append(token);
                    first = false;
                }
                string text = sb.ToString();
                
                var midText = new TextBlock
                {
                    Text = text,
                    Foreground = System.Windows.Media.Brushes.White,
                    Margin = new System.Windows.Thickness(0, 2, 0, 6)
                };
                MiddleTarget.Children.Add(midText);
                middleElement = midText;
            }

            var entry = new OrderEntry
            {
                Checkbox = orderCheckbox,
                LeftElement = leftContainer,
                MiddleElement = middleElement,
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
                MessageBox.Show("Выберите заказы для удаления.", "Удаление", MessageBoxButton.OK,
                    MessageBoxImage.Information);
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
                MiddleTarget?.Children.Remove(entry.MiddleElement);
                RightTarget?.Children.Remove(entry.RightElement);
                activeOrders.Remove(entry);
            }
        }
    }
}