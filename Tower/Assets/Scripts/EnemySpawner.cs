using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float waveInterval = 3f;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        // 等待游戏开始
        yield return new WaitForSeconds(1f);

        for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            Wave wave = waves[waveIndex];

            Debug.Log($"开始第 {waveIndex + 1} 波，敌人数量：{wave.num}，生成间隔：{wave.time}秒");

            // 生成当前波次的所有敌人
            for (int i = 0; i < wave.num; i++)
            {
                Vector3 spawnPos = startPoint != null ? startPoint.position : transform.position;
                Instantiate(wave.enemyPrefab, spawnPos, Quaternion.identity);

                // 使用 wave.time 作为波次内的生成间隔
                // 如果不是当前波次的最后一个敌人，等待 wave.time 秒
                if (i < wave.num - 1)
                {
                    yield return new WaitForSeconds(wave.time);
                }
            }

            // 波次之间的间隔
            // 如果不是最后一个波次，等待 waveInterval 秒
            if (waveIndex < waves.Count - 1)
            {
                Debug.Log($"等待 {waveInterval} 秒后开始下一波");
                yield return new WaitForSeconds(waveInterval);
            }
        }

        Debug.Log("所有波次生成完毕！等待所有敌人被消灭...");
        yield return null; // 确保最后一帧生成的敌人已被场景注册

        while (FindObjectsOfType<Enemy>().Length > 0)
        {
            yield return new WaitForSeconds(0.2f); // 定期检查
        }

        Debug.Log("所有敌人已被消灭，游戏胜利！");
        if (GameManager.Instance != null)
            GameManager.Instance.Win();
    }
}