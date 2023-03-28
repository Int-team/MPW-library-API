using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MPW
{
	//internal static class RectTransformExtensions
	//{
	//	public static void SetDefaultScale(this Transform transform)
	//	{
	//		transform.localScale = Vector3.one;
	//	}
	//	//public static void SetDefaultScale(this RectTransform transform)
	//	//{
	//	//	transform.localScale = new Vector3(1f, 1f, 1f);
	//	//}

	//	public static void SetPivotAndAnchorsAtPoint(this RectTransform transform, Vector2 point)
	//	{
	//		transform.pivot = point;
	//		transform.anchorMin = point;
	//		transform.anchorMax = point;
	//	}
	//	public static void SetPivotAndAnchors(this RectTransform transform, Vector2 pivot, Vector2 anchorMin, Vector2 anchorMax)
	//	{
	//		transform.pivot = pivot;
	//		transform.anchorMin = anchorMin;
	//		transform.anchorMax = anchorMax;
	//	}

	//	#region Get Size
	//	public static Vector2 GetSize(this RectTransform transform)
	//	{
	//		return transform.rect.size;
	//	}
	//	public static float GetWidth(this RectTransform transform)
	//	{
	//		return transform.rect.width;
	//	}
	//	public static float GetHeight(this RectTransform transform)
	//	{
	//		return transform.rect.height;
	//	}
	//	#endregion

	//	#region Set Size
	//	public static void SetSize(this RectTransform transform, Vector2 newSize)
	//	{
	//		SetWidth(transform, newSize.x);
	//		SetHeight(transform, newSize.y);
	//	}
	//	public static void SetWidth(this RectTransform transform, float newSize)
	//	{
	//		transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize);
	//	}
	//	public static void SetHeight(this RectTransform transform, float newSize)
	//	{
	//		transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize);
	//	}
	//	#endregion

	//	#region Set Position
	//	/*public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos) 
	//	{
	//		trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
	//	}

	//	public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos) 
	//	{
	//		trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
	//	}
	//	public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos) 
	//	{
	//		trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
	//	}
	//	public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos) 
	//	{
	//		trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
	//	}
	//	public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos) 
	//	{
	//		trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
	//	}*/
	//	#endregion
	//}

	internal static class TypeExtensions
	{
		#region Instance
		//* Property
		public static T GetPropertyValue<T>(this Type type, string name, object obj)
		{
			PropertyInfo propertyInfo = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
			return (T)propertyInfo.GetValue(obj);
		}
		public static void SetPropertyValue(this Type type, string name, object obj, object value)
		{
			PropertyInfo propertyInfo = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
			propertyInfo.SetValue(obj, value);
		}

		//* Field
		public static T GetFieldValue<T>(this Type type, string name, object obj)
		{
			FieldInfo fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public);
			return (T)fieldInfo.GetValue(obj);
		}
		public static void SetFieldValue(this Type type, string name, object obj, object value)
		{
			FieldInfo fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public);
			fieldInfo.SetValue(obj, value);
		}

		//* Method
		public static void InvokeMethod(this Type type, string name, object obj, params object[] args)
		{
			MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
			methodInfo.Invoke(obj, args);
		}
		public static T InvokeMethod<T>(this Type type, string name, object obj, params object[] args)
		{
			MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
			return (T)methodInfo.Invoke(obj, args);
		}
		#endregion

		#region Static
		//* Property
		internal static T GetStaticPropertyValue<T>(this Type type, string name)
		{
			PropertyInfo propertyInfo = type.GetProperty(name, BindingFlags.Static | BindingFlags.Public);
			return (T)propertyInfo.GetValue(null);
		}
		internal static void SetStaticPropertyValue(this Type type, string name, object value)
		{
			PropertyInfo propertyInfo = type.GetProperty(name, BindingFlags.Static | BindingFlags.Public);
			propertyInfo.SetValue(null, value);
		}

		//* Field
		internal static T GetStaticFieldValue<T>(this Type type, string name)
		{
			FieldInfo fieldInfo = type.GetField(name, BindingFlags.Static | BindingFlags.Public);
			return (T)fieldInfo.GetValue(null);
		}
		internal static void SetStaticFieldValue(this Type type, string name, object value)
		{
			FieldInfo fieldInfo = type.GetField(name, BindingFlags.Static | BindingFlags.Public);
			fieldInfo.SetValue(null, value);
		}

		//* Method
		internal static void InvokeStaticMethod(this Type type, string name, params object[] args)
		{
			MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public);
			if (methodInfo == null)
			{
				return;
			}
			methodInfo.Invoke(null, args);
		
		}
		internal static T InvokeStaticMethod<T>(this Type type, string name, params object[] args)
		{
			Debug.Log("InvokeStaticMethod: " + type + name + string.Concat(args));
			MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public);
			if (methodInfo == null)
			{
				return default(T);
			}

			return (T)methodInfo.Invoke(null, args);
		}
		internal static T InvokeStaticMethod<T>(this Type type, string name, Type[] types, params object[] args)
		{
			MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public, null, types, null);
			if (methodInfo == null)
			{
				return default(T);
			}

			return (T)methodInfo.Invoke(null, args);
		}
		#endregion
	}

	/// <summary>
	/// API for working with the MPW library.
	/// </summary>
	public static class MPWAPI //* I hope Zooi makes a "Mod dependency system" soon.
	{
		public static bool MPWFinded { get; private set; } = false;
		private const int NumberOfSearchCycles = 20;

		public static Transform SelectedWindowTransform => WindowManagerBehaviourType.GetStaticPropertyValue<Transform>("SelectedWindowTransform");
		public static MonoBehaviour[] Windows => WindowManagerBehaviourType.GetStaticPropertyValue<MonoBehaviour[]>("Windows");

		public static Transform WindowsCanvas => WindowManagerBehaviourType.GetStaticFieldValue<Transform>("WindowsCanvas");
		public static Transform ContextMenuCanvas => WindowManagerBehaviourType.GetStaticFieldValue<Transform>("ContextMenuCanvas");

		public static RectTransform GridlWithMinimizedWindows => WindowManagerBehaviourType.GetStaticFieldValue<RectTransform>("GridlWithMinimizedWindows");

		private static Type WindowManagerBehaviourType;
		private static Type MPWUIBuilderType;
		private static Type ResourcesType;

		private static MethodInfo CreateWindowMethod;
		private static MethodInfo CreateWarningWindowMethod;
		private static MethodInfo CreateWarningWindowWithButtonsMethod;
		private static MethodInfo CreateNormalWindowMethod;

		private static MethodInfo CreateAdvancedButtonMethod;

		private static ModMetaData ModMeta;

		#region [_]
		/// <summary>
		/// Should be called in the <c>OnLoad()</c> method.
		/// </summary>
		public static void Initialize()
		{
			ModMeta = ModAPI.Metadata;
			StartSearchingMPW();
		}

		private static bool TryGetMPW()
		{
			ModScript modScript = null;
			foreach (KeyValuePair<ModMetaData, ModScript> pair in ModLoader.ModScripts)
			{
				if (pair.Key.Active && pair.Key.Name == "MPW library" && pair.Key.Author == "Int Team")
				{
					modScript = pair.Value;
					break;
				}
			}
			if (modScript == null)
			{
				return false;
			}

			Type windowManager = modScript.LoadedAssembly.GetType("MPW.WindowManagerBehaviour");
			if (windowManager == null)
			{
				return false;
			}
			WindowManagerBehaviourType = windowManager;
			MPWFinded = true;
			
			GetMemders(modScript);
			return true;
		}
		private static void GetMemders(ModScript script)
		{
			Type stringType = typeof(string).MakeByRefType();
			Type spriteType = typeof(Sprite).MakeByRefType();

			CreateWindowMethod = WindowManagerBehaviourType.GetMethod("CreateWindow", BindingFlags.Static | BindingFlags.Public, null, new Type[2]
			{
				stringType,
				spriteType,
			}, null);
			CreateWarningWindowMethod = WindowManagerBehaviourType.GetMethod("CreateWarningWindow", BindingFlags.Static | BindingFlags.Public, null, new Type[3]
			{
				stringType,
				spriteType,
				stringType

			}, null);
			CreateWarningWindowWithButtonsMethod = WindowManagerBehaviourType.GetMethod("CreateWarningWindowWithButtons", BindingFlags.Static | BindingFlags.Public, null, new Type[4]
			{
				stringType,
				spriteType,
				stringType,
				typeof(UnityAction).MakeByRefType()

			}, null);
			CreateNormalWindowMethod = WindowManagerBehaviourType.GetMethod("CreateNormalWindow", BindingFlags.Static | BindingFlags.Public, null, new Type[2]
			{
				stringType,
				spriteType,
			}, null);
			
			MPWUIBuilderType = script.LoadedAssembly.GetType("MPW.MPWUIBuilder");
			CreateAdvancedButtonMethod = MPWUIBuilderType.GetMethod("CreateAdvancedButton", BindingFlags.Static | BindingFlags.Public, null, new Type[4]
			{
				typeof(Transform).MakeByRefType(),
				stringType,
				spriteType,
				typeof(UnityAction).MakeByRefType()
			}, null);

			ResourcesType = script.LoadedAssembly.GetType("MPW.Resources");
		}

		public static void StartSearchingMPW()
		{
			BackgroundItemLoader.Instance.StartCoroutine(SearchingMPWRoutine());
		}
		private static IEnumerator SearchingMPWRoutine()
		{
			for (int i = 0; i < NumberOfSearchCycles; i++)
			{
				yield return new WaitForSecondsRealtime(0.5f);
				if (TryGetMPW())
				{
					Debug.LogWarning(ModMeta.Name + ": MPWAPI: MPW finded! Search cycles: " + i);
					yield break;
				}
			}

			#region Check DialogBox
			bool dialogBoxAlreadyCreated = false;
			if (DialogBox.IsAnyDialogboxOpen)
			{
				DialogBox[] dialogBoxes = GameObject.Find("/Canvas/Dialog container").GetComponentsInChildren<DialogBox>(false);
				foreach (DialogBox box in dialogBoxes)
				{
					if (box.Title.StartsWith("MPW library not found"))
					{
						dialogBoxAlreadyCreated = true;
						box.DialogButtonHolder.GetChild(1).GetComponent<Button>().onClick.AddListener(StartSearchingMPW);
						break;
					}
				}
			}
			if (!dialogBoxAlreadyCreated)
			{
				DialogBox dialog = DialogBoxManager.Dialog("MPW library not found, may not be installed or enabled.", new DialogButton[3]
				{
					new DialogButton("Install", true, () =>
					{
						OpenLink("https://steamcommunity.com/sharedfiles/filedetails/?id=2953788932");
					}),
					new DialogButton("Retry", true, () =>
					{
						StartSearchingMPW();
					}),
					new DialogButton("Cancel", true),
				});
			}
			#endregion
		}

		private static void CheckMPW()
		{
			if (!MPWFinded)
			{
				throw new Exception("MPW library not found.");
			}
		}
		#endregion

		#region WindowManager
		/// <summary>
		/// Creates a window.
		/// </summary>
		/// <param name="name">Window name.</param>
		/// <param name="icon">Window icon.</param>
		/// <returns>A tuple with a <see cref="WindowShell"/> and its viewport (<see cref="RectTransform"/>).</returns>
		public static (WindowShell, RectTransform) CreateWindow(in string name = "New window", in Sprite icon = null)
		{
			CheckMPW();

			(MonoBehaviour behaviour, RectTransform rectT) = ((MonoBehaviour, RectTransform))CreateWindowMethod.Invoke(null, new object[2]
			{
				string.IsNullOrEmpty(name) ? Type.Missing : name,
				icon ?? Type.Missing,
			});

			WindowShell window = ScriptableObject.CreateInstance<WindowShell>();
			window.Initialize(behaviour);
			return (window, rectT);
		}

		/// <summary>
		/// Creates a warning window, with added <paramref name="text"/> and a vertical layout group. The window changes to fit the size of the content.
		/// </summary>
		/// <param name="text">Text in the window.</param>
		/// <returns>A tuple with a <see cref="WarningWindowShell"/> and its viewport (<see cref="RectTransform"/>).</returns>
		/// <inheritdoc cref="CreateWindow(string, Sprite)"/>
		public static (WarningWindowShell, RectTransform) CreateWarningWindow(in string name = null, in string text = null)
		{
			return CreateWarningWindow(name, null, text);
		}

		/// <inheritdoc cref="CreateWarningWindow(string, string)"/>
		public static (WarningWindowShell, RectTransform) CreateWarningWindow(in string name = null, in Sprite icon = null, in string text = null)
		{
			CheckMPW();
			(MonoBehaviour behaviour, RectTransform rectT) = ((MonoBehaviour, RectTransform)) CreateWarningWindowMethod.Invoke(null, new object[3]
			{ 
				string.IsNullOrEmpty(name) ? Type.Missing : name,
				icon ?? Type.Missing,
				text ?? Type.Missing 
			});

			WarningWindowShell window = ScriptableObject.CreateInstance<WarningWindowShell>();
			window.Initialize(behaviour);
			return (window, rectT);
		}

		///	<summary>
		///	Creates a warning window with <paramref name="text"/> added, 2 buttons at the bottom: Ok, Cancel and a vertical layout group. The window changes according to the size of the content.
		///	</summary>
		/// <inheritdoc cref="CreateWarningWindow(string, string)"/>
		/// <param name="okAction">Action when the OK button is pressed.</param>
		public static (WarningWindowShell, RectTransform) CreateWarningWindowWithButtons(in string name = "New window", in string text = null, in UnityAction okAction = null)
		{
			return CreateWarningWindowWithButtons(name, null, text, okAction);
		}

		///<inheritdoc cref="CreateWarningWindowWithButtons(string, string, UnityAction)"/>
		public static (WarningWindowShell, RectTransform) CreateWarningWindowWithButtons(in string name = "New window", in Sprite icon = null, in string text = null, in UnityAction okAction = null)
		{
			CheckMPW();
			(MonoBehaviour behaviour, RectTransform rectT) = ((MonoBehaviour, RectTransform)) CreateWarningWindowWithButtonsMethod.Invoke(null, new object[4]
			{
				string.IsNullOrEmpty(name) ? Type.Missing : name,
				icon ?? Type.Missing,
				text ?? Type.Missing,
				okAction ?? Type.Missing 
			});

			WarningWindowShell window = ScriptableObject.CreateInstance<WarningWindowShell>();
			window.Initialize(behaviour);
			return (window, rectT);
		}

		/// <summary>
		/// Creates a window that can be minimized.
		/// </summary>		
		/// <returns>A tuple with a <see cref="NormalWindowShell"/> and its viewport (<see cref="RectTransform"/>).</returns>
		/// <inheritdoc cref="CreateWindow(string, Sprite)"/>
		public static (NormalWindowShell, RectTransform) CreateNormalWindow(in string name = "New window", in Sprite icon = null)
		{
			CheckMPW();

			(MonoBehaviour behaviour, RectTransform rectT) = ((MonoBehaviour, RectTransform)) CreateNormalWindowMethod.Invoke(null, new object[2]
			{ 
				string.IsNullOrEmpty(name) ? Type.Missing : name,
				icon ?? Type.Missing,
			});

			NormalWindowShell window = ScriptableObject.CreateInstance<NormalWindowShell>();
			window.Initialize(behaviour);
			return (window, rectT);
		}
		#endregion

		#region Resources
		/// <summary>
		/// Get a sprite by <paramref name="name"/> from the MPW.
		/// </summary>
		/// <remarks>
		/// <b>Sprites:</b>
		/// CloseButton <br/>
		/// CloseButtonHighlighted <br/>
		/// MinimizeButton <br/>
		/// MinimizeButtonHighlighted <br/>
		/// BackButton <br/>
		/// BackButtonHighlighted <br/>
		/// BackButtonDisabled <br/>
		/// <br/>
		/// None <br/>
		/// Warning <br/>
		/// <br/>
		/// Checkbox <br/>
		/// CheckboxToggle <br/>
		/// Button <br/>
		/// Confirm <br/>
		/// CollapseButton <br/>
		/// CollapseListButtonHighlighted <br/>
		/// <br/>
		/// MPWMenuIcon <br/>
		/// MPWSettingsIcon <br/>
		/// SettingsIcon <br/>
		/// </remarks>
		/// <returns>
		/// Found <see cref="Sprite"/>, if not found, returns None sprite (red question mark).
		/// </returns>
		public static Sprite GetSprite(in string name)
		{
			CheckMPW();

			Type sprites = ResourcesType.GetNestedTypes(BindingFlags.Public).First((Type type) => type.Name == "Sprites");
			return sprites.GetStaticFieldValue<Sprite>(name) ?? sprites.GetStaticFieldValue<Sprite>("None");
		}
		#endregion

		#region MPWUIBuilder
		/// <summary>
		/// Play the blip if the MPW settings allow it.
		/// </summary>
		public static void Blip()
		{
			CheckMPW();
			MPWUIBuilderType.InvokeStaticMethod("Blip");
		}

		/// <summary>
		/// Creates a button with an <paramref name="icon"/> and <paramref name="text"/> below it.
		/// </summary>
		/// <param name="parent">The parent to which the button will be added.</param>
		/// <param name="buttonAction">Action when pressed.</param>
		/// <returns></returns>
		public static (RectTransform, Button) CreateAdvancedButton(in Transform parent, in string text = null, in Sprite icon = null, UnityAction buttonAction = null)
		{
			CheckMPW();
			return ((RectTransform, Button)) CreateAdvancedButtonMethod.Invoke(null, new object[4] {parent, text ?? Type.Missing, icon ?? Type.Missing, buttonAction ?? Type.Missing });
		}
		
		/// <summary>
		/// Creates a <see cref="Scrollbar"/> and adds a <see cref="ScrollRect"/> to the <paramref name="parent"/>.
		/// </summary>
		/// <returns>Added <see cref="ScrollRect"/>.</returns>
		public static ScrollRect CreateScrollRect(in Transform parent)
		{
			CheckMPW();
			return MPWUIBuilderType.InvokeStaticMethod<ScrollRect>("CreateScrollRect", new Type[1] { typeof(Transform).MakeByRefType()}, parent);
		}

		/// <summary>
		/// Method for creating settings.
		/// </summary>
		/// <remarks>
		/// Settings can have tags, they are written via <c>|</c> after the header.
		/// <code>
		/// [<see cref="SettingAttribute"/>(<see cref="SettingCategory.General"/>, "title|/category name/settings goup|order(from 0)")]
		/// </code>
		/// </remarks>
		/// <typeparam name="C">The class type in which the <c>Save()</c> and <c>ResetSettings()</c> methods are declared.</typeparam>
		/// <typeparam name="S">Class with settings. All public fields, properties and methods with <see cref="SettingAttribute"/> will be used to create settings.</typeparam>
		/// <param name="parent">The parent of the settings root.</param>
		/// <param name="settings">The current instance of the settings class.</param>
		/// <returns>The settings root, which is the parent object for a <see cref="VerticalLayoutGroup"/> with settings and a <see cref="HorizontalLayoutGroup"/> with Save and Reset buttons.</returns>
		public static RectTransform CreateSettings<C, S>(in Transform parent, S settings) where C : class where S : class
		{
			CheckMPW();

			MethodInfo method = MPWUIBuilderType.GetMethod("CreateSettings", BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(typeof(C), typeof(S));
			return (RectTransform) method.Invoke(null, new object[2] { parent, settings });
		}

		/// <summary>
		/// Creates a <see cref="VerticalLayoutGroup"/> with categories that have a colored bar on the left.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="changelog">An array with categories <c>(<see langword="string"/> category name, <see cref="Color"/> bar color, changes array)</c>, inside which there are arrays with changes <c><see langword="string"/> text</c>.</param>
		/// <returns></returns>
		public static RectTransform CreateChangelog(in Transform parent, in (string, Color, string[])[] changelog)
		{
			CheckMPW();

			return MPWUIBuilderType.InvokeStaticMethod<RectTransform>("CreateChangelog", new Type[2]
			{
				typeof(Transform).MakeByRefType(),
				typeof((string, Color, string[])[]).MakeByRefType()

			}, parent, changelog);
		}

		/// <summary>
		/// Creates a <see cref="VerticalLayoutGroup"/> with categories containing advanced buttons . When you click on the button, the text appears.
		/// </summary>
		/// <param name="parent">The parent of the <see cref="VerticalLayoutGroup"/> and text object. It must be a viewport.</param>
		/// <param name="categorysTuple">An array with categories <c>(<see langword="string"/> category name, buttons array)</c>, inside which there are arrays with buttons <c>(<see langword="string"/> name, <see cref="Sprite"/> icon, <see langword="bool"/> active, <see langword="string"/> text)</c>.</param>
		/// <param name="onButtonPressed">Action on button click. The argument is the button that was clicked.</param>
		/// <returns><see cref="RectTransform"/> of <see cref="VerticalLayoutGroup"/> and <see cref="RectTransform"/> of object with text.</returns>
		public static (RectTransform, RectTransform) CreateInfo(in Transform parent, in (string, (string, Sprite, bool, string)[])[] categorysTuple, Action<(string, Sprite, bool, string)> onButtonPressed)
		{
			CheckMPW();

			return MPWUIBuilderType.InvokeStaticMethod<(RectTransform, RectTransform)>("CreateInfo", new Type[3]
			{
				typeof(Transform).MakeByRefType(),
				typeof((string, (string, Sprite, bool, string)[])[]).MakeByRefType(),
				typeof(Action<(string, Sprite, bool, string)>)

			}, parent, categorysTuple, onButtonPressed);
		}
		#endregion

		#region Other
		public static void OpenLink(string url)
		{
			typeof(Utils).GetMethod("OpenURL", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[1] { url });
		}
		/// <summary>
		/// Creates and adds a button to the <see cref="HorizontalLayoutGroup"/> with window buttons (it also contains a close button).
		/// </summary>
		/// <param name="window">The window to which the button will be added to the group.</param>
		/// <param name="sprite">Default button sprite.</param>
		/// <param name="highlightedSprite">Button hover sprite.</param>
		/// <param name="disabledSprite">Disabled button sprite.</param>
		/// <param name="buttonAction">Action on button click.</param>
		/// <returns></returns>
		public static Button AddTopRightButton(in WindowShell window, in Sprite sprite, in Sprite highlightedSprite, in Sprite disabledSprite = null, in UnityAction buttonAction = null)
		{
			CheckMPW();

			Type spriteType = typeof(Sprite).MakeByRefType();
			return WindowManagerBehaviourType.InvokeStaticMethod<Button>("AddTopRightButton", new Type[5]
			{
				typeof(MonoBehaviour).MakeByRefType(),
				spriteType,
				spriteType,
				spriteType,
				typeof(UnityAction).MakeByRefType()

			}, window.Window, sprite, highlightedSprite, disabledSprite ?? Type.Missing, buttonAction ?? Type.Missing);
		}

		/// <summary>
		/// Add a <paramref name="buttonTransform"/> to the MPW button grid (it is located above the tools).
		/// </summary>
		/// <param name="buttonTransform"></param>
		public static void AddButtonToMenuContainer(in Transform buttonTransform)
		{
			CheckMPW();
			buttonTransform.SetParent(GameObject.Find("Canvas/Toolbar/MPW menu/Child container").transform);
		}
		#endregion
	}

	#region Shells
	public class WindowShell : ScriptableObject
	{
		public MonoBehaviour Window;
		public Type WindowType;

		public string Name
		{
			get
			{
				return WindowType.GetPropertyValue<string>("Name", Window);
			}
			set
			{
				WindowType.SetPropertyValue("Name", Window, value);
			}
		}
		public TextMeshProUGUI TitleTMP
		{
			get
			{
				return WindowType.GetFieldValue<TextMeshProUGUI>("TitleTMP", Window);
			}
			set
			{
				WindowType.SetFieldValue("TitleTMP", Window, value);
			}
		}

		public Sprite Icon
		{
			get
			{
				return WindowType.GetPropertyValue<Sprite>("Icon", Window);
			}
			set
			{
				WindowType.SetPropertyValue("Icon", Window, value);
			}
		}
		public Image IconImage
		{
			get
			{
				return WindowType.GetFieldValue<Image>("IconImage", Window);
			}
			set
			{
				WindowType.SetFieldValue("IconImage", Window, value);
			}
		}

		/// <summary>
		/// <see cref="RectTransform"/> an object with a <see cref="HorizontalLayoutGroup"/> with buttons, which is located in the upper right corner of the window.
		/// </summary>
		public RectTransform TopRightButtonsGroupRectTransform
		{
			get
			{
				return WindowType.GetFieldValue<RectTransform>("TopRightButtonsGroupRectTransform", Window);
			}
			set
			{
				WindowType.SetFieldValue("TopRightButtonsGroupRectTransform", Window, value);
			}
		}

		/// <summary>
		/// <see cref="RectTransform"/> an object with a <see cref="RectMask2D"/>.
		/// </summary>
		public RectTransform Viewport
		{
			get
			{
				return WindowType.GetFieldValue<RectTransform>("Viewport", Window);
			}
			set
			{
				WindowType.SetFieldValue("Viewport", Window, value);
			}
		}

		/// <summary>
		/// Called before the window closes.
		/// </summary>
		public UnityEvent OnClose
		{
			get
			{
				return WindowType.GetFieldValue<UnityEvent>("OnClose", Window);
			}
			set
			{
				WindowType.SetFieldValue("OnClose", Window, value);
			}
		}

		/// <summary>
		/// Window protection from closing.
		/// </summary>
		/// <remarks>
		/// Is an <see langword="enum"/> with a [<see cref="FlagsAttribute"/>].
		/// <br/>
		/// <b>Existing protection:</b> <br/>
		/// None = 0 <br/>
		///	AfterMapChange = 1 <br/>
		///	Always = 2 <br/>
		/// </remarks>
		public sbyte CurrentCloseProtection
		{
			get
			{
				return WindowType.GetFieldValue<sbyte>("CurrentCloseProtection", Window);
			}
			set
			{
				WindowType.SetFieldValue("CurrentCloseProtection", Window, value);
			}
		}

		public void Initialize(MonoBehaviour window)
		{
			Window = window;
			WindowType = window.GetType();
			OnClose.AddListener(() =>
			{
				Destroy(this);
			});
		}

		public void SetSize(Vector2 size)
		{
			WindowType.InvokeMethod("SetSize", Window, size);
		}
		public virtual void Close()
		{
			WindowType.InvokeMethod("Close", Window);
		}

		public bool Equals(WindowShell other)
		{
			Debug.Log("WindowShell Equals");
			if (other is null)
			{
				return false;
			}
			return Window == other.Window;
		}
		public override bool Equals(object obj)
		{
			Debug.Log("WindowShell Equals object");
			return Equals(obj as WindowShell);
		}

		public override string ToString()
		{
			return Name;
		}
		public override int GetHashCode()
		{
			return Window.GetHashCode();
		}
	}
	public class WarningWindowShell : WindowShell
	{
		/// <summary>
		/// The text that shows the warning window.
		/// </summary>
		public string Text
		{
			get
			{
				return WindowType.GetPropertyValue<string>("Text", Window);
			}
			set
			{
				WindowType.SetPropertyValue("Text", Window, value);
			}
		}
		public TextMeshProUGUI TextTMP
		{
			get
			{
				return WindowType.GetFieldValue<TextMeshProUGUI>("TextTMP", Window);
			}
			set
			{
				WindowType.SetFieldValue("TextTMP", Window, value);
			}
		}
	}
	public class NormalWindowShell : WindowShell
	{
		/// <value><see langword="true"/> if the window is minimized, <see langword="false"/> otherwise.</value>
		public bool Minimized
		{
			get
			{
				return WindowType.GetPropertyValue<bool>("Minimized", Window);
			}
			set
			{
				WindowType.SetPropertyValue("Minimized", Window, value);
			}
		}
		public MonoBehaviour MinimizedWindowButton
		{
			get
			{
				return WindowType.GetFieldValue<MonoBehaviour>("MinimizedWindowButton", Window);
			}
			set
			{
				WindowType.SetFieldValue("MinimizedWindowButton", Window, value);
			}
		}
	}
	#endregion
}
