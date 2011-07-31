using System.Text;
public static class StringHelpers {
    public static string Trim(this string value, int maxLength) {
        if (value.Length <= maxLength)
            return value;

        value = value.Substring(0, maxLength);
        value = value.Insert(value.Length, "...");

        return value;
    }

    public static string Repeat(string repeatCharacter, int repeatCount) {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i <= repeatCount; i++)
            sb.Append("-");

        return sb.ToString();
    }
}
