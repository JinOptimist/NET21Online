const BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5280';

export async function apiGet<T>(path: string): Promise<T> {
	const res = await fetch(`${BASE_URL}${path}`);
	if (!res.ok) {
		throw new Error(`GET ${path} failed: ${res.status}`);
	}
	return (await res.json()) as T;
}

export async function apiPost<TRequest, TResponse>(path: string, body: TRequest): Promise<TResponse> {
	const res = await fetch(`${BASE_URL}${path}`, {
		method: 'POST',
		headers: { 'Content-Type': 'application/json' },
		body: JSON.stringify(body),
	});
	if (!res.ok) {
		const text = await res.text();
		throw new Error(`POST ${path} failed: ${res.status} ${text}`);
	}
	return (await res.json()) as TResponse;
}
