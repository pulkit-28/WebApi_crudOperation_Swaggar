using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siddhupagal.Models;

namespace Siddhupagal.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandContext _dbContext;
        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            return await _dbContext.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            CommonResponse commonResponse = new CommonResponse
            {
                Message = "Exist in Database",
                Name = brand.Name,
                Category = brand.category
            };

            //return brand;
            return Ok(commonResponse);
            
        }
        [HttpPost("")]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            try
            {
                if (brand.Id <= 0)
                {
                    return BadRequest();
                }
                if (String.IsNullOrWhiteSpace(brand.Name) && String.IsNullOrWhiteSpace(brand.category))
                {
                    return BadRequest();

                }
                bool specialCharacter = Utility.hasSpecialChar(brand.Name);
                if(specialCharacter == true)
                {
                    return BadRequest();
                }
                bool numeric =Utility.hasNumeric(brand.Name);
                if(numeric == true)
                {
                    return BadRequest();
                }
             /*   if (String.IsNullOrWhiteSpace(brand.category))
                {
                    return BadRequest();
                }*/
                if (brand.IsActive <= 0)
                {
                    return BadRequest();
                }

                _dbContext.Brands.Add(brand);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBrands), new { id = brand.Id }, brand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
       

        [HttpPut("")]
        public async Task<IActionResult> PutBrand(Brand brand)
        {
            //step 1 get by id method  first will check particular id in exist in database or not if not exist throw error
            //step 2 
            bool updateRequire = false;
            //Here iam getting brand detail by id
            var brand1 = await _dbContext.Brands.FindAsync(brand.Id);
            if (brand1 == null)
            {
                return NotFound();
            }
            //Here i will check name is same or not
            if(brand1.Name != brand.Name)
            {
                brand.Name = brand1.Name;
                updateRequire = true;
            }
                if (brand.Id <= 0)
            {
                return BadRequest();
            }
            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                if (updateRequire == true)
                {


                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateCocurrentException)
            {
                if (!BrandAwailable(brand.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok();
        }

        private bool BrandAwailable(int id)
        {
            return (_dbContext.Brands?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if(_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if(brand == null)
            {
                return NotFound(); 
            }
            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
