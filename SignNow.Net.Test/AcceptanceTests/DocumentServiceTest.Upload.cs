using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignNow.Net.Test.Constants;
using System;
using System.IO;

namespace AcceptanceTests
{
    public partial class DocumentServiceTest
    {
        private readonly string assertMsg =
            "Document Upload result should contain non-null Id property value on successful upload";

        [TestMethod]
        public void DocumentUploadWorksForPdf()
        {
            using (var fileStream = File.OpenRead(PdfFilePath))
            {
                var uploadResponse = SignNowTestContext.Documents.UploadDocumentAsync(fileStream, pdfFileName).Result;

                DocumentId = uploadResponse.Id;

                Assert.IsNotNull(uploadResponse.Id, assertMsg);
            }
        }

        [TestMethod]
        public void DocumentUploadWithFieldExtractWorksForPdf()
        {
            using (var fileStream = File.OpenRead(PdfFilePath))
            {
                var uploadResponse = SignNowTestContext.Documents.UploadDocumentWithFieldExtractAsync(fileStream, pdfFileName).Result;

                DocumentId = uploadResponse.Id;

                Assert.IsNotNull(uploadResponse.Id, assertMsg);

                var document = SignNowTestContext.Documents.GetDocumentAsync(DocumentId).Result;

                // Check if fields were extracted (One field with role should be in the document)
                Assert.IsNotNull(document.Roles.Count);
            }
        }

        [TestMethod]
        public void DocumentUploadExceptionIsCorrect()
        {
            using (var fileStream = File.OpenRead(TxtFilePath))
            {
                var exception = Assert
                    .ThrowsException<AggregateException>(
                        () => SignNowTestContext.Documents.UploadDocumentAsync(fileStream, txtFileName).Result);

                Assert.AreEqual(ErrorMessages.InvalidFileType, exception.InnerException?.Message);
            }
        }

        [TestMethod]
        public void DocumentUploadWithFieldExtractExceptionIsCorrect()
        {
            using (var fileStream = File.OpenRead(TxtFilePath))
            {
                var exception = Assert
                    .ThrowsException<AggregateException>(
                        () => SignNowTestContext.Documents.UploadDocumentWithFieldExtractAsync(fileStream, txtFileName).Result);

                Assert.AreEqual(ErrorMessages.InvalidFileType, exception.InnerException?.Message);
            }
        }
    }
}
