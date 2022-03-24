using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShopFakePlayerData : CSingletonMonoBehaviour<ShopFakePlayerData>
{
    public int Coin;
    Dictionary<CGGameSceneData.EPlayerTrailerType, PlayerOwnCar> CarsStatus = new Dictionary<CGGameSceneData.EPlayerTrailerType, PlayerOwnCar>();

    public CGGameSceneData.EPlayerTrailerType InUseCar;

    private void Start()
    {
        // InitCarsStatus();
        LoadTestCarsStatus();
    }

    void InitCarsStatus()
    {
        for (int i = 0; i < CSaveManager.m_status.m_AllPlayerOwnCar.Length; i++)
            CarsStatus.Add((CGGameSceneData.EPlayerTrailerType)i, new PlayerOwnCar((CGGameSceneData.EPlayerTrailerType)i, CarStatus.Lock, false));
    }

    void LoadTestCarsStatus()
    {
        print(CSaveManager.m_status.m_AllPlayerOwnCar.Length);
        
        CSaveManager lTempSaveManager = CSaveManager.SharedInstance;
        for (int i = 0; i < CSaveManager.m_status.m_AllPlayerOwnCar.Length; i++)
            CarsStatus.Add((CGGameSceneData.EPlayerTrailerType)i, CSaveManager.m_status.m_AllPlayerOwnCar[i]);

        InUseCar = CSaveManager.m_status.m_CurPlayerTrailer;
        Coin = CSaveManager.m_status.Coin.Value;
    }

    public bool IsAfford(int Price)
    {
        return Coin >= Price;
    }

    public void Pay(int Price)
    {
        int lTempCoin = CSaveManager.m_status.Coin.Value - Price;
        Coin = lTempCoin;
        CSaveManager.m_status.Coin.Value = Coin;


    }

    public void BuyCar(Car NewCar)
    {
        CarsStatus[NewCar.Index].SetData(CarStatus.Bought, true);

        CSaveManager.m_status.m_AllPlayerOwnCar[(int)NewCar.Index].Status = CarStatus.Bought;

        CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
        lTempAudioManager.PlaySE(CSEPlayObj.ESE.eBuySkin);
    }

    public PlayerOwnCar GetPlayerOwnCar(Car NewCar)
    {
        return CarsStatus[NewCar.Index];
    }

    public CarStatus GetCarStatus(Car NewCar)
    {
        return CarsStatus[NewCar.Index].Status;
    }

}
