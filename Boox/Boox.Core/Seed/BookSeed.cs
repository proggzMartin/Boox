using Boox.Core.Models.Entities;

namespace Boox.Core.Seed
{
    public static class BookSeed
    {
        public static List<Book> Books = new List<Book>();

        private const int _listLengths = 13;
        private static Random r = new();

        //prices, 
        public static List<string> _authors, _descriptions, _genres, _ids, _titles;
        public static List<DateTime> _publishedDates;
        public static List<double> _prices;

        static BookSeed()
        {
            _authors = CreateStringList("Author", "joe", "kut", "Ralls, Kim" );
            _descriptions = CreateStringList("Description", "deploy", "applications" );
            _genres = CreateStringList("Genre", "com", "ter" );
            _ids = CreateStringList("B");
            _titles = CreateStringList("Title", "deploy", "ruby");

            _publishedDates = CreatePublishedDates();

            _prices = CreatePrices();

            for (int i = 0; i < _listLengths; i++)
            {
                Books.Add(
                    new Book()
                    {
                        //Id = _ids[i], //todo
                        Author = _authors[i],
                        Description = _descriptions[i],
                        Genre = _genres[i],
                        Title = _titles[i],
                        Published = _publishedDates[i],
                        Price = _prices[i]
                    } 
                );
            }
        }

        /// <summary>
        /// Fill out string-collection for seed in order to prevent
        /// copypaste code and get a more coherent structure
        /// </summary>
        /// <param name="prefixName">Prefix for non-specific names. Incremented with number.</param>
        /// <param name="customNames"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static List<string> CreateStringList(string prefixName, 
            params string[] customNames)
        {
            //Each name create 3 additional ones
            if (customNames.Length*3 > _listLengths)
            {
                throw new ArgumentException("Too many customnames, max allowed length is "+(_listLengths/3));
            }

            if (string.IsNullOrWhiteSpace(prefixName))
            {
                prefixName = "NoPrefix";
            }

            List<string> result = new();

            var customNamesDistortion = new List<string>();

            //Create some 'distortion' in the specific names
            foreach(var c in customNames)
            {
                customNamesDistortion.Add(c + 'B');
                customNamesDistortion.Add('A' + c);
                customNamesDistortion.Add('A' + c + 'B');
            }

            result.AddRange(customNamesDistortion);

            for (int i = customNamesDistortion.Count+1; i < _listLengths + 1; i++)
            {
                result.Add(prefixName + i);
            }

            return result;
        }

        private static List<double> CreatePrices()
        {
            List<double> result = new();

            for (int i = 0; i < _listLengths; i++)
            {
                //values interval [30, 45]
                result.Add(30 + 15 * r.NextDouble());
            }

            return result;
        }

        private static List<DateTime> CreatePublishedDates()
            => new List<DateTime>
            {
                new DateTime(2021, 1, 1),
                new DateTime(2021, 1, 13),
                new DateTime(2021, 3, 3),
                new DateTime(2021, 3, 15),

                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 4, 1),
                new DateTime(2020, 4, 26),

                new DateTime(2019, 1, 1),
                new DateTime(2018, 1, 1),
                new DateTime(2017, 1, 1),
                new DateTime(2016, 1, 1),

                new DateTime(2015, 1, 1),
            };
    }
}
