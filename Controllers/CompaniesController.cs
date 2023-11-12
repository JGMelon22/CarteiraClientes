using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class CompaniesController : Controller
{
    private readonly ICompanyRepository _repository;

    public CompaniesController(ICompanyRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var companies = await _repository.GetAllCompanies();
        return companies.Data != null
            ? View(companies.Data)
            : NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var company = await _repository.GetCompanyById(id);
        return company.Data != null
            ? View(company.Data)
            : NotFound(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddCompanyViewModel newCompany)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _repository.AddCompany(newCompany);
        return Ok();
    }

    // Chamar View Editar
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var company = await _repository.GetCompanyById(id);
        return company.Data != null
            ? View(company.Data)
            : NotFound(company);
    }

    // Action Post Editar Company
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateCompanyViewModel updatedCompany)
    {
        if (!ModelState.IsValid)
            return View(nameof(Edit));

        var company = await _repository.UpdateCompany(updatedCompany);
        return company.Data != null
            ? RedirectToAction(nameof(Index))
            : View(nameof(Edit));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _repository.RemoveCompany(id);
        return NoContent();
    }
}