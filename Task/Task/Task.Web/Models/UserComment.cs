using System;
using LinqToDB.Mapping;

namespace Task.Web.Models
{
    /// <summary>
    /// Комментарии пользователя.
    /// </summary>
    public class UserComment
    {
        /// <summary>
        /// Id.
        /// </summary>
        [PrimaryKey]
        public Guid UserCommentId { get; set; }
        /// <summary>
        /// Id пользователя, для которого оставили комментарий.
        /// </summary>
        [Column]
        public Guid CommentatorUserId { get; set; }
        /// <summary>
        /// Id пользователя, который оставил комментарий.
        /// </summary>
        [Column]
        public Guid CommentedUserId { get; set; }
        /// <summary>
        /// Содержание комментарий.
        /// </summary>
        [Column, NotNull]
        public string CommentContent { get; set; }
        /// <summary>
        /// Дата публикации.
        /// </summary>
        [Column]
        public DateTime PublicationDate { get; set; }
    }
}