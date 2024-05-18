using TaskBLL.Helpers;
using TaskBLL.Interfaces;
using TaskBLL.Models;
using TaskDAL.Entities;
using TaskDAL.Interfaces;
using TaskDAL.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TaskBLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task AddAsync(UserModel model)
        {
            if (await _userRepository.GetUserByUserName(model.UserName) == null)
            {
                // The key should not be here
                string password = EncryptionHelper.Encrypt(model.Password, "bcndhkth78qiiopp");

                var user = new User(model.Name, model.UserName, password);

                await _userRepository.AddAsync(user);
            }
            else
            {
                throw new Exception("The UserName exists");
            }

        }

        public async Task DeleteAsync(int modelId)
        {
            var user = await _userRepository.GetByIdAsync(modelId);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var userList = await _userRepository.GetAllWithDetails();
            var userModelList = new List<UserModel>();

            foreach (var user in userList)
            {
                var userModel = new UserModel(user.Id, user.Name, user.UserName, user.Password);
                if (user.Tasks != null)
                {
                    foreach(var task in user.Tasks)
                    {
                        userModel.TaskIds.Add(task.Id);
                    }
                }
                userModelList.Add(userModel);
            }

            return userModelList;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdWithDetails(id);
            var userModel = new UserModel(user.Id, user.Name, user.UserName, user.Password);

            if (user.Tasks != null)
            {
                foreach (var task in user.Tasks)
                {
                    userModel.TaskIds.Add(task.Id);
                }
            }

            return userModel;
        }

        public async Task UpdateAsync(UserModel model)
        {
            var user = await _userRepository.GetByIdWithDetails(model.Id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(model), "No such user to update");
            }

            user.UserName = model.UserName;
            user.Name = model.Name;

            await _userRepository.UpdateAsync(user);
        }

        public async Task<UserModel> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUserName(username);
            string decryptedPassword = EncryptionHelper.Decrypt(user.Password, "fakeEncryptionKey");

            if (password != decryptedPassword)
            {
                throw new InvalidOperationException("The password is incorrect");
            }

            var userModel = new UserModel(user.Id, user.Name, user.UserName, user.Password);

            return userModel;
        }
    }
}
