using UnityEngine;
using UnityEngine.Events;

public class Stove : BaseBlock
{
    [SerializeField] private FireObjects fireFoodlist;
    [SerializeField] private StoveFX stoveFX;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private AudioClip cookingSound; // 第一阶段烹饪音效
    [SerializeField] private AudioClip burningSound; // 第二阶段烘烤音效

    private float fireTime = 0;
    private FireFood fireFood;
    private StoveState state = StoveState.idle;
    private AudioSource audioSource;

    public enum StoveState
    {
        idle,
        cooking,
        burning
    }

    private void Awake()
    {
        // 添加AudioSource组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.spatialBlend = 1.0f; // 3D音效
        audioSource.minDistance = 1.0f;
        audioSource.maxDistance = 10.0f;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        switch (state)
        {
            case StoveState.idle:
                progressBarUI.Hide();
                break;
            case StoveState.cooking:
                progressBarUI.Show();
                progressBarUI.UpdateProcess(fireTime / fireFood.cooking_time);
                fireTime += Time.deltaTime;
                if (fireTime > fireFood.cooking_time)
                {
                    DestroyFood();
                    CreateKitchen(fireFood.output.prefab);
                    fireFoodlist.TryGetFireObjects(GetKitchen().KitchenObject(), out FireFood newFireFood);
                    Burning(newFireFood);
                }
                break;
            case StoveState.burning:
                fireTime += Time.deltaTime;
                progressBarUI.Show();
                progressBarUI.UpdateProcess(fireTime / fireFood.cooking_time);
                if (fireTime > fireFood.cooking_time)
                {
                    DestroyFood();
                    CreateKitchen(fireFood.output.prefab);
                    BeIdle();
                }
                break;
        }
    }

    public override void InterAct(Player player)
    {
        // 情况1：玩家手上有食物，火炉是空的 - 放上食物开始烹饪
        if (player.FoodARU() && fireFoodlist.TryGetFireObjects(player.GetKitchen().KitchenObject(), out FireFood fireFood))
        {
            if (FoodARU() == false)
            {
                TransferFood(player, this);
                Cooking(fireFood);
            }
        }
        // 情况2：玩家手上有碟子，火炉上有食物 - 把食物放到碟子里
        else if (player.FoodARU() && player.GetKitchen().TryGetComponent<Plate>(out Plate playerPlate))
        {
            if (FoodARU() == true)
            {
                KitchenObject stoveFood = GetKitchen().KitchenObject();
                if (stoveFood != null && playerPlate.AddKitchen(stoveFood))
                {
                    DestroyFood();
                    BeIdle();
                }
            }
        }
        // 情况3：火炉上有食物，玩家手上是空的 - 把食物给玩家
        else if (!player.FoodARU() && FoodARU() == true)
        {
            TransferFood(this, player);
            BeIdle();
        }
    }

    private void Cooking(FireFood fireFood)
    {
        fireTime = 0;
        this.fireFood = fireFood;
        state = StoveState.cooking;
        stoveFX.Show();
        PlayCookingSound(); // 播放烹饪音效
    }

    private void Burning(FireFood fireFood)
    {
        if (fireFood == null)
        {
            BeIdle();
            return;
        }
        this.fireFood = fireFood;
        fireTime = 0;
        state = StoveState.burning;
        stoveFX.Show();
        PlayBurningSound(); // 播放烘烤音效
    }

    private void BeIdle()
    {
        state = StoveState.idle;
        stoveFX.Hide();
        StopStoveSound(); // 停止音效
    }

    private void PlayCookingSound()
    {
        if (audioSource != null && cookingSound != null)
        {
            audioSource.clip = cookingSound;
            audioSource.volume = GetGlobalSFXVolume();
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("烹饪音效未设置或AudioSource为空");
        }
    }

    private void PlayBurningSound()
    {
        if (audioSource != null && burningSound != null)
        {
            audioSource.clip = burningSound;
            audioSource.volume = GetGlobalSFXVolume();
            audioSource.Play();
        }
        else if (audioSource != null && cookingSound != null)
        {
            // 如果没有设置烘烤音效，就使用烹饪音效
            PlayCookingSound();
        }
        else
        {
            Debug.LogWarning("音效未设置或AudioSource为空");
        }
    }

    private void StopStoveSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // 获取全局音效音量
    private float GetGlobalSFXVolume()
    {
        // 如果MusicManager存在，使用它的音效音量
        if (MusicManager.Instance != null)
        {
            return MusicManager.Instance.GetSFXVolume();
        }
        // 否则使用默认值
        return 1.0f;
    }
}