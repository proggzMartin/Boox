using Boox.Core.Models.Entities;

namespace Boox.Core.Models
{
    public class BookIdComparer : IComparer<string>
    {
        /// <summary>
        /// Compares bookId's.
        /// It is expected that the first character is a letter
        /// </summary>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public int Compare(string? leftKey, string? rightKey)
        {
            int leftId = 0, rightId = 0;
            bool leftParseOk = false, rightParseOk = false;

            try
            {
                leftParseOk = int.TryParse(leftKey.Substring(1), out leftId);
            } catch{}
            try
            {
                rightParseOk = int.TryParse(rightKey.Substring(1), out rightId);
            } catch{}

            var r = leftParseOk && rightParseOk
                        ? leftId - rightId //if both parses ok, do comparison
                        : leftParseOk
                            ? 1 //if only xParseOk (implicitly), then xId first
                            : rightParseOk
                                ? -1 //if only yParseOk (implicitly), then xId first
                                : 0; //else, both failed; do nothing.

            return r;
        }

    }
}
