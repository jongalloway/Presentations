using System;
using System.Globalization;
using System.Web;
using System.Web.ModelBinding;

namespace Build01.Model
{
    /// <summary>
    /// Returns the user name of the current user
    /// </summary>
    public class CurrentUserValueProvider : IValueProvider
    {
        private HttpContextBase _context;

        public CurrentUserValueProvider(HttpContextBase context)
        {
            _context = context;
        }

        public bool ContainsPrefix(string prefix)
        {
            // This value provider doesn't support prefixes
            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            var identity = _context.User.Identity;
            var username = identity.IsAuthenticated ? identity.Name : "";
            return new ValueProviderResult(username, username, CultureInfo.InvariantCulture);
        }   
    }
}