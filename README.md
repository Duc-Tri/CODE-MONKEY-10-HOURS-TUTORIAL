# CODE MONKEY 10 HOURS TUTORIAL

This repo follows the 10h+ Unity tutorial by Code Monkey

[Code Monkey Youtube video link](https://www.youtube.com/watch?v=AmGSEH7QcDg)

[Code Monkey Tutorial Site](https://unitycodemonkey.com/kitchenchaoscourse.php)

01.	00:00:00 Intro, Overview<br/>
			 - Summary of tutorial chapters, links to website, etc.
02.	00:12:24 Final Game Preview<br/>
			 - Overcooked like demo
03.	00:18:13 What you Should Know
04.	00:20:28 Create Project<br/>
			 - 3D (URP), check parameters
05.	00:25:21 Unity Layout<br/>
			 - Windows, Logs options
06.	00:32:02 Visual Studio<br/>
			 - Package Unity, Viasfora extension, coding style
07.	00:35:39 Code Style, Naming Rules<br/>
			 - PascalCase, CamelCase, SnakeCase
08.	00:39:30 Importing Assets
09.	00:41:19 Post Processing<br/>
			 - Different effects
10.	00:55:47 Character Controller<br/>
			 - Separate logic from visual in hierarchy<br/>
			 - Mind the input vector magnitude<br/>
			 - Time.deltaTime
11.	01:14:50 Character Visual, Rotation<br/>
			 - Child object doesn't move
12.	01:22:59 Animations<br/>
			 - Separate Animation from logic, with new MonoBehavior
13.	01:42:42 Cinemachine<br/>
			 - Virtual Camera with lots of effects
14.	01:48:32 Input System Refactor<br/>
			 - Import input package<br/>
			 - InputActions methods<br/>
			 - Binding for gamepad
15.	02:04:08 Collision Detection<br/>
			 - Physics.Raycast(), better : Physics.CapsuleCast()<br/>
			 - Make smooth movement while diagonaling !
16.	02:17:02 Clear Counter<br/>
			 - Separate logic from visual<br/>
			 - Dont use Tags !<br/>
			 - Raycast / RaycastAll with Layermask
17.	02:38:18 Interact Action, C# Events<br/>
			 - EventHandler
18.	02:47:42 Selected Counter Visual, Singleton Pattern<br/>
			 - Custom EventArgs<br/>
			 - Awake:parent initialisations / Start:children subscriptions
19.	03:11:18 Kitchen Object, Scriptable Objects<br/>
			 - Instantiate<br/>
			 - ScriptableObjects
20.	03:24:46 Kitchen Object Parent<br/>
			 - Spawn kitchen objects from counter to another counter
21.	03:37:47 Player Pick up, C# Interfaces<br/>
			 - Player & Counters implement same interface !
22.	03:49:23 Container Counter<br/>
			 - BaseCounter inheritance<br/>
			 - Basic animator
23.	04:13:02 Player Pick up, Drop Objects<br/>
			 - Prefab variants, with SCriptableObjects
24.	04:23:37 Cutting Counter, Interact Alternate<br/>
			 - Add more input
25.	04:37:10 Cutting Recipe SO<br/>
			 - Not all objects can be cut !
26.	04:46:33 Cutting Progress, World Canvas<br/>
			 - Canvas, Image.FillAmount & EventHandler
27.	05:05:51 Look At Camera<br/>
			 - LateUpdate(), Camera.main is cached
28.	05:14:24 Trash Counter<br/>
			 - Reuse of inheritance previously done !
29.	05:19:33 Stove Counter, State Machine<br/>
			 - Simple States (switch)<br/>
			 - Visual Particles Effect<br/>
			 - Progress Bar interface with EventHandler
30.	05:56:04 Plates Counter<br/>
			 - Visual dummies different from real spawns
31.	06:08:05 Plate Pick up Objects<br/>
			 - Both ways : plate on counter & ingredient on counter
32.	06:22:10 Plate Complete Visual<br/>
			 - Serializable a struct to expose in editor
33.	06:30:19 Plate World UI Icons<br/>
			 - Canvas UI icons, with a template (no prefab)
34.	06:44:24 Delivery Counter, Shader Graph<br/>
			 - Shader Graph gives a lot of possibilities !
35.	07:05:45 Delivery Manager<br/>
			 - Smart use of ScriptableObjects
36.	07:21:20 Delivery Manager UI<br/>
			 - Use of UI templates (everytime, destroy old & spawn new ones)
			 - EventHandlers all the way !
37.	07:39:24 Music<br/>
			 +
38.	07:43:22 Sound Effects<br/>
			 +
39.	08:06:36 Game Start<br/>
			 #
40.	08:21:20 Game Over<br/>
			 #
41.	08:30:13 Main Menu, Loading<br/>
			 #
42.	08:48:43 Pause, Clear Statics<br/>
			 #
43.	09:07:18 Options, Audio Levels<br/>
			 @
44.	09:22:20 Options, Key Rebinding<br/>
			 @
45.	09:45:29 Controller Input, Menu Navigation<br/>
			 @
46.	10:00:36 Polish<br/>
			 @
47.	10:44:01 Final Game<br/>
			 @
48.	10:47:30 CONGRATULATIONS!
