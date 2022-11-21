**WARNING: this documentation is deprecated and incomplete, it will be updated in the near furture.**

# Table of Contents

[[_TOC_]]

# Importing the package

Add this scoped registry.

```json
"scopedRegistries": [
    {
      "name": "Mr. Watts UPM Registry",
      "url": "https://gitlab.com/api/v4/projects/27157125/packages/npm/",
      "scopes": [
        "io.mrwatts"
      ]
    }
  ]
```

Add the following dependency in the **manifest.json** file in the "Packages" folder.

*"io.mrwatts.onion": "1.0.0"*

The version number should be the latest version of the package (unless you want to target an older version on purpose).

# Using prefabs

## General

 * All UI elements described below can be interacted with through 'physical' touch (near interaction) as well as 'air taps' (far interaction) unless otherwise stated.

 * All elements, with exception of the window, are based on default UnityUI elements and should be used accordingly. Eg. when assigning a click event to a button via script use the `onClick` event of the `Button` component, the `OnClick` from the `Interactable` component.

 * Text elements, or elements using text, all use TextMeshPro: `TMPro.TMP_Text` instead of `UnityEngine.UI.Text`. This also implies the use of `TMPro.TMP_InputField` instead of `UnityEngine.UI.InputField`. This is because the MRTK prefers assets that use TMP.

 * As a guideline to user friendly UI a minimum height for UI elements of 40px seems to be an acceptable size.

 * Most interactables have a standard version, and a version suffixed with '_Speech'. These speech versions have a SeeItSayIt-label. This label will show when hovering above the interactable to hint the voice command for that particular interactable. These voice commands should be configured separately in the MixedRealityToolkitConfigurationProfile.

 * Adding an element with a component that has `Raycast Target` enabled, will still block interaction with underlying elements (defined by hierarchy, not physical placement). This means that blocking interaction with a group of elements, like a full window (eg. while a popup is shown) with an invisible image/button still works like default UnityUI would.

## Making variants

When using the default prefabs, the prefab is often going to be expanded with project-specific scripts. When making a derivative prefab to use in a project, make sure to make a **'Prefab Variant'** and not an 'Original Prefab'.

Selecting 'Prefab Variant' ensures that when the default prefab is edited, the changes will still apply to this prefab. When making an 'Original Prefab', this link is broken.

## Disabling interactables

Interactables can be disabled. To communicate to the user which interactable are disabled, the interactable needs to indicate this visually. To simplify disabling interactable, they are all fitted with a `DisableHelper` component, which is already preconfigured.

When an interactable needs to be disabled, just call `SetActive(false)` on the `DisableHelper` of that interactable.

# Containers

## Window

The base of every UI panel should be a window object. All UI should be put inside the 'Canvas' (Window>Content>Canvas).

The window can be grabbed and moved by the titlebar. The 'Follow Me' button attaches the window to the user so it will follow wherever the user goes. The 'Close' button hides the window and it's content; the window will remain in the scene hierarchy but will be inactive.

### Resizing a window

A window has a `WindowSize` component. Resizing a window should always be done by changing the `width` and `height` values of this component and pressing the 'Update window size' button. This will make sure all underlying objects and components will get correctly scaled. All elements assigned under 'Items' are setup by default and should, in normal circumstances, not be changed.

Scaling a window differs from resizing a window. When scaling a window the `localScale` of the `Transform` component is used. This scale should **always** be `(1, 1, 1)`! This scale is used when the user 'scales' the window using the handles.

The window could be updated with handles to have the user also control the size (aspect ratio) of the window. Those handles should update the `width` and `height` values of the `WindowSize` component.

## Scrollview

The scrollbars of the scrollview are designed as the scrollbars used in the native HoloLens UI, eg. the filepicker. The handles are small and not meant to be grabbable (although, if you aim right, they can still be grabbed with a air tap). Scrolling can be done by dragging with an air tap or grabbing the scrollview with near interaction. Make sure to grab the scrollview in an empty space or on a non-interactable UI element otherwise the interaction would trigger the components that are within the scrollview.

The viewport of the scrollview uses a `RectMask2D` instead of an `Image` and a `Mask` because this works better with the MRTK materials (eg. for displaying the hover effect). Note that the hover effect will always be fully displayed as long as the item is partly visible (unmasked). Once the object is fcompletely hidden by the mask, the hover effect will be masked as well. To blend this visual difference, a slight 'fade' is used on the edges of the scrollview. This hides the hard edge of the mask.

When using the scrollview, all content must be placed in 'Content' (ScrollView>Viewport>Content). For the best experience, the 'Content' object will often be expanded with layout components (eg. `GridLayoutGroup`, `VerticalLayoutGroup` or `HorizontalLayoutGroup`) and a `ContentSizeFitter`. These components are not added by default, because some situations will require a different approach.

The 'ScrollViewVertical' and the 'ScrollViewHorizontal' objects should be used when only one scroll direction is needed, respectively vertical and horizontal. The 'ScrollView' object is designed to scroll both ways.

# Interactables

## Button

Buttons behave as very standard UnityUI buttons and will smoothly scale as with the canvas. Buttons that contain an image should also scale without any issue. When extreme scales are needed, the image might need to be edited manually. Images are set to a standard size of 50px (width and/or height, depending on the button).

## Toggle

### Checkmark

A checkmark consists of a checkbox and a label. It uses the UnityUI `Toggle` component.

### Toggle

 A toggle is a button-like element with an _on_ and _off_ state. It is, like the checkmark, based on the UnityUI `Toggle`, but has an additional `ShowHideOnToggle` component. Like the name implies, this component shows and hides objects based on the toggle's state. The list of objects to show/hide can also contain objects not part of the toggle, so it can be used to automatically show/hide items when interacting with the element.

## InputField

The InputField is (for now) the only UI element that uses `pointer` instead of `touch` events.

This does not raise any issues. It is however the reason why the focus does not leave the InputField when interacting with another UI element (like pressing a button). This only happens when closing the (holographic) keyboard with <kbd>&#10005;</kbd>. When using <kbd>&#x21B5;</kbd> to close the keyboard, the focus does leave the InputField as expected.
