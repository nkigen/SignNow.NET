using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignNow.Net.Interfaces;
using SignNow.Net.Internal.Extensions;
using SignNow.Net.Service;
using SignNow.Net.Test;

namespace AcceptanceTests
{
    [TestClass]
    public partial class DocumentServiceTest : AuthorizedApiTestBase
    {
        [TestMethod]
        public void ShouldGetDocumentInfo()
        {
            DocumentId = UploadTestDocument(PdfFilePath);
            var response = SignNowTestContext.Documents.GetDocumentAsync(DocumentId).Result;

            Assert.AreEqual(DocumentId, response.Id);
            Assert.AreEqual(1, response.PageCount);
            Assert.AreEqual(pdfFileName, response.OriginalName);
            Assert.AreEqual("DocumentUpload", response.Name);

            Assert.IsNotNull(response.UserId.ValidateId());
            Assert.IsNotNull(response.Created);
            Assert.IsNotNull(response.Updated);

            Assert.IsNull(response.OriginDocumentId);
            Assert.IsNull(response.OriginUserId);

            Assert.IsFalse(response.IsTemplate);
        }
    }
}
