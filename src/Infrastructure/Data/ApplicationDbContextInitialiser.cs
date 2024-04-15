using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using HotelManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        #region Seed Data (Roles, Users)

        var administratorRole = new IdentityRole(Roles.Administrator);
        var customerRole = new IdentityRole(Roles.Customer);
        var receptionistRole = new IdentityRole(Roles.Receptionist);
        var cleanerRole = new IdentityRole(Roles.Cleaner);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        if (_roleManager.Roles.All(r => r.Name != customerRole.Name))
        {
            await _roleManager.CreateAsync(customerRole);
        }

        if (_roleManager.Roles.All(r => r.Name != receptionistRole.Name))
        {
            await _roleManager.CreateAsync(receptionistRole);
        }

        if (_roleManager.Roles.All(r => r.Name != cleanerRole.Name))
        {
            await _roleManager.CreateAsync(cleanerRole);
        }

        // Create default users (admin, customer, receptionist, cleaner)
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
        var customer = new ApplicationUser { UserName = "customer@localhost", Email = "customer@localhost" };
        var receptionist = new ApplicationUser { UserName = "receptionist@localhost", Email = "receptionist@localhost" };
        var cleaner = new ApplicationUser { UserName = "cleaner@localhost", Email = "cleaner@localhost" };


        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        if (_userManager.Users.All(u => u.UserName != customer.UserName))
        {
            await _userManager.CreateAsync(customer, "Customer1!");
            if (!string.IsNullOrWhiteSpace(customerRole.Name))
            {
                await _userManager.AddToRolesAsync(customer, new[] { customerRole.Name });
            }
        }

        if (_userManager.Users.All(u => u.UserName != receptionist.UserName))
        {
            await _userManager.CreateAsync(receptionist, "Receptionist1!");
            if (!string.IsNullOrWhiteSpace(receptionistRole.Name))
            {
                await _userManager.AddToRolesAsync(receptionist, new[] { receptionistRole.Name });
            }
        }

        if (_userManager.Users.All(u => u.UserName != cleaner.UserName))
        {
            await _userManager.CreateAsync(cleaner, "Cleaner1!");
            if (!string.IsNullOrWhiteSpace(cleanerRole.Name))
            {
                await _userManager.AddToRolesAsync(cleaner, new[] { cleanerRole.Name });
            }
        }

        #endregion

        #region Seed Data (Rooms, Bookings)
        // Seed the rooms if necessary
        if (!_context.Rooms.Any())
        {
            _context.Rooms.Add(Room.Create(1, RoomStatus.New ,RoomType.Simple, true, true));
            _context.Rooms.Add(Room.Create(2, RoomStatus.NoIssue, RoomType.Double, true, true));
            _context.Rooms.Add(Room.Create(5, RoomStatus.Renovated, RoomType.Suite, true, true));

            await _context.SaveChangesAsync();
        }

        // Seed the bookings if necessary
        if (!_context.Bookings.Any())
        {
            var room = _context.Rooms.First();

            Booking.Create(DateTime.Now.AddDays(5), DateTime.Now.AddDays(10), room);

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
