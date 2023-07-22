using System;
using System.Collections;

using MPW;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModSettings
{
	public static Settings CurrentSettings;
	public class Settings
	{
		[Setting(SettingCategory.General, "Setting")]
		public bool Setting = false;
	}

	public const string JsonPath = "Mod_settings.json";

	/// <summary>
	/// Usually called in method OnLoad().
	/// </summary>
	public static void Load()
	{
		try
		{
			CurrentSettings = ModAPI.DeserialiseJSON<Settings>(JsonPath);
			Debug.Log($"{MPWAPI.ModMeta.Name}: Successful loading of <b>{JsonPath}</b>");
		}
		catch (Exception ex)
		{
			Debug.LogError($"{MPWAPI.ModMeta.Name}: Failed to load <b>{JsonPath}</b> : {ex.Message}");
			ResetSettings();
		}
	}
	public static void Save()
	{
		if (CurrentSettings == null)
		{
			ResetSettings();
		}

		try
		{
			ModAPI.SerialiseJSON(CurrentSettings, JsonPath);
			Debug.Log($"{MPWAPI.ModMeta.Name}: Successfully saving <b>{JsonPath}</b>");
		}
		catch (Exception ex)
		{
			Debug.Log($"{MPWAPI.ModMeta.Name}: Failed to save <b>{JsonPath}</b> : {ex.Message}");
		}
		ModAPI.Notify($"{MPWAPI.ModMeta.Name}: Saved");
	}

	public static Settings ResetSettings()
	{
		CurrentSettings = new Settings();
		try
		{
			ModAPI.DeleteJSON(JsonPath);
		}
		catch (Exception ex)
		{
			Debug.Log($"{MPWAPI.ModMeta.Name}: Failed to reset settings <b>{JsonPath}</b> : {ex.Message}");
		}
		ModAPI.Notify($"{MPWAPI.ModMeta.Name}: Settings reset");
		return CurrentSettings;
	}
}
public class ModMenu : MonoBehaviour
	{
		public static ModMenu LastOpenedModMenu;

		public NormalWindowShell Window;

		private const string WindowTitlePrefix = "[Mod] ";

		public class Tab
		{
			public readonly string Name;
			public readonly Sprite Icon;
			public GameObject RootObject;
			public Action CreateAction;

			public Tab(string name, Sprite icon)
			{
				Name = name;
				Icon = icon;
			}
		}
		public Tab CurrentTab;

		private Tab Menu;
		private Tab Settings;
		private Tab Info;
		private Tab Changelog;

		private (string, (string, Sprite, bool, string)[])[] InfoCategoryTuple;

		protected ScrollRect ScrollRect;

		protected Button BackButton;
		protected UnityAction BackAction;

		public static void CreateMenu()
		{
			(NormalWindowShell window, _) = MPWAPI.CreateNormalWindow();
			ModMenu modMenu = window.Window.gameObject.AddComponent<ModMenu>();
			modMenu.Window = window;
			modMenu.Initialize();
		}

		private void SetTabs()
		{
			Menu = new Tab("Menu", ModAPI.LoadSprite("sprites/Window/Menu Icon.png", 1f));
			Settings = new Tab("Settings", MPWAPI.GetSprite("SettingsIcon"));
			Info = new Tab("Info", ModAPI.LoadSprite("sprites/Window/Info Icon.png", 1f));
			Changelog = new Tab("Changelog", ModAPI.LoadSprite("sprites/Window/Changelog Main.png", 1f));
		}
		private void SetInfoCategoryTuple()
		{
			InfoCategoryTuple = new (string, (string, Sprite, bool, string)[])[] //my cutie :3
			{
				("Basics", new (string, Sprite, bool, string)[] //* category name, (button name, icon, active, desc)[]
				{
					("Button", ModAPI.LoadSprite("Thumbnail/.png", 1f, false), true, "<b><size=25>Title</size></b>"),
					
				}),
			};
		}

		public void Initialize() //* Awake not work
		{
			SetTabs();
			SetInfoCategoryTuple();

			CurrentTab = Menu;
			UpdateWindow();

			(RectTransform grid, GridLayoutGroup _) = UIExtensions.CreateGridLayoutGroup(Window.Viewport);

			ScrollRect = MPWAPI.CreateScrollRect(Window.Viewport);
			ScrollRect.content = grid;
			SetScroll(false);

			Menu.RootObject = grid.gameObject;

			//* Back button
			BackButton = MPWAPI.AddTopRightButton(Window, MPWAPI.GetSprite("BackButton"), MPWAPI.GetSprite("BackButtonHighlighted"), MPWAPI.GetSprite("BackButtonDisabled"), Back);
			BackButton.name = "Back button";
			BackButton.interactable = false;

			//* Settings
			MPWAPI.CreateAdvancedButton(grid, Settings.Name, Settings.Icon, CreateSettings);
			MPWAPI.CreateAdvancedButton(grid, Info.Name, Info.Icon, CreateInfo);
			MPWAPI.CreateAdvancedButton(grid, Changelog.Name, Changelog.Icon, CreateChangelog);

			LastOpenedModMenu = this;
		}

		public void CreateSettings()
		{
			StartCoroutine(CreateSettingsRoutine());
		}
		protected IEnumerator CreateSettingsRoutine()
		{
			yield return null;
			SetScroll(true);
			BackButton.interactable = true;
			Menu.RootObject.SetActive(false);
			CurrentTab = Settings;
			UpdateWindow();

			RectTransform settingsRoot = MPWAPI.CreateSettings<ModSettings, ModSettings.Settings>(Window.Viewport, ModSettings.CurrentSettings);
			ScrollRect.content = settingsRoot;
			CurrentTab.RootObject = settingsRoot.parent.gameObject;

			BackAction = ToMenu;
		}

		public void CreateInfo()
		{
			StartCoroutine(CreateInfoRoutine());
		}
		protected IEnumerator CreateInfoRoutine()
		{
			yield return null;
			SetScroll(true);
			BackButton.interactable = true;
			Menu.RootObject.SetActive(false);
			CurrentTab = Info;
			UpdateWindow();

			(RectTransform verticalRectTransform, RectTransform textBackground) info = (null, null);

			void backAction()
			{
				Destroy(info.verticalRectTransform.gameObject);
				Destroy(info.textBackground.gameObject);
				ToMenu();
			}

			info = MPWAPI.CreateInfo(Window.Viewport, InfoCategoryTuple, (button) =>
			{
				Window.Name = WindowTitlePrefix + button.Item1;
				Window.Icon = button.Item2;
				ScrollRect.content = info.textBackground;
				UIExtensions.ApplyScrollPosition(ScrollRect, 1f);

				BackAction = () =>
				{
					info.textBackground.gameObject.SetActive(false);

					info.verticalRectTransform.gameObject.SetActive(true);
					ScrollRect.content = info.verticalRectTransform;

					UpdateWindow();

					BackAction = backAction;
				};
			});
			ScrollRect.content = info.verticalRectTransform;
			UIExtensions.ApplyScrollPosition(ScrollRect, 1f);

			BackAction = backAction;
		}

		public void CreateChangelog()
		{
			StartCoroutine(CreateChangelogRoutine());
		}
		protected IEnumerator CreateChangelogRoutine()
		{
			yield return null;

			SetScroll(true);
			BackButton.interactable = true;
			Menu.RootObject.SetActive(false);
			CurrentTab = Changelog;
			UpdateWindow();

			(string, Color, string[])[] changelog = new (string, Color, string[])[]
			{
				("Main changes", new Color(0.66f, 0.42f, 0.76f), new string[]
				{
				}),
				("New", new Color(0.96f, 0.87f, 0.16f), new string[]
				{
				}),
				("Refinement", new Color(0.48f, 0.78f, 0.42f), new string[]
				{
				}),
				("Redrawn", new Color(1f, 0.69f, 0.25f), new string[]
				{
				}),
				("Removed", new Color(0.81f, 0.32f, 0.24f), new string[]
				{
				}),
				("Optimized", new Color(0.43f, 0.93f, 0.66f), new string[]
				{
				}),
				("Fixed", new Color(1f, 0.56f, 0.83f), new string[]
				{
				}),
			};

			RectTransform changelogRoot = MPWAPI.CreateChangelog(Window.Viewport, changelog);

			ScrollRect.content = changelogRoot;
			CurrentTab.RootObject = changelogRoot.gameObject;
			BackAction = ToMenu;
		}

		public void Back()
		{
			BackAction();
		}

		protected void ToMenu()
		{
			Menu.RootObject.SetActive(true);
			SetScroll(false);
			BackButton.interactable = false;

			Destroy(CurrentTab.RootObject);

			CurrentTab = Menu;
			UpdateWindow();
		}
		protected void UpdateWindow()
		{
			Window.Name = WindowTitlePrefix + CurrentTab.Name;
			Window.Icon = CurrentTab.Icon;
		}
		protected void SetScroll(bool active)
		{
			ScrollRect.enabled = active;
			ScrollRect.verticalScrollbar.interactable = active;
		}
	}
