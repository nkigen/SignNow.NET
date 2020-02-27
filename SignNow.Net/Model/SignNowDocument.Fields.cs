using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SignNow.Net.Interfaces;
using SignNow.Net.Model.FieldContents;

namespace SignNow.Net.Model
{
    /// <remarks>
    /// This part contains related to Fields and Fields value retrieval methods only.
    /// </remarks>
    public partial class SignNowDocument
    {
        /// <summary>
        /// All the document <see cref="TextContent"/> fields.
        /// </summary>
        [JsonProperty("texts")]
        internal IReadOnlyCollection<TextContent> Texts { get; private set; } = new List<TextContent>();

        /// <summary>
        /// All the document <see cref="HyperlinkContent"/> fields.
        /// </summary>
        [JsonProperty("hyperlinks")]
        internal IReadOnlyCollection<HyperlinkContent> Hyperlinks { get; private set; } = new List<HyperlinkContent>();

        /// <summary>
        /// All the documents <see cref="CheckboxContent"/> fields.
        /// </summary>
        [JsonProperty("checks")]
        internal IReadOnlyCollection<CheckboxContent> Checkboxes { get; private set; } = new List<CheckboxContent>();

        /// <summary>
        /// All the documents <see cref="AttachmentContent"/> fields.
        /// </summary>
        [JsonProperty("attachments")]
        internal IReadOnlyCollection<AttachmentContent> Attachments { get; private set; } = new List<AttachmentContent>();

        /// <summary>
        /// All the documents <see cref="EnumerationContent"/> fields.
        /// </summary>
        [JsonProperty("enumeration_options")]
        internal IReadOnlyCollection<EnumerationContent> Enumerations { get; private set; } = new List<EnumerationContent>();

        /// <summary>
        /// All the documents <see cref="RadiobuttonContent"/> fields.
        /// </summary>
        [JsonProperty("radiobuttons")]
        internal IReadOnlyCollection<RadiobuttonContent> Radiobuttons { get; private set; } = new List<RadiobuttonContent>();

        /// <summary>
        /// Find Field value by <see cref="Field"/> metadata.
        /// </summary>
        /// <param name="fieldMeta">Field metadata.</param>
        /// <returns><see cref="ISignNowFieldContent"/> object that represents state for <see cref="Field.Type"/></returns>
        public ISignNowFieldContent GetFieldContent(ISignNowField fieldMeta)
        {
            switch (fieldMeta?.FieldType())
            {
                case FieldType.Text:
                case FieldType.Dropdown:
                    return Texts.FirstOrDefault(txt => txt.Id == fieldMeta.GetFieldContentId());

                case FieldType.Signature:
                case FieldType.Initial:
                    return Signatures.FirstOrDefault(sig => sig.Id == fieldMeta.GetFieldContentId());

                case FieldType.Hyperlink:
                    return Hyperlinks.FirstOrDefault(lnk => lnk.Id == fieldMeta.GetFieldContentId());

                case FieldType.Checkbox:
                    var checkbox = Checkboxes.FirstOrDefault(cbox => cbox.Id == fieldMeta.GetFieldContentId());
                    if (!string.IsNullOrEmpty(checkbox?.Id))
                    {
                        checkbox.Data = ((Field)fieldMeta)?.JsonAttributes.PrefilledText == "1";
                    }

                    return checkbox;

                case FieldType.Attachment:
                    return Attachments.FirstOrDefault(atch => atch.Id == fieldMeta.GetFieldContentId());

                case FieldType.RadioButton:
                    return Radiobuttons.FirstOrDefault(radio => radio.Id == fieldMeta.GetFieldContentId());

                default:
                    return default;
            }
        }
    }
}
