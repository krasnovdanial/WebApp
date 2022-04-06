using System;
using LinqToDB.Mapping;

namespace Task.Web.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id.
        /// </summary>
        [PrimaryKey, Identity]
        public Guid UserId { get; set; }
        /// <summary>
        /// Id роли пользователя.
        /// </summary>
        [Column]
        public Guid UserRoleId { get; set; }
    }
}