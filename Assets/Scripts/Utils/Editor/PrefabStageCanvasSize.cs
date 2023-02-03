#if UNITY_EDITOR

using UnityEditor;


using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Watches for state changes of prefab stage. 
/// Adds special component to the root game object when prefab gets open,
/// destroys it when prefab is being closed.
/// </summary>
[InitializeOnLoad]
static class PrefabStageCanvasSize
{
	static PrefabStageCanvasSize()
	{
		if (EditorApplication.isPlaying) return;

		UnityEditor.SceneManagement.PrefabStage.prefabStageOpened += stage =>
			{
				// get RectTranform of prefab environment
				var envRT = stage.prefabContentsRoot.transform.parent as RectTransform;
				if (envRT == null) return; // non UI prefab

				var scaler =
				 stage.prefabContentsRoot.transform.parent.gameObject
				      .AddComponent<CanvasScaler>();
				scaler.referenceResolution = new Vector2(1080, 1920);
				scaler.matchWidthOrHeight = 1;
				scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

				scaler.hideFlags = HideFlags.DontSave;
			};
	}
}

#endif
