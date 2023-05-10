using KweetReadService.Models.MongoDb;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace KweetReadService.Data.Likes
{
    public interface ILikeMongoRepository<TDocument> where TDocument : IDocument
    {

        public Task DeleteByIdAsync(string id);

        Task InsertOneAsync(TDocument document);

        void FindOneAndDeleteAsync (
        Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

    }
}
