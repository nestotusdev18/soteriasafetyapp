using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Repository;
using Soteria.DataComponents.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection connection;
        private IDbTransaction transaction;
        private IGenericRepository genericRepository;
        private IUserRepository userRepository;
        private IStudentRepository studentRepository;
        private ISRORepository sroRepository;
        private IGatewayRepository gatewayRepository;
        private bool disposed;
        public UnitOfWork()
        {
            connection = new SqlConnection(ConfigHelper.GetDefaultConnectionString());
            connection.Open();
            transaction = connection.BeginTransaction();
        }
        public IGenericRepository GenericRepository
        {
            get { return genericRepository ?? (genericRepository = new GenericRepository(transaction)); }
        }

        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(transaction)); }
        }

        public IStudentRepository StudentRepository
        {
            get { return studentRepository ?? (studentRepository = new StudentRepository(transaction)); }
        }

        public ISRORepository SRORepository
        {
            get { return sroRepository ?? (sroRepository = new SRORepository(transaction)); }
        }

        public IGatewayRepository GatewayRepository
        {
            get { return gatewayRepository ?? (gatewayRepository = new GatewayRepository(transaction)); }
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                resetRepositories();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void resetRepositories()
        {
            genericRepository = null;
        }
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposed = true;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
