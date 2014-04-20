using SilberScarves.Models;
using SilberScarves.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SilberScarves.Repositories;

namespace SilberScarves.services
{
    public class ProductsService
    {

        private SilberScarvesDbContext _context;
        private Repository<ScarfItem> _scarfRepo;
        private Repository<Customer> _custRepo;
        private OrderRepository _orderRepo;
        private AddressRepository _addrRepo;
      

        public ProductsService()
        {
            _context = new SilberScarvesDbContext();
            _scarfRepo = new ScarfItemRepository(_context);
            _custRepo = new CustomerRepository(_context);
            _orderRepo = new OrderRepository(_context);
            _addrRepo = new AddressRepository(_context);   
        }

        public void shipOrder(long orderId)
        {
           ScarfOrder o =  _orderRepo.getById(orderId);
            o.hasShipped = true;
            _orderRepo.update(o);
        }

        public IEnumerable<ScarfOrder> getCustomerOrderHistory(Customer c)
        {
            List<ScarfOrder> orders =
                _orderRepo.getAll().Where(order => order.customer == c && order.isCart == false ).ToList();            
            return orders;
        }

        public List<ScarfOrder> getCheckedOutOrders()
        {
            
            return _orderRepo.getAll().Where(o => o.isCart == false && o.hasShipped == false).ToList();
        }

        public ScarfOrder getOrder(long id)
        {
            return _orderRepo.getById(id);
        }

        public void RemoveFromCart(Customer c, long id)
        {
            ScarfOrder s = _orderRepo.getCustomerCart(c);
            List<ScarfItem> newScarves = new List<ScarfItem>();
            foreach (var scarf in s.Scarves)
            {
                if (scarf.scarfId != id)
                {
                    newScarves.Add(scarf);
                }
            }
            s.Scarves = newScarves;
            _orderRepo.update(s);
        }

        public ScarfOrder getCustomerCart(Customer customer){
            ScarfOrder s;

            if (customer == null)
            {
                s = EmptyCart();
            }
            else
            {
                s = _orderRepo.getCustomerCart(customer);
                if (s == null)
                {
                    s = EmptyCart(customer);
                    _orderRepo.add(s);
                }
            }
            return s;
        }

        public IEnumerable<ScarfOrder> getCustomerOrders(Customer customer)
        {
            List <ScarfOrder> s;
            if (customer == null)
            {
                s = new List<ScarfOrder>();
                ScarfOrder emptyOrder = EmptyCart();
                emptyOrder.isCart = false;
                s.Add(emptyOrder);
            }
            else
            {
                s = _orderRepo.getAll().Where(o => o.customer == customer && o.isCart == false) as List<ScarfOrder>;
                if(s == null || s.Count == 0)
                {
                    s = new List<ScarfOrder>(0);
                    ScarfOrder order  = EmptyCart(customer);
                    order.isCart = false;
                    
                    _orderRepo.add(order);
                    s.Add(order);
                }
            }
            return s;
        }

        public IEnumerable<ScarfItem> getAllScarves()
        {
            return _scarfRepo.getAll();
        }

        public void addScarf(ScarfItem s)
        {
            _scarfRepo.add(s);
        }

        public ScarfItem getScarf(long scarfId)
        {
            return _scarfRepo.getById(scarfId);
        }

        public void deleteScarf(long scarfId)
        {
            ScarfItem s = getScarf(scarfId);
            _scarfRepo.delete(s);
        }

        public void updateScarf(ScarfItem scarf)
        {
            _scarfRepo.update(scarf);
        }

        public void addOrder(ScarfOrder order)
        {
            _orderRepo.add(order);
        }

        public void updateOrder(ScarfOrder order)
        {
            _orderRepo.update(order);
        }

        public IEnumerable<Customer> getAllCustomers()
        {
            return _custRepo.getAll();
        }

        public Customer findCustomerByUsername(String customerName)
        {
            Customer cust = _custRepo.getAll().Where(c => c.username == customerName).FirstOrDefault();
            cust.address = _addrRepo.getById(cust.addressId);
            return cust;
        }

        public ScarfOrder EmptyCart(Customer customer = null)
        {
            return new ScarfOrder() { isCart = true, hasBeenPaidFor = false, customer = customer, hasShipped = false, Scarves = new List<ScarfItem>(0) };
        }

    }
}