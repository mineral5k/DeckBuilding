using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHighlite : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        // LineRenderer 설정
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f; // 테두리 두께
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 5; // 사각형은 점 4개 + 닫기 위해 1개 더
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false; // 오브젝트를 따라 움직이게
        lineRenderer.enabled = false; // 처음엔 꺼둠

        // 테두리 색상 설정
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;

        DrawOutline();
    }

    void DrawOutline()
    {
        // 콜라이더의 크기와 오프셋을 계산해서 사각형 점 4개를 찍음
        Vector2 size = boxCollider.size;
        Vector2 offset = boxCollider.offset;

        Vector3[] positions = new Vector3[5];
        positions[0] = new Vector3(offset.x - size.x / 2, offset.y + size.y / 2, 0); // 좌상
        positions[1] = new Vector3(offset.x + size.x / 2, offset.y + size.y / 2, 0); // 우상
        positions[2] = new Vector3(offset.x + size.x / 2, offset.y - size.y / 2, 0); // 우하
        positions[3] = new Vector3(offset.x - size.x / 2, offset.y - size.y / 2, 0); // 좌하
        positions[4] = positions[0]; // 다시 시작점으로

        lineRenderer.SetPositions(positions);
    }

    void OnMouseEnter() => lineRenderer.enabled = true; // 마우스 올리면 켜기
    void OnMouseExit() => lineRenderer.enabled = false; // 마우스 나가면 끄기
}
