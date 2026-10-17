using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBasic : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Animator anim;
    [SerializeField] float dieAnimation;

    public int maxHp;
    public int currentHp;
    public int speedMove;
    public float stopDistan;
    public int damage;
    bool isDie;

    PlotController target;

    [SerializeField] GameObject hitEffect;

    Coroutine coSlowMove;
    void Awake()
    {
        currentHp = maxHp;
    }

    void Start()
    {
        GetPlotTarget();
    }

    void GetPlotTarget()
    {
        List<PlotController> _pltC = new List<PlotController>();
        for (int i = 0; i < GameManager.Instance.allPlot.Length; i++)
        {
            if (GameManager.Instance.allPlot[i].havePlant == true)
            {
                _pltC.Add(GameManager.Instance.allPlot[i]);
            }
        }
        if (_pltC.Count <= 0)
        {
            StartCoroutine(CoCallDie());
            return;
        }
        target = _pltC[Random.Range(0, _pltC.Count)];
        StartCoroutine(MoveToPlant());
        Flip();
    }

    void Flip()
    {
        if (target == null) return;

        Vector3 scale = transform.localScale;
        if (target.transform.position.x > transform.position.x)
        {
            // เป้าหมายอยู่ทางขวา -> หันขวา
            scale.x = Mathf.Abs(scale.x);
        }
        else if (target.transform.position.x < transform.position.x)
        {
            // เป้าหมายอยู่ทางซ้าย -> หันซ้าย
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    IEnumerator MoveToPlant()
    {
        float _distan = 9999;
        anim.SetTrigger("Move");
        Flip();
        while (_distan > stopDistan)
        {
            if (target.havePlant == false)
            {
                GetPlotTarget();
            }
            _distan = Vector3.Distance(transform.position, target.transform.position);
            float _step = speedMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _step);
            yield return null;
        }
        StartCoroutine(LoopAttack());
    }

    IEnumerator LoopAttack()
    {
        anim.SetTrigger("Attack");
        target.TakeDamage(damage);
        yield return new WaitForSeconds(3);
        if (target.havePlant == false)
        {
            GetPlotTarget();
            StartCoroutine(MoveToPlant());
            yield break;
        }
        StartCoroutine(LoopAttack());
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        coSlowMove = StartCoroutine(DelaySlowFromPlayerHit());
        hitEffect.SetActive(false);
        hitEffect.transform.localScale = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
        hitEffect.SetActive(true);
        currentHp--;
        if (currentHp <= 0 && !isDie)
        {
            isDie = true;
            StopAllCoroutines();
            StartCoroutine(CoCallDie());
        }
    }

    IEnumerator DelaySlowFromPlayerHit()
    {

        yield return new WaitForSeconds(2);
    }


    IEnumerator CoCallDie()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(dieAnimation);
        GameManager.Instance.houseCtr.CallEnemyDie();
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
