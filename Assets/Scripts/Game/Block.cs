using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;

    public delegate void OnBlockClicked(int index);

    private OnBlockClicked _onBlockClicked;
    
    public enum MarkerType {None, O, X}

    private int _blockIndex;
    private SpriteRenderer _spriteRenderer; // block 색상 변경 위해
    private Color _defaultBlockColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultBlockColor = _spriteRenderer.color;
    }

    // 초기화
    public void InitMarker(int bloakIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = bloakIndex;
        SetMarker(MarkerType.None);
        SetBlockColor(_defaultBlockColor);
        _onBlockClicked = onBlockClicked;
    }
    
    // 마커 설정
    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.None:
                markerSpriteRenderer.sprite = null;
                break;
            case MarkerType.O:
                markerSpriteRenderer.sprite = oSprite;
                break;
            case MarkerType.X:
                markerSpriteRenderer.sprite = xSprite;
                break;
        }
    }

    // 블럭 색상 설정
    public void SetBlockColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    // 블럭 터치 시
    public void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            return;
        
        _onBlockClicked?.Invoke(_blockIndex);
        
        Debug.Log("Selected Block : " + _blockIndex);
    }
}
