namespace _2048
{
    internal class Cell
    {
        public int Value { get; }
        public bool IsLock { get; private set; }

        public Cell(
            int value = 0)
        {
            Value = value;
        }

        public void Lock()
        {
            IsLock = true;
        }

        public void UnLock()
        {
            IsLock = false;
        }
    }
}