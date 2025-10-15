function refreshRates() {
    location.reload();
}

function showDatePicker() {
    const modal = new bootstrap.Modal(document.getElementById('dateModal'));
    modal.show();
}

function loadRatesByDate() {
    const selectedDate = document.getElementById('selectedDate').value;
    if (selectedDate) {
        window.location.href = `/Marketplace/ExchangeRates?date=${selectedDate}`;
    }
}

function searchCurrency() {
    const searchTerm = document.getElementById('currencySearch').value.toUpperCase();
    const table = document.getElementById('ratesTable');
    const rows = table.getElementsByTagName('tr');

    for (let i = 1; i < rows.length; i++) {
        const currencyCell = rows[i].cells[0].textContent.toUpperCase();
        rows[i].style.display = currencyCell.includes(searchTerm) ? '' : 'none';
    }
}

function showCurrencyDetails(currency, rate) {
    const baseCurrency = document.getElementById('baseCurrency')?.value || 'USD';
    document.getElementById('currencyModalTitle').textContent = `Детали валюты ${currency}`;
    document.getElementById('currencyModalBody').innerHTML = `
        <div class="row">
            <div class="col-md-6">
                <h6>Основная информация</h6>
                <p><strong>Код валюты:</strong> ${currency}</p>
                <p><strong>Название:</strong> ${getCurrencyFullName(currency)}</p>
                <p><strong>Курс к ${baseCurrency}:</strong> ${rate.toFixed(4)}</p>
            </div>
            <div class="col-md-6">
                <h6>Конвертация</h6>
                <div class="mb-2">
                    <label class="form-label">Сумма в ${currency}:</label>
                    <input type="number" class="form-control" id="amountInput" placeholder="Введите сумму">
                </div>
                <div class="mb-2">
                    <label class="form-label">Эквивалент в ${baseCurrency}:</label>
                    <input type="text" class="form-control" id="baseResult" readonly>
                </div>
                <button class="btn btn-primary btn-sm" onclick="convertCurrency('${currency}', ${rate}, '${baseCurrency}')">
                    Конвертировать
                </button>
            </div>
        </div>
    `;

    const modal = new bootstrap.Modal(document.getElementById('currencyModal'));
    modal.show();
}

function convertCurrency(currency, rate, baseCurrency) {
    const amount = parseFloat(document.getElementById('amountInput').value);
    if (!isNaN(amount)) {
        const baseAmount = amount / rate;
        document.getElementById('baseResult').value = baseAmount.toFixed(2);
    }
}

function getCurrencyFullName(code) {
    const names = {
        'USD': 'Доллар США',
        'EUR': 'Евро',
        'GBP': 'Британский фунт',
        'JPY': 'Японская иена',
        'CAD': 'Канадский доллар',
        'AUD': 'Австралийский доллар',
        'CHF': 'Швейцарский франк',
        'CNY': 'Китайский юань',
        'RUB': 'Российский рубль',
        'UAH': 'Украинская гривна'
    };
    return names[code] || '';
}

document.addEventListener('DOMContentLoaded', () => {
    const nameCells = document.querySelectorAll('[data-currency-name-for]');
    nameCells.forEach(cell => {
        const code = cell.getAttribute('data-currency-name-for');
        cell.textContent = getCurrencyFullName(code);
    });

    const changeCells = document.querySelectorAll('[data-change-for]');
    changeCells.forEach(cell => {
        cell.innerHTML = '<span class="badge bg-secondary">—</span>';
    });

    const searchInput = document.getElementById('currencySearch');
    if (searchInput) {
        searchInput.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                searchCurrency();
            }
        });
    }
});


