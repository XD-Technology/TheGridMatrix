namespace TheGridMatrix
{
    public class ScoreObject : GameSpawnObject
    {
        public override void OnCollideSpawn()
        {
            GameManager.Score += 25;
            Destroy(gameObject);
        }
    }
}
