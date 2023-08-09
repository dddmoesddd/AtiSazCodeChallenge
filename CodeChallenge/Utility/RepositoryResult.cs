namespace CodeChallenge.Utility
{
    public class RepositoryResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Entity { get; set; }

        public string Message { get; set; }

    }
}
