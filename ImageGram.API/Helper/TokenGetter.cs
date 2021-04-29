namespace ImageGram.API.Helper
{
    public static class TokenGetter
    {
        public static int GetUserId(this string requestToken)
        {
            var firstRemove = requestToken.Remove(0, 10);
            var startIndex = firstRemove.IndexOf(':');
            var userId = int.Parse(firstRemove.Remove(startIndex));

            return userId;
        }
    }
}