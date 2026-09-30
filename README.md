<img width="1776" height="220" alt="Ama Motion Automatiser Logo" src="https://github.com/user-attachments/assets/7ad13ea0-7f33-423a-a3f1-4c05c42cc742" />

> Coded by Amaryne B.

*A simple way to animate object motions in Unity. Heavily inspired by the [DoTween API](https://dotween.demigiant.com).*

<a name="Download"></a>
# [Download (Unity Package)](https://raw.github.com/TasDeNeige/AmaMotionAutomatiser/main/AmaMotionAutomatiser.unitypackage)
*This version was made primarily for Unity 6, but also works with older versions (such as Unity 2022).*
<br>

# Guide Overview
### Introduction
- [Install](#Introduction-Install)
- [General Knowledge](#Introduction-GeneralKnowledge)
- [How to use](#Introduction-HowToUse)
- [Available features](#Introduction-AvailableFeatures)

<br><br><br><br>

# Introduction
<a name="Introduction-Install"></a>
## Install
In order to install the Ama Motion Automatiser to one of your project, please [download the package](#Download) available on this Github page.<br>
Dropping it in your Unity project should be enough to start using the AMA.

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











<br><br><br>
# Available Features
<a name="Introduction-AvailableFeatures"></a>
This is an overview of the available features. For more information, please check the [Wiki](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki).

## [Move](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Animations#move)  

![AMAMove_Showcase](https://github.com/user-attachments/assets/e2a8dce1-2da9-44fa-9727-75f2bb1b6fd6)

`AMAmove` is used to move an object via its `transform`'s position.<br><br>

<br><br>
<a name="Animations-MoveUI"></a>
## [Move UI](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Animations#move-ui)

![AMAMoveUI_Showcase](https://github.com/user-attachments/assets/2254383e-1118-4b42-baaa-3de71a8cf360)

`AMAmove` is used to move an object via its `rectTransform`'s position.<br><br>

<br><br>
<a name="Animations-Rotate"></a>
## [Rotate](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Animations#rotate)

![AMARotate_Showcase](https://github.com/user-attachments/assets/d8397270-7029-4a2b-8598-c5129d607677)

`AMArotate` is used to rotate an object via its `transform`'s rotation.<br><br>

<br><br>
<a name="Animations-Scale"></a>
## [Scale](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Animations#scale)

![AMAScale_Showcase](https://github.com/user-attachments/assets/1bcc63ae-a907-497b-b5c5-2a36153847b7)

`AMAscale` is used to scale an object via its `transform`'s scale.<br><br>

<br><br>
<a name="Animations-Fade"></a>
## [Fade](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Animations#fade)

![AMAFade_Showcase](https://github.com/user-attachments/assets/923135d4-06ec-4fd2-bd91-ce5eb9d0510c)

`AMAfade` is used to fade a component's color between two colors.<br>

<br><br>
<a name="Animations-Shake"></a>
## [Shake](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Animations#shake)

![AMAShake_Showcase](https://github.com/user-attachments/assets/f06360b5-bc43-4c8e-a516-0b831fee4aa9)

`AMAshake` is used to create a 'shake' effect on an object's `transform`'s position.<br>
This function comes with two more parameters, such as the Shake Radius and the Delay Between Shakes.<br><br>
















<br><br><br>
# Miscellaneous
<a name="Misc-SetCurve"></a>
## [Set Curve](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#set-curve)
`.SetCurve` is an **extension method** of the Animation object.<br>
This function sets the curve used to moderate the animation's movement.<br><br>

Robert Penner's curves are predefined in the AMA and are available to use. They are stored in the `AMA.Axis` enum.<br>
You can also use custom curves for animations thanks to Unity's `AnimationCurve`.

<br>
A Curve Selector is also available with the 'Select curve' button for easier preview.<br>
<img width="686" height="537" alt="image" src="https://github.com/user-attachments/assets/3d0a61bc-6b6c-4117-9fc8-81c1fc9d4888" />



<br><br>
<a name="Misc-SnapEndValue"></a>
## [Snap to end value](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#snap-to-end-value)
`_snapToEndValue` is an **optional argument** present at the end of every AMA animation function.<br>
It is used to <ins>force apply</ins> the animation's end value to the object when the animation ends.<br>

> [!IMPORTANT]
> This variable is defaulted to true.

<br><br>
<a name="Misc-OnStart"></a>
## [On Start](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#on-start)
`.OnStart` is an **extension method** of the Animation object.<br>
This method calls a **given function** when the **animation starts**.
> [!NOTE]
> If the animation has a delay, it will be considered as 'started' when **the delay ends**.

<br><br>
<a name="Misc-OnEnd"></a>
## [On End](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#on-end)
`.OnEnd` is an **extension method** of the Animation object.<br>
This method calls a **given function** when the **animation ends**.

<br><br>
<a name="Misc-From"></a>
## [From](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#from)
`.From` is an **extension method** of the Animation object.<br>
This method changes the **starting value** of the animation. It overrides whatever the initial value is.

<br><br>
<a name="Misc-SetDelay"></a>
## [Set Delay](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#set-delay)
`.SetDelay` is an **extension method** of the Animation object.<br>
This method **adds a delay** to the animation. It is counted in seconds.
> [!NOTE]
> Delaying an animation will also delay the `OnStart` function call.

<br><br>
<a name="Misc-StopMA"></a>
## [Stop MA](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#stop-ma)
`.StopMA` is an **extension method** of a Unity Object.<br>
This method **stops every animation** attached to this object.

<br><br>
<a name="Misc-StopAll"></a>
## [Stop All](https://github.com/TasDeNeige/AmaMotionAutomatiser/wiki/Miscellaneous#stop-all)
`.StopAll` is a **method**.<br>
This method **stops every animation**.

***Code usage:***<br>
```cs
AMAMain.StopAll();
```
