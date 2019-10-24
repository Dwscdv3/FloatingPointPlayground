/**
 * Copyright (C) 2019  Dwscdv3 <dwscdv3@hotmail.com>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Windows;
using System.Windows.Controls;
using FloatingPointPlayground.UserControls;

namespace FloatingPointPlayground
{
    public partial class MainWindow : Window
    {
        private bool disableTextChangedEvent = false;

        public MainWindow()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            var bin = 0;
            for (var i = 0; i < 32; i += 1)
            {
                bin += ((flippersContainer.Children[31 - i] as BitFlipper).IsChecked == true ? 1 : 0) << i;
            }
            txtInt.Text = bin.ToString();
            txtIntHex.Text = "0x" + bin.ToString("X8");
            disableTextChangedEvent = true;
            txtFloat.Text = BitConverter.ToSingle(BitConverter.GetBytes(bin), 0).ToString();
            disableTextChangedEvent = false;
        }

        private void OnBitFlipperClick(object sender, RoutedEventArgs e) => Update();

        private void OnFloatInput(object sender, TextChangedEventArgs e)
        {
            if (!disableTextChangedEvent)
            {
                var textBox = sender as TextBox;

                VisualStateManager.GoToElementState(layoutRoot, "Default", true);

                if (float.TryParse(textBox.Text, out var f))
                {
                    var bin = BitConverter.ToInt32(BitConverter.GetBytes(f), 0);
                    for (var i = 0; i < 32; i += 1)
                    {
                        (flippersContainer.Children[i] as BitFlipper).IsChecked = (bin & (1 << (31 - i))) != 0;
                    }
                    txtInt.Text = bin.ToString();
                    txtIntHex.Text = "0x" + bin.ToString("X8");
                }
                else
                {
                    VisualStateManager.GoToElementState(layoutRoot, "ParseError", true);
                }
            }
        }
    }
}
