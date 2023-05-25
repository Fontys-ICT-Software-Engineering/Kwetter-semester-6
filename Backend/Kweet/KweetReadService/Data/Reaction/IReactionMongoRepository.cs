using KweetReadService.Models.MongoDb;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace KweetReadService.Data.Reaction
{
    public interface IReactionMongoRepository<TDocument> where TDocument : IDocument
    {
        IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        void InsertOne(TDocument document);

        TDocument FindById(string id);

        Task<TDocument> FindByIdAsync(string id);

        void DeleteById(string id);

        public Task DeleteByIdAsync(string id);

        Task InsertOneAsync(TDocument document);

        Task ReplaceOneAsync(TDocument document);

        Task DeleteByKweetId(
        Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteManyByUserID(
        Expression<Func<TDocument, bool>> filterExpression);

    }
}
