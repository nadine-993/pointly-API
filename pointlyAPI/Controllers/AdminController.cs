using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pointly.Data;
using Pointly.DTOs;
using Pointly.Models;
using BCrypt.Net;
using System.Data.SqlTypes;
using Poitnly.DTOs;

namespace Pointly.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize (Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AdminController (ApplicationDbContext context)
        {
            _context= context;
        }
        /// <summary>
        /// Create Initial Admin User (Run this once)
        /// POST: api/admin/initialize
        /// </summary>
        [HttpPost ("initialize")]
        [AllowAnonymous]
        public async Task <ActionResult> InitializeAdmin ()
        {
            var existingAdmin = await _context.Users.AnyAsync (u => u.Role == UserRole.Admin);
        if (existingAdmin)
            {
                return BadRequest (new { message = "Admin already exists"});

            }

            var adminUser = new User
            {
                Email = "admin@pointly.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword ("Admin123!"),
                FullName= "System Administrator",
                PhoneNumber = "+1234566454",
                Role = UserRole.Admin,
                IsActive = true
            };
            _context.Users.Add (adminUser);
            await _context.SaveChangesAsync();

            var admin = new Admin
            {
                UserId = adminUser.Id,
                TotalPointsPool = 1000000
            };

            _context.Admins.Add (admin);
            await _context.SaveChangesAsync();
            return Ok (new
            {
                message = "Admin initialized successfully",
                email = "admin@pointly.com",
                password = "Admin123!",
                note = "Please change the password after first login"
            });
        }

        ///<summary>
        /// Get Admin Dashboard Stats
        /// Get: api/admin/dashboard
        /// </summary>
        [HttpGet ("dashboard")]
        public async Task <ActionResult> GetDashboard ()
        {
            var admin = await _context.Admins
                .Include (a => a.User)
                .FirstOrDefaultAsync();
                var totalMerchants = await _context.Merchants.CountAsync();
                var totalChargers = await _context.Chargers.CountAsync ();
                var totalCustomers = await _context.Customers.CountAsync();
                var totalOffers = await _context.Offers.CountAsync();
                var activeOffers = await _context.Offers.CountAsync(o => o.IsActive);
                return Ok(new
                {
                    adminPointsPool = admin?.TotalPointsPool ?? 0,
                    totalMerchants,
                    totalChargers,
                    totalCustomers,
                    totalOffers,
                    activeOffers
                });
                       }

    /// <summary>
    /// Create a Mercant
    /// POST: api/admin/merchants
    /// </summary>
    [HttpPost("merchants")]
    public async Task <ActionResult<MerchantResponse>> CreateMerchant ([FromBody] CreateMerchantRequest request)
        {
            if (await _context.Users.AnyAsync (u => u.Email == request.Email))
            {
                return BadRequest (new {message = "Email laready exists"});

            }

            //Create user
            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword (request.Password),
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Role= UserRole.Merchant,
                IsActive = true
            };
            _context.Users.Add (user);
            await _context.SaveChangesAsync();
            var merchant = new Merchant
            {
                UserId= user.Id,
                BusinessName = request.BusinessName,
                Description = request.Description,
                Address = request.Address,
                Category = request.Category,
                PointsBalance = 0
            };

            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();

            return CreatedAtAction (nameof (GetMerchantById), new {id= merchant.Id}, new MerchantResponse
            {
                Id= merchant.Id,
                Email= user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                BusinessName = merchant.BusinessName,
                Description = merchant.Description,
                Address= merchant.Address,
                Category = merchant.Category,
                PointsBalance = merchant.PointsBalance,
                CreatedAt = merchant.CreatedAt
            });
        } 

        /// <summary>
        /// Get All Merchants
        /// Get: api/admin/merchants
        /// </summary>
        [HttpGet("merchants")]
        public async Task<ActionResult<IEnumerable<MerchantResponse>>> getAllMerchants()
        {
            var merchants = await _context.Merchants
                    .Include (m => m.User)
                    .Select (m=> new MerchantResponse
                    {
                        Id= m.Id,
                        Email = m.User.Email,
                        FullName = m.User.FullName,
                        PhoneNumber = m.User.PhoneNumber,
                        BusinessName = m.BusinessName,
                        Description = m.Description,
                        Address = m.Address,
                        Category = m.Category,
                        PointsBalance = m.PointsBalance, 
                        CreatedAt = m.CreatedAt
                    })
                    .ToListAsync ();
                    return Ok (merchants);
                            }
        /// <summary>
        /// Get Merchants by ID
        /// Get: api/admin/merchants/5
        /// </summary>
        [HttpGet("merchants/{id}")]
        public async Task <ActionResult<MerchantResponse>> GetMerchantById(int id)
        {
            var merchant = await _context.Merchants
            .Include(m => m.User)
            .FirstOrDefaultAsync (m => m.Id ==id);
            if (merchant == null)
            {
                return NotFound(new {message = "Merchant not found"});

            }
            return Ok (new MerchantResponse {
                Id = merchant.Id,
                Email = merchant.User.Email,
                FullName = merchant.User.FullName,
                PhoneNumber = merchant.User.PhoneNumber,
                BusinessName = merchant.BusinessName,
                Description = merchant.Description,
                Address = merchant.Address,
                Category = merchant.Category,
                PointsBalance = merchant.PointsBalance,
                CreatedAt = merchant.CreatedAt
            });
        }


        /// <summary>
        /// Create an Offer for a Merchant
        /// POST: api/admin/offers
        /// </summary>
        [HttpPost("offers")]
        public async Task <ActionResult<OfferResponse>> CreateOffer ([FromBody] CreateOfferRequest request)
        {
            var merchant = await _context.Merchants
                        .Include (m=> m.User)
                        .FirstOrDefaultAsync (m => m.Id == request.MerchantId);
                         if (merchant ==null)
            {
                return NotFound (new { message = "Merchant not found"});
            }
            var offer = new Offer
            {
                MerchantId = request.MerchantId,
                Title = request.Title,
                Description = request.Description,
                PointsCost = request.PointsCost,
                ImageUrl = request.ImageUrl,
                ValidFrom = request.ValidFrom,
                ValidUntil = request.ValidUntil,
                QuantityAvailable = request.QuantityAvailable,
                IsActive = true
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync ();

            return CreatedAtAction (nameof (GetOfferById), new {id = offer.Id}, new OfferResponse
            {
               
                Id = offer.Id,
                MerchantId = offer.MerchantId,
                MerchantName = merchant.BusinessName,
                Title = offer.Title,
                Description = offer.Description,
                PointsCost = offer.PointsCost,
                ImageUrl = offer.ImageUrl,
                ValidFrom = offer.ValidFrom,
                ValidUntil = offer.ValidUntil,
                IsActive = offer.IsActive,
                QuantityAvailable = offer.QuantityAvailable,
                CreatedAt = offer.CreatedAt
            });
            }

        ///<summary>
        /// Get All Offers
        /// Get: api/admin/offers
        /// </summary>
        [HttpGet ("offers")]
        public async Task <ActionResult <IEnumerable<OfferResponse>>> GetAllOffers()
        {
            var offer = await _context.Offers
                    .Include (o => o.Merchant)
                    .Select (o => new OfferResponse
                    {
                    Id = o.Id,
                    MerchantId = o.MerchantId,
                    MerchantName = o.Merchant.BusinessName,
                    Title = o.Title,
                    Description = o.Description,
                    PointsCost = o.PointsCost,
                    ImageUrl = o.ImageUrl,
                    ValidFrom = o.ValidFrom,
                    ValidUntil = o.ValidUntil,
                    IsActive = o.IsActive,
                    QuantityAvailable = o.QuantityAvailable,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();

            return Ok(offer);   
        }
       

       ///<summary>
       /// Get Offer by ID
       /// Get: api/admin/offers/5
       /// </summary>
       [HttpGet ("offers/{id}")]
       public async Task<ActionResult<OfferResponse>> GetOfferById(int id)
        {
            var offer = await _context.Offers
                .Include (o => o.Merchant)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (offer == null)
            {
                return NotFound (new {message = "Offer not found"});

            }
            return Ok (new OfferResponse
            {
                Id = offer.Id,
                MerchantId = offer.MerchantId,
                Title = offer.Title,
                Description = offer.Description,
                PointsCost = offer.PointsCost,
                ImageUrl = offer.ImageUrl,
                ValidFrom = offer.ValidFrom,
                ValidUntil = offer.ValidUntil,
                IsActive = offer.IsActive,
                QuantityAvailable = offer.QuantityAvailable,
                CreatedAt = offer.CreatedAt,
            });
        }

        ///<summary>
        /// Create a Charger
        /// Post : api/admin/chargers
        /// </summary>
        [HttpPost ("chargers")]
        public async Task <ActionResult <ChargerResponse>> CreateCharger ([FromBody] CreateChargerRequest request)
        {
            if (await _context.Users.AnyAsync (u => u.Email == request.Email))
            {
                return BadRequest (new {message = "Email already exists"});

            }

            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword (request.Password),
                FullName = request.FullName, 
                PhoneNumber = request.PhoneNumber,
                Role= UserRole.Charger,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //Get Current admin
            var admin = await _context.Admins.FirstOrDefaultAsync();

            //Create Charger Profile
            var charger = new Charger
            {
                UserId = user.Id,
                AvailablePoints = 0,
                AssignedByAdminId = admin?.Id
            };

            _context.Chargers.Add(charger);
            await _context.SaveChangesAsync();
            return CreatedAtAction (nameof (GetChargerById), new {id = charger.Id}, new ChargerResponse
            {
                Id = charger.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvailablePoints = charger.AvailablePoints,
                CreatedAt = charger.CreatedAt
            });
        }

        ///<summary>
        /// Get All Charger
        /// Get: api/admin/charger
        /// </summaary>
        [HttpGet("chargers")]
        public async Task <ActionResult<IEnumerable<ChargerResponse>>> GetAllChargers ()
        {
            var chargers = await _context.Chargers
                    .Include (c => c.User)
                    .Select (c => new ChargerResponse
                    {
                Id = c.Id,
                Email = c.User.Email,
                FullName = c.User.FullName,
                PhoneNumber = c.User.PhoneNumber,
                AvailablePoints = c.AvailablePoints,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();

            return Ok(chargers);
        }

        ///<summary>
        /// Get Charger by ID
        ///  Get: api/admin/chargers/5
        /// </summary>
        [HttpGet ("charger/{id}")]
        public async Task <ActionResult <ChargerResponse>> GetChargerById (int id)
        {
            var charger = await _context.Chargers
                        .Include (c => c.User)
                        .FirstOrDefaultAsync (c=> c.Id == id);
            if (charger == null)
            {
                return NotFound (new {message = "Charger not found"});

            }
            return Ok (new ChargerResponse
            {
                Id = charger.Id,
                Email = charger.User.Email,
                FullName = charger.User.FullName,
                PhoneNumber = charger.User.PhoneNumber,
                AvailablePoints = charger.AvailablePoints,
                CreatedAt = charger.CreatedAt
            });
        }

        ///<summary>
        /// Assign Point to Charger (admin -> Charger)
        /// POST: api/admin/assing-points
        /// </summary>
        [HttpPost ("assign-point")]
        public async Task <ActionResult> AssignPointToCharger ([FromBody] AssignPointsToChargerRequest request)
        {
            //Get admin
            var admin = await _context.Admins.FirstOrDefaultAsync();
            if (admin ==null)
            {
                return BadRequest (new {message = "Admin not found"});

            }

            // check if admin has enough points
            if (admin.TotalPointsPool <request.PointAmount)
            {
                return BadRequest (new{message = "Insufficient point in admin pool"});

            }

            // Get Charger
            var charger = await _context.Chargers.FindAsync (request.ChargerId);
            if (charger== null)
            {
                return NotFound (new {message = "Charger not found"});

            }
            //Deduct from admin pool
            admin.TotalPointsPool -= request.PointAmount;

          //Add to charger 
          charger.AvailablePoints += request.PointAmount;

          //Record the assignment
          var assignment = new PointAssignment
          {
            Type = AssignmentType.AdminToCharger,
            FromAdminId = admin.Id,
            ToChargerId = charger.Id,
            PointsAmount=request.PointAmount,
            Notes = request.Notes  
          };
        

               _context.PointAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Points assigned successfully",
                adminRemainingPoints = admin.TotalPointsPool,
                chargerNewBalance = charger.AvailablePoints
            });
        }
    }
}