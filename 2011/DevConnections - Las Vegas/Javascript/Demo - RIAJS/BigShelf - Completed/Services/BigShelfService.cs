// <copyright>
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace BigShelf
{
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.Web;
    using System.Web.Security;
    using BigShelf.Models;

    public enum Sort
    {
        None,
        Title,
        Author,
        Rating,
        MightRead
    }

    [RequiresAuthentication]
    [EnableClientAccess]
    public partial class BigShelfService : DbDomainService<BigShelfEntities>
    {
        public IQueryable<Book> GetBooks()
        {
            return this.DbContext.Books;
        }

        public IQueryable<Book> GetBooksForSearch(int[] profileIds, Sort sort, bool sortAscending)
        {
            IQueryable<Book> booksQuery = this.DbContext.Books;
            if (profileIds != null && profileIds.Length > 0)
            {
                // apply any profile ID filter
                booksQuery = booksQuery.Where(p => p.FlaggedBooks.Any(q => profileIds.Contains(q.ProfileId)));
            }

            return this.ApplyOrderBy(booksQuery, sort, sortAscending);
        }

        private IQueryable<Book> ApplyOrderBy(IQueryable<Book> booksQuery, Sort sort, bool sortAscending)
        {
            switch (sort)
            {
                case Sort.Title:
                    return sortAscending ? booksQuery.OrderBy(book => book.Title) : booksQuery.OrderByDescending(book => book.Title);
                case Sort.Author:
                    return sortAscending ? booksQuery.OrderBy(book => book.Author) : booksQuery.OrderByDescending(book => book.Author);
                case Sort.Rating:
                case Sort.MightRead:
                    // Non-Flagged books are always sorted to the end. Rated books sort based on
                    // their rating. Flagged books are sorted to the top for 'MightRead', and immediately
                    // after already rated books for 'Rating'.
                    int flaggedBookWeight = sort == Sort.Rating ? 0 : 6;
                    int authenticatedProfileId = this.GetUser().Id;

                    return
                        from book in booksQuery
                        let flaggedBook = book.FlaggedBooks.Where(p => p.ProfileId == authenticatedProfileId).FirstOrDefault()
                        let weighting = flaggedBook == null ? -1 : (flaggedBook.IsFlaggedToRead != 0 ? flaggedBookWeight : flaggedBook.Rating)
                        orderby sortAscending ? weighting : -weighting
                        select book;
                case Sort.None:
                default:
                    return booksQuery;
            }
        }

        private Profile GetUser()
        {
            var aspNetGuid = Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString();
            return this.DbContext.Profiles.Single(p => p.AspNetUserGuid == aspNetGuid);
        }

        public void InsertFlaggedBook(FlaggedBook entity)
        {
            EvaluateAndSetIsFlaggedToRead(entity);

            DbEntityEntry<FlaggedBook> entityEntry = this.DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbContext.FlaggedBooks.Add(entity);
            }
        }

        public void UpdateFlaggedBook(FlaggedBook currentEntity)
        {
            EvaluateAndSetIsFlaggedToRead(currentEntity);
            FlaggedBook originalEntity = this.ChangeSet.GetOriginal(currentEntity);
            this.DbContext.FlaggedBooks.AttachAsModified(currentEntity, originalEntity, this.DbContext);
        }

        private static void EvaluateAndSetIsFlaggedToRead(FlaggedBook entity)
        {
            // Determine IsFlaggedToRead based on Rating
            if (entity.Rating == 0)
            {
                entity.IsFlaggedToRead = 1;
            }
            else
            {
                entity.IsFlaggedToRead = 0;
            }
        }

        public void DeleteFlaggedBook(FlaggedBook entity)
        {
            DbEntityEntry<FlaggedBook> entityEntry = this.DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbContext.FlaggedBooks.Attach(entity);
                this.DbContext.FlaggedBooks.Remove(entity);
            }
        }

        public IQueryable<Friend> GetFriends()
        {
            return this.DbContext.Friends;
        }

        public void InsertFriend(Friend entity)
        {
            DbEntityEntry<Friend> entityEntry = this.DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbContext.Friends.Add(entity);
            }
        }

        public void UpdateFriend(Friend currentEntity)
        {
            Friend originalEntity = this.ChangeSet.GetOriginal(currentEntity);
            this.DbContext.Friends.AttachAsModified(currentEntity, originalEntity, this.DbContext);
        }

        public void DeleteFriend(Friend entity)
        {
            DbEntityEntry<Friend> entityEntry = this.DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbContext.Friends.Attach(entity);
                this.DbContext.Friends.Remove(entity);
            }
        }

        public IQueryable<Profile> GetProfiles()
        {
            return this.DbContext.Profiles;
        }

        public Profile GetProfileForSearch()
        {
            var authenticatedProfileId = this.GetUser().Id;
            return this.DbContext.Profiles.Include("Friends.FriendProfile").Include("FlaggedBooks").Single(p => p.Id == authenticatedProfileId);
        }

        public Profile GetProfileForProfileUpdate()
        {
            var authenticatedProfileId = this.GetUser().Id;
            return this.DbContext.Profiles.Include("Friends.FriendProfile").Single(p => p.Id == authenticatedProfileId);
        }

        public void InsertProfile(Profile entity)
        {
            DbEntityEntry<Profile> entityEntry = this.DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbContext.Profiles.Add(entity);
            }
        }

        public void UpdateProfile(Profile currentEntity)
        {
            Profile originalEntity = this.ChangeSet.GetOriginal(currentEntity);
            this.DbContext.Profiles.AttachAsModified(currentEntity, originalEntity, this.DbContext);
        }

        public void DeleteProfile(Profile entity)
        {
            DbEntityEntry<Profile> entityEntry = this.DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbContext.Profiles.Attach(entity);
                this.DbContext.Profiles.Remove(entity);
            }
        }
    }
}