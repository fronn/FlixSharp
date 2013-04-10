﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixSharp.Holders.RottenTomatoes;
using Newtonsoft.Json.Linq;
using FlixSharp.Holders;
using FlixSharp.Helpers.Async;
using FlixSharp.Helpers.RottenTomatoes;

namespace FlixSharp.Queries.RottenTomatoes
{
    public class Fill
    {
        public FillLists Lists = new FillLists();
        public FillTitles Titles = new FillTitles();

        internal static async Task<List<Title>> GetBaseTitleInfo(Task<JObject> json)
        {
            dynamic dynjson = await json;
            List<Title> movies = new List<Title>(dynjson.movies.Count);
            //dynamic dynjson = json.Result;
            ///rewrite this for dynamic usage?
            throw new Exception(dynjson.movies[0].id);
            //movies.AddRange(
            //    from movie
            //    in dynjson.Children()
            //    select new Title()
            //    {
            //        Id = movie["id"].ToString(),
            //        FullTitle = movie["title"].ToString(),
            //        Year = (String)movie["year"] != "" ? (Int32)movie["year"] : 0,
            //        Rating = (MpaaRating)Enum.Parse(typeof(MpaaRating), movie["mpaa_rating"].ToString().Replace("-","")),
            //        RunTime = (String)movie["runtime"] != "" ? (Int32)movie["runtime"] : 0,
            //        CriticsConsensus = (String)movie["critics_consensus"],
            //        Ratings = movie["ratings"].HasValues ? new List<Rating>
            //        {
            //            new Rating{ 
            //                Type = RatingType.Critic,
            //                RottenTomatoRating = movie["ratings"]["critics_rating"] != null && (String)movie["ratings"]["critics_rating"] != "" 
            //                ? (RottenRating)Enum.Parse(typeof(RottenRating), movie["ratings"]["critics_rating"].ToString().Replace(" ","")) : RottenRating.None,
            //                Score = movie["ratings"]["critics_score"] != null && (String)movie["ratings"]["critics_score"] != ""
            //                ? (Int32)movie["ratings"]["critics_score"] : -1
            //            },
            //            new Rating{ 
            //                Type = RatingType.Audience,
            //                RottenTomatoRating = movie["ratings"]["audience_rating"] != null && (String)movie["ratings"]["audience_rating"] != "" 
            //                ? (RottenRating)Enum.Parse(typeof(RottenRating), movie["ratings"]["audience_rating"].ToString().Replace(" ","")) : RottenRating.None,
            //                Score = movie["ratings"]["audience_score"] != null && (String)movie["ratings"]["audience_score"] != "" 
            //                ? (Int32)movie["ratings"]["audience_score"] : -1
            //            }
            //        } : new List<Rating>(),
            //        ReleaseDates = movie["release_dates"].HasValues ? new List<ReleaseDate>
            //        {///this is dumb, but I can't easily access the JProperty.Name value in a LINQ statement, for some reason
            //            new ReleaseDate
            //            {
            //                ReleaseType = ReleaseDateType.Theater,
            //                Date = movie["release_dates"]["theater"] != null ? (DateTime?)DateTime.Parse(movie["release_dates"]["theater"].ToString()) : null
            //            },
            //            new ReleaseDate
            //            {
            //                ReleaseType = ReleaseDateType.DVD,
            //                Date = movie["release_dates"]["dvd"] != null ? (DateTime?)DateTime.Parse(movie["release_dates"]["dvd"].ToString()) : null
            //            }
            //        } : new List<ReleaseDate>(),
            //        Synopsis = movie["synopsis"].ToString(),
            //        AlternateIds = movie["alternate_ids"] != null && movie["alternate_ids"].HasValues ? new List<AlternateId>
            //        {
            //            new AlternateId
            //            {
            //                Type = AlternateIdType.Imdb,
            //                Id = movie["alternate_ids"]["imdb"] != null && (String)movie["alternate_ids"]["imdb"] != "" 
            //                ? movie["alternate_ids"]["imdb"].ToString() : ""
            //            }
            //        } : new List<AlternateId>(),
            //        RottenTomatoesSiteUrl = movie["links"].HasValues ? movie["links"]["alternate"].ToString() : "",
            //        Studio = movie["studio"] != null ? movie["studio"].ToString() : "",
            //        Posters = movie["posters"] != null && movie["posters"].HasValues ? new List<Poster>
            //        {
            //            new Poster
            //            {
            //                 Type = PosterType.Thumbnail,
            //                 Url = (String)movie["posters"]["thumbnail"]
            //            },
            //            new Poster
            //            {
            //                 Type = PosterType.Profile,
            //                 Url = (String)movie["posters"]["profile"]
            //            },
            //            new Poster
            //            {
            //                 Type = PosterType.Detailed,
            //                 Url = (String)movie["posters"]["detailed"]
            //            },
            //            new Poster
            //            {
            //                 Type = PosterType.Original,
            //                 Url = (String)movie["posters"]["original"]
            //            }
            //        } : new List<Poster>(),
            //        Actors = movie["abridged_cast"] != null && movie["abridged_cast"].HasValues 
            //            ? new List<Person>(from actor 
            //                               in movie["abridged_cast"]
            //                               select new Person
            //                               {
            //                                    Id = (String)actor["id"],
            //                                    Name = (String)actor["name"],
            //                                    Characters = actor["characters"] != null
            //                                        ? new List<String>(actor["characters"].HasValues 
            //                                            ? actor["characters"].Select(a => a.ToString()) : new String[] { }) 
            //                                            : new List<String>()
            //                               }) : new List<Person>(),
            //        Directors = movie["abridged_directors"] != null && movie["abridged_directors"].HasValues
            //            ? new List<String>(from director
            //                                in movie["abridged_directors"]
            //                               select (String)director["name"]) : new List<String>(),
            //        Genres = movie["genres"] != null && movie["genres"].HasValues
            //            ? new List<String>(movie["genres"].Select(g => g.ToString())) : new List<String>()
            //    });

            return movies;
        }
    }
    public class FillLists
    {
        #region Movie Lists
        public async Task<Titles> GetBoxOffice(String Country = "us", Int32 Limit = 10)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.BoxOfficeUrl(Country, Limit));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }

        public async Task<Titles> GetInTheaters(String Country = "us", Int32 Limit = 10, Int32 Page = 1)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.InTheatersUrl(Country, Limit));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }

        public async Task<Titles> GetOpeningMovies(String Country = "us", Int32 Limit = 10)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.OpeningMoviesUrl(Country, Limit));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }
        public async Task<Titles> GetUpcomingMovies(String Country = "us", Int32 Limit = 10, Int32 Page = 1)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.UpcomingMoviesUrl(Country, Limit, Page));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }
        #endregion

        #region DVD Lists
        public async Task<Titles> GetTopRentals(String Country = "us", Int32 Limit = 10)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.TopRentalsUrl(Country, Limit));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }

        public async Task<Titles> GetCurrentReleaseDVDs(String Country = "us", Int32 Limit = 10, Int32 Page = 1)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.CurrentReleaseDVDsUrl(Country, Limit, Page));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }

        public async Task<Titles> GetNewReleaseDVDs(String Country = "us", Int32 Limit = 10, Int32 Page = 1)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.NewReleaseDVDsUrl(Country, Limit, Page));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }

        public async Task<Titles> GetUpcomingDVDs(String Country = "us", Int32 Limit = 10, Int32 Page = 1)
        {
            Login.CheckInformationSet();
            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.UpcomingDVDsUrl(Country, Limit, Page));

            return new Titles(await Fill.GetBaseTitleInfo(moviejson));
        }
        #endregion

        
    }

    public class FillTitles
    {
        #region Detailed Info
        /// <summary>
        /// Full title information from a Rotten Tomatoes Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Title> GetMoviesInfo(String Id)
        {
            Login.CheckInformationSet();

            var moviejson = AsyncHelpers.RottenTomatoesLoadJObjectAsync(
                UrlBuilder.MoviesInfoUrl(Id));

            return (await Fill.GetBaseTitleInfo(moviejson)).FirstOrDefault();
        }
        #endregion
    }
}
