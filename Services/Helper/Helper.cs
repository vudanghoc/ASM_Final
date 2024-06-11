using System.Globalization;
using System.Text;

namespace Services.Helper
{
    public static class HelperNomalize
    {
        public static string RemoveDiacritics(string text)
        {
            if (text == null)
            {
                return null;
            }
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static string NormalizeString(string input)
        {
            return string.Concat(input.Normalize(NormalizationForm.FormD)
                            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
                            .Normalize(NormalizationForm.FormC);
        }
    }
}
