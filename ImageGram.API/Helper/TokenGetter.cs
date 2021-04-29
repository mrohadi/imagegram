namespace ImageGram.API.Helper
{
    public static class TokenGetter
    {
        /// <summary>
        /// Get user currently logged in token
        /// </summary>
        /// <param name="requestToken">Token on the request header</param>
        /// <returns>UserId or accountid</returns>
        public static int GetUserId(this string requestToken)
        {
            var firstRemove = requestToken.Remove(0, 10);
            var startIndex = firstRemove.IndexOf(':');
            var userId = int.Parse(firstRemove.Remove(startIndex));

            return userId;
        }
    }
}