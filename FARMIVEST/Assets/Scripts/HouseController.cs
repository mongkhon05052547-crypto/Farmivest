using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HouseController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] CanvasGroup nightFilter;

    [SerializeField] GameObject[] positionSpawnEnemy;
    [SerializeField] GameObject enemyPrefab;

    bool canCallToNight = true;
    int countHaveEnemy;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (canCallToNight == false) return;
        ToNight();
    }

    void SpawnMonster()
    {
        Instantiate(enemyPrefab,
        positionSpawnEnemy[Random.Range(0, positionSpawnEnemy.Length)].transform.position,
        enemyPrefab.transform.rotation);
        countHaveEnemy++;
    }

    public void CallEnemyDie()
    {
        countHaveEnemy--;
        if (countHaveEnemy == 0)
        {
            //Change To Day
            ToNewDay();
        }
    }

    void ToNight()
    {
        canCallToNight = false;
        StartCoroutine(ControlerNightFilter(true));
        GameManager.Instance.canPlant = false;
        SpawnMonster();
    }

    void ToNewDay()
    {
        canCallToNight = true;
        StartCoroutine(ControlerNightFilter(false));
        GameManager.Instance.canPlant = true;
        GameManager.Instance.toolsCtr.AddLifeWater(3);
        foreach (PlotController _ap in GameManager.Instance.allPlot)
        {
            if (_ap.havePlant)
            {
                _ap.ModifyExp(10);
            }
        }
    }

    IEnumerator ControlerNightFilter(bool _isNight)
    {
        if (_isNight)
        {
            while (nightFilter.alpha < 1f)
            {
                nightFilter.alpha += Time.deltaTime;
                yield return null;
            }
            nightFilter.alpha = 1f;
        }
        else
        {
            while (nightFilter.alpha > 0f)
            {
                nightFilter.alpha -= Time.deltaTime;
                yield return null;
            }
            nightFilter.alpha = 0f;
        }
    }

}
