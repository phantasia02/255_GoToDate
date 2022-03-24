using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class PeopleMovement : MonoBehaviour
{
    [SerializeField] Transform[] Transs;
    int Index;
    [SerializeField] Vector2 EndPoint;

    // Start is called before the first frame update
    void Start()
    {
        Transs = (Random.Range(0, 100) < 50) ? Transs.Reverse().ToArray() : Transs;
        Index = Random.Range(0, Transs.Length);
        InitPosition();
        SetNextMove();
    }

    void InitPosition()
    {
        Vector2 RandomCircle = Random.insideUnitCircle * 4f;
        int InitTargetIndex = (int)Mathf.Repeat(Index + 1, Transs.Length);
        Vector3 InitStartPoint = new Vector3(Transs[Index].position.x, transform.position.y, Transs[Index].position.z);
        Vector3 InitEndPoint = new Vector3(Transs[InitTargetIndex].position.x + RandomCircle.x, transform.position.y, Transs[InitTargetIndex].position.z + RandomCircle.y);
        transform.position = Vector3.Lerp(InitStartPoint, InitEndPoint, Random.Range(0.1f, 0.9f));
        transform.LookAt(new Vector3(InitEndPoint.x, transform.position.y, InitEndPoint.y));
    }

    void SetNextMove()
    {
        Index = (int)Mathf.Repeat(Index + 1, Transs.Length);
        EndPoint = new Vector3(Transs[Index].position.x, Transs[Index].position.z);
        EndPoint += Random.insideUnitCircle * 4f;

        float Distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), EndPoint);
        transform.DOMove(new Vector3(EndPoint.x, transform.position.y, EndPoint.y), Distance * Random.Range(0.15f, 0.25f)).SetEase(Ease.Linear).OnComplete(SetNextMove);
        transform.DOLookAt(new Vector3(EndPoint.x, transform.position.y, EndPoint.y), 1f);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }


}
