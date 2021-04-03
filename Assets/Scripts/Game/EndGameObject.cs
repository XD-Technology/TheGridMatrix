namespace TheGridMatrix
{
    public class EndGameObject : GameSpawnObject
    {
        public override void OnCollideSpawn()
        {
            GameManager.EndGame();
        }
    }
}
