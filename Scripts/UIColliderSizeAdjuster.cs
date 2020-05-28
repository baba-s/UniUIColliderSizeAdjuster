using System;
using UnityEngine;
using UniUICollider;

namespace Kogane
{
	/// <summary>
	/// ボタンが押されて縮小した時に、当たり判定のサイズも一緒に縮小する現象を避けるためのコンポーネント
	/// 
	/// ボタンが押されて縮小する場合、当たり判定のサイズも一緒に縮小してしまう関係で
	/// ボタンの端っこをタップした時にタップした位置が当たり判定の領域から外れてしまい
	/// 演出上はボタンが押せているように見えるが実際にはボタンが押せていない、という状況が発生してしまう
	/// それを防ぐために、ボタンが縮小する時に当たり判定のサイズは維持するためのコンポーネント
	/// </summary>
	[Serializable]
	public sealed class UIColliderSizeAdjuster
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private Transform m_parent = default;

		//================================================================================
		// 変数
		//================================================================================
		private RectTransform m_self;
		private bool          m_isInit;

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UIColliderSizeAdjuster()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UIColliderSizeAdjuster( Transform parent )
		{
			m_parent = parent;
		}

		/// <summary>
		/// 当たり判定用のゲームオブジェクトを作成します
		/// </summary>
		private void Initialize()
		{
			if ( m_isInit ) return;
			m_isInit = true;

			var go = new GameObject( nameof( UIColliderSizeAdjuster ) );
			go.transform.SetParent( m_parent );
			m_self = go.AddComponent<RectTransform>();
			go.AddComponent<UICollider>();

			m_self.localRotation    = Quaternion.identity;
			m_self.localScale       = Vector3.one;
			m_self.anchoredPosition = Vector2.zero;
			m_self.sizeDelta        = Vector2.zero;
			m_self.anchorMin        = Vector2.zero;
			m_self.anchorMax        = Vector2.one;

			go.hideFlags = HideFlags.HideAndDontSave;
		}

		/// <summary>
		/// 当たり判定のサイズを維持します
		/// OnPointerDown、OnPointerUp のタイミングで呼び出すことを想定しています
		/// </summary>
		/// <param name="isDown">OnPointerDown のタイミングの場合 true</param>
		/// <param name="targetScale">押下中のボタンの localScale の値</param>
		/// <param name="scaleAdjust">押下中に当たり判定のサイズを大きくしたい場合 1 より大きい値を渡す</param>
		public void OnDown( bool isDown, float targetScale, float scaleAdjust = 1 )
		{
			Initialize();

			if ( !isDown )
			{
				m_self.localScale = Vector3.one;
				return;
			}

			var scale = Mathf.Max( 1, 1 / targetScale * scaleAdjust );

			m_self.localScale = new Vector3( scale, scale, 1 );
		}
	}
}