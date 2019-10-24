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

using System.Windows;
using System.Windows.Controls.Primitives;

namespace FloatingPointPlayground.UserControls
{
    public partial class BitFlipper : ToggleButton
    {
        static BitFlipper()
        {
            IsCheckedProperty.OverrideMetadata(
                forType: typeof(BitFlipper),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: false,
                    propertyChangedCallback: (d, e) =>
                    {
                        VisualStateManager.GoToElementState(
                            stateGroupsRoot: ((BitFlipper)d).viewbox,
                            stateName: (bool)e.NewValue ? "TrueValue" : "FalseValue",
                            useTransitions: true
                        );
                    }
                )
            );
        }

        public BitFlipper()
        {
            InitializeComponent();
            VisualStateManager.GoToElementState(viewbox, "FalseValue", false);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToElementState(viewbox, IsChecked == true ? "TrueValue" : "FalseValue", true);
        }
    }
}
