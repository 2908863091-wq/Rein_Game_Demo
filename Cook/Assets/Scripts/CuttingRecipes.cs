using System.Collections.Generic;
using UnityEngine;

// 这个类存储所有切割配方，永不丢失！
public static class CuttingRecipes
{
    // 食谱字典：输入预制体名称 -> (输出预制体名称, 需要切割次数)
    private static readonly Dictionary<string, RecipeData> _recipes = new Dictionary<string, RecipeData>();

    // 静态构造函数，在游戏开始时自动初始化
    static CuttingRecipes()
    {
        InitializeRecipes();
    }

    // 配方数据结构
    private struct RecipeData
    {
        public string outputPrefabName;
        public int cutCount;

        public RecipeData(string output, int count)
        {
            outputPrefabName = output;
            cutCount = count;
        }
    }

    // 初始化食谱数据（根据你的截图）
    private static void InitializeRecipes()
    {
        // 添加你的三个配方
        AddRecipe("Cabbage", "Cabbage_split", 5);
        AddRecipe("Cheese", "Cheese_split", 5);
        AddRecipe("Tomato", "Tomato_split", 5);

        Debug.Log($"已加载 {_recipes.Count} 个切割配方");
    }

    // 添加配方
    private static void AddRecipe(string inputName, string outputName, int cutCount)
    {
        if (!_recipes.ContainsKey(inputName))
        {
            _recipes.Add(inputName, new RecipeData(outputName, cutCount));
        }
    }

    // 获取配方（对外接口）
    public static bool TryGetRecipe(string inputName, out string outputName, out int cutCount)
    {
        if (_recipes.TryGetValue(inputName, out RecipeData recipe))
        {
            outputName = recipe.outputPrefabName;
            cutCount = recipe.cutCount;
            return true;
        }

        outputName = null;
        cutCount = 0;
        return false;
    }

    // 获取配方（只检查是否存在）
    public static bool HasRecipe(string inputName)
    {
        return _recipes.ContainsKey(inputName);
    }
}