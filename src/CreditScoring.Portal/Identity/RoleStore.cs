﻿using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Identity
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly string _connectionString;

        public RoleStore(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqlConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = GetDbConnection())
            {
                await connection.OpenAsync(cancellationToken);
                role.Id = await connection.QuerySingleAsync<string>($@"INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName])
                    VALUES (@{nameof(ApplicationRole.Id)}, @{nameof(ApplicationRole.Name)}, @{nameof(ApplicationRole.NormalizedName)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", role);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = GetDbConnection())
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [AspNetRoles] SET
                    [Name] = @{nameof(ApplicationRole.Name)},
                    [NormalizedName] = @{nameof(ApplicationRole.NormalizedName)}
                    WHERE [Id] = @{nameof(ApplicationRole.Id)}", role);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = GetDbConnection())
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [AspNetRoles] WHERE [Id] = @{nameof(ApplicationRole.Id)}", role);
            }

            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = GetDbConnection())
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationRole>($@"SELECT * FROM [AspNetRoles]
                    WHERE [Id] = @{nameof(roleId)}", new { roleId });
            }
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = GetDbConnection())
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationRole>($@"SELECT * FROM [AspNetRoles]
                    WHERE [NormalizedName] = @{nameof(normalizedRoleName)}", new { normalizedRoleName });
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
