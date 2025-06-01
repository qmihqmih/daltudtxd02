using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.View
{
    public partial class Botricoc : Window
    {
        public Botricoc()
        {
            InitializeComponent();
        }

        private void BtnArrange_Click(object sender, RoutedEventArgs e)
        {
            canvasFoundation.Children.Clear();

            if (!int.TryParse(txtPileCount.Text, out int n) || n < 1 || n > 12)
            {
                MessageBox.Show("Vui lòng nhập số lượng cọc từ 1 đến 12.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedSize = (cmbPileSize.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "200x200";
            var sizeParts = selectedSize.Split('x');
            if (sizeParts.Length != 2
                || !double.TryParse(sizeParts[0], NumberStyles.Number, CultureInfo.InvariantCulture, out double rectWidth)
                || !double.TryParse(sizeParts[1], NumberStyles.Number, CultureInfo.InvariantCulture, out double rectHeight))
            {
                MessageBox.Show("Kích thước cọc không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double diameter = (rectWidth + rectHeight) / 2;
            double minCenterToCenter = 3 * diameter;
            double minEdgeToCenter = diameter;

            double L = 0, B = 0;

            // Các vị trí tâm cọc sẽ lưu trong danh sách (x,y)
            var pileCenters = new System.Collections.Generic.List<(double x, double y)>();

            // Tính kích thước và vị trí cọc theo từng trường hợp
            switch (n)
            {
                case 3:
                    // 2 cọc hàng trên, 1 cọc giữa hàng dưới
                    L = minEdgeToCenter * 2 + 2 * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + minCenterToCenter + rectHeight;

                    // Tọa độ cọc (tính theo góc trên trái của đài)
                    double topY3 = minEdgeToCenter + rectHeight / 2;
                    double bottomY3 = topY3 + minCenterToCenter;

                    double leftX3 = minEdgeToCenter + rectWidth / 2;
                    double rightX3 = leftX3 + minCenterToCenter;
                    double middleX3 = (leftX3 + rightX3) / 2;

                    pileCenters.Add((leftX3, topY3));
                    pileCenters.Add((rightX3, topY3));
                    pileCenters.Add((middleX3, bottomY3));
                    break;

                case 5:
                    // 4 cọc 2 bên, 1 cọc trung tâm đài
                    L = minEdgeToCenter * 2 + 3 * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + minCenterToCenter + rectHeight;
                    double topY5 = minEdgeToCenter + rectHeight / 2;
                    double bottomY5 = topY5 + minCenterToCenter;

                    double leftX5 = minEdgeToCenter + rectWidth / 2;
                    double rightX5 = leftX5 + 3 * minCenterToCenter;

                    // 4 cọc 2 bên hàng trên và dưới
                    pileCenters.Add((leftX5, topY5));
                    pileCenters.Add((rightX5, topY5));
                    pileCenters.Add((leftX5, bottomY5));
                    pileCenters.Add((rightX5, bottomY5));

                    // 1 cọc ở giữa (hàng dưới, chính giữa)
                    double centerX5 = (leftX5 + rightX5) / 2;
                    pileCenters.Add((centerX5, bottomY5));
                    break;

                case 7:
                    // Hàng 1: 2 cọc
                    // Hàng 2: 3 cọc
                    // Hàng 3: 2 cọc (đối xứng)
                    L = minEdgeToCenter * 2 + 2 * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + 2 * minCenterToCenter + rectHeight * 3;

                    double y1_7 = minEdgeToCenter + rectHeight / 2;
                    double y2_7 = y1_7 + minCenterToCenter + rectHeight;
                    double y3_7 = y2_7 + minCenterToCenter + rectHeight;

                    double leftX7 = minEdgeToCenter + rectWidth / 2;
                    double rightX7 = leftX7 + minCenterToCenter;

                    // Hàng 1 (2 cọc)
                    pileCenters.Add((leftX7, y1_7));
                    pileCenters.Add((rightX7, y1_7));

                    // Hàng 2 (3 cọc)
                    double midX7 = (leftX7 + rightX7) / 2;
                    pileCenters.Add((leftX7 - minCenterToCenter / 2, y2_7));
                    pileCenters.Add((midX7, y2_7));
                    pileCenters.Add((rightX7 + minCenterToCenter / 2, y2_7));

                    // Hàng 3 (2 cọc đối xứng)
                    pileCenters.Add((leftX7, y3_7));
                    pileCenters.Add((rightX7, y3_7));
                    break;

                case 8:
                    // Tất cả ở mép đài, giả sử 4 cọc trên dưới, 2 bên (đối xứng)
                    L = minEdgeToCenter * 2 + 3 * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + 2 * minCenterToCenter + rectHeight;

                    double topY8 = minEdgeToCenter + rectHeight / 2;
                    double bottomY8 = topY8 + 2 * minCenterToCenter;
                    double leftX8 = minEdgeToCenter + rectWidth / 2;
                    double rightX8 = leftX8 + 3 * minCenterToCenter;

                    // 4 cọc hàng trên
                    pileCenters.Add((leftX8, topY8));
                    pileCenters.Add((leftX8 + minCenterToCenter, topY8));
                    pileCenters.Add((leftX8 + 2 * minCenterToCenter, topY8));
                    pileCenters.Add((rightX8, topY8));

                    // 4 cọc hàng dưới (đối xứng)
                    pileCenters.Add((leftX8, bottomY8));
                    pileCenters.Add((leftX8 + minCenterToCenter, bottomY8));
                    pileCenters.Add((leftX8 + 2 * minCenterToCenter, bottomY8));
                    pileCenters.Add((rightX8, bottomY8));
                    break;

                case 10:
                    // 3 cọc hàng 1, 4 cọc hàng 2, 3 cọc hàng 3 (đối xứng)
                    L = minEdgeToCenter * 2 + 3 * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + 2 * minCenterToCenter + rectHeight * 3;

                    double y1_10 = minEdgeToCenter + rectHeight / 2;
                    double y2_10 = y1_10 + minCenterToCenter + rectHeight;
                    double y3_10 = y2_10 + minCenterToCenter + rectHeight;

                    double leftX10 = minEdgeToCenter + rectWidth / 2;
                    double rightX10 = leftX10 + 2 * minCenterToCenter;

                    // Hàng 1 (3 cọc)
                    pileCenters.Add((leftX10, y1_10));
                    pileCenters.Add((leftX10 + minCenterToCenter, y1_10));
                    pileCenters.Add((rightX10, y1_10));

                    // Hàng 2 (4 cọc)
                    pileCenters.Add((leftX10, y2_10));
                    pileCenters.Add((leftX10 + minCenterToCenter / 2, y2_10));
                    pileCenters.Add((leftX10 + 1.5 * minCenterToCenter, y2_10));
                    pileCenters.Add((rightX10, y2_10));

                    // Hàng 3 (3 cọc đối xứng)
                    pileCenters.Add((leftX10, y3_10));
                    pileCenters.Add((leftX10 + minCenterToCenter, y3_10));
                    pileCenters.Add((rightX10, y3_10));
                    break;

                case 11:
                    // 4 cọc hàng 1, 3 cọc hàng 2, 4 cọc hàng 3
                    L = minEdgeToCenter * 2 + 3 * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + 2 * minCenterToCenter + rectHeight * 3;

                    // Tính tọa độ Y cho 3 hàng
                    double y1_11 = minEdgeToCenter + rectHeight / 2;
                    double y2_11 = y1_11 + minCenterToCenter + rectHeight;
                    double y3_11 = y2_11 + minCenterToCenter + rectHeight;

                    // Tính X trái và phải cho hàng 1 & 3
                    double leftX11 = minEdgeToCenter + rectWidth / 2;
                    double rightX11 = leftX11 + 2 * minCenterToCenter;
                    double centerX11 = (leftX11 + rightX11) / 2;

                    // Hàng 1 (4 cọc) - đối xứng theo centerX11
                    pileCenters.Add((centerX11 - 1.5 * minCenterToCenter, y1_11));
                    pileCenters.Add((centerX11 - 0.5 * minCenterToCenter, y1_11));
                    pileCenters.Add((centerX11 + 0.5 * minCenterToCenter, y1_11));
                    pileCenters.Add((centerX11 + 1.5 * minCenterToCenter, y1_11));

                    // Hàng 2 (3 cọc) - cân giữa
                    pileCenters.Add((centerX11 - minCenterToCenter, y2_11));
                    pileCenters.Add((centerX11, y2_11));
                    pileCenters.Add((centerX11 + minCenterToCenter, y2_11));

                    // Hàng 3 (4 cọc) - đối xứng theo centerX11
                    pileCenters.Add((centerX11 - 1.5 * minCenterToCenter, y3_11));
                    pileCenters.Add((centerX11 - 0.5 * minCenterToCenter, y3_11));
                    pileCenters.Add((centerX11 + 0.5 * minCenterToCenter, y3_11));
                    pileCenters.Add((centerX11 + 1.5 * minCenterToCenter, y3_11));
                    break;

                default:
                    // Trường hợp khác dùng ma trận mặc định (1,2,4,6,9,12)
                    int cols = 1, rows = 1;
                    if (n == 1)
                    {
                        cols = 1; rows = 1;
                    }
                    else if (n <= 4)
                    {
                        cols = 2; rows = 2;
                    }
                    else if (n <= 6)
                    {
                        cols = 3; rows = 2;
                    }
                    else if (n <= 9)
                    {
                        cols = 3; rows = 3;
                    }
                    else // 10-12
                    {
                        cols = 4; rows = 3;
                    }

                    L = minEdgeToCenter * 2 + (cols - 1) * minCenterToCenter + rectWidth;
                    B = minEdgeToCenter * 2 + (rows - 1) * minCenterToCenter + rectHeight;

                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols; c++)
                        {
                            int currentPileIndex = r * cols + c + 1;
                            if (currentPileIndex > n) break;

                            double x = minEdgeToCenter + c * minCenterToCenter + rectWidth / 2;
                            double y = minEdgeToCenter + r * minCenterToCenter + rectHeight / 2;
                            pileCenters.Add((x, y));
                        }
                    }
                    break;
            }

            txtFoundationSize.Text = $"Chiều dài đài (L): {L:F1} mm, Chiều rộng đài (B): {B:F1} mm";

            double canvasMargin = 20;
            double scale = Math.Min((canvasFoundation.ActualWidth - 2 * canvasMargin) / L, (canvasFoundation.ActualHeight - 2 * canvasMargin) / B);

            double startX = canvasMargin;
            double startY = canvasMargin;

            var foundationRect = new Rectangle
            {
                Width = L * scale,
                Height = B * scale,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.SetLeft(foundationRect, startX);
            Canvas.SetTop(foundationRect, startY);
            canvasFoundation.Children.Add(foundationRect);
            // Vẽ các cọc theo tọa độ đã tính
            foreach (var (x, y) in pileCenters)
            {
                var pileRect = new Rectangle
                {
                    Width = rectWidth * scale,
                    Height = rectHeight * scale,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1.5,
                    Fill = Brushes.LightBlue
                };
                Canvas.SetLeft(pileRect, startX + x * scale - (rectWidth * scale) / 2);
                Canvas.SetTop(pileRect, startY + y * scale - (rectHeight * scale) / 2);
                canvasFoundation.Children.Add(pileRect);
            }
        }
    }
}
