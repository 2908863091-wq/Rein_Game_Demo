using UnityEngine;
using UnityEngine.Events;

public class Cutting : BaseBlock
{
    [SerializeField] private ProgressBarUI progressBarUI;

    private int cuttingNumber = 0;
    private int requiredCuts = 0;
    private string currentOutputName;

    public static UnityEvent OnCutting;

    private void Awake()
    {
        if (OnCutting == null) OnCutting = new UnityEvent();
        OnCutting.AddListener(PlayChopSound);
    }

    public override void InterAct(Player player)
    {
        if (player.FoodARU())
        {
            bool playerHasPlate = player.GetKitchen().TryGetComponent<Plate>(out Plate plate);

            if (playerHasPlate)
            {
                if (FoodARU() == false)
                {
                    TransferFood(player, this);
                    ResetCuttingState();
                }
                else
                {
                    bool is_success = plate.AddKitchen(GetKitchen().KitchenObject());
                    if (is_success)
                    {
                        DestroyFood();
                        ResetCuttingState();
                    }
                }
            }
            else
            {
                if (FoodARU() == false)
                {
                    TransferFood(player, this);
                    ResetCuttingState();
                }
                else
                {
                    if (GetKitchen().TryGetComponent<Plate>(out plate))
                    {
                        KitchenObject playerFood = player.GetKitchen().KitchenObject();
                        if (playerFood != null && plate.AddKitchen(playerFood))
                        {
                            player.DestroyFood();
                        }
                    }
                }
            }
        }
        else
        {
            if (FoodARU() == true)
            {
                TransferFood(this, player);
                ResetCuttingState();
            }
        }

        if (player.FoodARU() && !FoodARU())
        {
            ResetCuttingState();
        }
    }

    public override void ExtraInterAct(Player player)
    {
        if (!FoodARU()) return;

        KitchenObject currentFood = GetKitchen().KitchenObject();
        if (currentFood == null) return;

        string foodName = GetFoodPrefabName(currentFood);

        // 使用静态类获取配方
        if (CuttingRecipes.TryGetRecipe(foodName, out string outputName, out int requiredCuts))
        {
            // 如果是第一次切割，初始化状态
            if (cuttingNumber == 0)
            {
                this.requiredCuts = requiredCuts;
                this.currentOutputName = outputName;
                progressBarUI.Show();

                // 调试信息
                Debug.Log($"开始切割: {foodName} -> {outputName} (需要 {requiredCuts} 次切割)");
            }

            cuttingNumber++;
            OnCutting?.Invoke();

            float progress = (float)cuttingNumber / requiredCuts;
            progressBarUI.UpdateProcess(progress);

            // 调试信息
            Debug.Log($"切割进度: {cuttingNumber}/{requiredCuts} ({progress:P0})");

            if (cuttingNumber == requiredCuts)
            {
                Debug.Log("切割完成！");
                CompleteCutting();
            }
        }
        else
        {
            Debug.LogWarning($"没有找到 {foodName} 的切割配方");
        }
    }

    private string GetFoodPrefabName(KitchenObject food)
    {
        // 方法1：优先使用预制体名称
        if (food.prefab != null)
            return food.prefab.name;

        // 方法2：使用对象名称（备选）
        return food.name;
    }

    private void CompleteCutting()
    {
        progressBarUI.Hide();
        DestroyFood();

        // 调试信息
        Debug.Log($"尝试加载预制体: {currentOutputName}");

        // 直接加载 GameObject 预制体并创建
        GameObject prefab = Resources.Load<GameObject>($"KitchenObjects/{currentOutputName}");

        if (prefab != null)
        {
            Debug.Log($"成功加载预制体: {prefab.name}");
            CreateKitchen(prefab);
            Debug.Log($"已创建食材: {currentOutputName}");
        }
        else
        {
            Debug.LogError($"无法加载预制体: {currentOutputName}");

            // 尝试不带路径的加载
            prefab = Resources.Load<GameObject>(currentOutputName);
            if (prefab != null)
            {
                Debug.Log($"通过直接名称加载成功: {prefab.name}");
                CreateKitchen(prefab);
            }
            else
            {
                Debug.LogError($"通过任何方法都无法加载预制体: {currentOutputName}");
            }
        }

        ResetCuttingState();
    }

    private void ResetCuttingState()
    {
        cuttingNumber = 0;
        requiredCuts = 0;
        currentOutputName = null;
        progressBarUI.Hide();
    }

    private void PlayChopSound()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayChopSound();
    }
}