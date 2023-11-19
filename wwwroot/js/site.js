// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let clientCompanyButton = document.getElementById('relationshipButton');
let clientCompanyChartButton = document.getElementById('chartButton');
let topClientsButton = document.getElementById('topClientsButton');
let pagedClientsButtonButton = document.getElementById('pagedClientsButton');
let spinnerLoading = document.getElementById('loadingSpinner');

clientCompanyButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

topClientsButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

pagedClientsButtonButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}

clientCompanyChartButton.onclick = () => {
    spinnerLoading.style.display = 'block';
}