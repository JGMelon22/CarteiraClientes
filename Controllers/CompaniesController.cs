using CarteiraClientes.Controllers.ExtensionMethods;
using CarteiraClientes.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class CompaniesController : Controller
{
    private readonly IValidator<CompanyInputViewModel> _companyInputValidator;
    private readonly ICompanyRepository _repository;
    private readonly IPaginationService _service;

    public CompaniesController(ICompanyRepository repository, IPaginationService service,
        IValidator<CompanyInputViewModel> companyInputValidator)
    {
        _repository = repository;
        _service = service;
        _companyInputValidator = companyInputValidator;
    }

    // View Listar top 100 empresas
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var companies = await _repository.GetAllCompaniesAsync();
        return companies.Data != null
            ? View(companies.Data)
            : NoContent();
    }

    // Todas as empresas paginadas
    [HttpGet]
    public async Task<IActionResult> IndexPaged(int pageNumber = 1, int pageSize = 15)
    {
        var companies = await _service.PagingCompanyAsync(pageNumber, pageSize);
        return companies.Data != null
            ? View(companies.Data)
            : NoContent();
    }

    // View Detalhes empresa selecionada
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var company = await _repository.GetCompanyByIdCompiledEfCoreQueryAsync(id); // GetCompanyById
        return company.Data != null
            ? View(company.Data)
            : NotFound(company);
    }

    // View Adicionar nova empresa
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Action criar nova empresa
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompanyInputViewModel newCompanyInput)
    {
        var result = await _companyInputValidator.ValidateAsync(newCompanyInput);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Create));
        }

        await _repository.AddCompanyAsync(newCompanyInput);
        return RedirectToAction(nameof(Index));
    }

    // Chamar View Editar
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var company = await _repository.GetCompanyByIdCompiledEfCoreQueryAsync(id);
        return company.Data != null
            ? View(company.Data)
            : NotFound(company);
    }

    // Action Post Editar Company
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompanyInputViewModel updatedCompany)
    {
        var result = await _companyInputValidator.ValidateAsync(updatedCompany);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Edit));
        }

        var company = await _repository.UpdateCompanyAsync(id, updatedCompany);
        return company.Data != null
            ? RedirectToAction(nameof(Index))
            : View(nameof(Edit));
    }

    // Chamar View Deletar empresa
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var company = await _repository.GetCompanyByIdCompiledEfCoreQueryAsync(id);
        return company.Data != null
            ? View(company.Data)
            : NotFound(company);
    }

    // Action deletar empresa
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _repository.RemoveCompanyAsync(id);
        return RedirectToAction(nameof(Index));
    }
}