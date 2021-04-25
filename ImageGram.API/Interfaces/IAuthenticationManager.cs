using System.Collections.Generic;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface IAuthenticationManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string Authenticate(Account account);
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        IDictionary<string, string> Tokens { get; }
    }
}