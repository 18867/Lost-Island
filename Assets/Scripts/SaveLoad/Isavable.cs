
public interface ISavable 
{
    void SavableRegister()
    {
        SaveLoadManager.Instance.Register(this);//统一实现我们的注册方法
    }

    GameSaveData GenerateSaveData();

    void RestoreGameData(GameSaveData saveData);

}
