using TaskBLL.Helpers;
using TaskBLL.Interfaces;
using TaskBLL.Models;
using TaskDAL.Entities;
using TaskDAL.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TaskBLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task AddAsync(UserModel model)
        {
            if (_userRepository.GetUserByUserName(model.UserName) == null)
            {
                var userList = await _userRepository.GetAllAsync();
                var last = userList.LastOrDefault();
                var newIndex = last == null ? 1 : last.Id + 1;
                // The key should not be here
                string password = EncryptionHelper.Encrypt(model.Password, "bcndhkth78qiiopp");

                var user = new User(newIndex, model.Name, model.UserName, model.Email, password);

                await _userRepository.AddAsync(user);
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
                var userModel = new UserModel(user.Id, user.Name, user.UserName, user.Email, user.Password);
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
            var userModel = new UserModel(user.Id, user.Name, user.UserName, user.Email, user.Password);

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
            user.Email = model.Email;
            user.Name = model.Name;

            await _userRepository.UpdateAsync(user);
        }
    }
}
