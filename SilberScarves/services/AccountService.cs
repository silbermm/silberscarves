using SilberScarves.Models;
using SilberScarves.Models.Security;
using SilberScarves.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilberScarves.services
{
    public class AccountService
    {

        private SilberScarvesDbContext _context;
        private Repository<Customer> _customerRepository;
        private Repository<Address> _addressRepository;
        private Repository<User> _userRepository;
        private Repository<Roles> _roleRepository;

        public AccountService()
        {
            this._context = new SilberScarvesDbContext();
            this._customerRepository = new CustomerRepository(this._context);
            this._addressRepository = new AddressRepository(this._context);
            this._userRepository = new UserRepository(this._context);
            this._roleRepository = new RolesRepository(this._context);
        }

        public AccountService(SilberScarvesDbContext context)
        {
            this._context = context;
            this._customerRepository = new CustomerRepository(context);
            this._addressRepository = new AddressRepository(context);
        }

        public Customer findCustomer(String username)
        {
            return _customerRepository.getAll().Where(c => c.username == username).FirstOrDefault();
        }

        public User findUser(String username)
        {
            return _userRepository.getAll().Where(u => u.Username == username).FirstOrDefault();
        }



    }
}