using System.Text;
using System.Web;

namespace ALEXforums.Services.UriOperations
{
    public class UriOperationsCyrillic : IUriOperations
    {
        public string DecodeUri(string uri)
        {
            return HttpUtility.UrlDecode(uri.Replace('_', ' '));
        }

        public string EncodeUri(string uri)
        {
            StringBuilder encodedString = new();

            foreach (char c in uri)
            {
                if (c == '/')
                {
                    encodedString.Append(Uri.EscapeDataString(c.ToString()));
                }
                else if (c == ' ')
                {
                    encodedString.Append('_');
                }
                else
                {
                    encodedString.Append(c);
                }
            }

            return encodedString.ToString();
        }
    }
}
