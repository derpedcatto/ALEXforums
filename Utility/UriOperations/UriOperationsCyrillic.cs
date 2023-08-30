using System.Text;
using System.Web;

namespace ALEXforums.Utility.UriOperations
{
	public static class UriOperationsCyrillic
	{
		public static string DecodeUri(string uri)
		{
			return HttpUtility.UrlDecode(uri);
		}

		public static string EncodeUri(string uri)
		{
			StringBuilder encodedString = new();

			foreach (char c in uri) 
			{
				if (c == '/' || c == '-' || c == ' ')
				{
					encodedString.Append(Uri.EscapeDataString(c.ToString()));
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
