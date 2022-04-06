using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToDB;
using LinqToDB.Data;

namespace Task.Web.Repositories
{
    /// <summary>
    /// Содержит методы для работы с определенной таблицей из бд.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public abstract class Repository<TEntity>
        where TEntity : class
    {
        private readonly DataConnection _dbConnection;

        protected ITable<TEntity> Entities => _dbConnection.GetTable<TEntity>();

        /// <summary>
        /// Создает репозиторий.
        /// </summary>
        /// <param name="dbConnection">Бд, к которой будут осуществляться запросы.</param>
        protected Repository(DataConnection dbConnection)
        {
            CheckOnNull(dbConnection, nameof(dbConnection));

            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Получает все записи из таблицы.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        /// <summary>
        /// Получает все записи из таблицы, которые удовлетворяют <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Предикат, по которому фильтруются записи.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            CheckOnNull(predicate, nameof(predicate));

            return Entities
                    .Where(predicate)
                    .ToList();
        }

        /// <summary>
        /// Удаляет все записи, которые удовлетворяют <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Количество удаленных записей.</returns>
        public virtual int DeleteByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            CheckOnNull(predicate, nameof(predicate));

            return Entities
                .Delete(predicate);
        }

        /// <summary>
        /// Добавляет запись в бд. Если этого сделать не удалось
        /// , то возвращается false, иначе true.
        /// </summary>
        /// <param name="entity">Добавляемая запись.</param>
        /// <returns></returns>
        public virtual bool TryAdd(TEntity entity)
        {
            CheckOnNull(entity, nameof(entity));

            try
            {
                _dbConnection.Insert(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void CheckOnNull(object obj, string paramName)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}