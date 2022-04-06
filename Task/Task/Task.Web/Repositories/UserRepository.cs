using LinqToDB.Data;
using Task.Web.Models;

namespace Task.Web.Repositories
{

    /// <summary>
    /// Содержит методы для работы с пользователями из бд.
    /// </summary>
    public class UserRepository: Repository<User>
    {
        /// <inheritdoc/>
        public UserRepository(DataConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
