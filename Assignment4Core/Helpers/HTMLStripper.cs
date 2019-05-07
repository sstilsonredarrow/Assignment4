using System;
namespace Assignment4Core.Helpers
{
    public class HTMLStripper
    {
        public HTMLStripper()
        {
        }

        public string RemoveHTML(string input)
        {
            if (!input.Contains("<"))
            {
                return input;
            }
            int ix = input.IndexOf("<", StringComparison.Ordinal);
            int iy = input.IndexOf(">", StringComparison.Ordinal) + 1;
            return RemoveHTML(input.Remove(ix, iy - ix));
        }
    }
}
