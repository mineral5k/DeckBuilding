using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierArrow : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int segmentCount = 20; // 선을 얼마나 부드럽게 쪼갤 것인가
    public Transform startPoint;  // 선택된 카드 위치
    public GameObject arrowHead;
    public bool isDraw = false;


    private void Update()
    {
        if (isDraw) // 마우스를 누르고 있을 때만 표시
        {
            DrawCurve();
        }
        else
        {
            lineRenderer.positionCount = 0; // 마우스 떼면 선 숨기기
            arrowHead.SetActive(false);
        }
    }

    public void DrawCurve()
    {
        arrowHead.SetActive(true);
        Vector3 p0 = startPoint.position;
        Vector3 p2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        // P1(조절점) 계산: 시작점과 끝점의 중간에서 위로 조금 띄움
        Vector3 p1 = (p0 + p2) / 2f;
        p1.y += 3f; // 이 값을 조절해서 곡선이 얼마나 높게 휠지 결정.

        lineRenderer.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);
            // 2차 베지어 공식 적용
            Vector3 point = Mathf.Pow(1 - t, 2) * p0 +
                            2 * (1 - t) * t * p1 +
                            Mathf.Pow(t, 2) * p2;

            lineRenderer.SetPosition(i, point);
        }
        arrowHead.transform.position = p2;
        // 방향도 마우스 방향을 바라보게 설정 (Optional)
        Vector2 direction = p2 - lineRenderer.GetPosition(segmentCount - 2);
        arrowHead.transform.up = direction;
    }
}
