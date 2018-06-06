using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_seed.Models{


    public class User
    {
        
        [BsonId]
        public ObjectId ID { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string meta { get; set; }

    }

    public class UserContext 
    {
        private readonly IMongoDatabase _database = null;

        public UserContext(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }
    }

    public interface IUserRepository 
    {
        Task< IEnumerable<User> > GetAllUsers();

        Task<User> GetUser(string id);

        // add new note document
        Task<string> CreateUser(User item);

        // remove a user
        Task<bool> DeleteUser(string id);

        // update just a given user
        Task<bool> UpdateUseer(string id, User user);
    }

    public class UserRepository : IUserRepository
    {

        private readonly UserContext _context = null;

        public UserRepository(IOptions<DBSettings> settings)
        {
            _context = new UserContext(settings);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUser(string id)
        {
            try
            {
                return await _context.Users.Find(n => n.ID.Equals(id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CreateUser(User item)
        {
            try
            {
                await _context.Users.InsertOneAsync(item);
                return "yo";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                var actionResult = await _context.Users.DeleteOneAsync(
                    Builders<User>.Filter.Eq("Id", id)
                );

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> UpdateUseer(string id, User user)
        {
            try
            {
                var actionResult = await _context.Users
                    .ReplaceOneAsync(n => n.ID.Equals(id), user, new UpdateOptions { IsUpsert = true });

                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }


}
