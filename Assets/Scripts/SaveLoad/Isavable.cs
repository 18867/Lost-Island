
public interface ISavable 
{
    void SavableRegister()
    {
        SaveLoadManager.Instance.Register(this);//ͳһʵ�����ǵ�ע�᷽��
    }

    GameSaveData GenerateSaveData();

    void RestoreGameData(GameSaveData saveData);

}
