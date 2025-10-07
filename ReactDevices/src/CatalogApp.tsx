import React from 'react';

type Device = {
	id: number;
	name: string;
	price: number;
	imagePath: string;
	typeDevice?: { name: string } | null;
	category?: { name: string } | null;
	canDelete?: boolean;
};

type CatalogResponse = {
	items: Device[];
	pageIndex: number;
	totalPages: number;
	totalItems: number;
};

const apiBase = '/api/CatalogApi';

function fetchDevices(params: { pageIndex?: number; minPrice?: string | number; maxPrice?: string | number; signal?: AbortSignal; }): Promise<CatalogResponse> {
	const sp = new URLSearchParams();
	sp.set('pageIndex', String(params.pageIndex ?? 1));
	if (params.minPrice !== undefined && params.minPrice !== '') sp.set('minPrice', String(params.minPrice));
	if (params.maxPrice !== undefined && params.maxPrice !== '') sp.set('maxPrice', String(params.maxPrice));
	return fetch(`${apiBase}?${sp.toString()}`, { signal: params.signal }).then(r => {
		if (!r.ok) throw new Error('Failed to load devices');
		return r.json();
	});
}

export default function CatalogApp() {
	const [minPrice, setMinPrice] = React.useState<string>('');
	const [maxPrice, setMaxPrice] = React.useState<string>('');
	const [pageIndex, setPageIndex] = React.useState<number>(1);
	const [data, setData] = React.useState<CatalogResponse>({ items: [], pageIndex: 1, totalPages: 1, totalItems: 0 });
	const [loading, setLoading] = React.useState<boolean>(false);
	const [error, setError] = React.useState<string>('');

	const load = React.useCallback((opts?: { page?: number; min?: string; max?: string; }) => {
		const ctrl = new AbortController();
		setLoading(true);
		setError('');
		fetchDevices({ pageIndex: opts?.page ?? pageIndex, minPrice: opts?.min ?? minPrice, maxPrice: opts?.max ?? maxPrice, signal: ctrl.signal })
			.then(json => setData(json))
			.catch(e => { if (e.name !== 'AbortError') setError(e.message || 'Error'); })
			.finally(() => setLoading(false));
		return () => ctrl.abort();
	}, [pageIndex, minPrice, maxPrice]);

	React.useEffect(() => {
		const dispose = load();
		return dispose;
	}, [pageIndex]);

	function applyPriceFilter(e: React.FormEvent) {
		e.preventDefault();
		setPageIndex(1);
		load({ page: 1 });
	}

	function resetFilters() {
		setMinPrice('');
		setMaxPrice('');
		setPageIndex(1);
		load({ page: 1, min: '', max: '' });
	}

	const gotoPage = (p: number) => {
		if (p < 1 || p > (data.totalPages || 1)) return;
		setPageIndex(p);
	};

	const pagesMiddle: number[] = [];
	for (let i = Math.max(2, data.pageIndex - 1); i <= Math.min((data.totalPages || 1) - 1, data.pageIndex + 1); i++) {
		pagesMiddle.push(i);
	}

	return (
		<div className="catalog">
			<div className="name">
				<h2>Весь каталог</h2>
				<button className="selecter" onClick={() => {
					const el = document.querySelector('.filters-sidebar');
					if (el) el.classList.toggle('active');
				}}>Фильтры <span>↓</span></button>
			</div>

			<div className="catalog-container">
				<div className="filters-sidebar">
					<div className="filter-block">
						<h3>Категории</h3>
						<ul className="filter-list">
							<li><a className="active">Все товары</a></li>
							<li><a>Ноутбуки</a></li>
							<li><a>Смартфоны</a></li>
							<li><a>Планшеты</a></li>
							<li><a>Компьютеры</a></li>
							<li><a>Аксессуары</a></li>
						</ul>
					</div>

					<form className="filter-block" onSubmit={applyPriceFilter}>
						<h3>Цена</h3>
						<div className="price-filter">
							<div className="price-inputs">
								<input type="number" placeholder="От" id="MinPrice" className="min-price" value={minPrice} onChange={e => setMinPrice(e.target.value)} />
								<input type="number" placeholder="До" id="MaxPrice" className="max-price" value={maxPrice} onChange={e => setMaxPrice(e.target.value)} />
							</div>
							<button className="apply-price-btn" type="submit">Применить</button>
						</div>
					</form>

					<div className="filter-block">
						<h3>Производитель</h3>
						<ul className="filter-list">
							<li><label><input type="checkbox" value="apple" /> Apple</label></li>
							<li><label><input type="checkbox" value="samsung" /> Samsung</label></li>
							<li><label><input type="checkbox" value="huawei" /> Huawei</label></li>
							<li><label><input type="checkbox" value="xiaomi" /> Xiaomi</label></li>
							<li><label><input type="checkbox" value="asus" /> ASUS</label></li>
							<li><label><input type="checkbox" value="hp" /> HP</label></li>
							<li><label><input type="checkbox" value="dell" /> Dell</label></li>
						</ul>
					</div>

					<div className="filter-block">
						<h3>Рейтинг</h3>
						<ul className="filter-list">
							<li><label><input type="checkbox" value="5" /> ★★★★★</label></li>
							<li><label><input type="checkbox" value="4" /> ★★★★☆ и выше</label></li>
							<li><label><input type="checkbox" value="3" /> ★★★☆☆ и выше</label></li>
							<li><label><input type="checkbox" value="2" /> ★★☆☆☆ и выше</label></li>
							<li><label><input type="checkbox" value="1" /> ★☆☆☆☆ и выше</label></li>
						</ul>
					</div>

					<button className="reset-filters-btn" type="button" onClick={resetFilters}>Сбросить фильтры</button>
				</div>

				<div className="main-content">
					<div className="blocks">
						{loading && <p>Загрузка...</p>}
						{!loading && data.items && data.items.length === 0 && <p>Нет устройств для отображения.</p>}
						{error && <p style={{ color: 'red' }}>{error}</p>}
						{(data.items || []).map(device => (
							<div className="comp" data-id={device.id} key={device.id}>
								<div className="info">
									<a href={`/compshop/ProductInfo?id=${device.id}`}>
										<img src={device.imagePath} alt={device.name} className="imgDevice" />
										<p className="name">{device.name}</p>
										<p className="type">{device.typeDevice?.name} {device.category?.name}</p>
										<p className="cost">
											<span className="spanCost">{device.price} Byn</span>
										</p>
									</a>
									<div className="btns">
										<div className="btn">
											<a href={`/compshop/add-to-cart?id=${device.id}`}>В корзину</a>
										</div>
										{device.canDelete && (
											<div className="btn delete">
												<a className="delete-link" href={`/compshop/delete?id=${device.id}`}>Удалить</a>
											</div>
										)}
									</div>
								</div>
							</div>
						))}
					</div>

					<div className="pagination">
						<div className="pagination-controls">
							{(data.pageIndex > 1)
								? <a className="arrow" onClick={() => gotoPage(data.pageIndex - 1)}>&larr;</a>
								: <span className="arrow disabled">&larr;</span>}

							<a className={`page ${data.pageIndex === 1 ? 'active' : ''}`} onClick={() => gotoPage(1)}>1</a>

							{data.pageIndex > 3 && <span className="dots">...</span>}

							{pagesMiddle.map(i => (
								<a key={i} className={`page ${data.pageIndex === i ? 'active' : ''}`} onClick={() => gotoPage(i)}>{i}</a>
							))}

							{data.pageIndex < (data.totalPages || 1) - 2 && <span className="dots">...</span>}

							{(data.totalPages || 1) > 1 && (
								<a className={`page ${data.pageIndex === data.totalPages ? 'active' : ''}`} onClick={() => gotoPage(data.totalPages)}>{data.totalPages}</a>
							)}

							{(data.pageIndex < (data.totalPages || 1))
								? <a className="arrow" onClick={() => gotoPage(data.pageIndex + 1)}>&rarr;</a>
								: <span className="arrow disabled">&rarr;</span>}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}


