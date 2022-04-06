using LinqToDB.Configuration;
using LinqToDB.Data;

namespace Task.Web.Db
{
    /// <summary>
    /// Бд магазина.
    /// </summary>
    public class ShopDbContext : DataConnection
    {
        /// <summary>
        /// Создает экземпляр бд.
        /// </summary>
        /// <param name="options">Настройки подключения к бд.</param>
        public ShopDbContext(LinqToDbConnectionOptions<ShopDbContext> options) :
            base(options)
        {

        }
    }
}
