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
    }

    private void UpdateScale( bool isDown )
    {
        var scale = transform.localScale.x;
        m_adjuster.OnDown( isDown, scale, 1.1f );
    }
}
```
