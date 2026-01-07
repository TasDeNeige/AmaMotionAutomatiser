<img width="1776" height="220" alt="Ama Motion Automatiser Logo" src="https://github.com/user-attachments/assets/7ad13ea0-7f33-423a-a3f1-4c05c42cc742" />

> Coded by Amaryne B.

*A simple way to animate object motions in Unity. Heavily inspired by the [DoTween API](https://dotween.demigiant.com).*

# [Download (Unity Package)](https://raw.github.com/TasDeNeige/AmaMotionAutomatiser/main/AmaMotionAutomatiser.unitypackage)
*This version was made for Unity 6*
<br>

# Guide Overview
### Introduction
- [Install](#Introduction-Install)
- [How to use](#Introduction-HowToUse)
- [General Knowledge](#Introduction-GeneralKnowledge)

### Animations
- [Move](#Animations-Move)
- [Move UI](#Animations-MoveUI)
- [Rotate](#Animations-Rotate)
- [Scale](#Animations-Scale)
- [Fade](#Animations-Fade)
- [Shake](#Animations-Shake)

### Miscellaneous
- [Set Curve](#Misc-SetCurve)
- [Snap to end value](#Misc-SnapEndValue)
- [On Start](#Misc-OnStart)
- [On End](#Misc-OnEnd)
- [From](#Misc-From)
- [Set Delay](#Misc-SetDelay)
- [Stop MA](#Misc-StopMA)
- [Stop All](#Misc-StopAll)

<br><br><br><br>

# Introduction
<a name="Introduction-Install"></a>
## Install
In order to install the Ama Motion Automatiser to one of your project, please download the package available on this Github page.
Dropping it in your project should be enough to start using the AMA.

<a name="Introduction-HowToUse"></a>
## How to use
There are two main ways of using the AMA:
### 1) In code
At the top of your `.cs` file, add this line:
```cs
using AMA;
```
<br>

The Ama Motion Automatiser's **functions** are <ins>extension functions</ins>, which means they are to be called from a Unity Object.<br>
Example with the AMAMove function:
```cs
transform.AMAmove(Axis.x, 1f, 2f);
```
> *Here, the function is called via Unity's `Transform`. The functions available depends on the object's type (`Transform`, `Quaternion`, `Color`, ...).*<br>

**Features** are available to extend the control over your animation. They are <ins>extension functions</ins> of the Animation object.<br>
Example with adding a curve to the animation:
```cs
transform.AMAmove(Axis.x, 1f, 2f).SetCurve(Curves.Back_Out);
```

You can add as many feature as you want. (Feature order doesn't matter)
```cs
transform.AMAmove(Axis.x, 1f, 2f).From(Vector3.zero).SetCurve(Curves.Back_Out).SetDelay(5f);
```

### 2) With a component
Creating an animation can be as simple as adding a component to an object (directly inside Unity).<br>
Components are named `AMA Animation_<AnimationType>`.<br><br>

Every feature is available on the animation's component.<br>
<img width="410" height="427" alt="image" src="https://github.com/user-attachments/assets/9b00db11-7445-4449-843f-5da1239069fb" />
<br><br>
> [!WARNING]
> Make sure to use the right component for your animation needs. For example, some components work only with a `RectTransform`. 

<a name="Introduction-GeneralKnowledge"></a>
## General Knowledge
AMA functions usually follow this pattern:<br>
```cs
transform.AMAmove(Axis.x,  // aka '_selectedAxis' -> Axis to animate on
                  1f,      // aka '_endPos' -> Final position
                  2f);     // aka '_duration' -> Animation duration
```
<br>
Features can be added to an animation. For more informations, please check the Miscellaneous section.








<br><br><br>
# Animations
<a name="Animations-Move"></a>
## Move  
`AMAmove` is used to move an object via its `transform`'s position.<br><br>
***Code usage:***<br>
Multiple versions are available, depending on your needs:<br>
| Function name  | Value changed |
| ------------- | ------------- |
| `AMAmove`  | `transform.position`  |
| `AMAlocalMove`  | `transform.localPosition`  |<br>

*Example:*
```cs
transform.AMAmove(Axis.x,  // aka '_selectedAxis' -> Axis to animate on | AMA.Axis
                1f,      // aka '_endRotation' -> Final position | float or Vector3
                2f);     // aka '_duration' -> Animation duration | float
```
> [!TIP]
> In code, the `_endPos` parameter can be either a `float` or a `Vector3`!<br>

***Version with component:***<br>
<img width="410" height="427" alt="image" src="https://github.com/user-attachments/assets/9b00db11-7445-4449-843f-5da1239069fb" />


<br><br>
<a name="Animations-MoveUI"></a>
## Move UI
`AMAmove` is used to move an object via its `rectTransform`'s position.<br><br>
***Code usage:***<br>
Multiple versions are available, depending on your needs:<br>
| Function name  | Value changed |
| ------------- | ------------- |
| `AMAmove`  | `rectTransform.position`  |
| `AMAlocalMove`  | `rectTransform.localPosition`  |
| `AMAanchoredPosMove`  | `rectTransform.anchoredPosition`  |
| `AMAanchoredPos3dMove`  | `rectTransform.anchoredPosition3D`  |

*Example:*
```cs
GetComponent<RectTransform>().anchoredPosition(Axis.x,  // aka '_selectedAxis' -> Axis to animate on | AMA.Axis
                                             1f,      // aka '_endRotation' -> Final position | float or Vector3
                                             2f);     // aka '_duration' -> Animation duration | float
```
> [!TIP]
> In code, the `_endPos` parameter can be either a `float` or a `Vector3`!<br>

***Version with component:***<br>
<img width="429" height="429" alt="image" src="https://github.com/user-attachments/assets/9be97c72-62b3-4d37-960e-cabbd2fef343" />

<br><br>
<a name="Animations-Rotate"></a>
## Rotate
`AMArotate` is used to rotate an object via its `transform`'s rotation.<br><br>
***Code usage:***<br>
Multiple versions are available, depending on your needs:<br>
| Function name  | Value changed |
| ------------- | ------------- |
| `AMArotate`  | `transform.rotation`  |
| `AMArotateEuler`  | `transform.rotation.eulerAngles`  |
| `AMAlocalRotate`  | `transform.localRotation`  |
| `AMAlocalRotateEuler`  | `transform.localRotation.eulerAngles`  |

*Example:*
```cs
transform.AMArotate(Axis.x,  // aka '_selectedAxis' -> Axis to animate on | AMA.Axis
                    1f,      // aka '_endRotation' -> Final rotation | float or Quaternion
                    2f);     // aka '_duration' -> Animation duration | float
```
> [!TIP]
> In code, the `_endPos` parameter can be either a `float` or a `Quaternion`/`Vector3`!<br>

***Version with component:***<br>
<img width="430" height="447" alt="image" src="https://github.com/user-attachments/assets/072f1cdd-2552-4a1e-96d7-03d5e362540a" />

<br><br>
<a name="Animations-Scale"></a>
## Scale
`AMAscale` is used to scale an object via its `transform`'s scale.<br><br>
***Code usage:***<br>
*Example:*
```cs
transform.AMAscale(Axis.x, 1f, 2f);
```
> [!TIP]
> In code, the `_endPos` parameter can be either a `float` or a `Vector3`!<br>

***Version with component:***<br>
<img width="430" height="409" alt="image" src="https://github.com/user-attachments/assets/7766099c-966e-48dd-b2e0-5bdbf5cbe591" />

<br><br>
<a name="Animations-Fade"></a>
## Fade
`AMAfade` is used to fade a component's color between two colors.<br>
You can fade color on `UnityEngine.UI.Image`, `TMPro.TMP_Text` and `Material`.<br>
Fading on **alpha only** is also possible thanks to the `AMAfadeAlpha` function.<br><br>

***Code usage:***<br>
Multiple versions are available, depending on your needs:<br>
| Function name  | Value changed |
| ------------- | ------------- |
| `AMAfade`  | `Material.color` or `UnityEngine.UI.Image.color` or `TMP_Text.color`  |
| `AMAfadeAlpha`  | `Material.color.a` or `UnityEngine.UI.Image.color.a` or `TMP_Text.color.a`  |

*Example:*
```cs
image.AMAfade(Color.red,  // aka '_endColor' -> Final color | UnityEngine.Color
              2f);        // aka '_duration' -> Animation duration | float
```

***Version with component:***<br>
<img width="429" height="319" alt="image" src="https://github.com/user-attachments/assets/ed606700-bf6e-4c06-98e5-65ab3661f3af" />

<br><br>
<a name="Animations-Shake"></a>
## Shake
`AMAshake` is used to create a 'shake' effect on an object's `transform`'s position.<br>
This function comes with two more parameters, such as the Shake Radius and the Delay Between Shakes.<br><br>

***Code usage:***<br>
*Example:*
```cs
transform.AMAshake(Axis.All,  // aka '_selectedAxis' -> Axis to animate on | AMA.Axis
                   5f,        // aka '_shakeRadius' -> Radius in which the object can be shaken in | float
                   2f,        // aka '_duration' -> Animation duration | float
                   0.01f);    // OPTIONAL, default = 0.0f | aka '_delayBetweenShakes' -> Delay between each shake | float
```

***Version with component:***<br>
<img width="431" height="342" alt="image" src="https://github.com/user-attachments/assets/704d1341-c556-4bc9-83b7-7627abe20470" />




<br><br><br>
# Miscellaneous
<a name="Misc-SetCurve"></a>
## Set Curve
`.SetCurve` is an **extension method** of the Animation object.<br>
This function sets the curve used to moderate the animation's movement.<br><br>

Robert Penner's curves are predefined in the AMA and are available to use. They are stored in the `AMA.Axis` enum.<br>
You can also use custom curves for animations thanks to Unity's `AnimationCurve`.

***Code usage:***<br>
```cs
transform.AMAmove(Axis.x, 1f, 2f).SetCurve(Curves.Back_Out); // aka '_curve' -> Selected curve | AMA.Curves or UnityEngine.AnimationCurve
```

***Version with component:***<br>
<img width="431" height="427" alt="image" src="https://github.com/user-attachments/assets/92df59ca-faa2-47ec-a70e-a37eb7446d85" />
<br><br>
A Curve Selector is also available with the 'Select curve' button for easier preview.<br>
<img width="686" height="537" alt="image" src="https://github.com/user-attachments/assets/3d0a61bc-6b6c-4117-9fc8-81c1fc9d4888" />



<br><br><br>
<a name="Misc-SnapEndValue"></a>
## Snap to end value
`_snapToEndValue` is an **optional argument** present at the end of every AMA animation function.<br>
It is used to <ins>force apply</ins> the animation's end value to the object when the animation ends.<br>

> [!IMPORTANT]
> This variable is defaulted to true.

***Code usage:***<br>
```cs
transform.AMAmove(Axis.x, 1f, 2f, false); // aka '_snapToEndValue' -> Snaps to end value | bool
```

***Version with component:***<br>
<img width="430" height="425" alt="image" src="https://github.com/user-attachments/assets/9e4d8b79-9bdd-444d-ba1a-0ad5fbf3e873" />

<br><br><br>
<a name="Misc-OnStart"></a>
## On Start
`.OnStart` is an **extension method** of the Animation object.<br>
This method calls a **given function** when the **animation starts**.
> [!NOTE]
> If the animation has a delay, it will be considered as 'started' when **the delay ends**.

***Code usage:***<br>
```cs
transform.AMAmove(Axis.x, 1f, 2f).OnStart(() => Debug.Log("Hello world!")); // aka '_action' -> Function to call | AMA.MAfunction (delegate void)
```

***Version with component:***<br>
<img width="429" height="523" alt="image" src="https://github.com/user-attachments/assets/faf372eb-1d34-4c20-b6e9-924652307f55" />

<br><br><br>
<a name="Misc-OnEnd"></a>
## On End
`.OnEnd` is an **extension method** of the Animation object.<br>
This method calls a **given function** when the **animation ends**.

***Code usage:***<br>
```cs
transform.AMAmove(Axis.x, 1f, 2f).OnEnd(() => Debug.Log("Hello world!")); // aka '_action' -> Function to call | AMA.MAfunction (delegate void)
```

***Version with component:***<br>
<img width="429" height="527" alt="image" src="https://github.com/user-attachments/assets/74c08aa2-5e7c-49c7-ab99-fed355030576" />

<br><br><br>
<a name="Misc-From"></a>
## From
`.From` is an **extension method** of the Animation object.<br>
This method changes the **starting value** of the animation. It overrides whatever the initial value is.

***Code usage:***<br>
```cs
transform.AMAmove(Axis.x, 1f, 2f).From(Vector3.zero);
```
> [!WARNING]
> For now, the value to give to the `.From` method depends on the Animated Object's type.<br>
> For `AMAmove`, it's a `Vector3`

***Version with component:***<br>
<img width="428" height="424" alt="image" src="https://github.com/user-attachments/assets/1243ea2e-12be-4f34-8482-59ee09fe9f0d" />

<br><br><br>
<a name="Misc-SetDelay"></a>
## Set Delay
`.SetDelay` is an **extension method** of the Animation object.<br>
This method **adds a delay** to the animation. It is counted in seconds.
> [!NOTE]
> Delaying an animation will also delay the `OnStart` function call.

***Code usage:***<br>
```cs
transform.AMAmove(Axis.x, 1f, 2f).SetDelay(4f); // aka '_delay' -> Seconds to wait for | float
```

***Version with component:***<br>
<img width="428" height="443" alt="image" src="https://github.com/user-attachments/assets/6cb038c4-87db-4cd9-8525-31187af7575a" />

<br><br><br>
<a name="Misc-StopMA"></a>
## Stop MA
`.StopMA` is an **extension method** of a Unity Object.<br>
This method **stops every animation** attached to this object.

***Code usage:***<br>
```cs
transform.StopMA();
```

<br><br><br>
<a name="Misc-StopAll"></a>
## Stop All
`.StopAll` is a **method**.<br>
This method **stops every animation**.

***Code usage:***<br>
```cs
AMAMain.StopAll();
```
