using System.Collections;
using System.Reflection;

using UnityEngine;
using UnityEngine.UI;

namespace MPW
{
	internal static class RectTransformExtensions
	{
		public static void SetDefaultScale(this Transform transform)
		{
			transform.localScale = Vector3.one;
		}

		public static void SetPivotAndAnchorsAtPoint(this RectTransform transform, Vector2 point)
		{
			transform.pivot = point;
			transform.anchorMin = point;
			transform.anchorMax = point;
		}
		public static void SetPivotAndAnchors(this RectTransform transform, Vector2 pivot, Vector2 anchorMin, Vector2 anchorMax)
		{
			transform.pivot = pivot;
			transform.anchorMin = anchorMin;
			transform.anchorMax = anchorMax;
		}

		#region Get Size
		public static Vector2 GetSize(this RectTransform transform)
		{
			return transform.rect.size;
		}
		public static float GetWidth(this RectTransform transform)
		{
			return transform.rect.width;
		}
		public static float GetHeight(this RectTransform transform)
		{
			return transform.rect.height;
		}
		#endregion

		#region Set Size
		public static void SetSize(this RectTransform transform, Vector2 newSize)
		{
			SetWidth(transform, newSize.x);
			SetHeight(transform, newSize.y);
		}
		public static void SetWidth(this RectTransform transform, float newSize)
		{
			transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize);
		}
		public static void SetHeight(this RectTransform transform, float newSize)
		{
			transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize);
		}
		#endregion

		#region Set Position
		/*public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos) 
		{
			trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
		}

		public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos) 
		{
			trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
		}
		public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos) 
		{
			trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
		}
		public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos) 
		{
			trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
		}
		public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos) 
		{
			trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
		}*/
		#endregion
	}
	internal static class UIExtensions
	{
		public static void HighlightButton(Button button)
		{
            ColorBlock colors = button.colors;
            colors.normalColor = new Color(1.3f, 1.3f, 1.3f, 1f);
            button.colors = colors;
        }

        public static void ApplyScrollPosition(ScrollRect scrollRect, float verticalPosition)
		{
			scrollRect.StartCoroutine(ApplyScrollPositionRoutine(scrollRect, verticalPosition));
		}
		private static IEnumerator ApplyScrollPositionRoutine(ScrollRect scrollRect, float verticalPosition)
		{
			yield return null;
			scrollRect.verticalNormalizedPosition = verticalPosition;
			LayoutRebuilder.MarkLayoutForRebuild((RectTransform)scrollRect.transform);
			//scrollRect.Rebuild(CanvasUpdate.PostLayout);
			//LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.transform);
		}

		/// <summary>
		/// Creates an object with <see cref="GridLayoutGroup"/> and <see cref="ContentSizeFitter"/>.
		/// </summary>
		/// <param name="parent">The parent object of the grid layout group.</param>
		/// <returns><see cref="RectTransform"/> and <see cref="GridLayoutGroup"/> of the created object.</returns>
		public static (RectTransform, GridLayoutGroup) CreateGridLayoutGroup(in Transform parent)
		{
			GameObject gridObject = new GameObject("Grid layout group");
			gridObject.AddComponent<CanvasRenderer>();

			RectTransform rectTransform = gridObject.AddComponent<RectTransform>();
			rectTransform.SetParent(parent);
			rectTransform.SetDefaultScale();
			rectTransform.SetPivotAndAnchors(new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(1f, 1f));
			//RectTransformExtensions.SetSize(gridObjectImage.rectTransform, new Vector2(95f, 95f));
			//gridObjectImage.rectTransform.localScale = new Vector2(0.95f, 0.95f);
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.anchoredPosition = Vector2.zero;

			GridLayoutGroup gridLayout = gridObject.AddComponent<GridLayoutGroup>();
			gridLayout.padding = new RectOffset(30, 30, 30, 30);
			gridLayout.cellSize = new Vector2(150f, 150f);
			gridLayout.spacing = new Vector2(10f, 10f);
			gridLayout.childAlignment = TextAnchor.MiddleCenter;

			ContentSizeFitter contentSizeFitter = gridObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			return (rectTransform, gridLayout);
		}

		/// <summary>
		/// Creates an object with <see cref="VerticalLayoutGroup"/> and <see cref="ContentSizeFitter"/>.
		/// </summary>
		/// <param name="parent">The parent object of the vertical layout group.</param>
		/// <returns><see cref="RectTransform"/> and <see cref="VerticalLayoutGroup"/> of the created object.</returns>
		public static (RectTransform, VerticalLayoutGroup) CreateVerticalLayoutGroup(in Transform parent)
		{
			GameObject verticalObject = new GameObject("Vertical layout group");
			verticalObject.transform.SetParent(parent);
			verticalObject.AddComponent<CanvasRenderer>();

			RectTransform verticalObjectRectTransform = verticalObject.AddComponent<RectTransform>();

			verticalObjectRectTransform.SetDefaultScale();
			verticalObjectRectTransform.SetPivotAndAnchors(new Vector2(0.5f, 1f), new Vector2(0f, 1f), new Vector2(1f, 1f));
			verticalObjectRectTransform.sizeDelta = Vector2.zero;
			verticalObjectRectTransform.anchoredPosition = Vector2.zero;

			VerticalLayoutGroup verticalLayout = verticalObject.AddComponent<VerticalLayoutGroup>();
			verticalLayout.padding = new RectOffset(20, 20, 30, 30);
			verticalLayout.spacing = 20f;
			verticalLayout.childAlignment = TextAnchor.UpperCenter;

			verticalLayout.childControlHeight = true;
			verticalLayout.childControlWidth = true;
			verticalLayout.childForceExpandHeight = false;
			verticalLayout.childForceExpandWidth = true;

			ContentSizeFitter contentSizeFitter = verticalObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			return (verticalObjectRectTransform, verticalLayout);
		}
	}
}
