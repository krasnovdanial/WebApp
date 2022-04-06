using System;
using LinqToDB;
using LinqToDB.Data;
using Task.Web.Models;


namespace Task.Web.Repositories
{
    /// <summary>
    /// Содержит методы для работы с комментариями из бд.
    /// </summary>
    public class CommentRepository : Repository<UserComment>
    {
        /// <inheritdoc/>
        public CommentRepository(DataConnection dbConnection) : base(dbConnection)
        {
        }

        /// <inheritdoc/>
        public override bool TryAdd(UserComment entity)
        {
            CheckOnNull(entity, nameof(entity));

            entity.UserCommentId = Guid.NewGuid();
            entity.PublicationDate = DateTime.Now;

            var amountOFInserted = Entities
                .Value(comment => comment.UserCommentId, entity.UserCommentId)
                .Value(comment => comment.CommentatorUserId, entity.CommentatorUserId)
                .Value(comment => comment.CommentedUserId, entity.CommentedUserId)
                .Value(comment => comment.PublicationDate, entity.PublicationDate)
                .Value(comment => comment.CommentContent, entity.CommentContent)
                .Insert();

            return amountOFInserted == 1;
        }
    }
}
