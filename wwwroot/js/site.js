// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Select action tag per id 
let clientCompanyButton = document.getElementById('relationshipButton');
let clientCompanyChartButton = document.getElementById('chartButton');
let topClientsButton = document.getElementById('topClientsButton');
let pagedClientsButtonButton = document.getElementById('pagedClientsButton');
let topCompaniesButton = document.getElementById('topCompaniesButton');
let pagedCompaniesButtonButton = document.getElementById('pagedCompaniesButton');
let relationshipPagedButton = document.getElementById('relationshipPagedButton');
let spinnerLoading = document.getElementById('loadingSpinner');

// Then set spinner visibility to 'block' on click event, just to depict a better loading response to the user
clientCompanyButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

topClientsButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

pagedClientsButtonButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

topCompaniesButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

pagedCompaniesButtonButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

clientCompanyChartButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

relationshipPagedButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}