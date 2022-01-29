﻿using Core.Entities;

namespace Boox.Core.Seed
{
    public static class BookSeed
    {
        public static List<Book> Books = new List<Book>();

        private const int _listLengths = 13;
        private static Random r = new();

        //prices, 
        private static List<string> _authors, _descriptions, _genres, _ids, _titles;
        private static List<DateTime> _publishedDates;
        private static List<double> _prices;

        static BookSeed()
        {
            _authors = CreateStringList("Author", "joe", "kut" );
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
                        Id = _ids[i],
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
        /// copypaste code and get coherent structure
        /// </summary>
        /// <param name="customNames">Actual names to add into array. Adds from start</param>
        /// <param name="prefixName">The prefix before count-number</param>
        /// <param name="resultLength">total length of resulting collection</param>
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

            //Create 'distortion' in names
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
        {
            List<DateTime> result = new();

            for (int i = 0; i < _listLengths; i++)
            {
                //just take some time back.
                var subtractDays = 60 + 4000 * r.NextDouble();

                result.Add(DateTime.Now.Subtract(
                    TimeSpan.FromDays(subtractDays)));
            }

            return result;
        }
    }
}