namespace AvigilonAlarmDemoApp.DataAccesslayer.Formaters
{
    /// <summary>
    /// Class used to format json
    /// </summary>
    public class JsonFormatters
    {
       private const char k_escapeChar = '\\';
        private const string k_spaceChar = " ";
        private const string k_indentationStr = "    ";

        /// <summary>
        /// Enum to determine whether we should increase, decrease or take no action on the indentation level.
        /// </summary>
        private enum IndentationCounterAction_t
        {
            None,
            IncreaseCount,
            DecreaseCount
        }

        /// <summary>
        /// Formats the json to readable string.
        /// </summary>
        /// <param name="str">The json string to format.</param>
        /// <returns>Readable json string.</returns>
        public static string FormatJson(string str)
        {
            int indentCount = 0;
            bool bIsQuoted = false;
            char prevJsonChar = ' ';
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char jsonChar in str)
            {
                if (jsonChar == '{' || jsonChar == '[')
                {
                    sb.Append(jsonChar);
                    if (!bIsQuoted)
                    {
                        AppendNewLineAndAddIndents_(sb, ref indentCount, IndentationCounterAction_t.IncreaseCount);
                    }
                }
                else if (jsonChar == '}' || jsonChar == ']')
                {
                    if (!bIsQuoted)
                    {
                        AppendNewLineAndAddIndents_(sb, ref indentCount, IndentationCounterAction_t.DecreaseCount);
                    }
                    sb.Append(jsonChar);
                }
                else if (jsonChar == '"')
                {
                    sb.Append(jsonChar);
                    if (prevJsonChar != k_escapeChar)
                        bIsQuoted = !bIsQuoted;
                }
                else if (jsonChar == ',')
                {
                    sb.Append(jsonChar);
                    if (!bIsQuoted)
                    {
                        AppendNewLineAndAddIndents_(sb, ref indentCount, IndentationCounterAction_t.None);
                    }
                }
                else if (jsonChar == ':')
                {
                    sb.Append(jsonChar);
                    if (!bIsQuoted)
                        sb.Append(k_spaceChar);
                }
                else
                {
                    sb.Append(jsonChar);
                }
                prevJsonChar = jsonChar;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Appends a new line to the string builder and adds indents.
        /// </summary>
        /// <param name="stringBuilder">String builder to print pretty json.</param>
        /// <param name="indentCount">The indentation level.</param>
        /// <param name="indentCountAction">Enum to determine whether we should increase, decrease or take no action on the indentation level.</param>
        private static void AppendNewLineAndAddIndents_(
            System.Text.StringBuilder stringBuilder,
            ref int indentCount,
            IndentationCounterAction_t indentCountAction)
        {
            stringBuilder.AppendLine();
            if (indentCountAction == IndentationCounterAction_t.IncreaseCount)
                indentCount = ++indentCount;
            else if (indentCountAction == IndentationCounterAction_t.DecreaseCount)
                indentCount = --indentCount;
            stringBuilder.Append(System.String.Concat(System.Linq.Enumerable.Repeat(k_indentationStr, indentCount)));
        }
    }
}