namespace Model
{
    public class HistoricalData
    {
        private Tile origin;

        public Tile tile { get; private set; }
        public Direction direction { get; private set; }

        public HistoricalData(Tile previousTile, Direction previousDir)
        {
            this.direction = previousDir;
            this.tile = previousTile;
        }

        public HistoricalData(Tile origin)
        {
            this.origin = origin;
        }
    }
}
