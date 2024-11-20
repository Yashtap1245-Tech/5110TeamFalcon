using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System;

namespace UnitTests.Model
{
    [TestFixture]
    public class CommentModelTests
    {
        #region Should_Initialize_Id_With_Unique_Guid

        // Test case to check if ID is properly initialized with a unique GUID
        [Test]
        public void Should_Initialize_Id_With_Unique_Guid()
        {
            // Arrange & Act
            var comment = new CommentModel();

            // Assert
            Assert.That(comment.Id, Is.Not.Null.Or.Empty);  // ID should not be null or empty
            Assert.That(Guid.TryParse(comment.Id, out _), Is.True);  // ID should be a valid GUID
        }

        #endregion Should_Initialize_Id_With_Unique_Guid

        #region Should_Set_Id_Property_Manually

        // Test case to check if the ID can be manually set
        [Test]
        public void Should_Set_Id_Property_Manually()
        {
            // Arrange
            var comment = new CommentModel();
            var newId = "12345678-1234-1234-1234-123456789abc";

            // Act
            comment.Id = newId;

            // Assert
            Assert.That(comment.Id, Is.EqualTo(newId));  // ID should be set to the new value
            Assert.That(Guid.TryParse(comment.Id, out _), Is.True);  // Ensure the new ID is a valid GUID
        }

        #endregion Should_Set_Id_Property_Manually

        #region Should_Set_Comment_Property_Correctly

        // Test case to check if Comment can be set correctly
        [Test]
        public void Should_Set_Comment_Property_Correctly()
        {
            // Arrange
            var commentText = "This is a test comment.";
            var comment = new CommentModel
            {
                Comment = commentText
            };

            // Act & Assert
            Assert.That(comment.Comment, Is.EqualTo(commentText));  // Comment should be correctly set
        }

        #endregion Should_Set_Comment_Property_Correctly
    }
}
