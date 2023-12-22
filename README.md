# SfListview.SelectedItem

When Binding SfListView.SelectedItem using Mode=TwoWay, Changing the selected item via
a UI interaction updates the bound property.  However, updating the bound property 
from code does not cause SfListView.SelectedItem to update and the associated events are not raised.

To illustrate:

1: Run the sample
2: Select a color in the color list
 - ColorPalette.SelectedItem is updated.
 - The SelectedItem and SelectionChanged UI components updated to reflect the selected color.

3: Select 'Reset SelectedItem' in the UI
 - ColorPalette.SelectedItem is set to the first color in the list. 
 - ColorPalette.SelectedItem raised the PropertyChanged event.
 - The selected item in the color list is NOT updated.
 - The SelectedItem and SelectionChanged UI components are NOT updated.

# Debugging Notes:

To debug the issue, run the sample in a debugger, select a color, then place a break point on ColorPalette.SelectedItem.set.
Select the 'Reset SelectedItem' button to to set PaletterColor.SelectedItem to the the first color in the list and
hit the break point.

The backing field is updated and PropertyChanged is raised for 'SelectedItem' but SfListView does not update SelectedItem and
no events are raised by SfListView.


