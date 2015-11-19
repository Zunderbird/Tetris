
namespace Assets.MVC.Model
{
    public static class Configuration
    {
        public static int BoardWidth { get; set; }
        public static int BoardHeight { get; set; }

        public static int PlayersCount { get; set; }
        public const int MAX_PLAYERS_COUNT = 2;

        static Configuration()
        {
            BoardWidth = 10;
            BoardHeight = 24;

            PlayersCount = 1;
        }
    }
}
