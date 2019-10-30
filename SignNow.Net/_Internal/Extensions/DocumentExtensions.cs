using System;
using System.Text.RegularExpressions;

namespace SignNow.Net.Internal.Extensions
{
    static class DocumentExtensions
    {
        /// <summary>
        /// Pattern for Document Identyty
        /// Allowed format must be: 40-chars
        /// Allowed chars: digist, symbols a-z A-Z, underscore
        /// </summary>
        private const string documentIdPattern = @"^[a-zA-Z0-9_]{40,40}$";

        /// <summary>
        /// Validates Document ID
        /// </summary>
        /// <param name="documentId">Identity of the document</param>
        public static string ValidateDocumentId(this string documentId)
        {
            var regex = new Regex(documentIdPattern);

            if (!regex.IsMatch(documentId))
            {
                throw new ArgumentException(
                    "Invalid format of Document Id <" + documentId + ">. " +
                    "The required format: 40 characters long, case-sensitive, letters and numbers, underscore allowed."
                    );
            }

            return documentId;
        }
    }
}
