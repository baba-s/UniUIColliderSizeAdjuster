# UniUIColliderSizeAdjuster

ボタンが押されて縮小した時に、当たり判定のサイズも一緒に縮小する現象を避けるためのコンポーネント

## 使用例

```cs
using Kogane;
using UnityEngine;
using UnityEngine.EventSystems;

public class Example :
    MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    private UIColliderSizeAdjuster m_adjuster;

    private void Awake()
    {
        m_adjuster = new UIColliderSizeAdjuster( transform );
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        UpdateScale( true );
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        UpdateScale( false );

        Debug.Log( "ピカチュウ" );
    }

    private void UpdateScale( bool isDown )
    {
        var scale = isDown ? 0.8f : 1;
        transform.localScale = new Vector3( scale, scale, 1 );
        m_adjuster.OnDown( isDown, scale, 1.1f );
    }
}
```

![Image (30)](https://user-images.githubusercontent.com/6134875/84901024-d96cb180-b0e5-11ea-9024-5c9a23ff80c4.gif)

* ボタンを押した時にボタンが縮小してカーソルがボタンの範囲外になってしまっても  
カーソルを離した時に OnPointerUp 関数が正常に呼び出れるようになります  
