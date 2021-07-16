using System;
using System.Linq;
using System.Linq.Expressions;
using LinqLearn.Models;
using LinqLearn.Extensions;

namespace LinqLearn.Services {
    public class GameFilterProvider : IGameFilterProvider {
        public Func<Game, bool> GetFilterFunction(GameSearchSettings searchSettingsCollection) {
            Expression<Func<Game, bool>> filterExpression = game => true;

            filterExpression = filterExpression.And(FilterName(searchSettingsCollection))
                .And(FilterGenres(searchSettingsCollection))
                .And(FilterMinValue(searchSettingsCollection))
                .And(FilterMaxValue(searchSettingsCollection));

            return filterExpression.Compile();
        }

        private Expression<Func<Game, bool>> FilterName(GameSearchSettings searchSettingsCollection) {
            if (searchSettingsCollection.Name != default(string))
                return game => game.Name == searchSettingsCollection.Name;

            return game => true;
        }

        private Expression<Func<Game, bool>> FilterGenres(GameSearchSettings searchSettingsCollection) {
            if (searchSettingsCollection.Genres.Count != 0)
                return game => searchSettingsCollection.Genres.Intersect(game.Genres).Count() != 0;

            return game => true;
        }

        private Expression<Func<Game, bool>> FilterMinValue(GameSearchSettings searchSettingsCollection) {
            if (searchSettingsCollection.MinPrice.HasValue)
                return game => game.Price >= searchSettingsCollection.MinPrice.Value;

            return game => true;
        }

        private Expression<Func<Game, bool>> FilterMaxValue(GameSearchSettings searchSettingsCollection) {
            if (searchSettingsCollection.MaxPrice.HasValue)
                return game => game.Price <= searchSettingsCollection.MaxPrice.Value;

            return game => true;
        }
    }
}
