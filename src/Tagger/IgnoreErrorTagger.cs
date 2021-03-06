using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

namespace IgnoreFiles
{
    class IgnoreErrorTagger : ITagger<IErrorTag>
    {
        private const string _searchText = "../";

        public IEnumerable<ITagSpan<IErrorTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan currSpan in spans)
            {
                var text = currSpan.GetText();
                int loc = text.IndexOf(_searchText, StringComparison.Ordinal);

                if (loc > -1)
                {
                    var length = text.Contains(" ") ? text.IndexOf(" ") : text.Length;
                    var span = new SnapshotSpan(currSpan.Snapshot, currSpan.Start, length);
                    var tag = new ErrorTag(PredefinedErrorTypeNames.SyntaxError);
                    yield return new TagSpan<ErrorTag>(span, tag);
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
    }
}
