using Core.Domain.Interfaces.UnitOfWork;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly string connectionString;
		private IDbConnection connection;
		private bool disposed;

		public UnitOfWork()
		{
			connectionString = string.Empty;
		}

		public IDbConnection Connection
		{
			get
			{
				if (connection == null)
				{
					connection = new SqlConnection(connectionString);
				}

				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				return connection;
			}
		}

		public IDbTransaction Transaction { get; private set; }

		public void BeginTransaction()
		{
			disposed = false;

			if (Transaction == null)
				Transaction = Connection.BeginTransaction();
			else
				throw new InvalidOperationException("Não há transação aberta.");
		}

		public void Commit()
		{
			if (Transaction != null)
				Transaction.Commit();
			else
				throw new InvalidOperationException("Não há transação aberta.");
		}

		public void RollBack()
		{
			if (Transaction != null)
				Transaction.Rollback();
			else
				throw new InvalidOperationException("Não há transação aberta.");
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				if (Connection != null)
				{
					Connection.Close();
					Connection.Dispose();
				}

				if (Transaction != null)
					Transaction.Dispose();
			}

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
