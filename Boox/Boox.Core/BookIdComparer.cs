using Boox.Core.Models.Entities;

namespace Boox.Core.Models
{
    public class BookIdComparer : IComparer<string>
    {
        //public int Compare(Book? x, Book? y)
        //{
        //    int xId, yId;

        //    var xParseOk = int.TryParse(x.Id.Substring(1), out xId);
        //    var yParseOk = int.TryParse(x.Id.Substring(1), out yId);

        //    return xParseOk && yParseOk
        //                ? xId - yId //if both parses ok, do comparison
        //                : xParseOk
        //                    ? 1 //if only xParseOk (implicitly), then xId first
        //                    : yParseOk
        //                        ? -1 //if only yParseOk (implicitly), then xId first
        //                        : 0; //else, both failed; do nothing.
        //}

        public int Compare(string? leftKey, string? rightKey)
        {
            int leftId, rightId;

            var leftParseOk = int.TryParse(leftKey.Substring(1), out leftId);
            var rightParseOk = int.TryParse(rightKey.Substring(1), out rightId);

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
